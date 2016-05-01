namespace S5GUIEditor.Options
{
    partial class ButtonWidgetOptions
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
            this.gbOButton = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tOHighLighted = new S5GUIEditor.Options.TextureOptions();
            this.tODisabled = new S5GUIEditor.Options.TextureOptions();
            this.tOPressed = new S5GUIEditor.Options.TextureOptions();
            this.tOHover = new S5GUIEditor.Options.TextureOptions();
            this.tONormal = new S5GUIEditor.Options.TextureOptions();
            this.updateWidgetOptions = new S5GUIEditor.Options.UpdateWidgetOptions();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.s5StringOptions = new S5GUIEditor.Options.S5StringOptions();
            this.tbLuaCommand = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbHighLighted = new System.Windows.Forms.CheckBox();
            this.cbDisabled = new System.Windows.Forms.CheckBox();
            this.tooltipOptions = new S5GUIEditor.Options.TooltipOptions();
            this.gbOButton.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOButton
            // 
            this.gbOButton.Controls.Add(this.tooltipOptions);
            this.gbOButton.Controls.Add(this.groupBox2);
            this.gbOButton.Controls.Add(this.updateWidgetOptions);
            this.gbOButton.Controls.Add(this.groupBox1);
            this.gbOButton.Controls.Add(this.tbLuaCommand);
            this.gbOButton.Controls.Add(this.label1);
            this.gbOButton.Controls.Add(this.cbHighLighted);
            this.gbOButton.Controls.Add(this.cbDisabled);
            this.gbOButton.Location = new System.Drawing.Point(0, 0);
            this.gbOButton.Name = "gbOButton";
            this.gbOButton.Size = new System.Drawing.Size(996, 282);
            this.gbOButton.TabIndex = 3;
            this.gbOButton.TabStop = false;
            this.gbOButton.Text = "Button";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tOHighLighted);
            this.groupBox2.Controls.Add(this.tODisabled);
            this.groupBox2.Controls.Add(this.tOPressed);
            this.groupBox2.Controls.Add(this.tOHover);
            this.groupBox2.Controls.Add(this.tONormal);
            this.groupBox2.Location = new System.Drawing.Point(654, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 147);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Button Textures:";
            // 
            // tOHighLighted
            // 
            this.tOHighLighted.Location = new System.Drawing.Point(35, 118);
            this.tOHighLighted.Name = "tOHighLighted";
            this.tOHighLighted.Size = new System.Drawing.Size(221, 23);
            this.tOHighLighted.TabIndex = 4;
            // 
            // tODisabled
            // 
            this.tODisabled.Location = new System.Drawing.Point(35, 93);
            this.tODisabled.Name = "tODisabled";
            this.tODisabled.Size = new System.Drawing.Size(221, 23);
            this.tODisabled.TabIndex = 3;
            // 
            // tOPressed
            // 
            this.tOPressed.Location = new System.Drawing.Point(35, 68);
            this.tOPressed.Name = "tOPressed";
            this.tOPressed.Size = new System.Drawing.Size(221, 23);
            this.tOPressed.TabIndex = 2;
            // 
            // tOHover
            // 
            this.tOHover.Location = new System.Drawing.Point(35, 43);
            this.tOHover.Name = "tOHover";
            this.tOHover.Size = new System.Drawing.Size(221, 23);
            this.tOHover.TabIndex = 1;
            // 
            // tONormal
            // 
            this.tONormal.Location = new System.Drawing.Point(35, 18);
            this.tONormal.Name = "tONormal";
            this.tONormal.Size = new System.Drawing.Size(221, 23);
            this.tONormal.TabIndex = 0;
            // 
            // updateWidgetOptions
            // 
            this.updateWidgetOptions.Location = new System.Drawing.Point(654, 23);
            this.updateWidgetOptions.Name = "updateWidgetOptions";
            this.updateWidgetOptions.Size = new System.Drawing.Size(310, 75);
            this.updateWidgetOptions.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.s5StringOptions);
            this.groupBox1.Location = new System.Drawing.Point(320, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 75);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Short Cut String:";
            // 
            // s5StringOptions
            // 
            this.s5StringOptions.Location = new System.Drawing.Point(6, 17);
            this.s5StringOptions.Name = "s5StringOptions";
            this.s5StringOptions.Size = new System.Drawing.Size(239, 52);
            this.s5StringOptions.TabIndex = 0;
            // 
            // tbLuaCommand
            // 
            this.tbLuaCommand.Location = new System.Drawing.Point(119, 78);
            this.tbLuaCommand.Name = "tbLuaCommand";
            this.tbLuaCommand.Size = new System.Drawing.Size(142, 20);
            this.tbLuaCommand.TabIndex = 24;
            this.tbLuaCommand.TextChanged += new System.EventHandler(this.tbLuaCommand_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Lua Command:";
            // 
            // cbHighLighted
            // 
            this.cbHighLighted.AutoSize = true;
            this.cbHighLighted.Location = new System.Drawing.Point(23, 52);
            this.cbHighLighted.Name = "cbHighLighted";
            this.cbHighLighted.Size = new System.Drawing.Size(83, 17);
            this.cbHighLighted.TabIndex = 22;
            this.cbHighLighted.Text = "HighLighted";
            this.cbHighLighted.UseVisualStyleBackColor = true;
            this.cbHighLighted.CheckedChanged += new System.EventHandler(this.cbHighLighted_CheckedChanged);
            // 
            // cbDisabled
            // 
            this.cbDisabled.AutoSize = true;
            this.cbDisabled.Location = new System.Drawing.Point(23, 29);
            this.cbDisabled.Name = "cbDisabled";
            this.cbDisabled.Size = new System.Drawing.Size(67, 17);
            this.cbDisabled.TabIndex = 21;
            this.cbDisabled.Text = "Disabled";
            this.cbDisabled.UseVisualStyleBackColor = true;
            this.cbDisabled.CheckedChanged += new System.EventHandler(this.cbDisabled_CheckedChanged);
            // 
            // tooltipOptions
            // 
            this.tooltipOptions.Location = new System.Drawing.Point(23, 129);
            this.tooltipOptions.Name = "tooltipOptions";
            this.tooltipOptions.Size = new System.Drawing.Size(580, 132);
            this.tooltipOptions.TabIndex = 29;
            // 
            // ButtonWidgetOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOButton);
            this.Name = "ButtonWidgetOptions";
            this.Size = new System.Drawing.Size(996, 282);
            this.gbOButton.ResumeLayout(false);
            this.gbOButton.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOButton;
        private System.Windows.Forms.CheckBox cbDisabled;
        private System.Windows.Forms.CheckBox cbHighLighted;
        private System.Windows.Forms.TextBox tbLuaCommand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private S5StringOptions s5StringOptions;
        private System.Windows.Forms.GroupBox groupBox2;
        private UpdateWidgetOptions updateWidgetOptions;
        private TextureOptions tOHighLighted;
        private TextureOptions tODisabled;
        private TextureOptions tOPressed;
        private TextureOptions tOHover;
        private TextureOptions tONormal;
        private TooltipOptions tooltipOptions;

    }
}
