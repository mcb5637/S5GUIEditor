namespace S5GUIEditor.Options
{
    partial class ProgressBarWidgetOptions
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
            this.gbOProgressBar = new System.Windows.Forms.GroupBox();
            this.tbProgressBarLimit = new System.Windows.Forms.TextBox();
            this.tbProgressBarValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.updateWidgetOptions = new S5GUIEditor.Options.UpdateWidgetOptions();
            this.toBackground = new S5GUIEditor.Options.TextureOptions();
            this.gbOProgressBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOProgressBar
            // 
            this.gbOProgressBar.Controls.Add(this.tbProgressBarLimit);
            this.gbOProgressBar.Controls.Add(this.tbProgressBarValue);
            this.gbOProgressBar.Controls.Add(this.label2);
            this.gbOProgressBar.Controls.Add(this.label1);
            this.gbOProgressBar.Controls.Add(this.updateWidgetOptions);
            this.gbOProgressBar.Controls.Add(this.toBackground);
            this.gbOProgressBar.Location = new System.Drawing.Point(0, 0);
            this.gbOProgressBar.Name = "gbOProgressBar";
            this.gbOProgressBar.Size = new System.Drawing.Size(996, 123);
            this.gbOProgressBar.TabIndex = 4;
            this.gbOProgressBar.TabStop = false;
            this.gbOProgressBar.Text = "ProgressBar";
            // 
            // tbProgressBarLimit
            // 
            this.tbProgressBarLimit.Location = new System.Drawing.Point(148, 85);
            this.tbProgressBarLimit.Name = "tbProgressBarLimit";
            this.tbProgressBarLimit.Size = new System.Drawing.Size(40, 20);
            this.tbProgressBarLimit.TabIndex = 5;
            this.tbProgressBarLimit.TextChanged += new System.EventHandler(this.tbProgressBarLimit_TextChanged);
            // 
            // tbProgressBarValue
            // 
            this.tbProgressBarValue.Location = new System.Drawing.Point(148, 61);
            this.tbProgressBarValue.Name = "tbProgressBarValue";
            this.tbProgressBarValue.Size = new System.Drawing.Size(40, 20);
            this.tbProgressBarValue.TabIndex = 4;
            this.tbProgressBarValue.TextChanged += new System.EventHandler(this.tbProgressBarValue_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ProgressBarLimit:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ProgressBarValue:";
            // 
            // updateWidgetOptions
            // 
            this.updateWidgetOptions.Location = new System.Drawing.Point(288, 30);
            this.updateWidgetOptions.Name = "updateWidgetOptions";
            this.updateWidgetOptions.Size = new System.Drawing.Size(310, 75);
            this.updateWidgetOptions.TabIndex = 1;
            // 
            // toBackground
            // 
            this.toBackground.Location = new System.Drawing.Point(20, 30);
            this.toBackground.Name = "toBackground";
            this.toBackground.Size = new System.Drawing.Size(221, 23);
            this.toBackground.TabIndex = 0;
            // 
            // ProgressBarWidgetOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOProgressBar);
            this.Name = "ProgressBarWidgetOptions";
            this.Size = new System.Drawing.Size(996, 126);
            this.gbOProgressBar.ResumeLayout(false);
            this.gbOProgressBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOProgressBar;
        private TextureOptions toBackground;
        private UpdateWidgetOptions updateWidgetOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbProgressBarLimit;
        private System.Windows.Forms.TextBox tbProgressBarValue;
        private System.Windows.Forms.Label label2;
    }
}
