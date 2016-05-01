namespace S5GUIEditor.Options
{
    partial class TooltipOptions
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
            this.gbName = new System.Windows.Forms.GroupBox();
            this.cbEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.s5soText = new S5GUIEditor.Options.S5StringOptions();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTargetWidget = new System.Windows.Forms.TextBox();
            this.cbControlTargetWidgetDisplayState = new System.Windows.Forms.CheckBox();
            this.tbLuaCommand = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbName.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbName
            // 
            this.gbName.Controls.Add(this.tbLuaCommand);
            this.gbName.Controls.Add(this.groupBox1);
            this.gbName.Controls.Add(this.label2);
            this.gbName.Controls.Add(this.cbControlTargetWidgetDisplayState);
            this.gbName.Controls.Add(this.tbTargetWidget);
            this.gbName.Controls.Add(this.label1);
            this.gbName.Controls.Add(this.cbEnabled);
            this.gbName.Location = new System.Drawing.Point(0, 0);
            this.gbName.Name = "gbName";
            this.gbName.Size = new System.Drawing.Size(580, 132);
            this.gbName.TabIndex = 0;
            this.gbName.TabStop = false;
            this.gbName.Text = "Tooltip:";
            // 
            // cbEnabled
            // 
            this.cbEnabled.AutoSize = true;
            this.cbEnabled.Location = new System.Drawing.Point(23, 30);
            this.cbEnabled.Name = "cbEnabled";
            this.cbEnabled.Size = new System.Drawing.Size(65, 17);
            this.cbEnabled.TabIndex = 0;
            this.cbEnabled.Text = "Enabled";
            this.cbEnabled.UseVisualStyleBackColor = true;
            this.cbEnabled.CheckedChanged += new System.EventHandler(this.cbEnabled_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.s5soText);
            this.groupBox1.Location = new System.Drawing.Point(318, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(251, 92);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Text:";
            // 
            // s5soText
            // 
            this.s5soText.Location = new System.Drawing.Point(6, 23);
            this.s5soText.Name = "s5soText";
            this.s5soText.Size = new System.Drawing.Size(239, 52);
            this.s5soText.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "TargetWidget:";
            // 
            // tbTargetWidget
            // 
            this.tbTargetWidget.Location = new System.Drawing.Point(139, 76);
            this.tbTargetWidget.Name = "tbTargetWidget";
            this.tbTargetWidget.Size = new System.Drawing.Size(163, 20);
            this.tbTargetWidget.TabIndex = 3;
            this.tbTargetWidget.TextChanged += new System.EventHandler(this.tbTargetWidget_TextChanged);
            // 
            // cbControlTargetWidgetDisplayState
            // 
            this.cbControlTargetWidgetDisplayState.AutoSize = true;
            this.cbControlTargetWidgetDisplayState.Location = new System.Drawing.Point(23, 53);
            this.cbControlTargetWidgetDisplayState.Name = "cbControlTargetWidgetDisplayState";
            this.cbControlTargetWidgetDisplayState.Size = new System.Drawing.Size(183, 17);
            this.cbControlTargetWidgetDisplayState.TabIndex = 4;
            this.cbControlTargetWidgetDisplayState.Text = "ControlTargetWidgetDisplayState";
            this.cbControlTargetWidgetDisplayState.UseVisualStyleBackColor = true;
            this.cbControlTargetWidgetDisplayState.CheckedChanged += new System.EventHandler(this.cbControlTargetWidgetDisplayState_CheckedChanged);
            // 
            // tbLuaCommand
            // 
            this.tbLuaCommand.Location = new System.Drawing.Point(139, 102);
            this.tbLuaCommand.Name = "tbLuaCommand";
            this.tbLuaCommand.Size = new System.Drawing.Size(163, 20);
            this.tbLuaCommand.TabIndex = 6;
            this.tbLuaCommand.TextChanged += new System.EventHandler(this.tbLuaCommand_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "LuaUpdateCommand:";
            // 
            // TooltipOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbName);
            this.Name = "TooltipOptions";
            this.Size = new System.Drawing.Size(580, 132);
            this.gbName.ResumeLayout(false);
            this.gbName.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbName;
        private System.Windows.Forms.CheckBox cbEnabled;
        private System.Windows.Forms.GroupBox groupBox1;
        private S5StringOptions s5soText;
        private System.Windows.Forms.CheckBox cbControlTargetWidgetDisplayState;
        private System.Windows.Forms.TextBox tbTargetWidget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbLuaCommand;
        private System.Windows.Forms.Label label2;
    }
}
