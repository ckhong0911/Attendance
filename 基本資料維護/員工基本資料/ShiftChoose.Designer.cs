namespace Attendance
{
    partial class ShiftChoose
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Dt = new System.Windows.Forms.DataGridView();
            this.BtnOK = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Dt)).BeginInit();
            this.SuspendLayout();
            // 
            // Dt
            // 
            this.Dt.AllowUserToAddRows = false;
            this.Dt.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            this.Dt.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.Dt.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Dt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.Dt.Location = new System.Drawing.Point(44, 40);
            this.Dt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Dt.Name = "Dt";
            this.Dt.ReadOnly = true;
            this.Dt.RowHeadersWidth = 28;
            this.Dt.RowTemplate.Height = 27;
            this.Dt.Size = new System.Drawing.Size(409, 351);
            this.Dt.TabIndex = 0;
            this.Dt.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dt_CellContentClick);
            // 
            // BtnOK
            // 
            this.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnOK.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnOK.Location = new System.Drawing.Point(44, 409);
            this.BtnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(409, 46);
            this.BtnOK.TabIndex = 1;
            this.BtnOK.Text = "確定";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.TrueValue = "Yes";
            this.Column1.Width = 40;
            // 
            // ShiftChoose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 490);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.Dt);
            this.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ShiftChoose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "班別選擇";
            ((System.ComponentModel.ISupportInitialize)(this.Dt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Dt;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    }
}