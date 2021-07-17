using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

 
        private void Btn_Login_Click(object sender, EventArgs e)
        {

        }

        private void Tb_Account_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Tb_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(Tb_Password.Text))
                    return;     //空值跳出


                //SqlCommand cmd = new SqlCommand("SELECT ID,name FROM [Account] WHERE Password = @Password And idenable = 'Y'", sqlConnection1);
                //cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = Tb_Password.Text;
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da.Fill(ds);    //使用DB語法將搜尋結果匯入資料集

                //if (ds.Tables[0].Rows.Count == 0)
                //{
                //    MessageBox.Show("無此人員資料" + "\r\n" + "tài khoản không tồn tại hoặc mật khẩu sai",
                //         "Chintek", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    Tb_Password.Text = "";
                //    return;     //無資料跳出
                //}
                //else
                //{   //資料正確清除密碼進入主畫面
                //    Tb_Password.Text = "";
                //    //呼叫建構子(取出登入人員姓名,工號)                   
                //    MainMenu f = new MainMenu(ds.Tables[0].Rows[0][1].ToString(), ds.Tables[0].Rows[0][0].ToString());
                //    this.Hide();  //登入表單隱藏
                //    ds.Tables.Clear();  //清除資料集
                //    f.ShowDialog();     //執行強制回應視窗
                //}
            }
        }


    }
}
