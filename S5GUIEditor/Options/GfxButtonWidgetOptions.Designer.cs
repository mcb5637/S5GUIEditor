namespace S5GUIEditor.Options
{
    partial class GfxButtonWidgetOptions
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
            this.gbOGfxButton = new System.Windows.Forms.GroupBox();
            this.tOIconMaterial = new S5GUIEditor.Options.TextureOptions();
            this.gbOGfxButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOGfxButton
            // 
            this.gbOGfxButton.Controls.Add(this.tOIconMaterial);
            this.gbOGfxButton.Location = new System.Drawing.Point(0, 0);
            this.gbOGfxButton.Name = "gbOGfxButton";
            this.gbOGfxButton.Size = new System.Drawing.Size(996, 66);
            this.gbOGfxButton.TabIndex = 3;
            this.gbOGfxButton.TabStop = false;
            this.gbOGfxButton.Text = "Gfx Button";
            // 
            // tOIconMaterial
            // 
            this.tOIconMaterial.Location = new System.Drawing.Point(19, 25);
            this.tOIconMaterial.Name = "tOIconMaterial";
            this.tOIconMaterial.Size = new System.Drawing.Size(221, 23);
            this.tOIconMaterial.TabIndex = 0;
            // 
            // GfxButtonWidgetOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOGfxButton);
            this.Name = "GfxButtonWidgetOptions";
            this.Size = new System.Drawing.Size(996, 66);
            this.gbOGfxButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOGfxButton;
        private TextureOptions tOIconMaterial;
    }
}
