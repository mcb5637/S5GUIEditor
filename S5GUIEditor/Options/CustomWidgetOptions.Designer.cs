namespace S5GUIEditor.Options
{
    partial class CustomWidgetOptions
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
            this.gbOCustom = new System.Windows.Forms.GroupBox();
            this.btnEncodeColor = new System.Windows.Forms.Button();
            this.tbCustomClassName = new System.Windows.Forms.ComboBox();
            this.lbStringUserVarEx = new System.Windows.Forms.Label();
            this.lbIntUserVarEx = new System.Windows.Forms.Label();
            this.tbStringUserVarDefaultValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbStringUserVar = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbIntUserVarDefaultValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbIntUserVar = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbOCustom.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOCustom
            // 
            this.gbOCustom.Controls.Add(this.btnEncodeColor);
            this.gbOCustom.Controls.Add(this.tbCustomClassName);
            this.gbOCustom.Controls.Add(this.lbStringUserVarEx);
            this.gbOCustom.Controls.Add(this.lbIntUserVarEx);
            this.gbOCustom.Controls.Add(this.tbStringUserVarDefaultValue);
            this.gbOCustom.Controls.Add(this.label4);
            this.gbOCustom.Controls.Add(this.cbStringUserVar);
            this.gbOCustom.Controls.Add(this.label5);
            this.gbOCustom.Controls.Add(this.tbIntUserVarDefaultValue);
            this.gbOCustom.Controls.Add(this.label3);
            this.gbOCustom.Controls.Add(this.cbIntUserVar);
            this.gbOCustom.Controls.Add(this.label2);
            this.gbOCustom.Controls.Add(this.label1);
            this.gbOCustom.Location = new System.Drawing.Point(0, 0);
            this.gbOCustom.Name = "gbOCustom";
            this.gbOCustom.Size = new System.Drawing.Size(996, 126);
            this.gbOCustom.TabIndex = 3;
            this.gbOCustom.TabStop = false;
            this.gbOCustom.Text = "Custom";
            // 
            // btnEncodeColor
            // 
            this.btnEncodeColor.Location = new System.Drawing.Point(307, 60);
            this.btnEncodeColor.Name = "btnEncodeColor";
            this.btnEncodeColor.Size = new System.Drawing.Size(75, 23);
            this.btnEncodeColor.TabIndex = 14;
            this.btnEncodeColor.Text = "Color";
            this.btnEncodeColor.UseVisualStyleBackColor = true;
            this.btnEncodeColor.Click += new System.EventHandler(this.BtnEncodeColor_Click);
            // 
            // tbCustomClassName
            // 
            this.tbCustomClassName.FormattingEnabled = true;
            this.tbCustomClassName.Location = new System.Drawing.Point(128, 27);
            this.tbCustomClassName.Name = "tbCustomClassName";
            this.tbCustomClassName.Size = new System.Drawing.Size(278, 21);
            this.tbCustomClassName.TabIndex = 13;
            this.tbCustomClassName.SelectedIndexChanged += new System.EventHandler(this.TbCustomClassName_SelectedIndexChanged);
            this.tbCustomClassName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbCustomClassName_KeyDown);
            // 
            // lbStringUserVarEx
            // 
            this.lbStringUserVarEx.AutoSize = true;
            this.lbStringUserVarEx.Location = new System.Drawing.Point(412, 94);
            this.lbStringUserVarEx.Name = "lbStringUserVarEx";
            this.lbStringUserVarEx.Size = new System.Drawing.Size(36, 13);
            this.lbStringUserVarEx.TabIndex = 12;
            this.lbStringUserVarEx.Text = "var ex";
            // 
            // lbIntUserVarEx
            // 
            this.lbIntUserVarEx.AutoSize = true;
            this.lbIntUserVarEx.Location = new System.Drawing.Point(388, 65);
            this.lbIntUserVarEx.Name = "lbIntUserVarEx";
            this.lbIntUserVarEx.Size = new System.Drawing.Size(36, 13);
            this.lbIntUserVarEx.TabIndex = 11;
            this.lbIntUserVarEx.Text = "var ex";
            // 
            // tbStringUserVarDefaultValue
            // 
            this.tbStringUserVarDefaultValue.Location = new System.Drawing.Point(242, 91);
            this.tbStringUserVarDefaultValue.Name = "tbStringUserVarDefaultValue";
            this.tbStringUserVarDefaultValue.Size = new System.Drawing.Size(164, 20);
            this.tbStringUserVarDefaultValue.TabIndex = 10;
            this.tbStringUserVarDefaultValue.TextChanged += new System.EventHandler(this.tbStringUserVarDefaultValue_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "DefaultValue:";
            // 
            // cbStringUserVar
            // 
            this.cbStringUserVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStringUserVar.FormattingEnabled = true;
            this.cbStringUserVar.Items.AddRange(new object[] {
            "0",
            "1"});
            this.cbStringUserVar.Location = new System.Drawing.Point(128, 91);
            this.cbStringUserVar.Name = "cbStringUserVar";
            this.cbStringUserVar.Size = new System.Drawing.Size(31, 21);
            this.cbStringUserVar.TabIndex = 8;
            this.cbStringUserVar.SelectedIndexChanged += new System.EventHandler(this.cbStringUserVar_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "StringUserVariable";
            // 
            // tbIntUserVarDefaultValue
            // 
            this.tbIntUserVarDefaultValue.Location = new System.Drawing.Point(242, 62);
            this.tbIntUserVarDefaultValue.Name = "tbIntUserVarDefaultValue";
            this.tbIntUserVarDefaultValue.Size = new System.Drawing.Size(59, 20);
            this.tbIntUserVarDefaultValue.TabIndex = 6;
            this.tbIntUserVarDefaultValue.TextChanged += new System.EventHandler(this.tbIntUserVarDefaultValue_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "DefaultValue:";
            // 
            // cbIntUserVar
            // 
            this.cbIntUserVar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIntUserVar.FormattingEnabled = true;
            this.cbIntUserVar.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbIntUserVar.Location = new System.Drawing.Point(128, 62);
            this.cbIntUserVar.Name = "cbIntUserVar";
            this.cbIntUserVar.Size = new System.Drawing.Size(31, 21);
            this.cbIntUserVar.TabIndex = 4;
            this.cbIntUserVar.SelectedIndexChanged += new System.EventHandler(this.cbIntUserVar_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IntegerUserVariable";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "CustomClassName:";
            // 
            // CustomWidgetOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbOCustom);
            this.Name = "CustomWidgetOptions";
            this.Size = new System.Drawing.Size(996, 126);
            this.gbOCustom.ResumeLayout(false);
            this.gbOCustom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOCustom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbIntUserVarDefaultValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbIntUserVar;
        private System.Windows.Forms.TextBox tbStringUserVarDefaultValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbStringUserVar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbIntUserVarEx;
        private System.Windows.Forms.Label lbStringUserVarEx;
        private System.Windows.Forms.ComboBox tbCustomClassName;
        private System.Windows.Forms.Button btnEncodeColor;
    }
}
