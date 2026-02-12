using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;
using S5GUIEditor.Options;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Security.Policy;

namespace S5GUIEditor
{
    public partial class MainForm : Form
    {
        private string NodeMap;
        private string tmpPath;
        // private System.Windows.Forms.TreeView treeViewWidgets;
        private const int MAPSIZE = 128;
        private StringBuilder NewNodeMap = new StringBuilder(MAPSIZE);

        SpecialBonusBaseShitWidget baseWidget;
        ContainerWidget firstWidget;
        float zoom = 1;
        PointF origin = new PointF(0, 0);
        enum State { Move, MoveWidget, ResizeWidget, None }
        State curState = State.None;
        PointF mousePosition;
        Widget activeWidget;
        PointF oldWidgetPos;
        SizeF oldWidgetSize;
        Dictionary<Type, WidgetOptions[]> optionsBoxes;
        public MainForm()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            pBGameView.MouseWheel += new MouseEventHandler(pBGameView_MouseWheel);
            pBGameView.KeyDown += new KeyEventHandler(pBGameView_KeyDown);
            treeViewWidgets.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeViewWidgets.CheckBoxes = true;

            this.treeViewWidgets.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            this.treeViewWidgets.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView1_DragOver);
            this.treeViewWidgets.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            this.treeViewWidgets.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeViewWidgets.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);

            ImageList TreeviewIL = new ImageList();
            TreeviewIL.Images.Add(Properties.Resources.FolderNode);
            TreeviewIL.Images.Add(Properties.Resources.WidgetNode);
            this.treeViewWidgets.ImageList = TreeviewIL;
            this.treeViewWidgets.HideSelection = false;
            this.treeViewWidgets.ItemHeight = this.treeViewWidgets.ItemHeight + 3;

            optionsBoxes = new Dictionary<Type, WidgetOptions[]>()
            {
                {typeof(StaticWidget), new WidgetOptions[]{staticWidgetOptions}},
                {typeof(GfxButtonWidget), new WidgetOptions[]{buttonWidgetOptions, gfxButtonWidgetOptions}},
                {typeof(TextButtonWidget), new WidgetOptions[]{buttonWidgetOptions, textButtonWidgetOptions}},
                {typeof(StaticTextWidget), new WidgetOptions[]{staticTextWidgetOptions}},
                {typeof(CustomWidget), new WidgetOptions[]{customWidgetOptions}},
                {typeof(ProgressBarWidget), new WidgetOptions[]{progressBarWidgetOptions}},
                {typeof(PureTooltipWidget), new WidgetOptions[]{pureTooltipWidgetOptions}},
                {typeof(ContainerWidget), new WidgetOptions[]{}},
                {typeof(SpecialBonusBaseShitWidget), new WidgetOptions[]{}}
            };
            CreateNewGUI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string directory in Directory.GetDirectories(Path.GetTempPath(), "s5guiEdit-*"))
            {
                try
                {
                    Directory.Delete(directory, true);
                }
                catch { }
            }

            tmpPath = Path.GetTempPath() + "s5guiEdit-" + Guid.NewGuid().ToString("N").Substring(24);
            Directory.CreateDirectory(tmpPath);
            UnpackHelperFiles();

            Environment.CurrentDirectory = tmpPath;

            Credits c = new Credits();
            c.ShowDialog();

            RegistryKey workingDir = Registry.CurrentUser.OpenSubKey("SOFTWARE\\bobby\\Settlers HoK GUI Editor");
            if (workingDir == null)
            {
                if (!SetWorkingDirectory())
                    this.Close();
                else
                    UnpackS5Data();
            }
            else
                GlobalSettings.DataPath = (string)workingDir.GetValue("WorkingDirectory") + "/";

            Text += typeof(MainForm).Assembly.GetName().Version;

            // auto load last opened
            if (workingDir != null)
            {
                string lastOpenedFile = workingDir.GetValue("LastOpenedFile") as string;
                if (File.Exists(lastOpenedFile))
                {
                    GlobalSettings.LastLoadPath = Path.GetDirectoryName(lastOpenedFile);
                    LoadGuiXml(lastOpenedFile);
                }
            }
        }

        private bool SetWorkingDirectory()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            MessageBox.Show("Choose a folder as your working directory! (50MB free memory required)");
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RegistryKey workingDir = Registry.CurrentUser.CreateSubKey("SOFTWARE\\bobby\\Settlers HoK GUI Editor");
                workingDir.SetValue("WorkingDirectory", fbd.SelectedPath);
                GlobalSettings.DataPath = fbd.SelectedPath + "/";
                return true;
            }
            else
                return false;
        }

        private void UnpackS5Data()
        {
            if (GlobalSettings.DataPath == null)
            {
                MessageBox.Show("Please set your Working Directory first.");
                return;
            }

            RegistryKey rk = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Blue Byte\\The Settlers - Heritage of Kings");
            string s5InstallPath;

            if (rk != null && (rk.GetValue("InstallPath") as string) != null)
            {
                s5InstallPath = rk.GetValue("InstallPath") as string;
            }
            else
            {
                if (!ManualPathSelectionForUnpackingS5Data(out s5InstallPath))
                    return;
            }

            foreach (string folder in new string[] { "graphics/textures/gui", "menu", "text" })
            {
                foreach (string bbaFile in new string[] { "/base/data.bba", "/extra2/bba/patch.bba", "/extra2/bba/data.bba", "/extra2/bba/patche2.bba" })
                {
                    string path = s5InstallPath + bbaFile;
                    if (!File.Exists(path))
                        continue;
                    string args = "\"" + path + "\" \"" + GlobalSettings.DataPath.Substring(0, GlobalSettings.DataPath.Length - 1) + "\" \"" + folder + "\"";
                    ProcessStartInfo psi = new ProcessStartInfo(tmpPath + "/bbaTool.exe", args);
                    //psi.CreateNoWindow = true;
                    //psi.UseShellExecute = false;
                    //psi.WindowStyle = ProcessWindowStyle.Hidden;
                    Process proc = Process.Start(psi);
                    proc.WaitForExit();
                }
            }

            if (Directory.Exists(s5InstallPath + "/base/shr"))
            {
                // history edition
                foreach (string source in new string[] { "/base/shr/", "/extra2/shr/" })
                {
                    foreach (string folder in new string[] { "graphics/textures/gui", "menu", "text" })
                    {
                        string sourceFolder = s5InstallPath + source + folder;
                        string dstFolder = GlobalSettings.DataPath + folder;
                        if (Directory.Exists(sourceFolder))
                            CopyFilesRecursively(sourceFolder, dstFolder);
                    }
                }

            }
        }

        // https://stackoverflow.com/questions/58744/copy-the-entire-contents-of-a-directory-in-c-sharp
        private void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        private bool ManualPathSelectionForUnpackingS5Data(out string outS5InstallPath)
        {
            // manual game detection
            outS5InstallPath = "";

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            MessageBox.Show("No installation found. Please selected the main folder manually.");
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bool pathOk = Directory.Exists(fbd.SelectedPath + "/base") && Directory.Exists(fbd.SelectedPath + "/bin");
                if (!pathOk)
                {
                    // manual folder did not contain game
                    MessageBox.Show("The selected folder seems invalid.");
                    return false;
                }

                outS5InstallPath = fbd.SelectedPath;
                return true;
            }
            else
            {
                // user dismissed file browser dialog
                return false;
            }
        }

        private void setWorkingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetWorkingDirectory();
        }

        private void unpackS5DataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnpackS5Data();
        }

        private void UnpackHelperFiles()
        {
            Assembly localAssembly = Assembly.GetExecutingAssembly();
            string[] resourceFiles = localAssembly.GetManifestResourceNames();

            try
            {
                foreach (string resourceName in resourceFiles)
                {
                    string[] nameParts = resourceName.Split('.');
                    if (nameParts[1] != "Helper")
                        continue;

                    Stream resourceStream = localAssembly.GetManifestResourceStream(resourceName);

                    string storeFilename = tmpPath + '/'
                        + nameParts[nameParts.Length - 2]
                        + '.' + nameParts[nameParts.Length - 1];

                    storeFilename = storeFilename.Replace('@', '.');

                    using (FileStream fs = new FileStream(storeFilename, FileMode.Create))
                    {
                        resourceStream.CopyTo(fs);
                    }
                }
            }
            catch
            {
                MessageBox.Show("A problem occured while creating the Helperfiles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you really want to quit?", "Settlers HoK GUI Editor", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            try
            {
                Directory.Delete(tmpPath, true);
            }
            catch { }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewGUI();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = GlobalSettings.DataPath; // working directory
            if (GlobalSettings.LastLoadPath != null)
                ofd.InitialDirectory = GlobalSettings.LastLoadPath; // last opened
            ofd.Filter = "S5 GUI File (*.xml)|*.xml";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadGuiXml(ofd.FileName);

                RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\bobby\\Settlers HoK GUI Editor", writable: true);
                if (rk == null)
                    rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\bobby\\Settlers HoK GUI Editor");
                if (rk != null)
                {
                    rk.SetValue("LastOpenedFile", ofd.FileName);
                    GlobalSettings.LastLoadPath = Path.GetDirectoryName(ofd.FileName);
                }
            }
        }

        private void LoadGuiXml(string xmlPath)
        {
            XDocument xd = XDocument.Load(xmlPath);
            XElement root = xd.Element("root");
            treeViewWidgets.Nodes.Clear();
            baseWidget = new SpecialBonusBaseShitWidget(root, null);
            firstWidget = baseWidget.SubWidgets[0] as ContainerWidget;
            treeViewWidgets.Nodes.Add(baseWidget.SubWidgets[0].TreeNode);
            pBGameView.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = GlobalSettings.DataPath;
            sfd.Filter = "S5 GUI File (*.xml)|*.xml";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XDocument xd = new XDocument();
                XElement root = baseWidget.GetXml();
                xd.Add(root);
                xd.Save(sfd.FileName);

            }
        }

        private void CreateNewGUI()
        {
            treeViewWidgets.Nodes.Clear();
            RectangleF defaultPosAndSize = new RectangleF(0, 0, 1024, 768);
            baseWidget = new SpecialBonusBaseShitWidget(null, defaultPosAndSize);
            firstWidget = new ContainerWidget(baseWidget, defaultPosAndSize);
            baseWidget.SubWidgets.Add(firstWidget);
            baseWidget.Name = "GUIRoot";
            firstWidget.Name = "Root";
            firstWidget.TreeNode.Text = "Root";
            treeViewWidgets.Nodes.Add(firstWidget.TreeNode);
            pBGameView.Invalidate();
        }

        private void pBGameView_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.InterpolationMode = GlobalSettings.InterpolationMode;
            g.DrawImage(Properties.Resources.WorldView, origin.X, origin.Y, 1024f * zoom, 768f * zoom);

            if (baseWidget != null)
                baseWidget.DrawWidget(g, zoom, origin);
            if (activeWidget != null)
            {
                activeWidget.ShowWidget(g);
                if (curState == State.MoveWidget || curState == State.ResizeWidget)
                    activeWidget.ParentNode.ShowWidget(g);
            }
        }

        private void pBGameView_MouseWheel(object sender, MouseEventArgs e)
        {
            float exp = (float)Math.Pow(1.1, (double)e.Delta / 120.0);
            PointF origMP = new PointF((e.X - origin.X) / zoom, (e.Y - origin.Y) / zoom);
            zoom *= exp;
            PointF newCursorPos = new PointF(origMP.X * zoom, origMP.Y * zoom);
            origin = new PointF(e.X - newCursorPos.X, e.Y - newCursorPos.Y);
            tSLZoom.Text = Math.Round(zoom * 100) + "%";
            pBGameView.Invalidate();
        }

        private void pBGameView_MouseDown(object sender, MouseEventArgs e)
        {
            mousePosition = new PointF(e.X, e.Y);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (activeWidget != null && !(activeWidget is SpecialBonusBaseShitWidget))
                    {
                        if (Cursor == Cursors.SizeNWSE)
                        {
                            curState = State.ResizeWidget;
                            oldWidgetSize = new SizeF(activeWidget.PositionAndSize.Width, activeWidget.PositionAndSize.Height);
                        }
                        else
                        {
                            curState = State.MoveWidget;
                            oldWidgetPos = activeWidget.PositionAndSize.Location;
                        }
                        treeViewWidgets.SelectedNode = activeWidget.TreeNode;
                    }
                    break;
                case MouseButtons.Middle:
                    curState = State.Move;
                    Cursor = Cursors.SizeAll;
                    break;
                case MouseButtons.Right:
                    if (activeWidget != null)
                    {
                        activeWidget.Visible = false;
                        activeWidget.TreeNode.Checked = false;
                        pBGameView.Invalidate();
                        treeViewWidgets.Invalidate();
                    }
                    break;
            }
        }

        private void pBGameView_MouseMove(object sender, MouseEventArgs e)
        {
            switch (curState)
            {
                case State.Move:
                    origin = new PointF(origin.X + (e.X - mousePosition.X), origin.Y + (e.Y - mousePosition.Y));
                    mousePosition = new PointF(e.X, e.Y);
                    break;
                case State.MoveWidget:
                    Cursor = Cursors.SizeAll;
                    float dx = (e.X - mousePosition.X) / zoom;
                    float dy = (e.Y - mousePosition.Y) / zoom;
                    float x = (float)Math.Round(oldWidgetPos.X + dx);
                    float y = (float)Math.Round(oldWidgetPos.Y + dy);

                    PointF newPos = new PointF(x, y);
                    if (x < 0)
                        newPos.X = 0;
                    else if (x + activeWidget.PositionAndSize.Width > activeWidget.ParentNode.PositionAndSize.Width)
                        newPos.X = activeWidget.ParentNode.PositionAndSize.Width - activeWidget.PositionAndSize.Width;
                    if (y < 0)
                        newPos.Y = 0;
                    else if (y + activeWidget.PositionAndSize.Height > activeWidget.ParentNode.PositionAndSize.Height)
                        newPos.Y = activeWidget.ParentNode.PositionAndSize.Height - activeWidget.PositionAndSize.Height;
                    activeWidget.PositionAndSize = new RectangleF(newPos.X, newPos.Y, activeWidget.PositionAndSize.Width, activeWidget.PositionAndSize.Height);
                    UpdateCurrentWidgetInfo();
                    break;
                case State.ResizeWidget:
                    dx = (e.X - mousePosition.X) / zoom;
                    dy = (e.Y - mousePosition.Y) / zoom;
                    float width = (float)Math.Round(oldWidgetSize.Width + dx);
                    float height = (float)Math.Round(oldWidgetSize.Height + dy);
                    SizeF newSize = new SizeF(width, height);
                    if (width <= 0)
                        newSize.Width = 1;
                    else if (activeWidget.PositionAndSize.X + width > activeWidget.ParentNode.PositionAndSize.Width)
                        newSize.Width = activeWidget.ParentNode.PositionAndSize.Width - activeWidget.PositionAndSize.X;
                    if (height <= 0)
                        newSize.Height = 1;
                    else if (activeWidget.PositionAndSize.Y + height > activeWidget.ParentNode.PositionAndSize.Height)
                        newSize.Height = activeWidget.ParentNode.PositionAndSize.Height - activeWidget.PositionAndSize.Y;
                    activeWidget.PositionAndSize = new RectangleF(activeWidget.PositionAndSize.X, activeWidget.PositionAndSize.Y, newSize.Width, newSize.Height);
                    UpdateCurrentWidgetInfo();
                    break;
                default:
                    if (baseWidget != null)
                    {
                        activeWidget = baseWidget.GetWidgetUnderPosition(new Point(e.X, e.Y));
                        if (activeWidget != null)
                        {
                            UpdateCurrentWidgetInfo();
                            Cursor = Cursors.Default;
                            curState = State.None;
                            if (e.X >= (activeWidget.CurrentPositionAndSize.X + activeWidget.CurrentPositionAndSize.Width - 5) && e.Y >= (activeWidget.CurrentPositionAndSize.Y + activeWidget.CurrentPositionAndSize.Height - 5))
                                if (e.X <= (activeWidget.CurrentPositionAndSize.X + activeWidget.CurrentPositionAndSize.Width) && e.Y <= (activeWidget.CurrentPositionAndSize.Y + activeWidget.CurrentPositionAndSize.Height))
                                    Cursor = Cursors.SizeNWSE;

                        }
                    }

                    break;
            }
            pBGameView.Invalidate();
            pBGameView.Focus();
        }

        private void UpdateCurrentWidgetInfo()
        {
            labelActiveWidget.Text = activeWidget.Name;
            lCwType.Text = Widget.WidgetTypes[activeWidget.GetType()].ClassName;
            lCwPos.Text = "X: " + activeWidget.PositionAndSize.X + " Y: " + activeWidget.PositionAndSize.Y;
            lCwSize.Text = "W: " + activeWidget.PositionAndSize.Width + " H: " + activeWidget.PositionAndSize.Height;
        }

        private void pBGameView_MouseUp(object sender, MouseEventArgs e)
        {
            curState = State.None;
            Cursor = Cursors.Default;
        }

        private void pBGameView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                origin = new PointF(0, 0);
                zoom = 1.0f;
                tSLZoom.Text = "100%";
                pBGameView.Invalidate();

            }
        }

        private void treeViewWidgets_AfterCheck(object sender, TreeViewEventArgs e)
        {
            (e.Node.Tag as Widget).Visible = e.Node.Checked;
            pBGameView.Invalidate();
        }

        private void btnUncheck_Click(object sender, EventArgs e)
        {
            UncheckNodes(treeViewWidgets.Nodes);
        }

        private void UncheckNodes(TreeNodeCollection Nodes)
        {
            foreach (TreeNode node in Nodes)
            {
                node.Checked = false;
                Widget widget = node.Tag as Widget;
                widget.Visible = false;
                if (widget is ContainerWidget)
                    UncheckNodes(widget.TreeNode.Nodes);
            }
        }

        private void treeViewWidgets_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.IsSelected)
            {
                Rectangle d = e.Bounds;
                d.X += 1;
                d.Width -= 3;
                // d.Height -= 1;
                e.Graphics.FillRectangle(SystemBrushes.MenuHighlight, d);
            }



            TextRenderer.DrawText(e.Graphics,
                                   e.Node.Text,
                                   e.Node.TreeView.Font,
                                   e.Node.Bounds,
                                   e.Node.ForeColor);

        }

        private void treeViewWidgets_MouseUp(object sender, MouseEventArgs e)
        {
            if (treeViewWidgets.SelectedNode != null)
                activeWidget = treeViewWidgets.SelectedNode.Tag as Widget;
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                TreeNode node = treeViewWidgets.GetNodeAt(p);
                if (node != null)
                {
                    treeViewWidgets.SelectedNode = node;
                    Widget widget = node.Tag as Widget;
                    tsmiAdd.Visible = false;
                    tsmiDelete.Visible = true;
                    tsmiCopy.Visible = true;
                    tsmiOptions.Visible = true;
                    if (widget is ContainerWidget)
                        tsmiAdd.Visible = true;
                    if (widget.Equals(firstWidget))
                        tsmiDelete.Visible = false;
                    cmsTree.Show(treeViewWidgets, p);
                }
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            Widget toDelete = treeViewWidgets.SelectedNode.Tag as Widget;

            toDelete.ParentNode.SubWidgets.Remove(toDelete);
            treeViewWidgets.Nodes.Remove(treeViewWidgets.SelectedNode);
            pBGameView.Invalidate();
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            Widget toCopy = treeViewWidgets.SelectedNode.Tag as Widget;
            XDocument xd = new XDocument();
            xd.Add(toCopy.GetXml());
            Clipboard.SetData(DataFormats.StringFormat, xd.ToString());

        }

        private void luaExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Widget toCopy = treeViewWidgets.SelectedNode.Tag as Widget;
            try
            {
                string l = toCopy.GetLua();
                Clipboard.SetData(DataFormats.StringFormat, l);
            }
            catch (InvalidOperationException er)
            {
                MessageBox.Show(er.Message, "error:");
            }
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            ContainerWidget toInsertInto = treeViewWidgets.SelectedNode.Tag as ContainerWidget;
            string toParse = (string)Clipboard.GetData(DataFormats.StringFormat);
            try
            {
                XDocument xd = XDocument.Parse(toParse);
                Widget toInsert = Widget.ParseWidget(xd.Element("WidgetList"), toInsertInto);
                toInsertInto.SubWidgets.Add(toInsert);
            }
            catch { MessageBox.Show("Not a widget! -______-"); }
        }

        private void tsmiCreateWidget(object sender, EventArgs e)
        {
            ContainerWidget parentNode = treeViewWidgets.SelectedNode.Tag as ContainerWidget;
            Type widgetType = Widget.TypeFromString((sender as ToolStripMenuItem).Tag as string);
            ConstructorInfo info = widgetType.GetConstructor(new Type[] { typeof(ContainerWidget), typeof(RectangleF) });
            Widget widget = (Widget)info.Invoke(new object[] { parentNode, new RectangleF(0, 0, 100, 100) });
            parentNode.SubWidgets.Add(widget);
        }

        private void tsmiRename_Click(object sender, EventArgs e)
        {
            TreeNode node = treeViewWidgets.SelectedNode;
            treeViewWidgets.LabelEdit = true;
            node.BeginEdit();
        }

        private void treeViewWidgets_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            treeViewWidgets.LabelEdit = false;
            if (e.Label == null)
                return;
            string newText = e.Label;
            (e.Node.Tag as Widget).Name = newText;
        }

        private void tsmiOptions_Click(object sender, EventArgs e)
        {
            OpenWidgetOptions(treeViewWidgets.SelectedNode.Tag as Widget);
            tcMain.SelectedIndex = 1;
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedIndex == 1)
                if (treeViewWidgets.SelectedNode != null)
                    OpenWidgetOptions(treeViewWidgets.SelectedNode.Tag as Widget);
        }

        private void pBGameView_DoubleClick(object sender, EventArgs e)
        {
            if (activeWidget != null)
            {
                OpenWidgetOptions(activeWidget);
                tcMain.SelectedIndex = 1;
            }
        }

        public void OpenWidgetOptions(Widget widgetToChange)
        {
            foreach (WidgetOptions[] woA in optionsBoxes.Values.ToArray())
            {
                foreach (WidgetOptions wo in woA)
                {
                    wo.Visible = false;
                }
            }
            foreach (WidgetOptions wo in optionsBoxes[widgetToChange.GetType()])
            {
                wo.Visible = true;
                wo.LoadOptions(widgetToChange);
            }
            baseWidgetOptions.Visible = true;
            baseWidgetOptions.LoadOptions(widgetToChange);
        }

        private void pixelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            smoothToolStripMenuItem.Checked = false;
            pixelToolStripMenuItem.Checked = true;
            GlobalSettings.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            pBGameView.Invalidate();
        }

        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            smoothToolStripMenuItem.Checked = true;
            pixelToolStripMenuItem.Checked = false;
            GlobalSettings.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
            pBGameView.Invalidate();
        }


        private void treeView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.treeViewWidgets.SelectedNode = this.treeViewWidgets.GetNodeAt(e.X, e.Y);
        }
        private void treeView1_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeView1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void treeView1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false) && this.NodeMap != null && this.NodeMap != "")
            {
                TreeNode MovingNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                string[] NodeIndexes = this.NodeMap.Split('|');
                TreeNodeCollection InsertCollection = this.treeViewWidgets.Nodes;
                for (int i = 0; i < NodeIndexes.Length - 1; i++)
                {
                    InsertCollection = InsertCollection[Int32.Parse(NodeIndexes[i])].Nodes;
                }

                if (InsertCollection != null && NodeIndexes.Length > 1)
                {
                    treeViewWidgets.SuspendLayout();
                    Widget moveWidget = MovingNode.Tag as Widget;
                    ContainerWidget wContainer = firstWidget;
                    for (int i = 1; i < NodeIndexes.Length - 1; i++)
                    {
                        int idx = int.Parse(NodeIndexes[i]);
                        wContainer = wContainer.SubWidgets[idx] as ContainerWidget;
                    }
                    wContainer.SubWidgets.Insert(int.Parse(NodeIndexes.Last()), null);
                    ContainerWidget oldParent = moveWidget.ParentNode;
                    moveWidget.ParentNode = wContainer;
                    oldParent.SubWidgets.Remove(moveWidget);
                    int repIdx = wContainer.SubWidgets.IndexOf(null);
                    wContainer.SubWidgets[repIdx] = moveWidget;
                    TreeNode newNode = (TreeNode)MovingNode.Clone();
                    RecursivelyCopyLinks(MovingNode, newNode);

                    InsertCollection.Insert(Int32.Parse(NodeIndexes[NodeIndexes.Length - 1]), newNode);
                    this.treeViewWidgets.SelectedNode = InsertCollection[Int32.Parse(NodeIndexes[NodeIndexes.Length - 1])];
                    MovingNode.Remove();
                    treeViewWidgets.ResumeLayout(true);
                    pBGameView.Invalidate();
                }
            }
        }

        private void RecursivelyCopyLinks(TreeNode tnBase, TreeNode tnCopy)
        {
            tnCopy.Tag = tnBase.Tag;
            Widget w = tnBase.Tag as Widget;
            w.TreeNode = tnCopy;

            for (int i = 0; i < tnCopy.Nodes.Count; i++)
                RecursivelyCopyLinks(tnBase.Nodes[i], tnCopy.Nodes[i]);
        }

        private void treeView1_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            TreeNode NodeOver = this.treeViewWidgets.GetNodeAt(this.treeViewWidgets.PointToClient(Cursor.Position));
            TreeNode NodeMoving = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");


            // A bit long, but to summarize, process the following code only if the nodeover is null
            // and either the nodeover is not the same thing as nodemoving UNLESSS nodeover happens
            // to be the last node in the branch (so we can allow drag & drop below a parent branch)
            if (NodeOver != null && (NodeOver != NodeMoving || (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))))
            {
                int OffsetY = this.treeViewWidgets.PointToClient(Cursor.Position).Y - NodeOver.Bounds.Top;
                int NodeOverImageWidth = this.treeViewWidgets.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
                Graphics g = this.treeViewWidgets.CreateGraphics();

                // Image index of 1 is the non-folder icon

                if (NodeOver.ImageIndex == 1)
                {
                    #region Standard Node
                    if (OffsetY < (NodeOver.Bounds.Height / 2))
                    {
                        //this.lblDebug.Text = "top";

                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                this.NodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }
                        #endregion
                        #region Store the placeholder info into a pipe delimited string
                        SetNewNodeMap(NodeOver, false);
                        if (SetMapsEqual() == true)
                            return;
                        #endregion
                        #region Clear placeholders above and below
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        this.DrawLeafTopPlaceholders(NodeOver);
                        #endregion
                    }
                    else
                    {
                        //this.lblDebug.Text = "bottom";

                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                this.NodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }
                        #endregion
                        #region Allow drag drop to parent branches
                        TreeNode ParentDragDrop = null;
                        // If the node the mouse is over is the last node of the branch we should allow
                        // the ability to drop the "nodemoving" node BELOW the parent node
                        if (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))
                        {
                            int XPos = this.treeViewWidgets.PointToClient(Cursor.Position).X;
                            if (XPos < NodeOver.Bounds.Left)
                            {
                                ParentDragDrop = NodeOver.Parent;

                                if (XPos < (ParentDragDrop.Bounds.Left - this.treeViewWidgets.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width))
                                {
                                    if (ParentDragDrop.Parent != null)
                                        ParentDragDrop = ParentDragDrop.Parent;
                                }
                            }
                        }
                        #endregion
                        #region Store the placeholder info into a pipe delimited string
                        // Since we are in a special case here, use the ParentDragDrop node as the current "nodeover"
                        SetNewNodeMap(ParentDragDrop != null ? ParentDragDrop : NodeOver, true);
                        if (SetMapsEqual() == true)
                            return;
                        #endregion
                        #region Clear placeholders above and below
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        DrawLeafBottomPlaceholders(NodeOver, ParentDragDrop);
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region Folder Node
                    if (OffsetY < (NodeOver.Bounds.Height / 3))
                    {
                        //this.lblDebug.Text = "folder top";

                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                this.NodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }
                        #endregion
                        #region Store the placeholder info into a pipe delimited string
                        SetNewNodeMap(NodeOver, false);
                        if (SetMapsEqual() == true)
                            return;
                        #endregion
                        #region Clear placeholders above and below
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        this.DrawFolderTopPlaceholders(NodeOver);
                        #endregion
                    }
                    else if ((NodeOver.Parent != null && NodeOver.Index == 0) && (OffsetY > (NodeOver.Bounds.Height - (NodeOver.Bounds.Height / 3))))
                    {
                        //this.lblDebug.Text = "folder bottom";

                        #region If NodeOver is a child then cancel
                        TreeNode tnParadox = NodeOver;
                        while (tnParadox.Parent != null)
                        {
                            if (tnParadox.Parent == NodeMoving)
                            {
                                this.NodeMap = "";
                                return;
                            }

                            tnParadox = tnParadox.Parent;
                        }
                        #endregion
                        #region Store the placeholder info into a pipe delimited string
                        SetNewNodeMap(NodeOver, true);
                        if (SetMapsEqual() == true)
                            return;
                        #endregion
                        #region Clear placeholders above and below
                        this.Refresh();
                        #endregion
                        #region Draw the placeholders
                        DrawFolderTopPlaceholders(NodeOver);
                        #endregion
                    }
                    else
                    {
                        //this.lblDebug.Text = "folder over";

                        if (NodeOver.Nodes.Count > 0)
                        {
                            NodeOver.Expand();
                            //this.Refresh();
                        }
                        else
                        {
                            #region Prevent the node from being dragged onto itself
                            if (NodeMoving == NodeOver)
                                return;
                            #endregion
                            #region If NodeOver is a child then cancel
                            TreeNode tnParadox = NodeOver;
                            while (tnParadox.Parent != null)
                            {
                                if (tnParadox.Parent == NodeMoving)
                                {
                                    this.NodeMap = "";
                                    return;
                                }

                                tnParadox = tnParadox.Parent;
                            }
                            #endregion
                            #region Store the placeholder info into a pipe delimited string
                            SetNewNodeMap(NodeOver, false);
                            NewNodeMap = NewNodeMap.Insert(NewNodeMap.Length, "|0");

                            if (SetMapsEqual() == true)
                                return;
                            #endregion
                            #region Clear placeholders above and below
                            this.Refresh();
                            #endregion
                            #region Draw the "add to folder" placeholder
                            DrawAddToFolderPlaceholder(NodeOver);
                            #endregion
                        }
                    }
                    #endregion
                }
            }
        }

        #region Helper Methods
        private void DrawLeafTopPlaceholders(TreeNode NodeOver)
        {
            Graphics g = this.treeViewWidgets.CreateGraphics();

            int NodeOverImageWidth = this.treeViewWidgets.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
            int LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            int RightPos = this.treeViewWidgets.Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Top + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Y),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Top - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Top + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Top - 5)};


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

        }//eom

        private void DrawLeafBottomPlaceholders(TreeNode NodeOver, TreeNode ParentDragDrop)
        {
            Graphics g = this.treeViewWidgets.CreateGraphics();

            int NodeOverImageWidth = this.treeViewWidgets.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
            // Once again, we are not dragging to node over, draw the placeholder using the ParentDragDrop bounds
            int LeftPos, RightPos;
            if (ParentDragDrop != null)
                LeftPos = ParentDragDrop.Bounds.Left - (this.treeViewWidgets.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width + 8);
            else
                LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            RightPos = this.treeViewWidgets.Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Bottom),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Bottom - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Bottom - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Bottom - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Bottom + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Bottom),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Bottom - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Bottom - 5)};


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Bottom), new Point(RightPos, NodeOver.Bounds.Bottom));
        }//eom

        private void DrawFolderTopPlaceholders(TreeNode NodeOver)
        {
            Graphics g = this.treeViewWidgets.CreateGraphics();
            int NodeOverImageWidth = this.treeViewWidgets.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;

            int LeftPos, RightPos;
            LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            RightPos = this.treeViewWidgets.Width - 4;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 4),
                                                   new Point(LeftPos, NodeOver.Bounds.Top + 4),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Y),
                                                   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
                                                   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Top - 4),
                                                    new Point(RightPos, NodeOver.Bounds.Top + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Top - 5)};


            g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

        }//eom
        private void DrawAddToFolderPlaceholder(TreeNode NodeOver)
        {
            Graphics g = this.treeViewWidgets.CreateGraphics();
            int RightPos = NodeOver.Bounds.Right + 6;
            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2)),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 5)};

            this.Refresh();
            g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
        }//eom

        private void SetNewNodeMap(TreeNode tnNode, bool boolBelowNode)
        {
            NewNodeMap.Length = 0;

            if (boolBelowNode)
                NewNodeMap.Insert(0, (int)tnNode.Index + 1);
            else
                NewNodeMap.Insert(0, (int)tnNode.Index);
            TreeNode tnCurNode = tnNode;

            while (tnCurNode.Parent != null)
            {
                tnCurNode = tnCurNode.Parent;

                if (NewNodeMap.Length == 0 && boolBelowNode == true)
                {
                    NewNodeMap.Insert(0, (tnCurNode.Index + 1) + "|");
                }
                else
                {
                    NewNodeMap.Insert(0, tnCurNode.Index + "|");
                }
            }
        }//oem

        private bool SetMapsEqual()
        {
            if (this.NewNodeMap.ToString() == this.NodeMap)
                return true;
            else
            {
                this.NodeMap = this.NewNodeMap.ToString();
                return false;
            }
        }





        //oem
        #endregion
    }
}
