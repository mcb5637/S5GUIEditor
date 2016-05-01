namespace S5GUIEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.treeViewWidgets = new System.Windows.Forms.TreeView();
            this.cmsTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
            this.eToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eGUIXCStaticWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gfxButtonWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textButtonWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staticTextWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBarWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pureTooltipWidgetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRename = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tSLZoom = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.setWorkingDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unpackS5DataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interpolationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pixelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smoothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lCwSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lCwPos = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lCwType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelActiveWidget = new System.Windows.Forms.Label();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpView = new System.Windows.Forms.TabPage();
            this.pBGameView = new System.Windows.Forms.PictureBox();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.pnlOptionSelect = new System.Windows.Forms.FlowLayoutPanel();
            this.baseWidgetOptions = new S5GUIEditor.Options.BaseWidgetOptions();
            this.staticWidgetOptions = new S5GUIEditor.Options.StaticWidgetOptions();
            this.buttonWidgetOptions = new S5GUIEditor.Options.ButtonWidgetOptions();
            this.gfxButtonWidgetOptions = new S5GUIEditor.Options.GfxButtonWidgetOptions();
            this.textButtonWidgetOptions = new S5GUIEditor.Options.TextButtonWidgetOptions();
            this.staticTextWidgetOptions = new S5GUIEditor.Options.StaticTextWidgetOptions();
            this.customWidgetOptions = new S5GUIEditor.Options.CustomWidgetOptions();
            this.progressBarWidgetOptions = new S5GUIEditor.Options.ProgressBarWidgetOptions();
            this.pureTooltipWidgetOptions = new S5GUIEditor.Options.PureTooltipWidgetOptions();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTree.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBGameView)).BeginInit();
            this.tpOptions.SuspendLayout();
            this.pnlOptionSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewWidgets
            // 
            this.treeViewWidgets.AllowDrop = true;
            this.treeViewWidgets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewWidgets.HideSelection = false;
            this.treeViewWidgets.Location = new System.Drawing.Point(12, 169);
            this.treeViewWidgets.Name = "treeViewWidgets";
            this.treeViewWidgets.Size = new System.Drawing.Size(332, 662);
            this.treeViewWidgets.TabIndex = 0;
            this.treeViewWidgets.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewWidgets_AfterLabelEdit);
            this.treeViewWidgets.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewWidgets_AfterCheck);
            this.treeViewWidgets.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeViewWidgets_DrawNode);
            this.treeViewWidgets.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeViewWidgets_MouseUp);
            // 
            // cmsTree
            // 
            this.cmsTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiOptions,
            this.tsmiRename,
            this.tsmiCopy,
            this.tsmiDelete});
            this.cmsTree.Name = "cmsTree";
            this.cmsTree.Size = new System.Drawing.Size(156, 114);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.tsmiPaste});
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(155, 22);
            this.tsmiAdd.Text = "Add Subwidget";
            // 
            // tsmiNew
            // 
            this.tsmiNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eToolStripMenuItem,
            this.eGUIXCStaticWidgetToolStripMenuItem,
            this.gfxButtonWidgetToolStripMenuItem,
            this.textButtonWidgetToolStripMenuItem,
            this.staticTextWidgetToolStripMenuItem,
            this.customWidgetToolStripMenuItem,
            this.progressBarWidgetToolStripMenuItem,
            this.pureTooltipWidgetToolStripMenuItem});
            this.tsmiNew.Name = "tsmiNew";
            this.tsmiNew.Size = new System.Drawing.Size(102, 22);
            this.tsmiNew.Text = "New";
            // 
            // eToolStripMenuItem
            // 
            this.eToolStripMenuItem.Name = "eToolStripMenuItem";
            this.eToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.eToolStripMenuItem.Tag = "EGUIX::CContainerWidget";
            this.eToolStripMenuItem.Text = "ContainerWidget";
            this.eToolStripMenuItem.Click += new System.EventHandler(this.tsmiCreateWidget);
            // 
            // eGUIXCStaticWidgetToolStripMenuItem
            // 
            this.eGUIXCStaticWidgetToolStripMenuItem.Name = "eGUIXCStaticWidgetToolStripMenuItem";
            this.eGUIXCStaticWidgetToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.eGUIXCStaticWidgetToolStripMenuItem.Tag = "EGUIX::CStaticWidget";
            this.eGUIXCStaticWidgetToolStripMenuItem.Text = "StaticWidget";
            this.eGUIXCStaticWidgetToolStripMenuItem.Click += new System.EventHandler(this.tsmiCreateWidget);
            // 
            // gfxButtonWidgetToolStripMenuItem
            // 
            this.gfxButtonWidgetToolStripMenuItem.Name = "gfxButtonWidgetToolStripMenuItem";
            this.gfxButtonWidgetToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.gfxButtonWidgetToolStripMenuItem.Tag = "EGUIX::CGfxButtonWidget";
            this.gfxButtonWidgetToolStripMenuItem.Text = "GfxButtonWidget";
            this.gfxButtonWidgetToolStripMenuItem.Click += new System.EventHandler(this.tsmiCreateWidget);
            // 
            // textButtonWidgetToolStripMenuItem
            // 
            this.textButtonWidgetToolStripMenuItem.Name = "textButtonWidgetToolStripMenuItem";
            this.textButtonWidgetToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.textButtonWidgetToolStripMenuItem.Tag = "EGUIX::CTextButtonWidget";
            this.textButtonWidgetToolStripMenuItem.Text = "TextButtonWidget";
            this.textButtonWidgetToolStripMenuItem.Click += new System.EventHandler(this.tsmiCreateWidget);
            // 
            // staticTextWidgetToolStripMenuItem
            // 
            this.staticTextWidgetToolStripMenuItem.Name = "staticTextWidgetToolStripMenuItem";
            this.staticTextWidgetToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.staticTextWidgetToolStripMenuItem.Tag = "EGUIX::CStaticTextWidget";
            this.staticTextWidgetToolStripMenuItem.Text = "StaticTextWidget";
            this.staticTextWidgetToolStripMenuItem.Click += new System.EventHandler(this.tsmiCreateWidget);
            // 
            // customWidgetToolStripMenuItem
            // 
            this.customWidgetToolStripMenuItem.Name = "customWidgetToolStripMenuItem";
            this.customWidgetToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.customWidgetToolStripMenuItem.Tag = "EGUIX::CCustomWidget";
            this.customWidgetToolStripMenuItem.Text = "CustomWidget";
            this.customWidgetToolStripMenuItem.Click += new System.EventHandler(this.tsmiCreateWidget);
            // 
            // progressBarWidgetToolStripMenuItem
            // 
            this.progressBarWidgetToolStripMenuItem.Name = "progressBarWidgetToolStripMenuItem";
            this.progressBarWidgetToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.progressBarWidgetToolStripMenuItem.Tag = "EGUIX::CProgressBarWidget";
            this.progressBarWidgetToolStripMenuItem.Text = "ProgressBarWidget";
            this.progressBarWidgetToolStripMenuItem.Click += new System.EventHandler(this.tsmiCreateWidget);
            // 
            // pureTooltipWidgetToolStripMenuItem
            // 
            this.pureTooltipWidgetToolStripMenuItem.Name = "pureTooltipWidgetToolStripMenuItem";
            this.pureTooltipWidgetToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.pureTooltipWidgetToolStripMenuItem.Tag = "EGUIX::CPureTooltipWidget";
            this.pureTooltipWidgetToolStripMenuItem.Text = "PureTooltipWidget";
            this.pureTooltipWidgetToolStripMenuItem.Click += new System.EventHandler(this.tsmiCreateWidget);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Size = new System.Drawing.Size(102, 22);
            this.tsmiPaste.Text = "Paste";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // tsmiOptions
            // 
            this.tsmiOptions.Name = "tsmiOptions";
            this.tsmiOptions.Size = new System.Drawing.Size(155, 22);
            this.tsmiOptions.Text = "Options";
            this.tsmiOptions.Click += new System.EventHandler(this.tsmiOptions_Click);
            // 
            // tsmiRename
            // 
            this.tsmiRename.Name = "tsmiRename";
            this.tsmiRename.Size = new System.Drawing.Size(155, 22);
            this.tsmiRename.Text = "Rename";
            this.tsmiRename.Click += new System.EventHandler(this.tsmiRename_Click);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(155, 22);
            this.tsmiCopy.Text = "Copy";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(155, 22);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.tSLZoom,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1396, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // tSLZoom
            // 
            this.tSLZoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tSLZoom.AutoSize = false;
            this.tSLZoom.Name = "tSLZoom";
            this.tSLZoom.Size = new System.Drawing.Size(86, 15);
            this.tSLZoom.Text = "100%";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setWorkingDirectoryToolStripMenuItem,
            this.unpackS5DataToolStripMenuItem,
            this.interpolationToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(62, 22);
            this.toolStripDropDownButton2.Text = "Settings";
            // 
            // setWorkingDirectoryToolStripMenuItem
            // 
            this.setWorkingDirectoryToolStripMenuItem.Name = "setWorkingDirectoryToolStripMenuItem";
            this.setWorkingDirectoryToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.setWorkingDirectoryToolStripMenuItem.Text = "Set Working Directory";
            this.setWorkingDirectoryToolStripMenuItem.Click += new System.EventHandler(this.setWorkingDirectoryToolStripMenuItem_Click);
            // 
            // unpackS5DataToolStripMenuItem
            // 
            this.unpackS5DataToolStripMenuItem.Name = "unpackS5DataToolStripMenuItem";
            this.unpackS5DataToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.unpackS5DataToolStripMenuItem.Text = "Unpack S5 Data";
            this.unpackS5DataToolStripMenuItem.Click += new System.EventHandler(this.unpackS5DataToolStripMenuItem_Click);
            // 
            // interpolationToolStripMenuItem
            // 
            this.interpolationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pixelToolStripMenuItem,
            this.smoothToolStripMenuItem});
            this.interpolationToolStripMenuItem.Name = "interpolationToolStripMenuItem";
            this.interpolationToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.interpolationToolStripMenuItem.Text = "Interpolation";
            // 
            // pixelToolStripMenuItem
            // 
            this.pixelToolStripMenuItem.Checked = true;
            this.pixelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pixelToolStripMenuItem.Name = "pixelToolStripMenuItem";
            this.pixelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pixelToolStripMenuItem.Text = "Pixel";
            this.pixelToolStripMenuItem.Click += new System.EventHandler(this.pixelToolStripMenuItem_Click);
            // 
            // smoothToolStripMenuItem
            // 
            this.smoothToolStripMenuItem.Name = "smoothToolStripMenuItem";
            this.smoothToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.smoothToolStripMenuItem.Text = "Smooth";
            this.smoothToolStripMenuItem.Click += new System.EventHandler(this.smoothToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lCwSize);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lCwPos);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lCwType);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.labelActiveWidget);
            this.groupBox2.Location = new System.Drawing.Point(12, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 135);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Widget under Mouse:";
            // 
            // lCwSize
            // 
            this.lCwSize.AutoSize = true;
            this.lCwSize.Location = new System.Drawing.Point(98, 93);
            this.lCwSize.Name = "lCwSize";
            this.lCwSize.Size = new System.Drawing.Size(10, 13);
            this.lCwSize.TabIndex = 8;
            this.lCwSize.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Size:";
            // 
            // lCwPos
            // 
            this.lCwPos.AutoSize = true;
            this.lCwPos.Location = new System.Drawing.Point(98, 70);
            this.lCwPos.Name = "lCwPos";
            this.lCwPos.Size = new System.Drawing.Size(10, 13);
            this.lCwPos.TabIndex = 6;
            this.lCwPos.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Position:";
            // 
            // lCwType
            // 
            this.lCwType.AutoSize = true;
            this.lCwType.Location = new System.Drawing.Point(98, 47);
            this.lCwType.Name = "lCwType";
            this.lCwType.Size = new System.Drawing.Size(10, 13);
            this.lCwType.TabIndex = 4;
            this.lCwType.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // labelActiveWidget
            // 
            this.labelActiveWidget.AutoSize = true;
            this.labelActiveWidget.Location = new System.Drawing.Point(98, 24);
            this.labelActiveWidget.Name = "labelActiveWidget";
            this.labelActiveWidget.Size = new System.Drawing.Size(10, 13);
            this.labelActiveWidget.TabIndex = 1;
            this.labelActiveWidget.Text = "-";
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpView);
            this.tcMain.Controls.Add(this.tpOptions);
            this.tcMain.Location = new System.Drawing.Point(350, 34);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1035, 798);
            this.tcMain.TabIndex = 5;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpView
            // 
            this.tpView.Controls.Add(this.pBGameView);
            this.tpView.Location = new System.Drawing.Point(4, 22);
            this.tpView.Name = "tpView";
            this.tpView.Padding = new System.Windows.Forms.Padding(3);
            this.tpView.Size = new System.Drawing.Size(1027, 772);
            this.tpView.TabIndex = 0;
            this.tpView.Text = "View";
            this.tpView.UseVisualStyleBackColor = true;
            // 
            // pBGameView
            // 
            this.pBGameView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBGameView.BackColor = System.Drawing.Color.White;
            this.pBGameView.Location = new System.Drawing.Point(0, 2);
            this.pBGameView.Name = "pBGameView";
            this.pBGameView.Size = new System.Drawing.Size(1025, 769);
            this.pBGameView.TabIndex = 2;
            this.pBGameView.TabStop = false;
            this.pBGameView.Paint += new System.Windows.Forms.PaintEventHandler(this.pBGameView_Paint);
            this.pBGameView.DoubleClick += new System.EventHandler(this.pBGameView_DoubleClick);
            this.pBGameView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pBGameView_MouseDown);
            this.pBGameView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pBGameView_MouseMove);
            this.pBGameView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pBGameView_MouseUp);
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.pnlOptionSelect);
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptions.Size = new System.Drawing.Size(1027, 772);
            this.tpOptions.TabIndex = 1;
            this.tpOptions.Text = "Widget Options";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // pnlOptionSelect
            // 
            this.pnlOptionSelect.AutoScroll = true;
            this.pnlOptionSelect.Controls.Add(this.baseWidgetOptions);
            this.pnlOptionSelect.Controls.Add(this.staticWidgetOptions);
            this.pnlOptionSelect.Controls.Add(this.buttonWidgetOptions);
            this.pnlOptionSelect.Controls.Add(this.gfxButtonWidgetOptions);
            this.pnlOptionSelect.Controls.Add(this.textButtonWidgetOptions);
            this.pnlOptionSelect.Controls.Add(this.staticTextWidgetOptions);
            this.pnlOptionSelect.Controls.Add(this.customWidgetOptions);
            this.pnlOptionSelect.Controls.Add(this.progressBarWidgetOptions);
            this.pnlOptionSelect.Controls.Add(this.pureTooltipWidgetOptions);
            this.pnlOptionSelect.Location = new System.Drawing.Point(3, 6);
            this.pnlOptionSelect.Name = "pnlOptionSelect";
            this.pnlOptionSelect.Size = new System.Drawing.Size(1021, 766);
            this.pnlOptionSelect.TabIndex = 2;
            // 
            // baseWidgetOptions
            // 
            this.baseWidgetOptions.Location = new System.Drawing.Point(3, 3);
            this.baseWidgetOptions.Name = "baseWidgetOptions";
            this.baseWidgetOptions.Size = new System.Drawing.Size(996, 119);
            this.baseWidgetOptions.TabIndex = 7;
            this.baseWidgetOptions.Visible = false;
            // 
            // staticWidgetOptions
            // 
            this.staticWidgetOptions.Location = new System.Drawing.Point(3, 128);
            this.staticWidgetOptions.Name = "staticWidgetOptions";
            this.staticWidgetOptions.Size = new System.Drawing.Size(996, 66);
            this.staticWidgetOptions.TabIndex = 8;
            this.staticWidgetOptions.Visible = false;
            // 
            // buttonWidgetOptions
            // 
            this.buttonWidgetOptions.Location = new System.Drawing.Point(3, 200);
            this.buttonWidgetOptions.Name = "buttonWidgetOptions";
            this.buttonWidgetOptions.Size = new System.Drawing.Size(996, 282);
            this.buttonWidgetOptions.TabIndex = 9;
            this.buttonWidgetOptions.Visible = false;
            // 
            // gfxButtonWidgetOptions
            // 
            this.gfxButtonWidgetOptions.Location = new System.Drawing.Point(3, 488);
            this.gfxButtonWidgetOptions.Name = "gfxButtonWidgetOptions";
            this.gfxButtonWidgetOptions.Size = new System.Drawing.Size(996, 66);
            this.gfxButtonWidgetOptions.TabIndex = 10;
            this.gfxButtonWidgetOptions.Visible = false;
            // 
            // textButtonWidgetOptions
            // 
            this.textButtonWidgetOptions.Location = new System.Drawing.Point(3, 560);
            this.textButtonWidgetOptions.Name = "textButtonWidgetOptions";
            this.textButtonWidgetOptions.Size = new System.Drawing.Size(996, 193);
            this.textButtonWidgetOptions.TabIndex = 11;
            this.textButtonWidgetOptions.Visible = false;
            // 
            // staticTextWidgetOptions
            // 
            this.staticTextWidgetOptions.Location = new System.Drawing.Point(3, 759);
            this.staticTextWidgetOptions.Name = "staticTextWidgetOptions";
            this.staticTextWidgetOptions.Size = new System.Drawing.Size(996, 200);
            this.staticTextWidgetOptions.TabIndex = 12;
            this.staticTextWidgetOptions.Visible = false;
            // 
            // customWidgetOptions
            // 
            this.customWidgetOptions.Location = new System.Drawing.Point(3, 965);
            this.customWidgetOptions.Name = "customWidgetOptions";
            this.customWidgetOptions.Size = new System.Drawing.Size(996, 126);
            this.customWidgetOptions.TabIndex = 13;
            this.customWidgetOptions.Visible = false;
            // 
            // progressBarWidgetOptions
            // 
            this.progressBarWidgetOptions.Location = new System.Drawing.Point(3, 1097);
            this.progressBarWidgetOptions.Name = "progressBarWidgetOptions";
            this.progressBarWidgetOptions.Size = new System.Drawing.Size(996, 126);
            this.progressBarWidgetOptions.TabIndex = 14;
            this.progressBarWidgetOptions.Visible = false;
            // 
            // pureTooltipWidgetOptions
            // 
            this.pureTooltipWidgetOptions.Location = new System.Drawing.Point(3, 1229);
            this.pureTooltipWidgetOptions.Name = "pureTooltipWidgetOptions";
            this.pureTooltipWidgetOptions.Size = new System.Drawing.Size(996, 166);
            this.pureTooltipWidgetOptions.TabIndex = 15;
            this.pureTooltipWidgetOptions.Visible = false;
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1396, 843);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.treeViewWidgets);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settlers HoK GUI Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.cmsTree.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tpView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBGameView)).EndInit();
            this.tpOptions.ResumeLayout(false);
            this.pnlOptionSelect.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewWidgets;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.PictureBox pBGameView;
        private System.Windows.Forms.ToolStripLabel tSLZoom;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelActiveWidget;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpView;
        private System.Windows.Forms.TabPage tpOptions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lCwType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lCwPos;
        private System.Windows.Forms.Label lCwSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip cmsTree;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiNew;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem eToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eGUIXCStaticWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gfxButtonWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textButtonWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staticTextWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem progressBarWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pureTooltipWidgetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiRename;
        private System.Windows.Forms.FlowLayoutPanel pnlOptionSelect;
        private System.Windows.Forms.BindingSource bindingSource1;
        private Options.BaseWidgetOptions baseWidgetOptions;
        private Options.StaticWidgetOptions staticWidgetOptions;
        private Options.ButtonWidgetOptions buttonWidgetOptions;
        private Options.GfxButtonWidgetOptions gfxButtonWidgetOptions;
        private Options.TextButtonWidgetOptions textButtonWidgetOptions;
        private Options.StaticTextWidgetOptions staticTextWidgetOptions;
        private Options.CustomWidgetOptions customWidgetOptions;
        private Options.ProgressBarWidgetOptions progressBarWidgetOptions;
        private Options.PureTooltipWidgetOptions pureTooltipWidgetOptions;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem setWorkingDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unpackS5DataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interpolationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pixelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smoothToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    }
}

