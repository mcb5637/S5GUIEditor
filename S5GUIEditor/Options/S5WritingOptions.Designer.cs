namespace S5GUIEditor.Options
{
    partial class S5WritingOptions
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnChooseFontPath = new System.Windows.Forms.Button();
            this.gBText = new System.Windows.Forms.GroupBox();
            this.s5soText = new S5GUIEditor.Options.S5StringOptions();
            this.label2 = new System.Windows.Forms.Label();
            this.gbName = new System.Windows.Forms.GroupBox();
            this.btnTransperent = new System.Windows.Forms.Button();
            this.btnSetBlack = new System.Windows.Forms.Button();
            this.btnSetWhite = new System.Windows.Forms.Button();
            this.pbColor = new System.Windows.Forms.PictureBox();
            this.tbFontPath = new System.Windows.Forms.TextBox();
            this.tbStringFrameDistance = new System.Windows.Forms.TextBox();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.gBText.SuspendLayout();
            this.gbName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "FontPath:";
            // 
            // btnChooseFontPath
            // 
            this.btnChooseFontPath.Location = new System.Drawing.Point(209, 17);
            this.btnChooseFontPath.Name = "btnChooseFontPath";
            this.btnChooseFontPath.Size = new System.Drawing.Size(55, 21);
            this.btnChooseFontPath.TabIndex = 2;
            this.btnChooseFontPath.Text = "Choose";
            this.btnChooseFontPath.Click += new System.EventHandler(this.btnChooseFont_Click);
            // 
            // gBText
            // 
            this.gBText.Controls.Add(this.s5soText);
            this.gBText.Location = new System.Drawing.Point(19, 44);
            this.gBText.Name = "gBText";
            this.gBText.Size = new System.Drawing.Size(246, 78);
            this.gBText.TabIndex = 4;
            this.gBText.TabStop = false;
            this.gBText.Text = "Text:";
            // 
            // s5soText
            // 
            this.s5soText.Location = new System.Drawing.Point(6, 19);
            this.s5soText.Name = "s5soText";
            this.s5soText.Size = new System.Drawing.Size(239, 52);
            this.s5soText.TabIndex = 3;
            this.s5soText.ContentChanged += new System.EventHandler(this.s5soText_ContentChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "StringFrameDistance:";
            // 
            // gbName
            // 
            this.gbName.Controls.Add(this.btnTransperent);
            this.gbName.Controls.Add(this.btnSetBlack);
            this.gbName.Controls.Add(this.btnSetWhite);
            this.gbName.Controls.Add(this.pbColor);
            this.gbName.Controls.Add(this.tbFontPath);
            this.gbName.Controls.Add(this.tbStringFrameDistance);
            this.gbName.Controls.Add(this.label1);
            this.gbName.Controls.Add(this.label2);
            this.gbName.Controls.Add(this.pbPreview);
            this.gbName.Controls.Add(this.gBText);
            this.gbName.Controls.Add(this.btnChooseFontPath);
            this.gbName.Location = new System.Drawing.Point(0, 0);
            this.gbName.Name = "gbName";
            this.gbName.Size = new System.Drawing.Size(525, 170);
            this.gbName.TabIndex = 6;
            this.gbName.TabStop = false;
            this.gbName.Text = "name:";
            // 
            // btnTransperent
            // 
            this.btnTransperent.BackColor = System.Drawing.SystemColors.Control;
            this.btnTransperent.Location = new System.Drawing.Point(381, 78);
            this.btnTransperent.Name = "btnTransperent";
            this.btnTransperent.Size = new System.Drawing.Size(82, 20);
            this.btnTransperent.TabIndex = 46;
            this.btnTransperent.Text = "Transparent";
            this.btnTransperent.UseVisualStyleBackColor = false;
            this.btnTransperent.Click += new System.EventHandler(this.btnTransperent_Click);
            // 
            // btnSetBlack
            // 
            this.btnSetBlack.BackColor = System.Drawing.SystemColors.Control;
            this.btnSetBlack.Location = new System.Drawing.Point(381, 102);
            this.btnSetBlack.Name = "btnSetBlack";
            this.btnSetBlack.Size = new System.Drawing.Size(82, 20);
            this.btnSetBlack.TabIndex = 45;
            this.btnSetBlack.Text = "Black";
            this.btnSetBlack.UseVisualStyleBackColor = false;
            this.btnSetBlack.Click += new System.EventHandler(this.btnSetBlack_Click);
            // 
            // btnSetWhite
            // 
            this.btnSetWhite.Location = new System.Drawing.Point(381, 54);
            this.btnSetWhite.Name = "btnSetWhite";
            this.btnSetWhite.Size = new System.Drawing.Size(82, 20);
            this.btnSetWhite.TabIndex = 44;
            this.btnSetWhite.Text = "White";
            this.btnSetWhite.UseVisualStyleBackColor = false;
            this.btnSetWhite.Click += new System.EventHandler(this.btnSetWhite_Click);
            // 
            // pbColor
            // 
            this.pbColor.BackgroundImage = global::S5GUIEditor.Properties.Resources.TransparentBackground;
            this.pbColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColor.Location = new System.Drawing.Point(298, 44);
            this.pbColor.Name = "pbColor";
            this.pbColor.Size = new System.Drawing.Size(77, 77);
            this.pbColor.TabIndex = 43;
            this.pbColor.TabStop = false;
            this.pbColor.Click += new System.EventHandler(this.pbColor_Click);
            this.pbColor.Paint += new System.Windows.Forms.PaintEventHandler(this.pbColor_Paint);
            // 
            // tbFontPath
            // 
            this.tbFontPath.Location = new System.Drawing.Point(75, 18);
            this.tbFontPath.Name = "tbFontPath";
            this.tbFontPath.Size = new System.Drawing.Size(128, 20);
            this.tbFontPath.TabIndex = 42;
            // 
            // tbStringFrameDistance
            // 
            this.tbStringFrameDistance.Location = new System.Drawing.Point(421, 18);
            this.tbStringFrameDistance.Name = "tbStringFrameDistance";
            this.tbStringFrameDistance.Size = new System.Drawing.Size(86, 20);
            this.tbStringFrameDistance.TabIndex = 6;
            this.tbStringFrameDistance.TextChanged += new System.EventHandler(this.tbStringFrameDistance_TextChanged);
            // 
            // pbPreview
            // 
            this.pbPreview.BackgroundImage = global::S5GUIEditor.Properties.Resources.TransparentBackground;
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPreview.Location = new System.Drawing.Point(19, 135);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(488, 32);
            this.pbPreview.TabIndex = 1;
            this.pbPreview.TabStop = false;
            this.pbPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPreview_Paint);
            // 
            // S5WritingOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbName);
            this.Name = "S5WritingOptions";
            this.Size = new System.Drawing.Size(525, 170);
            this.gBText.ResumeLayout(false);
            this.gbName.ResumeLayout(false);
            this.gbName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Button btnChooseFontPath;
        private S5StringOptions s5soText;
        private System.Windows.Forms.GroupBox gBText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbName;
        private System.Windows.Forms.TextBox tbStringFrameDistance;
        private System.Windows.Forms.TextBox tbFontPath;
        private System.Windows.Forms.PictureBox pbColor;
        private System.Windows.Forms.Button btnTransperent;
        private System.Windows.Forms.Button btnSetBlack;
        private System.Windows.Forms.Button btnSetWhite;
    }
}
