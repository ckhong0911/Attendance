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
            DpQuit.Format = DateTimePickerFormat.Custom;
        }

        private void BtnSave_Click(object sender, EventArgs e)
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

            if (Rb1.Checked == true)
                arr[3] = "男";
            else
                arr[3] = "女";

            string birthday = DpBirthday.Text;
            string dateIn = DpIn.Text;
            string quit = DpQuit.Text;
            string insertQuery = "";

            string cardNumberQuery = "Select * From [員工基本資料] Where 員工卡號='" + TbCardNumber.Text + "' And 離職='false'";
            DataTable table = SqlCRUD.SqlQuery(cardNumberQuery, "mdb");

            if (table.Rows.Count == 0)
            {
                if (cb1.Checked == true)
                {
                    insertQuery = "Insert into [員工基本資料] (員工卡號,員工編號,員工姓名,性別,生日,到職日,職務屬性,特休天數,離職) values" +
                        "('" + arr[0] + "','" + arr[1] + "','" + arr[2] + "','" + arr[3] + "','" + birthday + "','" + dateIn +
                        "','" + arr[4] + "','" + arr[5] + "','false')";
                }
                else
                {
                    insertQuery = "Insert into [員工基本資料] (員工卡號,員工編號,員工姓名,性別,生日,到職日,職務屬性,特休天數,離職,離職日) values" +
                        "('" + arr[0] + "','" + arr[1] + "','" + arr[2] + "','" + arr[3] + "','" + birthday + "','" + dateIn +
                        "','" + arr[4] + "','" + arr[5] + "','true','" + quit + "')";
                }
            }
            else
            {
                if (cb1.Checked == true)
                {
                    insertQuery = "Update [員工基本資料] Set 員工編號='" + arr[1] + "',員工姓名='" + arr[2] + "',性別='" + arr[3] +
                        "',生日='" + birthday + "',到職日='" + dateIn + "',職務屬性='" + arr[4] + "',特休天數='" + arr[5] +
                        "' Where 員工卡號='" + arr[0] + "'";
                }
                else
                {
                    insertQuery = "Update [員工基本資料] Set 員工編號='" + arr[1] + "',員工姓名='" + arr[2] + "',性別='" + arr[3] +
                        "',生日='" + birthday + "',到職日='" + dateIn + "',職務屬性='" + arr[4] + "',特休天數='" + arr[5] +
                        "',離職='true',離職日='" + quit + "' Where 員工卡號='" + arr[0] + "'";
                }
            }

            new SqlCRUD(insertQuery, "mdb");
            MessageBox.Show("儲存完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            ClearAllContorler();
            TbCardNumber.Text = "";
            TbCardNumber.Focus();
        }

        private void cb1_CheckedChanged(object sender, EventArgs e)
        {
            if (cb1.Checked == true)
            {
                label10.Visible = false;
                DpQuit.Visible = false;
            }
            else
            {
                label10.Visible = true;
                DpQuit.Visible = true;
                DpQuit.Value = DateTime.Today;
            }

        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            string cardNumberQuery = "Select * From [員工基本資料] Where 員工卡號='" + TbCardNumber.Text + "' And 離職='false'";
            DataTable table = SqlCRUD.SqlQuery(cardNumberQuery, "mdb");

            if (table.Rows.Count == 1)
            {
                TbID.Text = table.Rows[0][2].ToString();
                TbName.Text = table.Rows[0][3].ToString();

                if (table.Rows[0][4].ToString() == "男")
                    Rb1.Checked = true;
                else
                    Rb2.Checked = true;

                DpBirthday.Value = Convert.ToDateTime(table.Rows[0][5]);
                DpIn.Value = Convert.ToDateTime(table.Rows[0][6]);
                comboBox1.Text = table.Rows[0][8].ToString();
                TbVacation.Text = table.Rows[0][9].ToString();
            }
            else
            {
                ClearAllContorler();
            }

            panel1.Visible = true;
        }

        public void ClearAllContorler()
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
            DpQuit.Visible = false;
        }

    }
}
