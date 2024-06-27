namespace S5GUIEditor.Options
{
    partial class TextButtonWidgetOptions
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
            this.gbOTextButton = new System.Windows.Forms.GroupBox();
            this.s5WritingOptions = new S5GUIEditor.Options.S5WritingOptions();
            this.CB_Centered = new System.Windows.Forms.CheckBox();
            this.gbOTextButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOTextButton
            // 
            this.gbOTextButton.Controls.Add(this.CB_Centered);
            this.gbOTextButton.Controls.Add(this.s5WritingOptions);
            this.gbOTextButton.Location = new System.Drawing.Point(0, 0);
            this.gbOTextButton.Name = "gbOTextButton";
            this.gbOTextButton.Size = new System.Drawing.Size(996, 193);
            this.gbOTextButton.TabIndex = 4;
            this.gbOTextButton.TabStop = false;
            this.gbOTextButton.Text = "Text Button";
            // 
            // s5WritingOptions
            // 
            this.s5WritingOptions.Location = new System.Drawing.Point(22, 19);
            this.s5WritingOptions.Name = "s5WritingOptions";
            this.s5WritingOptions.Size = new System.Drawing.Size(525, 170);
            this.s5WritingOptions.TabIndex = 0;
            // 
            // CB_Centered
            // 
            this.CB_Centered.AutoSize = true;
            this.CB_Centered.Location = new System.Drawing.Point(553, 19);
            this.CB_Centered.Name = "CB_Centered";
            this.CB_Centered.Size = new System.Drawing.Size(69, 17);
            this.CB_Centered.TabIndex = 1;
            this.CB_Centered.Text = "Centered";
            this.CB_Centered.UseVisualStyleBackColor = true;
            this.CB_Centered.CheckedChanged += new System.EventHandler(this.CB_Centered_CheckedChanged);
            // 
            // TextButtonWidgetOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOTextButton);
            this.Name = "TextButtonWidgetOptions";
            this.Size = new System.Drawing.Size(996, 193);
            this.gbOTextButton.ResumeLayout(false);
            this.gbOTextButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOTextButton;
        private S5WritingOptions s5WritingOptions;
        private System.Windows.Forms.CheckBox CB_Centered;
    }
}
