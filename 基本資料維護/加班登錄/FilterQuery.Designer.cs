namespace Attendance
{
    partial class FilterQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.Tb_Search = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Dt = new System.Windows.Forms.DataGridView();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Dt)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(27, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "輸入關鍵字:";
            // 
            // Tb_Search
            // 
            this.Tb_Search.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Tb_Search.Location = new System.Drawing.Point(150, 28);
            this.Tb_Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Tb_Search.Name = "Tb_Search";
            this.Tb_Search.Size = new System.Drawing.Size(172, 31);
            this.Tb_Search.TabIndex = 6;
            this.Tb_Search.TextChanged += new System.EventHandler(this.Tb_Search_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(39, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "(人名或編號)";
            // 
            // Dt
            // 
            this.Dt.AllowUserToAddRows = false;
            this.Dt.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender;
            this.Dt.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Dt.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Dt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.Dt.Location = new System.Drawing.Point(32, 96);
            this.Dt.Name = "Dt";
            this.Dt.ReadOnly = true;
            this.Dt.RowHeadersWidth = 28;
            this.Dt.RowTemplate.Height = 27;
            this.Dt.Size = new System.Drawing.Size(290, 293);
            this.Dt.TabIndex = 8;
            // 
            // BtnAdd
            // 
            this.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnAdd.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnAdd.Location = new System.Drawing.Point(32, 410);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(290, 38);
            this.BtnAdd.TabIndex = 9;
            this.BtnAdd.Text = "加入";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.TrueValue = "Yes";
            this.Column1.Width = 40;
            // 
            // FilterQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 467);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.Dt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Tb_Search);
            this.Controls.Add(this.label3);
            this.Name = "FilterQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "選擇人員";
            ((System.ComponentModel.ISupportInitialize)(this.Dt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Tb_Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView Dt;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    }
}