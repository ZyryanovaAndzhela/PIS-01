namespace gos_uslugi
{
    partial class Услуги
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonServiceSearch = new System.Windows.Forms.Button();
            this.textBoxServiceSearch = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 71);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(618, 578);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Описание";
            this.columnHeader1.Width = 350;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Цена";
            this.columnHeader2.Width = 100;
            // 
            // buttonServiceSearch
            // 
            this.buttonServiceSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonServiceSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonServiceSearch.Location = new System.Drawing.Point(508, 16);
            this.buttonServiceSearch.Name = "buttonServiceSearch";
            this.buttonServiceSearch.Size = new System.Drawing.Size(122, 34);
            this.buttonServiceSearch.TabIndex = 3;
            this.buttonServiceSearch.Text = "Поиск";
            this.buttonServiceSearch.UseVisualStyleBackColor = true;
            this.buttonServiceSearch.Click += new System.EventHandler(this.buttonServiceSearch_Click);
            // 
            // textBoxServiceSearch
            // 
            this.textBoxServiceSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxServiceSearch.Location = new System.Drawing.Point(12, 16);
            this.textBoxServiceSearch.Name = "textBoxServiceSearch";
            this.textBoxServiceSearch.Size = new System.Drawing.Size(455, 34);
            this.textBoxServiceSearch.TabIndex = 4;
            // 
            // Услуги
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 678);
            this.Controls.Add(this.textBoxServiceSearch);
            this.Controls.Add(this.buttonServiceSearch);
            this.Controls.Add(this.listView1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Услуги";
            this.ShowIcon = false;
            this.Text = "Услуги";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonServiceSearch;
        private System.Windows.Forms.TextBox textBoxServiceSearch;
    }
}