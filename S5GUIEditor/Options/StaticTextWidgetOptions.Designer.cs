namespace S5GUIEditor.Options
{
    partial class StaticTextWidgetOptions
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
            this.gbOStaticText = new System.Windows.Forms.GroupBox();
            this.tbLineDistanceFactor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNumberOfLinesToPrint = new System.Windows.Forms.TextBox();
            this.tbFirstLineToPrint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.updateWidgetOptions = new S5GUIEditor.Options.UpdateWidgetOptions();
            this.s5woText = new S5GUIEditor.Options.S5WritingOptions();
            this.tOBackground = new S5GUIEditor.Options.TextureOptions();
            this.gbOStaticText.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOStaticText
            // 
            this.gbOStaticText.Controls.Add(this.tbLineDistanceFactor);
            this.gbOStaticText.Controls.Add(this.label3);
            this.gbOStaticText.Controls.Add(this.tbNumberOfLinesToPrint);
            this.gbOStaticText.Controls.Add(this.tbFirstLineToPrint);
            this.gbOStaticText.Controls.Add(this.label2);
            this.gbOStaticText.Controls.Add(this.label1);
            this.gbOStaticText.Controls.Add(this.updateWidgetOptions);
            this.gbOStaticText.Controls.Add(this.s5woText);
            this.gbOStaticText.Controls.Add(this.tOBackground);
            this.gbOStaticText.Location = new System.Drawing.Point(0, 0);
            this.gbOStaticText.Name = "gbOStaticText";
            this.gbOStaticText.Size = new System.Drawing.Size(996, 200);
            this.gbOStaticText.TabIndex = 3;
            this.gbOStaticText.TabStop = false;
            this.gbOStaticText.Text = "StaticText";
            // 
            // tbLineDistanceFactor
            // 
            this.tbLineDistanceFactor.Location = new System.Drawing.Point(293, 138);
            this.tbLineDistanceFactor.Name = "tbLineDistanceFactor";
            this.tbLineDistanceFactor.Size = new System.Drawing.Size(36, 20);
            this.tbLineDistanceFactor.TabIndex = 8;
            this.tbLineDistanceFactor.TextChanged += new System.EventHandler(this.tbLineDistanceFactor_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "LineDistanceFactor:";
            // 
            // tbNumberOfLinesToPrint
            // 
            this.tbNumberOfLinesToPrint.Location = new System.Drawing.Point(134, 164);
            this.tbNumberOfLinesToPrint.Name = "tbNumberOfLinesToPrint";
            this.tbNumberOfLinesToPrint.Size = new System.Drawing.Size(36, 20);
            this.tbNumberOfLinesToPrint.TabIndex = 6;
            this.tbNumberOfLinesToPrint.TextChanged += new System.EventHandler(this.tbNumberOfLinesToPrint_TextChanged);
            // 
            // tbFirstLineToPrint
            // 
            this.tbFirstLineToPrint.Location = new System.Drawing.Point(134, 138);
            this.tbFirstLineToPrint.Name = "tbFirstLineToPrint";
            this.tbFirstLineToPrint.Size = new System.Drawing.Size(36, 20);
            this.tbFirstLineToPrint.TabIndex = 5;
            this.tbFirstLineToPrint.TextChanged += new System.EventHandler(this.tbFirstLineToPrint_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "NumberOfLinesToPrint:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "FirstLineToPrint:";
            // 
            // updateWidgetOptions
            // 
            this.updateWidgetOptions.Location = new System.Drawing.Point(19, 54);
            this.updateWidgetOptions.Name = "updateWidgetOptions";
            this.updateWidgetOptions.Size = new System.Drawing.Size(310, 75);
            this.updateWidgetOptions.TabIndex = 2;
            // 
            // s5woText
            // 
            this.s5woText.Location = new System.Drawing.Point(395, 25);
            this.s5woText.Name = "s5woText";
            this.s5woText.Size = new System.Drawing.Size(525, 170);
            this.s5woText.TabIndex = 1;
            // 
            // tOBackground
            // 
            this.tOBackground.Location = new System.Drawing.Point(19, 25);
            this.tOBackground.Name = "tOBackground";
            this.tOBackground.Size = new System.Drawing.Size(221, 23);
            this.tOBackground.TabIndex = 0;
            // 
            // StaticTextWidgetOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOStaticText);
            this.Name = "StaticTextWidgetOptions";
            this.Size = new System.Drawing.Size(996, 200);
            this.gbOStaticText.ResumeLayout(false);
            this.gbOStaticText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOStaticText;
        private TextureOptions tOBackground;
        private S5WritingOptions s5woText;
        private UpdateWidgetOptions updateWidgetOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNumberOfLinesToPrint;
        private System.Windows.Forms.TextBox tbFirstLineToPrint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLineDistanceFactor;
    }
}
