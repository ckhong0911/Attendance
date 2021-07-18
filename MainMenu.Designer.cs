
namespace Attendance
{
    partial class MainMenu
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
            this.BtnWorkShiftSetting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnWorkShiftSetting
            // 
            this.BtnWorkShiftSetting.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.BtnWorkShiftSetting.Location = new System.Drawing.Point(51, 49);
            this.BtnWorkShiftSetting.Name = "BtnWorkShiftSetting";
            this.BtnWorkShiftSetting.Size = new System.Drawing.Size(215, 55);
            this.BtnWorkShiftSetting.TabIndex = 0;
            this.BtnWorkShiftSetting.Text = "班別設定";
            this.BtnWorkShiftSetting.UseVisualStyleBackColor = true;
            this.BtnWorkShiftSetting.Click += new System.EventHandler(this.BtnWorkShiftSetting_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 379);
            this.Controls.Add(this.BtnWorkShiftSetting);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainMenu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnWorkShiftSetting;
    }
}