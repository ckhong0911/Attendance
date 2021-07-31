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
    public partial class EmployDataCreate : Form
    {
        public EmployDataCreate()
        {
            InitializeComponent();

            DpBirthday.Format = DateTimePickerFormat.Custom;
            DpIn.Format = DateTimePickerFormat.Custom;
            DpBirthday2.Format = DateTimePickerFormat.Custom;
            DpIn2.Format = DateTimePickerFormat.Custom;
            DpQuit.Format = DateTimePickerFormat.Custom;
        }

        //控制項復歸清除
        public void ClearAllContorler()
        {
            TbName.Text = "";
            TbName2.Text = "";
            TbID.Text = "";
            TbID2.Text = "";
            Rb1.Checked = false;
            Rb2.Checked = false;
            Rb3.Checked = false;
            Rb4.Checked = false;
            DpBirthday.Value = DateTime.Today;
            DpIn.Value = DateTime.Today;
            comboBox1.Text = "";
            TbVacation.Text = "";
            DpBirthday2.Value = DateTime.Today;
            DpIn2.Value = DateTime.Today;
            comboBox2.Text = "";
            TbVacation2.Text = "";
            cb1.Checked = true;
            DpQuit.Visible = false;
        }

        //新增資料
        private void BtnSave_Click_1(object sender, EventArgs e)
        {
            string[] arr = { TbCardNumber.Text, TbID.Text, TbName.Text, "", comboBox1.Text, TbVacation.Text };
            int num1;

            if (arr[0] == "")
            {
                MessageBox.Show("未輸入員工卡號", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (arr[2] == "")
            {
                MessageBox.Show("未輸入姓名", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (arr[1] == "")
            {
                MessageBox.Show("未輸入員工編號", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (Rb1.Checked == false && Rb2.Checked == false)
            {
                MessageBox.Show("未選擇性別", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (arr[4] == "")
            {
                MessageBox.Show("未輸入職務屬性", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (int.TryParse(TbVacation.Text, out num1) == false)
            {
                MessageBox.Show("特休天數必須為數字", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (MessageBox.Show("是否新增?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
            if (Rb1.Checked == true)
                arr[3] = "男";
            else
                arr[3] = "女";

            string birthday = DpBirthday.Text;
            string dateIn = DpIn.Text;
            string quit = DpQuit.Text;
            string insertQuery = "";

                string cardNumberQuery = "Select 離職 From [員工基本資料] Where 員工卡號='" + TbCardNumber.Text + "' And 離職='false'";
            DataTable table = SqlCRUD.SqlQuery(cardNumberQuery, "mdb");

            if (table.Rows.Count == 0)
            {
                if (cb1.Checked == true)
                {
                    insertQuery = "Insert into [員工基本資料] (員工卡號,員工編號,員工姓名,性別,生日,到職日,職務屬性,特休天數,離職) values" +
                        "('" + arr[0] + "','" + arr[1] + "','" + arr[2] + "','" + arr[3] + "','" + birthday + "','" + dateIn +
                        "','" + arr[4] + "','" + arr[5] + "','false')";

                    new SqlCRUD(insertQuery, "mdb");
                    MessageBox.Show("新增完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                }

            if (Rb3.Checked == true)
                arr[3] = "男";
            else
                arr[3] = "女";

            string birthday = DpBirthday2.Text;
            string dateIn = DpIn2.Text;
            string quit = DpQuit.Text;
            string updateQuery = "";

            if (MessageBox.Show("是否更新?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string cardNumberQuery = "Select * From [員工基本資料] Where 員工卡號='" + arr[0] + "' And 離職='false'";
                DataTable table = SqlCRUD.SqlQuery(cardNumberQuery, "mdb");

                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("卡號無資料", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    ClearAllContorler();
                    txtCardNumber.Text = "";
            }
            else
            {
                if (cb1.Checked == true)
                {
                        "',生日='" + birthday + "',到職日='" + dateIn + "',職務屬性='" + arr[4] + "',特休天數='" + arr[5] +
                        "' Where 員工卡號='" + arr[0] + "'";
                }
                else
                {
                        "',生日='" + birthday + "',到職日='" + dateIn + "',職務屬性='" + arr[4] + "',特休天數='" + arr[5] +
                        "',離職='true',離職日='" + quit + "' Where 員工卡號='" + arr[0] + "'";
                }
            }

                new SqlCRUD(updateQuery, "mdb");
                MessageBox.Show("更新完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                ClearAllContorler();
                txtCardNumber.Text = "";
                txtCardNumber.Focus();
                panel1.Visible = false;
            }
        }

            ClearAllContorler();
        }

        {
            if (cb1.Checked == true)
            {
                DpQuit.Visible = false;
            }
            else
            {
                DpQuit.Visible = true;
                DpQuit.Value = DateTime.Today;
            }
        }

        //索引書籤切換
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])        //新增資料
            {
                TbCardNumber.Focus();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])   //更新資料
            {
                txtCardNumber.Focus();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])   //刪除資料
            {
                string searchQuery = "Select * From [員工基本資料]";
                DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

                Dt.DataSource = table;
                Dt.Columns[2].Width = 150;
                Dt.Columns[1].Visible = false;

                //除首欄，其他欄位設定為Readonly
                Dt.ReadOnly = false;
                for (int i = 1; i < Dt.ColumnCount; i++)
                {
                    Dt.Columns[i].ReadOnly = true;
        }
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])   //刪除資料
            {
                string searchQuery = "Select * From [員工基本資料]";
                DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

        {

            {

                else

            }
            {
            }

        }

        {
            TbName.Text = "";
            TbID.Text = "";
            Rb1.Checked = false;
            Rb2.Checked = false;
            DpBirthday.Value = DateTime.Today;
            DpIn.Value = DateTime.Today;
            comboBox1.Text = "";
            TbVacation.Text = "";
            cb1.Checked = true;
                            label14.Visible = false;
            DpQuit.Visible = false;
        }

                        panel1.Visible = true;
                        tabControl1.SelectedTab = tabControl1.TabPages["tabPage2"];
                    }
            }
            
    }
}
}
