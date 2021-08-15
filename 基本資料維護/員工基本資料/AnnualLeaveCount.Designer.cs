namespace Attendance
{
    partial class AnnualLeaveCount
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
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DpIn = new System.Windows.Forms.DateTimePicker();
            this.Btn_Count = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.UD_Year = new System.Windows.Forms.NumericUpDown();
            this.lblname = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.UD_Year)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(30, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 37;
            this.label1.Text = "員工姓名:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(50, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 25);
            this.label6.TabIndex = 38;
            this.label6.Text = "到職日:";
            // 
            // DpIn
            // 
            this.DpIn.CalendarFont = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.DpIn.CustomFormat = "yyyy/MM/dd";
            this.DpIn.Font = new System.Drawing.Font("微軟正黑體", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.DpIn.Location = new System.Drawing.Point(134, 95);
            this.DpIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DpIn.Name = "DpIn";
            this.DpIn.Size = new System.Drawing.Size(132, 31);
            this.DpIn.TabIndex = 36;
            this.DpIn.Value = new System.DateTime(2021, 7, 25, 0, 0, 0, 0);
            // 
            // Btn_Count
            // 
            this.Btn_Count.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Btn_Count.Location = new System.Drawing.Point(610, 89);
            this.Btn_Count.Name = "Btn_Count";
            this.Btn_Count.Size = new System.Drawing.Size(75, 37);
            this.Btn_Count.TabIndex = 39;
            this.Btn_Count.Text = "計算";
            this.Btn_Count.UseVisualStyleBackColor = true;
            this.Btn_Count.Click += new System.EventHandler(this.Btn_Count_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(305, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 25);
            this.label2.TabIndex = 40;
            this.label2.Text = "計算截止: 西元";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(30, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 25);
            this.label3.TabIndex = 42;
            this.label3.Text = "特休天數:";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblValue.ForeColor = System.Drawing.Color.Blue;
            this.lblValue.Location = new System.Drawing.Point(133, 159);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(0, 25);
            this.lblValue.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(541, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 25);
            this.label5.TabIndex = 45;
            this.label5.Text = "年";
            // 
            // UD_Year
            // 
            this.UD_Year.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.UD_Year.Location = new System.Drawing.Point(453, 91);
            this.UD_Year.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.UD_Year.Minimum = new decimal(new int[] {
            1911,
            0,
            0,
            0});
            this.UD_Year.Name = "UD_Year";
            this.UD_Year.Size = new System.Drawing.Size(82, 34);
            this.UD_Year.TabIndex = 46;
            this.UD_Year.Value = new decimal(new int[] {
            1911,
            0,
            0,
            0});
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblname.ForeColor = System.Drawing.Color.Blue;
            this.lblname.Location = new System.Drawing.Point(129, 33);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(72, 25);
            this.lblname.TabIndex = 47;
            this.lblname.Text = "步郝蒜";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(70, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 25);
            this.label4.TabIndex = 48;
            this.label4.Text = "說明:";
            // 
            // txtContent
            // 
            this.txtContent.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtContent.Location = new System.Drawing.Point(134, 221);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(551, 147);
            this.txtContent.TabIndex = 49;
            // 
            // AnnualLeaveCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 399);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblname);
            this.Controls.Add(this.UD_Year);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Btn_Count);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DpIn);
            this.Name = "AnnualLeaveCount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "特休天數計算";
            ((System.ComponentModel.ISupportInitialize)(this.UD_Year)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Btn_Count;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DateTimePicker DpIn;
        public System.Windows.Forms.NumericUpDown UD_Year;
        private System.Windows.Forms.Label lblname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtContent;
    }
}