
namespace Attendance
{
    partial class HumanResource
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
            this.Dt = new System.Windows.Forms.DataGridView();
            this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            ((System.ComponentModel.ISupportInitialize)(this.Dt)).BeginInit();
            this.SuspendLayout();
            // 
            // Dt
            // 
            this.Dt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dt.Location = new System.Drawing.Point(40, 297);
            this.Dt.Name = "Dt";
            this.Dt.Size = new System.Drawing.Size(602, 207);
            this.Dt.TabIndex = 0;
            // 
            // oleDbConnection1
            // 
            this.oleDbConnection1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Develop\\Attendance\\Attendance\\bin" +
    "\\Debug\\AppData\\WorkOrder.mdb";
            // 
            // HumanResource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 534);
            this.Controls.Add(this.Dt);
            this.Name = "HumanResource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增人員基本資料";
            ((System.ComponentModel.ISupportInitialize)(this.Dt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Dt;
        private System.Data.OleDb.OleDbConnection oleDbConnection1;
    }
}