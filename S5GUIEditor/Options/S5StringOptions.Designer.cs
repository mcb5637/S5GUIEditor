namespace S5GUIEditor.Options
{
    partial class S5StringOptions
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
            this.tbStringTableKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRawString = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbStringTableKey
            // 
            this.tbStringTableKey.Location = new System.Drawing.Point(95, 3);
            this.tbStringTableKey.Name = "tbStringTableKey";
            this.tbStringTableKey.Size = new System.Drawing.Size(139, 20);
            this.tbStringTableKey.TabIndex = 0;
            this.tbStringTableKey.TextChanged += new System.EventHandler(this.tbStringTableKey_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "StringTableKey:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Raw String:";
            // 
            // tbRawString
            // 
            this.tbRawString.Location = new System.Drawing.Point(95, 29);
            this.tbRawString.Name = "tbRawString";
            this.tbRawString.Size = new System.Drawing.Size(139, 20);
            this.tbRawString.TabIndex = 3;
            this.tbRawString.TextChanged += new System.EventHandler(this.tbRawString_TextChanged);
            // 
            // S5StringOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRawString);
            this.Controls.Add(this.tbStringTableKey);
            this.Controls.Add(this.label2);
            this.Name = "S5StringOptions";
            this.Size = new System.Drawing.Size(239, 52);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbStringTableKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRawString;
    }
}
