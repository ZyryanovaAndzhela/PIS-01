namespace gos_uslugi
{
    partial class Заявки
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
            this.listViewRequests = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxFilterStatus = new System.Windows.Forms.ComboBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonApplyFilter = new System.Windows.Forms.Button();
            this.buttonCreateRequest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewRequests
            // 
            this.listViewRequests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listViewRequests.FullRowSelect = true;
            this.listViewRequests.HideSelection = false;
            this.listViewRequests.Location = new System.Drawing.Point(12, 72);
            this.listViewRequests.Name = "listViewRequests";
            this.listViewRequests.Size = new System.Drawing.Size(916, 653);
            this.listViewRequests.TabIndex = 0;
            this.listViewRequests.UseCompatibleStateImageBehavior = false;
            this.listViewRequests.View = System.Windows.Forms.View.Details;
            this.listViewRequests.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewRequests_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Услуга";
            this.columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Дата создания";
            this.columnHeader3.Width = 140;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Дата завершения";
            this.columnHeader5.Width = 170;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Статус";
            this.columnHeader6.Width = 140;
            // 
            // comboBoxFilterStatus
            // 
            this.comboBoxFilterStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxFilterStatus.FormattingEnabled = true;
            this.comboBoxFilterStatus.Location = new System.Drawing.Point(12, 17);
            this.comboBoxFilterStatus.Name = "comboBoxFilterStatus";
            this.comboBoxFilterStatus.Size = new System.Drawing.Size(271, 37);
            this.comboBoxFilterStatus.TabIndex = 1;
            this.comboBoxFilterStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterStatus_SelectedIndexChanged);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSearch.Location = new System.Drawing.Point(299, 17);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(225, 34);
            this.textBoxSearch.TabIndex = 2;
            // 
            // buttonApplyFilter
            // 
            this.buttonApplyFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonApplyFilter.Location = new System.Drawing.Point(530, 15);
            this.buttonApplyFilter.Name = "buttonApplyFilter";
            this.buttonApplyFilter.Size = new System.Drawing.Size(118, 39);
            this.buttonApplyFilter.TabIndex = 3;
            this.buttonApplyFilter.Text = "Поиск";
            this.buttonApplyFilter.UseVisualStyleBackColor = true;
            this.buttonApplyFilter.Click += new System.EventHandler(this.buttonApplyFilter_Click);
            // 
            // buttonCreateRequest
            // 
            this.buttonCreateRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCreateRequest.Location = new System.Drawing.Point(726, 17);
            this.buttonCreateRequest.Name = "buttonCreateRequest";
            this.buttonCreateRequest.Size = new System.Drawing.Size(202, 37);
            this.buttonCreateRequest.TabIndex = 4;
            this.buttonCreateRequest.Text = "Создать заявку";
            this.buttonCreateRequest.UseVisualStyleBackColor = true;
            this.buttonCreateRequest.Click += new System.EventHandler(this.buttonCreateRequest_Click);
            // 
            // Заявки
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 725);
            this.Controls.Add(this.buttonCreateRequest);
            this.Controls.Add(this.buttonApplyFilter);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.comboBoxFilterStatus);
            this.Controls.Add(this.listViewRequests);
            this.Name = "Заявки";
            this.ShowIcon = false;
            this.Text = "Заявки";
            this.Load += new System.EventHandler(this.Заявки_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewRequests;
        private System.Windows.Forms.ColumnHeader columnHeaderID;
        private System.Windows.Forms.ColumnHeader columnHeaderServiceId;
        private System.Windows.Forms.ColumnHeader columnHeaderDateCreation;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ComboBox comboBoxFilterStatus;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonApplyFilter;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button buttonCreateRequest;
    }
}