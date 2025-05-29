namespace gos_uslugi
{
    partial class ИзменениеПравилУслуги
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
            this.buttonSaveRules = new System.Windows.Forms.Button();
            this.dataGridViewRules = new System.Windows.Forms.DataGridView();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConditionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConditionValues = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperatorValues = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TermOfServiceProvision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonDeleteRule = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRules)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSaveRules
            // 
            this.buttonSaveRules.Location = new System.Drawing.Point(381, 617);
            this.buttonSaveRules.Name = "buttonSaveRules";
            this.buttonSaveRules.Size = new System.Drawing.Size(156, 37);
            this.buttonSaveRules.TabIndex = 1;
            this.buttonSaveRules.Text = "Сохранить";
            this.buttonSaveRules.UseVisualStyleBackColor = true;
            this.buttonSaveRules.Click += new System.EventHandler(this.buttonSaveRules_Click);
            // 
            // dataGridViewRules
            // 
            this.dataGridViewRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Description,
            this.ConditionType,
            this.ConditionValues,
            this.OperatorValues,
            this.TermOfServiceProvision});
            this.dataGridViewRules.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewRules.Name = "dataGridViewRules";
            this.dataGridViewRules.RowHeadersVisible = false;
            this.dataGridViewRules.RowHeadersWidth = 51;
            this.dataGridViewRules.RowTemplate.Height = 24;
            this.dataGridViewRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRules.Size = new System.Drawing.Size(692, 589);
            this.dataGridViewRules.TabIndex = 2;
            // 
            // Description
            // 
            this.Description.HeaderText = "Описание";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.Width = 125;
            // 
            // ConditionType
            // 
            this.ConditionType.HeaderText = "Тип условия";
            this.ConditionType.MinimumWidth = 6;
            this.ConditionType.Name = "ConditionType";
            this.ConditionType.Width = 125;
            // 
            // ConditionValues
            // 
            this.ConditionValues.HeaderText = "Значение условия";
            this.ConditionValues.MinimumWidth = 6;
            this.ConditionValues.Name = "ConditionValues";
            this.ConditionValues.Width = 125;
            // 
            // OperatorValues
            // 
            this.OperatorValues.HeaderText = "Оператор";
            this.OperatorValues.MinimumWidth = 6;
            this.OperatorValues.Name = "OperatorValues";
            this.OperatorValues.Width = 125;
            // 
            // TermOfServiceProvision
            // 
            this.TermOfServiceProvision.HeaderText = "Срок";
            this.TermOfServiceProvision.MinimumWidth = 6;
            this.TermOfServiceProvision.Name = "TermOfServiceProvision";
            this.TermOfServiceProvision.Width = 125;
            // 
            // buttonDeleteRule
            // 
            this.buttonDeleteRule.Location = new System.Drawing.Point(127, 617);
            this.buttonDeleteRule.Name = "buttonDeleteRule";
            this.buttonDeleteRule.Size = new System.Drawing.Size(156, 37);
            this.buttonDeleteRule.TabIndex = 3;
            this.buttonDeleteRule.Text = "Удалить";
            this.buttonDeleteRule.UseVisualStyleBackColor = true;
            this.buttonDeleteRule.Click += new System.EventHandler(this.buttonDeleteRule_Click);
            // 
            // ИзменениеПравилУслуги
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 678);
            this.Controls.Add(this.buttonDeleteRule);
            this.Controls.Add(this.dataGridViewRules);
            this.Controls.Add(this.buttonSaveRules);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ИзменениеПравилУслуги";
            this.ShowIcon = false;
            this.Text = "Изменение Правил Услуги";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRules)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonSaveRules;
        private System.Windows.Forms.DataGridView dataGridViewRules;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConditionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConditionValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperatorValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn TermOfServiceProvision;
        private System.Windows.Forms.Button buttonDeleteRule;
    }
}