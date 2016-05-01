namespace S5GUIEditor.Options
{
    partial class BaseWidgetOptions
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
            this.gbOWidget = new System.Windows.Forms.GroupBox();
            this.tbZPriority = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbGroup = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbFTNBFF = new System.Windows.Forms.CheckBox();
            this.cbFTHMEF = new System.Windows.Forms.CheckBox();
            this.cbIsShown = new System.Windows.Forms.CheckBox();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbYPos = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tBXPos = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbWidgetName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbOWidget.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOWidget
            // 
            this.gbOWidget.Controls.Add(this.tbZPriority);
            this.gbOWidget.Controls.Add(this.label11);
            this.gbOWidget.Controls.Add(this.tbGroup);
            this.gbOWidget.Controls.Add(this.label10);
            this.gbOWidget.Controls.Add(this.cbFTNBFF);
            this.gbOWidget.Controls.Add(this.cbFTHMEF);
            this.gbOWidget.Controls.Add(this.cbIsShown);
            this.gbOWidget.Controls.Add(this.tbHeight);
            this.gbOWidget.Controls.Add(this.label9);
            this.gbOWidget.Controls.Add(this.tbWidth);
            this.gbOWidget.Controls.Add(this.label8);
            this.gbOWidget.Controls.Add(this.tbYPos);
            this.gbOWidget.Controls.Add(this.label7);
            this.gbOWidget.Controls.Add(this.tBXPos);
            this.gbOWidget.Controls.Add(this.label6);
            this.gbOWidget.Controls.Add(this.tbWidgetName);
            this.gbOWidget.Controls.Add(this.label5);
            this.gbOWidget.Location = new System.Drawing.Point(0, 0);
            this.gbOWidget.Name = "gbOWidget";
            this.gbOWidget.Size = new System.Drawing.Size(996, 119);
            this.gbOWidget.TabIndex = 1;
            this.gbOWidget.TabStop = false;
            this.gbOWidget.Text = "Widget";
            // 
            // tbZPriority
            // 
            this.tbZPriority.Location = new System.Drawing.Point(558, 60);
            this.tbZPriority.Name = "tbZPriority";
            this.tbZPriority.Size = new System.Drawing.Size(45, 20);
            this.tbZPriority.TabIndex = 17;
            this.tbZPriority.TextChanged += new System.EventHandler(this.tbZPriority_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(508, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "ZPriority:";
            // 
            // tbGroup
            // 
            this.tbGroup.Location = new System.Drawing.Point(558, 30);
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Size = new System.Drawing.Size(169, 20);
            this.tbGroup.TabIndex = 15;
            this.tbGroup.TextChanged += new System.EventHandler(this.tbGroup_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(508, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Group:";
            // 
            // cbFTNBFF
            // 
            this.cbFTNBFF.AutoSize = true;
            this.cbFTNBFF.Location = new System.Drawing.Point(289, 81);
            this.cbFTNBFF.Name = "cbFTNBFF";
            this.cbFTNBFF.Size = new System.Drawing.Size(158, 17);
            this.cbFTNBFF.TabIndex = 13;
            this.cbFTNBFF.Text = "ForceToNeverBeFoundFlag";
            this.cbFTNBFF.UseVisualStyleBackColor = true;
            this.cbFTNBFF.CheckedChanged += new System.EventHandler(this.cbFTNBFF_CheckedChanged);
            // 
            // cbFTHMEF
            // 
            this.cbFTHMEF.AutoSize = true;
            this.cbFTHMEF.Location = new System.Drawing.Point(289, 56);
            this.cbFTHMEF.Name = "cbFTHMEF";
            this.cbFTHMEF.Size = new System.Drawing.Size(185, 17);
            this.cbFTHMEF.TabIndex = 12;
            this.cbFTHMEF.Text = "ForceToHandleMouseEventsFlag";
            this.cbFTHMEF.UseVisualStyleBackColor = true;
            this.cbFTHMEF.CheckedChanged += new System.EventHandler(this.cbFTHMEF_CheckedChanged);
            // 
            // cbIsShown
            // 
            this.cbIsShown.AutoSize = true;
            this.cbIsShown.Location = new System.Drawing.Point(289, 31);
            this.cbIsShown.Name = "cbIsShown";
            this.cbIsShown.Size = new System.Drawing.Size(67, 17);
            this.cbIsShown.TabIndex = 11;
            this.cbIsShown.Text = "IsShown";
            this.cbIsShown.UseVisualStyleBackColor = true;
            this.cbIsShown.CheckedChanged += new System.EventHandler(this.cbIsShown_CheckedChanged);
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(194, 82);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(45, 20);
            this.tbHeight.TabIndex = 9;
            this.tbHeight.TextChanged += new System.EventHandler(this.tbHeight_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(145, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "H:";
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(194, 57);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(45, 20);
            this.tbWidth.TabIndex = 7;
            this.tbWidth.TextChanged += new System.EventHandler(this.tbWidth_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(145, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "W:";
            // 
            // tbYPos
            // 
            this.tbYPos.Location = new System.Drawing.Point(70, 82);
            this.tbYPos.Name = "tbYPos";
            this.tbYPos.Size = new System.Drawing.Size(45, 20);
            this.tbYPos.TabIndex = 5;
            this.tbYPos.TextChanged += new System.EventHandler(this.tbYPos_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Y:";
            // 
            // tBXPos
            // 
            this.tBXPos.Location = new System.Drawing.Point(70, 57);
            this.tBXPos.Name = "tBXPos";
            this.tBXPos.Size = new System.Drawing.Size(45, 20);
            this.tBXPos.TabIndex = 3;
            this.tBXPos.TextChanged += new System.EventHandler(this.tBXPos_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "X:";
            // 
            // tbWidgetName
            // 
            this.tbWidgetName.Enabled = false;
            this.tbWidgetName.Location = new System.Drawing.Point(70, 27);
            this.tbWidgetName.Name = "tbWidgetName";
            this.tbWidgetName.Size = new System.Drawing.Size(169, 20);
            this.tbWidgetName.TabIndex = 1;
            this.tbWidgetName.TextChanged += new System.EventHandler(this.tbWidgetName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Name:";
            // 
            // BaseWidgetOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOWidget);
            this.Name = "BaseWidgetOptions";
            this.Size = new System.Drawing.Size(996, 119);
            this.gbOWidget.ResumeLayout(false);
            this.gbOWidget.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOWidget;
        private System.Windows.Forms.TextBox tbZPriority;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbGroup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox cbFTNBFF;
        private System.Windows.Forms.CheckBox cbFTHMEF;
        private System.Windows.Forms.CheckBox cbIsShown;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbYPos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tBXPos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbWidgetName;
        private System.Windows.Forms.Label label5;

    }
}
