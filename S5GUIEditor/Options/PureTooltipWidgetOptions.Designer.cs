namespace S5GUIEditor.Options
{
    partial class PureTooltipWidgetOptions
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
            this.gbOPureTooltip = new System.Windows.Forms.GroupBox();
            this.tooltipOptions = new S5GUIEditor.Options.TooltipOptions();
            this.gbOPureTooltip.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOPureTooltip
            // 
            this.gbOPureTooltip.Controls.Add(this.tooltipOptions);
            this.gbOPureTooltip.Location = new System.Drawing.Point(0, 0);
            this.gbOPureTooltip.Name = "gbOPureTooltip";
            this.gbOPureTooltip.Size = new System.Drawing.Size(996, 166);
            this.gbOPureTooltip.TabIndex = 3;
            this.gbOPureTooltip.TabStop = false;
            this.gbOPureTooltip.Text = "PureTooltip";
            // 
            // tooltipOptions
            // 
            this.tooltipOptions.Location = new System.Drawing.Point(19, 23);
            this.tooltipOptions.Name = "tooltipOptions";
            this.tooltipOptions.Size = new System.Drawing.Size(580, 132);
            this.tooltipOptions.TabIndex = 1;
            // 
            // PureTooltipWidgetOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOPureTooltip);
            this.Name = "PureTooltipWidgetOptions";
            this.Size = new System.Drawing.Size(996, 166);
            this.gbOPureTooltip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOPureTooltip;
        private TooltipOptions tooltipOptions;
    }
}
