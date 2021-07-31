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
                string insertQuery = "";

                string cardNumberQuery = "Select 離職 From [員工基本資料] Where 員工卡號='" + TbCardNumber.Text + "' And 離職='false'";
                DataTable table = SqlCRUD.SqlQuery(cardNumberQuery, "mdb");

                if (table.Rows.Count == 0)
                {
                    insertQuery = "Insert into [員工基本資料] (員工卡號,員工編號,員工姓名,性別,生日,到職日,職務屬性,特休天數,離職) values" +
                            "('" + arr[0] + "','" + arr[1] + "','" + arr[2] + "','" + arr[3] + "','" + birthday + "','" + dateIn +
                            "','" + arr[4] + "','" + arr[5] + "','false')";

                    new SqlCRUD(insertQuery, "mdb");
                    MessageBox.Show("新增完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show("此卡號已被使用", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                ClearAllContorler();
                TbCardNumber.Text = "";
                TbCardNumber.Focus();
            }

        }

        //更新資料
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string[] arr = { txtCardNumber.Text, TbID2.Text, TbName2.Text, "", comboBox2.Text, TbVacation2.Text };
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
            else if (Rb3.Checked == false && Rb4.Checked == false)
            {
                MessageBox.Show("未選擇性別", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (arr[4] == "")
            {
                MessageBox.Show("未輸入職務屬性", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else if (int.TryParse(TbVacation2.Text, out num1) == false)
            {
                MessageBox.Show("特休天數必須為數字", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
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
                        updateQuery = "Update [員工基本資料] Set 員工編號='" + arr[1] + "',員工姓名='" + arr[2] + "',性別='" + arr[3] +
                            "',生日='" + birthday + "',到職日='" + dateIn + "',職務屬性='" + arr[4] + "',特休天數='" + arr[5] +
                            "' Where 員工卡號='" + arr[0] + "'";
                    }
                    else
                    {
                        updateQuery = "Update [員工基本資料] Set 員工編號='" + arr[1] + "',員工姓名='" + arr[2] + "',性別='" + arr[3] +
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

        //更新資料頁面 查詢按鈕
        private void BtnQuery_Click_1(object sender, EventArgs e)
        {
            string cardNumberQuery = "Select * From [員工基本資料] Where 員工卡號='" + txtCardNumber.Text + "' And 離職='false'";
            DataTable table = SqlCRUD.SqlQuery(cardNumberQuery, "mdb");

            if (table.Rows.Count == 1)
            {
                TbID2.Text = table.Rows[0][2].ToString();
                TbName2.Text = table.Rows[0][3].ToString();

                if (table.Rows[0][4].ToString() == "男")
                    Rb3.Checked = true;
                else
                    Rb4.Checked = true;

                DpBirthday2.Value = Convert.ToDateTime(table.Rows[0][5]);
                DpIn2.Value = Convert.ToDateTime(table.Rows[0][6]);
                comboBox2.Text = table.Rows[0][8].ToString();
                TbVacation2.Text = table.Rows[0][9].ToString();
                panel1.Visible = true;
            }
            else
            {
                MessageBox.Show("無此人員資料", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                ClearAllContorler();
            }
        }

        //在職[核准方塊]
        private void cb1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cb1.Checked == true)
            {
                label14.Visible = false;
                DpQuit.Visible = false;
            }
            else
            {
                label14.Visible = true;
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

                Dt2.DataSource = table;
                Dt2.Columns[2].Width = 150;
                Dt2.Columns[1].Visible = false;

                //除首欄，其他欄位設定為Readonly
                Dt2.ReadOnly = false;
                for (int i = 1; i < Dt2.ColumnCount; i++)
                {
                    Dt2.Columns[i].ReadOnly = true;
                }
            }
        }

        //Dt表格首欄僅允許勾選一個
        private void Dt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            for (int i = 0; i <= Dt.RowCount - 1; i++)
            {
                if ((bool)(Dt.Rows[i].Cells[0].Value = true))
                    if (i == index)
                        continue;
                    else
                        Dt.Rows[i].Cells[0].Value = false;
            }
        }

        //Dt2表格首欄僅允許勾選一個
        private void Dt2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            for (int i = 0; i <= Dt2.RowCount - 1; i++)
            {
                if ((bool)(Dt2.Rows[i].Cells[0].Value = true))
                    if (i == index)
                        continue;
                    else
                        Dt2.Rows[i].Cells[0].Value = false;
            }
        }

        //刪除勾選資料
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Dt.Rows.Count - 1; i++)
            {
                if (Dt.Rows[i].Cells[0].Value != null)
                    if (Dt.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        string cardNumber = Dt.Rows[i].Cells[2].Value.ToString();
                        string id = Dt.Rows[i].Cells[3].Value.ToString();
                        string name = Dt.Rows[i].Cells[4].Value.ToString();

                        if (MessageBox.Show("是否刪除以下此筆資料?" + "\r\n" + "員工卡號:" + cardNumber + "\r\n" + "員工編號:" + id +
                            "\r\n" + "員工姓名:" + name, "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            string deleteQuery = "Delete From [員工基本資料] WHERE 員工卡號='" + cardNumber + "' And 員工編號='" +
                                id + "' And 員工姓名='" + name + "'";
                            new SqlCRUD(deleteQuery, "mdb");
                        }

                        MessageBox.Show("刪除完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        tabControl1_SelectedIndexChanged(this,e);
                        return;
                    }
            }
        }

        //資料篩選(刪除資料頁面)
        public void Filter(string keyword)
        {
            string searchQuery = "Select * From [員工基本資料] WHERE 員工卡號 Like '%" + keyword + "%' Or 員工編號 Like '%" + keyword +
                "%' Or 員工姓名 Like '%" + keyword + "' Or 職務屬性 Like '%" + keyword + "'";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            Dt.DataSource = table;
        }

        //資料篩選(查詢資料頁面)
        public void Filter2(string keyword)
        {
            string searchQuery = "Select * From [員工基本資料] WHERE 員工卡號 Like '%" + keyword + "%' Or 員工編號 Like '%" + keyword +
                "%' Or 員工姓名 Like '%" + keyword + "' Or 職務屬性 Like '%" + keyword + "'";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            Dt2.DataSource = table;
        }

        //關鍵字搜尋(刪除資料頁面)
        private void TbSearch_TextChanged(object sender, EventArgs e)
        {
            Filter(TbSearch.Text);
        }

        //關鍵字搜尋(查詢資料頁面)
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Filter2(txtSearch.Text);
        }

        //更新勾選資料(自動前往更新資料頁面)
        private void BtnUpdating_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Dt2.Rows.Count - 1; i++)
            {
                if (Dt2.Rows[i].Cells[0].Value != null)
                    if (Dt2.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        string cardNumber = Dt2.Rows[i].Cells[2].Value.ToString();
                        string id = Dt2.Rows[i].Cells[3].Value.ToString();
                        string name = Dt2.Rows[i].Cells[4].Value.ToString();
                        string sex = Dt2.Rows[i].Cells[5].Value.ToString();
                        string quit = Dt2.Rows[i].Cells[11].Value.ToString();

                        txtCardNumber.Text = cardNumber;
                        TbName2.Text = name;
                        TbID2.Text = id;

                        if (sex == "男")
                            Rb3.Checked = true;
                        else
                            Rb4.Checked = true;

                        DpBirthday2.Value = Convert.ToDateTime(Dt2.Rows[i].Cells[6].Value);
                        DpIn2.Value = Convert.ToDateTime(Dt2.Rows[i].Cells[7].Value);
                        comboBox2.Text = Dt2.Rows[i].Cells[9].Value.ToString();
                        TbVacation2.Text = Dt2.Rows[i].Cells[10].Value.ToString();
                        
                        if (quit == "true")
                        {
                            cb1.Checked = false;
                            label14.Visible = true;
                            DpQuit.Visible = true;
                            DpQuit.Value = Convert.ToDateTime(Dt2.Rows[i].Cells[8].Value);
                        }
                        else
                        {
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
