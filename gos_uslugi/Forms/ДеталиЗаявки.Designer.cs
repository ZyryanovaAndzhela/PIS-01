namespace gos_uslugi
{
    partial class ДеталиЗаявки
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
            this.labelID = new System.Windows.Forms.Label();
            this.labelEmployeeId = new System.Windows.Forms.Label();
            this.labelForeignerId = new System.Windows.Forms.Label();
            this.labelServiceId = new System.Windows.Forms.Label();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.dateTimePickerCreation = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerCompletion = new System.Windows.Forms.DateTimePicker();
            this.textBoxDeadline = new System.Windows.Forms.TextBox();
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.buttonEditRequest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(49, 47);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(89, 29);
            this.labelID.TabIndex = 0;
            this.labelID.Text = "labelID";
            // 
            // labelEmployeeId
            // 
            this.labelEmployeeId.AutoSize = true;
            this.labelEmployeeId.Location = new System.Drawing.Point(54, 108);
            this.labelEmployeeId.Name = "labelEmployeeId";
            this.labelEmployeeId.Size = new System.Drawing.Size(195, 29);
            this.labelEmployeeId.TabIndex = 1;
            this.labelEmployeeId.Text = "labelEmployeeId";
            // 
            // labelForeignerId
            // 
            this.labelForeignerId.AutoSize = true;
            this.labelForeignerId.Location = new System.Drawing.Point(59, 177);
            this.labelForeignerId.Name = "labelForeignerId";
            this.labelForeignerId.Size = new System.Drawing.Size(192, 29);
            this.labelForeignerId.TabIndex = 2;
            this.labelForeignerId.Text = "labelForeignerId";
            // 
            // labelServiceId
            // 
            this.labelServiceId.AutoSize = true;
            this.labelServiceId.Location = new System.Drawing.Point(64, 245);
            this.labelServiceId.Name = "labelServiceId";
            this.labelServiceId.Size = new System.Drawing.Size(167, 29);
            this.labelServiceId.TabIndex = 3;
            this.labelServiceId.Text = "labelServiceId";
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(69, 309);
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.Size = new System.Drawing.Size(399, 34);
            this.textBoxStatus.TabIndex = 4;
            // 
            // dateTimePickerCreation
            // 
            this.dateTimePickerCreation.Enabled = false;
            this.dateTimePickerCreation.Location = new System.Drawing.Point(69, 371);
            this.dateTimePickerCreation.Name = "dateTimePickerCreation";
            this.dateTimePickerCreation.Size = new System.Drawing.Size(248, 34);
            this.dateTimePickerCreation.TabIndex = 5;
            // 
            // dateTimePickerCompletion
            // 
            this.dateTimePickerCompletion.CustomFormat = "";
            this.dateTimePickerCompletion.Enabled = false;
            this.dateTimePickerCompletion.Location = new System.Drawing.Point(69, 436);
            this.dateTimePickerCompletion.Name = "dateTimePickerCompletion";
            this.dateTimePickerCompletion.Size = new System.Drawing.Size(248, 34);
            this.dateTimePickerCompletion.TabIndex = 6;
            // 
            // textBoxDeadline
            // 
            this.textBoxDeadline.Location = new System.Drawing.Point(64, 501);
            this.textBoxDeadline.Name = "textBoxDeadline";
            this.textBoxDeadline.ReadOnly = true;
            this.textBoxDeadline.Size = new System.Drawing.Size(404, 34);
            this.textBoxDeadline.TabIndex = 7;
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.Location = new System.Drawing.Point(64, 555);
            this.richTextBoxResult.Name = "richTextBoxResult";
            this.richTextBoxResult.ReadOnly = true;
            this.richTextBoxResult.Size = new System.Drawing.Size(404, 96);
            this.richTextBoxResult.TabIndex = 8;
            this.richTextBoxResult.Text = "";
            // 
            // buttonEditRequest
            // 
            this.buttonEditRequest.Location = new System.Drawing.Point(451, 12);
            this.buttonEditRequest.Name = "buttonEditRequest";
            this.buttonEditRequest.Size = new System.Drawing.Size(179, 36);
            this.buttonEditRequest.TabIndex = 9;
            this.buttonEditRequest.Text = "Изменить";
            this.buttonEditRequest.UseVisualStyleBackColor = true;
            this.buttonEditRequest.Click += new System.EventHandler(this.buttonEditRequest_Click);
            // 
            // ДеталиЗаявки
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 678);
            this.Controls.Add(this.buttonEditRequest);
            this.Controls.Add(this.richTextBoxResult);
            this.Controls.Add(this.textBoxDeadline);
            this.Controls.Add(this.dateTimePickerCompletion);
            this.Controls.Add(this.dateTimePickerCreation);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.labelServiceId);
            this.Controls.Add(this.labelForeignerId);
            this.Controls.Add(this.labelEmployeeId);
            this.Controls.Add(this.labelID);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ДеталиЗаявки";
            this.ShowIcon = false;
            this.Text = "ДеталиЗаявки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label labelEmployeeId;
        private System.Windows.Forms.Label labelForeignerId;
        private System.Windows.Forms.Label labelServiceId;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.DateTimePicker dateTimePickerCreation;
        private System.Windows.Forms.DateTimePicker dateTimePickerCompletion;
        private System.Windows.Forms.TextBox textBoxDeadline;
        private System.Windows.Forms.RichTextBox richTextBoxResult;
        private System.Windows.Forms.Button buttonEditRequest;
    }
}