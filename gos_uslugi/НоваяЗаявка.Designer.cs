namespace gos_uslugi
{
    partial class НоваяЗаявка
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxService = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.labelRuleParameter = new System.Windows.Forms.Label();
            this.labelRuleType = new System.Windows.Forms.Label();
            this.labelRuleTerm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxService
            // 
            this.comboBoxService.FormattingEnabled = true;
            this.comboBoxService.Location = new System.Drawing.Point(152, 25);
            this.comboBoxService.Name = "comboBoxService";
            this.comboBoxService.Size = new System.Drawing.Size(469, 37);
            this.comboBoxService.TabIndex = 0;
            this.comboBoxService.SelectedIndexChanged += new System.EventHandler(this.comboBoxService_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Услуга";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Правила услуги";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(237, 582);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(152, 46);
            this.buttonCreate.TabIndex = 3;
            this.buttonCreate.Text = "Создать";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // labelRuleParameter
            // 
            this.labelRuleParameter.AutoSize = true;
            this.labelRuleParameter.Location = new System.Drawing.Point(25, 218);
            this.labelRuleParameter.Name = "labelRuleParameter";
            this.labelRuleParameter.Size = new System.Drawing.Size(130, 29);
            this.labelRuleParameter.TabIndex = 4;
            this.labelRuleParameter.Text = "Параметр";
            // 
            // labelRuleType
            // 
            this.labelRuleType.AutoSize = true;
            this.labelRuleType.Location = new System.Drawing.Point(25, 165);
            this.labelRuleType.Name = "labelRuleType";
            this.labelRuleType.Size = new System.Drawing.Size(188, 29);
            this.labelRuleType.TabIndex = 5;
            this.labelRuleType.Text = "Тип параметра";
            // 
            // labelRuleTerm
            // 
            this.labelRuleTerm.AutoSize = true;
            this.labelRuleTerm.Location = new System.Drawing.Point(25, 271);
            this.labelRuleTerm.Name = "labelRuleTerm";
            this.labelRuleTerm.Size = new System.Drawing.Size(70, 29);
            this.labelRuleTerm.TabIndex = 6;
            this.labelRuleTerm.Text = "Срок";
            // 
            // НоваяЗаявка
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 678);
            this.Controls.Add(this.labelRuleTerm);
            this.Controls.Add(this.labelRuleType);
            this.Controls.Add(this.labelRuleParameter);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxService);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "НоваяЗаявка";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Новая Заявка";
            this.Load += new System.EventHandler(this.НоваяЗаявка_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxService;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Label labelRuleParameter;
        private System.Windows.Forms.Label labelRuleType;
        private System.Windows.Forms.Label labelRuleTerm;
    }
}