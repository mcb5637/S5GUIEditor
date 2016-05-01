namespace S5GUIEditor.Options
{
    partial class TextureOptions
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
            this.lTextureName = new System.Windows.Forms.Label();
            this.btnChoose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lTextureName
            // 
            this.lTextureName.AutoSize = true;
            this.lTextureName.Location = new System.Drawing.Point(3, 4);
            this.lTextureName.Name = "lTextureName";
            this.lTextureName.Size = new System.Drawing.Size(74, 13);
            this.lTextureName.TabIndex = 0;
            this.lTextureName.Text = "TextureName:";
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(128, -1);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(93, 22);
            this.btnChoose.TabIndex = 1;
            this.btnChoose.Text = "Choose";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // TextureOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.lTextureName);
            this.Name = "TextureOptions";
            this.Size = new System.Drawing.Size(221, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lTextureName;
        private System.Windows.Forms.Button btnChoose;
    }
}
