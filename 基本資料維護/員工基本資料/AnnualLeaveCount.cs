using System;
using System.Windows.Forms;

namespace Attendance
{
    public partial class AnnualLeaveCount : Form
    {
        int flag;
        EmployDataCreate emp;

        public AnnualLeaveCount()
        {
            InitializeComponent();
        }

        public AnnualLeaveCount(string name, DateTime date, int flag, EmployDataCreate emp)
        {
            InitializeComponent();
            this.flag = flag;
            this.emp = emp;

            lblname.Text = name;
            DpIn.Value = date;
            DpIn.Format = DateTimePickerFormat.Custom;
            DateTime currentTime = System.DateTime.Now;
            UD_Year.Value = currentTime.Year;
        }

        private void Btn_Count_Click(object sender, EventArgs e)
        {
            /* =========勞動基準法=========
             * (1)6個月以上1年未滿者，3日。
             * (2)1年以上2年未滿者，7日。
             * (3)2年以上3年未滿者，10日。
             * (4)3年以上5年未滿者，每年14日。
             * (5)5年以上10年未滿者，每年15日。
             * (6)10年以上者，每1年加給1日，加至30日為止。
             ==============================*/

            DpIn.Format = DateTimePickerFormat.Custom;
            DateTime dateLine = Convert.ToDateTime(UD_Year.Value + "/12/31");

            int value = new TimeSpan(dateLine.Ticks - DpIn.Value.Ticks).Days;
            double result = Convert.ToDouble(value) / 365;


            if (result < 0.502)
            {
                lblValue.Text = "0";
                txtContent.Text = "年資總計: " + Math.Floor(result) + "年" + "\r\n" + "特休總計: 0天";
            }
            else if (result >= 0.502 && result < 1)
            {
                string[] arr = DpIn.Text.Split('/');        //字串分裂並以虛擬陣列儲存
                int mm = Convert.ToInt32(arr[1]) + 6;       //將月份加6個月做為到職滿半年的日期
                DateTime halfYearDate = Convert.ToDateTime(arr[0] + "/" + Convert.ToString(mm).PadLeft(2,'0') + "/" + arr[2]);   //顯示滿半年日期 

                int days = new TimeSpan(dateLine.Ticks - halfYearDate.Ticks).Days;    //年底"12/31"減去人員到職"滿半年日期"
                double divide = Convert.ToDouble(days) / 30;    //除去30天，每滿 0.5 給假 1 天(半年總計3天)
                double x= 0.0;   //儲存商數

                //依照商數對應休假天數
                if (divide < 1) x = 0;
                else if (divide >= 1 && divide < 2) x = 0.5;
                else if (divide >= 2 && divide < 3) x = 1;
                else if (divide >= 3 && divide < 4) x = 1.5;
                else if (divide >= 4 && divide < 5) x = 2;
                else if (divide >= 5 && divide < 6.07) x = 2.5;
                else if (divide >= 6.07) x = 3;

                lblValue.Text = x.ToString();
                txtContent.Text = "年資滿半年，未滿1年" + "\r\n" + "本年度特休總計: " + x.ToString() + "天" + "\r\n" + "休假起始: " +
                        halfYearDate.ToString("yyyy/MM/dd") + " ~ " + arr[0] + "/12/31";
            }
            else if (result >= 1 && result < 2)
            {
                if (result >= 1 && result < 1.502)
                {
                    string[] arr = DpIn.Text.Split('/');               //字串分裂並以虛擬陣列儲存
                    int mm = (Convert.ToInt32(arr[1]) + 6) % 12;       //將月份加6個月做為到職滿半年的日期
                    DateTime halfYearDate = Convert.ToDateTime((Convert.ToInt32(arr[0]) + 1) + "/" + Convert.ToString(mm) + "/" + arr[2]);   //顯示滿半年日期 

                    int yy = Convert.ToInt32(arr[0]) + 1;       //將月份加6個月做為到職滿一年的日期
                    DateTime wholeYearDate = Convert.ToDateTime(Convert.ToString(yy) + "/" + arr[1] + "/" + arr[2]).AddDays(-1);   //顯示到職滿1年日期 

                    int days = new TimeSpan(dateLine.Ticks - wholeYearDate.Ticks).Days;    //年底"12/31"減去人員到職"滿半年日期"
                    double divide = Convert.ToDouble(days) / 52.14;    //1年7天特休

                    double x = 0.0;   //儲存商數

                    //依照商數對應休假天數
                    if (divide < 0.502) x = 0;
                    else if (divide >= 0.502 && divide < 1) x = 0.5;
                    else if (divide >= 1 && divide < 1.502) x = 1;
                    else if (divide >= 1.502 && divide < 2) x = 1.5;
                    else if (divide >= 2 && divide < 2.502) x = 2;
                    else if (divide >= 2.502 && divide < 3) x = 2.5;
                    else if (divide >= 3 && divide < 3.502) x = 3;
                    else x = 3.5;

                    double equal = 3 + x;

                    lblValue.Text = equal.ToString();
                    txtContent.Text = "年資總計" + Math.Floor(result) + "年" + "\r\n" + "本年度特休總計: " + equal.ToString() + "天" + "\r\n" + "第一段休假起始: " +
                            halfYearDate.ToString("yyyy/MM/dd") + " ~ " + wholeYearDate.ToString("yyyy/MM/dd") + " 可休 3 天" + "\r\n" + "第二段休假起始: " +
                            wholeYearDate.AddDays(1).ToString("yyyy/MM/dd") + " ~ " + UD_Year.Value + "/12/31" + " 可休 " + x + " 天";
                }
                else if (result >= 1.502 && result < 2)
                {
                    string[] arr = DpIn.Text.Split('/');        //字串分裂並以虛擬陣列儲存
                    int yy = Convert.ToInt32(arr[0]) + 1;       //計算到職滿一年的日期
                    DateTime wholeYearDate = Convert.ToDateTime(Convert.ToString(yy) + "/" + arr[1] + "/" + arr[2]).AddDays(-1);   //顯示到職滿1年日期 

                    int days = new TimeSpan(dateLine.Ticks - wholeYearDate.Ticks).Days;    //年底"12/31"減去人員到職"滿一年日期"
                    double divide = Convert.ToDouble(days) / 52.14;    //1年7天特休
                    double x = 0.0;   //儲存商數

                    if (divide >= 3.502 && divide < 4) x = 3.5;
                    else if (divide >= 4 && divide < 4.5) x = 4;
                    else if (divide >= 4.5 && divide < 5) x = 4.5;
                    else if (divide >= 5 && divide < 5.5) x = 5;
                    else if (divide >= 5.5 && divide < 6) x = 5.5;
                    else if (divide >= 6 && divide < 6.5) x = 6;
                    else if (divide >= 6.5 && divide < 7) x = 6.5;
                    else x = 7;

                    int mm = Convert.ToInt32(arr[1]) + 6;       //將月份加6個月做為到職滿半年的日期
                    DateTime halfYearDate = Convert.ToDateTime(arr[0] + "/" + Convert.ToString(mm) + "/" + arr[2]);   //顯示滿半年日期 
                    DateTime lastYearDate = Convert.ToDateTime(arr[0] + "/12/31");

                    int days2 = new TimeSpan(lastYearDate.Ticks - halfYearDate.Ticks).Days;    //年底"12/31"減去人員到職"滿半年日期"
                    double divide2 = Convert.ToDouble(days2) / 30;    //除去30天，每滿 0.5 給假 1 天(半年總計3天)
                    double y = 0.0;   //儲存商數

                    //依照商數對應休假天數
                    if (divide2 < 1) y = 0;
                    else if (divide2 >= 1 && divide2 < 2) y = 0.5;
                    else if (divide2 >= 2 && divide2 < 3) y = 1;
                    else if (divide2 >= 3 && divide2 < 4) y = 1.5;
                    else if (divide2 >= 4 && divide2 < 5) y = 2;
                    else if (divide2 >= 5 && divide2 < 6.07) y = 2.5;
                    else if (divide2 >= 6.07) y = 3;

                    if ( x == 7)
                    {
                        lblValue.Text = (x + (3 - y)).ToString();
                        txtContent.Text = "年資總計" + Math.Floor(result) + "年" + "\r\n" + "本年度特休總計: " + (x + (3 - y)).ToString() + "天" + "\r\n" + "休假起始: " +
                                wholeYearDate.AddDays(1).ToString("yyyy/MM/dd") + " ~ " + UD_Year.Value + "/12/31" + " 可休 " + x + " 天";
                    }
                    else
                    {
                        lblValue.Text = (x + (3 - y)).ToString();
                        txtContent.Text = "年資總計" + Math.Floor(result) + "年" + "\r\n" + "本年度特休總計: " + (x + (3 - y)).ToString() + "天" + "\r\n" + "第一段休假起始: " +
                                yy + "/01/01" + " ~ " + wholeYearDate.ToString("yyyy/MM/dd") + " 可休 " + (3 - y) + " 天" + "\r\n" + "第二段休假起始: " +
                                wholeYearDate.AddDays(1).ToString("yyyy/MM/dd") + " ~ " + UD_Year.Value + "/12/31" + " 可休 " + x + " 天";
                    }
                    
                }         
            }
            else if (result >= 2 && result < 3)
            {
                lblValue.Text = "10";
                txtContent.Text = "年資總計: " + Math.Floor(result) + "年" + "\r\n" + "特休總計: 10天";
            }
            else if (result >= 3 && result < 5)
            {
                lblValue.Text = "14";
                txtContent.Text = "年資總計: " + Math.Floor(result) + "年" + "\r\n" + "特休總計: 14天";
            }
            else if (result >= 5 && result <= 10)
            {
                lblValue.Text = "15";
                txtContent.Text = "年資總計: " + Math.Floor(result) + "年" + "\r\n" + "特休總計: 15天";
            }
            else if (result > 10)
            {
                int total = (Convert.ToInt32(Math.Floor(result)) - 10) + 15;
                if (total > 30) total = 30;
                lblValue.Text = total.ToString();
                txtContent.Text = "年資總計: " + Math.Floor(result) + "年" + "\r\n" + "特休總計: " + total + "天";
            }
                
        }

        private void Rb_1_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_1.Checked)
            {
                //手動填寫
                lblValue.Enabled = true;
                panel1.Visible = false;
                panel2.Visible = false;
                lblValue.Focus();
            }
            else
            {
                //公式導入
                lblValue.Enabled = false;
                panel1.Visible = true;
                panel2.Visible = true;
            }             
        }

        private void BtnOutputAnnualLeave_Click(object sender, EventArgs e)
        {
            double count;

            if (lblValue.Text.Length > 0)
            {
                if (double.TryParse(lblValue.Text, out count) == true)
                {
                    if (flag == 1)
                        emp.TbVacation.Text = lblValue.Text;
                    else
                        emp.TbVacation2.Text = lblValue.Text;

                    Close();
                }                  
                else
                {
                    MessageBox.Show("特休天數必須為數字", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    lblValue.Text = "";
                    lblValue.Focus();
                    return;
                }                   
            }
            else
            {
                MessageBox.Show("特休天數不可空白", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                lblValue.Focus();
                return;
            }
        }

    }
}
