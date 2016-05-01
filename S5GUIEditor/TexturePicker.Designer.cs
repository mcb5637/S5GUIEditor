namespace S5GUIEditor
{
    partial class TexturePicker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TexturePicker));
            this.lTP = new System.Windows.Forms.Label();
            this.tbTexturePath = new System.Windows.Forms.TextBox();
            this.btnSearchFile = new System.Windows.Forms.Button();
            this.tcCoords = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbYGrid = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbXGrid = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbYCustom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbXCustom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbHGrid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbWGrid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.gpSize = new System.Windows.Forms.GroupBox();
            this.pColor = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbPickedTexture = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbTexture = new System.Windows.Forms.PictureBox();
            this.btnNoTexture = new System.Windows.Forms.Button();
            this.btnSetWhite = new System.Windows.Forms.Button();
            this.btnSetBlack = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnTransperent = new System.Windows.Forms.Button();
            this.tcCoords.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gpSize.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPickedTexture)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTexture)).BeginInit();
            this.SuspendLayout();
            // 
            // lTP
            // 
            this.lTP.AutoSize = true;
            this.lTP.Location = new System.Drawing.Point(9, 18);
            this.lTP.Name = "lTP";
            this.lTP.Size = new System.Drawing.Size(71, 13);
            this.lTP.TabIndex = 1;
            this.lTP.Text = "Texture Path:";
            // 
            // tbTexturePath
            // 
            this.tbTexturePath.Location = new System.Drawing.Point(86, 15);
            this.tbTexturePath.Name = "tbTexturePath";
            this.tbTexturePath.Size = new System.Drawing.Size(228, 20);
            this.tbTexturePath.TabIndex = 2;
            // 
            // btnSearchFile
            // 
            this.btnSearchFile.Location = new System.Drawing.Point(320, 15);
            this.btnSearchFile.Name = "btnSearchFile";
            this.btnSearchFile.Size = new System.Drawing.Size(99, 20);
            this.btnSearchFile.TabIndex = 3;
            this.btnSearchFile.Text = "Choose Texture";
            this.btnSearchFile.UseVisualStyleBackColor = true;
            this.btnSearchFile.Click += new System.EventHandler(this.btnSearchFile_Click);
            // 
            // tcCoords
            // 
            this.tcCoords.Controls.Add(this.tabPage1);
            this.tcCoords.Controls.Add(this.tabPage2);
            this.tcCoords.Enabled = false;
            this.tcCoords.Location = new System.Drawing.Point(12, 353);
            this.tcCoords.Name = "tcCoords";
            this.tcCoords.SelectedIndex = 0;
            this.tcCoords.Size = new System.Drawing.Size(111, 78);
            this.tcCoords.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbYGrid);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.tbXGrid);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(103, 52);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Grid";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbYGrid
            // 
            this.tbYGrid.Location = new System.Drawing.Point(32, 29);
            this.tbYGrid.Name = "tbYGrid";
            this.tbYGrid.Size = new System.Drawing.Size(45, 20);
            this.tbYGrid.TabIndex = 29;
            this.tbYGrid.Text = "0";
            this.tbYGrid.TextChanged += new System.EventHandler(this.tbYGrid_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Y:";
            // 
            // tbXGrid
            // 
            this.tbXGrid.Location = new System.Drawing.Point(32, 4);
            this.tbXGrid.Name = "tbXGrid";
            this.tbXGrid.Size = new System.Drawing.Size(45, 20);
            this.tbXGrid.TabIndex = 27;
            this.tbXGrid.Text = "0";
            this.tbXGrid.TextChanged += new System.EventHandler(this.tbXGrid_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "X:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbYCustom);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.tbXCustom);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(103, 52);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Custom";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbYCustom
            // 
            this.tbYCustom.Location = new System.Drawing.Point(32, 29);
            this.tbYCustom.Name = "tbYCustom";
            this.tbYCustom.Size = new System.Drawing.Size(45, 20);
            this.tbYCustom.TabIndex = 21;
            this.tbYCustom.Text = "0";
            this.tbYCustom.TextChanged += new System.EventHandler(this.tbYCustom_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Y:";
            // 
            // tbXCustom
            // 
            this.tbXCustom.Location = new System.Drawing.Point(32, 4);
            this.tbXCustom.Name = "tbXCustom";
            this.tbXCustom.Size = new System.Drawing.Size(45, 20);
            this.tbXCustom.TabIndex = 19;
            this.tbXCustom.Text = "0";
            this.tbXCustom.TextChanged += new System.EventHandler(this.tbXCustom_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "X:";
            // 
            // tbHGrid
            // 
            this.tbHGrid.Location = new System.Drawing.Point(36, 47);
            this.tbHGrid.Name = "tbHGrid";
            this.tbHGrid.Size = new System.Drawing.Size(45, 20);
            this.tbHGrid.TabIndex = 33;
            this.tbHGrid.Text = "32";
            this.tbHGrid.TextChanged += new System.EventHandler(this.tbH_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "H:";
            // 
            // tbWGrid
            // 
            this.tbWGrid.Location = new System.Drawing.Point(36, 22);
            this.tbWGrid.Name = "tbWGrid";
            this.tbWGrid.Size = new System.Drawing.Size(45, 20);
            this.tbWGrid.TabIndex = 31;
            this.tbWGrid.Text = "32";
            this.tbWGrid.TextChanged += new System.EventHandler(this.tbW_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "W:";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(97, 22);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(69, 45);
            this.btnSelectAll.TabIndex = 31;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // gpSize
            // 
            this.gpSize.Controls.Add(this.tbHGrid);
            this.gpSize.Controls.Add(this.btnSelectAll);
            this.gpSize.Controls.Add(this.label5);
            this.gpSize.Controls.Add(this.label6);
            this.gpSize.Controls.Add(this.tbWGrid);
            this.gpSize.Enabled = false;
            this.gpSize.Location = new System.Drawing.Point(129, 353);
            this.gpSize.Name = "gpSize";
            this.gpSize.Size = new System.Drawing.Size(185, 78);
            this.gpSize.TabIndex = 32;
            this.gpSize.TabStop = false;
            this.gpSize.Text = "Size:";
            // 
            // pColor
            // 
            this.pColor.BackColor = System.Drawing.Color.White;
            this.pColor.BackgroundImage = global::S5GUIEditor.Properties.Resources.TransparentBackground;
            this.pColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pColor.Location = new System.Drawing.Point(440, 353);
            this.pColor.Name = "pColor";
            this.pColor.Size = new System.Drawing.Size(78, 78);
            this.pColor.TabIndex = 35;
            this.pColor.Click += new System.EventHandler(this.panel3_Click);
            this.pColor.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Controls.Add(this.pbPickedTexture);
            this.panel2.Location = new System.Drawing.Point(440, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 300);
            this.panel2.TabIndex = 34;
            // 
            // pbPickedTexture
            // 
            this.pbPickedTexture.BackColor = System.Drawing.Color.Transparent;
            this.pbPickedTexture.Location = new System.Drawing.Point(0, 0);
            this.pbPickedTexture.Name = "pbPickedTexture";
            this.pbPickedTexture.Size = new System.Drawing.Size(400, 300);
            this.pbPickedTexture.TabIndex = 0;
            this.pbPickedTexture.TabStop = false;
            this.pbPickedTexture.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPickedTexture_Paint);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.pbTexture);
            this.panel1.Location = new System.Drawing.Point(19, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 300);
            this.panel1.TabIndex = 33;
            this.panel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel1_Scroll);
            // 
            // pbTexture
            // 
            this.pbTexture.BackColor = System.Drawing.Color.Transparent;
            this.pbTexture.Location = new System.Drawing.Point(0, 0);
            this.pbTexture.Name = "pbTexture";
            this.pbTexture.Size = new System.Drawing.Size(400, 300);
            this.pbTexture.TabIndex = 0;
            this.pbTexture.TabStop = false;
            this.pbTexture.Paint += new System.Windows.Forms.PaintEventHandler(this.pbTexture_Paint);
            // 
            // btnNoTexture
            // 
            this.btnNoTexture.Location = new System.Drawing.Point(440, 15);
            this.btnNoTexture.Name = "btnNoTexture";
            this.btnNoTexture.Size = new System.Drawing.Size(92, 20);
            this.btnNoTexture.TabIndex = 36;
            this.btnNoTexture.Text = "No Texture";
            this.btnNoTexture.UseVisualStyleBackColor = true;
            this.btnNoTexture.Click += new System.EventHandler(this.btnNoTexture_Click);
            // 
            // btnSetWhite
            // 
            this.btnSetWhite.Location = new System.Drawing.Point(524, 363);
            this.btnSetWhite.Name = "btnSetWhite";
            this.btnSetWhite.Size = new System.Drawing.Size(82, 20);
            this.btnSetWhite.TabIndex = 37;
            this.btnSetWhite.Text = "White";
            this.btnSetWhite.UseVisualStyleBackColor = false;
            this.btnSetWhite.Click += new System.EventHandler(this.btnSetWhite_Click);
            // 
            // btnSetBlack
            // 
            this.btnSetBlack.BackColor = System.Drawing.SystemColors.Control;
            this.btnSetBlack.Location = new System.Drawing.Point(524, 411);
            this.btnSetBlack.Name = "btnSetBlack";
            this.btnSetBlack.Size = new System.Drawing.Size(82, 20);
            this.btnSetBlack.TabIndex = 38;
            this.btnSetBlack.Text = "Black";
            this.btnSetBlack.UseVisualStyleBackColor = false;
            this.btnSetBlack.Click += new System.EventHandler(this.btnSetBlack_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(738, 353);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(102, 78);
            this.btnBack.TabIndex = 39;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnTransperent
            // 
            this.btnTransperent.BackColor = System.Drawing.SystemColors.Control;
            this.btnTransperent.Location = new System.Drawing.Point(524, 387);
            this.btnTransperent.Name = "btnTransperent";
            this.btnTransperent.Size = new System.Drawing.Size(82, 20);
            this.btnTransperent.TabIndex = 40;
            this.btnTransperent.Text = "Transparent";
            this.btnTransperent.UseVisualStyleBackColor = false;
            this.btnTransperent.Click += new System.EventHandler(this.btnTransperent_Click);
            // 
            // TexturePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 439);
            this.Controls.Add(this.btnTransperent);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSetBlack);
            this.Controls.Add(this.btnSetWhite);
            this.Controls.Add(this.btnNoTexture);
            this.Controls.Add(this.pColor);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gpSize);
            this.Controls.Add(this.tcCoords);
            this.Controls.Add(this.btnSearchFile);
            this.Controls.Add(this.tbTexturePath);
            this.Controls.Add(this.lTP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TexturePicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TexturePicker";
            this.tcCoords.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.gpSize.ResumeLayout(false);
            this.gpSize.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPickedTexture)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTexture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTexture;
        private System.Windows.Forms.Label lTP;
        private System.Windows.Forms.TextBox tbTexturePath;
        private System.Windows.Forms.Button btnSearchFile;
        private System.Windows.Forms.TabControl tcCoords;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbHGrid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbWGrid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbYGrid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbXGrid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbYCustom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbXCustom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.GroupBox gpSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pbPickedTexture;
        private System.Windows.Forms.Panel pColor;
        private System.Windows.Forms.Button btnNoTexture;
        private System.Windows.Forms.Button btnSetWhite;
        private System.Windows.Forms.Button btnSetBlack;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnTransperent;
    }
}