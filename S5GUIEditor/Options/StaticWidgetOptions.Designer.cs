namespace S5GUIEditor.Options
{
    partial class StaticWidgetOptions
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
            this.gbOStatic = new System.Windows.Forms.GroupBox();
            this.tOBackground = new S5GUIEditor.Options.TextureOptions();
            this.gbOStatic.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOStatic
            // 
            this.gbOStatic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOStatic.Controls.Add(this.tOBackground);
            this.gbOStatic.Location = new System.Drawing.Point(0, 0);
            this.gbOStatic.Name = "gbOStatic";
            this.gbOStatic.Size = new System.Drawing.Size(996, 66);
            this.gbOStatic.TabIndex = 2;
            this.gbOStatic.TabStop = false;
            this.gbOStatic.Text = "Static";
            // 
            // tOBackground
            // 
            this.tOBackground.Location = new System.Drawing.Point(19, 25);
            this.tOBackground.Name = "tOBackground";
            this.tOBackground.Size = new System.Drawing.Size(221, 23);
            this.tOBackground.TabIndex = 0;
            // 
            // StaticWidgetOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOStatic);
            this.Name = "StaticWidgetOptions";
            this.Size = new System.Drawing.Size(996, 66);
            this.gbOStatic.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOStatic;
        private TextureOptions tOBackground;
    }
}
