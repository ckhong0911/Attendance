/* ==============================================================================
* 功能描述：加班登錄 新建(C)、讀取(R)、更新(U)、刪除(D)
* 編 寫 者：Hugo
* 更新日期：2021/8/22
* ===============================================================================*/


using System;
using System.Data;
using System.Windows.Forms;


namespace Attendance
{
    public partial class OverTime : Form
    {
        public OverTime()
        {
            InitializeComponent();
            OTdate.Format = DateTimePickerFormat.Custom;

            //欄位1~6、9設定為Readonly
            Dt_1.ReadOnly = false;
            for (int i = 0; i <= 6; i++)
            {
                Dt_1.Columns[i].ReadOnly = true;
            }
            Dt_1.Columns[9].ReadOnly = true;
        }

        //索引書籤切換
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])        //新增資料
            {

            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])   //更新資料
            {
                DatePicker1.Format = DateTimePickerFormat.Custom;
                DatePicker2.Format = DateTimePickerFormat.Custom;
                TimePicker1.Format = DateTimePickerFormat.Custom;
                TimePicker2.Format = DateTimePickerFormat.Custom;
                TimePicker3.Format = DateTimePickerFormat.Custom;
                TimePicker4.Format = DateTimePickerFormat.Custom;
                TimePicker5.Format = DateTimePickerFormat.Custom;
                TimePicker6.Format = DateTimePickerFormat.Custom;

                
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])   //刪除資料
            {
                InitialData(Dt_3);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])   //查詢資料
            {
                InitialData(Dt_4);
            }
        }

        //選擇人員(新增資料)
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            FilterQuery f = new FilterQuery(this,OTdate.Value);
            f.ShowDialog();
        }

        //垃圾桶移除功能
        private void IconBox_Click(object sender, EventArgs e)
        {
            if (Dt_1.RowCount > 0)
            {
                if (MessageBox.Show("是否移除勾選項目?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    for (int i = Dt_1.Rows.Count - 1; i >= 0; i--)
                        if (Dt_1.Rows[i].Cells[0].Value != null)
                            if (Dt_1.Rows[i].Cells[0].Value.Equals("Yes"))
                                Dt_1.Rows.RemoveAt(i);
                }
            }
            
        }

        //點選儲存格，首欄變化
        private void Dt_1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if (colIndex == 0)
            {
                if (Dt_1.Rows[index].Cells[0].Value == null)
                    Dt_1.Rows[index].Cells[0].Value = "Yes";
                else if (Dt_1.Rows[index].Cells[0].Value.ToString() == "Yes")
                    Dt_1.Rows[index].Cells[0].Value = "No";
                else
                    Dt_1.Rows[index].Cells[0].Value = "Yes";
            }

        }

        //加班起始及結束輸入事件
        private void Dt_1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //宣告儲存格填入座標並檢查填寫加班結束時間時，是否已填好加班起始時間
                int x = Dt_1.CurrentCell.ColumnIndex - 1;
                int y = Dt_1.CurrentCell.RowIndex;

                if (x < 7) return;

                try
                {
                    //宣告上班時間及下班時間的值
                    DateTime dt1 = Convert.ToDateTime(Dt_1.Rows[y].Cells[4].Value.ToString());
                    DateTime dt2 = Convert.ToDateTime(Dt_1.Rows[y].Cells[5].Value.ToString());
                    string val = Dt_1.Rows[y].Cells[x].Value.ToString();
                    string[] str = val.Split(':');

                    if (val.Length == 0) return;
                    else if (str.Length != 2)
                    {
                        ExceptionBox(val + " 非標準時間格式" + "\r\n" + "範例: 17:30(24小時制)", x, y);
                        return;
                    }
                    else if (str[0].Length != 2 || str[1].Length != 2)
                    {
                        ExceptionBox(val + " 非標準時間格式" + "\r\n" + "範例: 17:30(24小時制)", x, y);
                        return;
                    }

                    //宣告輸入的值並與下班時間做比對，確定加班起始時間在下班時間後
                    DateTime dt3 = Convert.ToDateTime(Convert.ToDateTime(Dt_1.Rows[y].Cells[7].Value.ToString()));
                    int t1 = dt2.TimeOfDay.CompareTo(dt3.TimeOfDay);

                    if (x == 7)
                    {
                        if (t1 > 0)
                        {
                            ExceptionBox("加班時間必須在下班時間後", x, y);
                            return;
                        }
                        else if (Dt_1.Rows[y].Cells[8].Value.ToString() != null)
                        {
                            int t4 = dt3.TimeOfDay.CompareTo(Convert.ToDateTime(Dt_1.Rows[y].Cells[8].Value).TimeOfDay);
                            if (t4 == 0)
                            {
                                ExceptionBox("加班結束時間不可等於加班起始時間", x, y);
                                return;
                            }
                        }

                        //將區間時間換算成分鐘填入加班時數欄位
                        double result = TimeDiff(dt3, Convert.ToDateTime(Dt_1.Rows[y].Cells[8].Value)) / 60;
                        if (result < 0) result += 24;
                        OtHourSum(result, y);

                    }
                    else if (x == 8)
                    {
                        DateTime dt4 = Convert.ToDateTime(Dt_1.Rows[y].Cells[x].Value);
                        int t2 = dt1.TimeOfDay.CompareTo(dt4.TimeOfDay);
                        int t3 = dt2.TimeOfDay.CompareTo(dt4.TimeOfDay);
                        int t4 = dt3.TimeOfDay.CompareTo(dt4.TimeOfDay);

                        if (t2 < 0 && t3 > 0)
                        {
                            ExceptionBox("加班結束時間不可在上班時間", x, y);
                            return;
                        }
                        else if (t4 == 0)
                        {
                            ExceptionBox("加班結束時間不可等於加班起始時間", x, y);
                            return;
                        }

                        //將區間時間換算成分鐘填入加班時數欄位
                        double result = TimeDiff(dt3, dt4) / 60;
                        if (result < 0) result += 24;
                        OtHourSum(result, y);
                        
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }

        //變更 Enter 動作
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.Handled = true;

                System.Windows.Forms.SendKeys.Send("{TAB}");

            }
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            {
                case System.Windows.Forms.Keys.Enter:
                    {
                        System.Windows.Forms.SendKeys.Send("{TAB}");
                        }

                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //Datagridview初始化method
        private void InitialData(DataGridView grid)
        {
            string searchQuery = "Select * From [加班時數登錄] Order by 加班日期 Desc,員工編號 Asc";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            grid.DataSource = table;

            for (int i = 1; i <= grid.ColumnCount - 1; i++)
                grid.Columns[i].Width = 115;

            grid.Columns[1].Visible = false;
            grid.Columns[4].Width = 80;
            grid.Columns[7].Width = 130;

            grid.Columns["上班時間"].DefaultCellStyle.Format = "HH:mm";
            grid.Columns["下班時間"].DefaultCellStyle.Format = "HH:mm";
            grid.Columns["加班起始"].DefaultCellStyle.Format = "HH:mm";
            grid.Columns["加班結束"].DefaultCellStyle.Format = "HH:mm";

            //除首欄，其他欄位設定為Readonly
            grid.ReadOnly = false;
            for (int i = 1; i < grid.ColumnCount; i++)
            {
                grid.Columns[i].ReadOnly = true;
            }
        }

        //Datagridview僅允許勾選一個method
        private void AllowSelectOne(DataGridView grid, int rowIndex)
        {
            for (int i = 0; i <= grid.RowCount - 1; i++)
            {
                if ((bool)(grid.Rows[i].Cells[0].Value = true))
                    if (i == rowIndex)
                        continue;
                    else
                        grid.Rows[i].Cells[0].Value = false;
            }
        }

        //時間運算method
        private double TimeDiff(DateTime start, DateTime end)
        {
            TimeSpan span = end - start;
            double totalMinutes = span.TotalMinutes;
            return totalMinutes;
        }

        //加班時數總計method
        private void OtHourSum(double hour, int rowIndex)
        {
            double result = Math.Round(hour, 2);
            Dt_1.Rows[rowIndex].Cells[9].Value = result.ToString();
        }

        //Exception method
        private void ExceptionBox(string msg, int x, int y)
        {
            MessageBox.Show(msg, "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            Dt_1.Rows[y].Cells[x].Value = "";
            Dt_1.Rows[y].Cells[9].Value = "";
            Dt_1.CurrentCell = Dt_1[x, y];
        }

        //資料篩選method
        public void Filter(string keyword, DataGridView grid)
        {
            string searchQuery = "Select * From [加班時數登錄] WHERE 員工編號 Like '%" + keyword + "%' Or 員工姓名 Like '%" + keyword +
                "%' Or 班別='" + keyword + "' Or 加班日期 Like '%" + keyword + "'";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            grid.DataSource = table;
        }

        //儲存(新增資料頁面)
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (Dt_1.Rows.Count == 0)
            {
                MessageBox.Show("表格無資料", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            if (MessageBox.Show("是否儲存表格資料?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                for (int i = 0; i <= Dt_1.Rows.Count - 1; i++)
                {
                    if (Dt_1.Rows[i].Cells[9].Value == null)
                    {
                        MessageBox.Show("尚有未完成填寫項目", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                for (int i = 0; i <= Dt_1.Rows.Count - 1; i++)
                {
                    string id = Dt_1.Rows[i].Cells[1].Value.ToString();
                    string name = Dt_1.Rows[i].Cells[2].Value.ToString();
                    string shift = Dt_1.Rows[i].Cells[3].Value.ToString();
                    string on = Dt_1.Rows[i].Cells[4].Value.ToString();
                    string off = Dt_1.Rows[i].Cells[5].Value.ToString();
                    string date = Dt_1.Rows[i].Cells[6].Value.ToString();
                    string start = Dt_1.Rows[i].Cells[7].Value.ToString();
                    string end = Dt_1.Rows[i].Cells[8].Value.ToString();
                    string total = Dt_1.Rows[i].Cells[9].Value.ToString();

                    string insertQuery = "Insert into [加班時數登錄] (員工編號,員工姓名,班別,上班時間,下班時間,加班日期,加班起始,加班結束,加班時數) values" +
                    "('" + id + "','" + name + "','" + shift + "','" + on + "','" + off + "','" + date + "','" + start + "','" + end + "','" + total + "')";

                    new SqlCRUD(insertQuery, "mdb");
                }

                MessageBox.Show("加班登錄完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                Dt_1.Rows.Clear();
            }
        }

        //關鍵字搜尋(刪除資料頁面)
        private void TbSearch_TextChanged(object sender, EventArgs e)
        {
            Filter(txtSearch.Text, Dt_3);
        }

        //關鍵字搜尋(查詢資料頁面)
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Filter(txtSearch.Text, Dt_4);
        }

        //表格首欄僅允許勾選一個(刪除資料頁面)
        private void Dt_3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AllowSelectOne(Dt_3, e.RowIndex);
        }

        //表格首欄僅允許勾選一個(查詢資料頁面)
        private void Dt_4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AllowSelectOne(Dt_4, e.RowIndex);
        }

        //刪除勾選資料(刪除資料頁面)
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Dt_3.Rows.Count - 1; i++)
            {
                if (Dt_3.Rows[i].Cells[0].Value != null)
                    if (Dt_3.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        string empid = Dt_3.Rows[i].Cells[2].Value.ToString();
                        string name = Dt_3.Rows[i].Cells[3].Value.ToString();
                        string date = Dt_3.Rows[i].Cells[7].Value.ToString();

                        if (MessageBox.Show("是否刪除以下此筆資料?" + "\r\n" + "員工編號:" + empid + "\r\n" + "員工姓名:" + name +
                            "\r\n" + "加班日期:" + date, "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            string deleteQuery = "Delete From [加班時數登錄] WHERE 員工編號='" + empid + "' And 加班日期='" + date + "'";
                            new SqlCRUD(deleteQuery, "mdb");

                            MessageBox.Show("刪除完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            tabControl1_SelectedIndexChanged(this, e);
                            return;
                        }
                    }
            }
        }

        //更新勾選資料(查詢資料頁面)
        private void BtnUpdating_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Dt_4.Rows.Count - 1; i++)
            {
                if (Dt_4.Rows[i].Cells[0].Value != null)
                    if (Dt_4.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        string shift = Dt_4.Rows[i].Cells[2].Value.ToString();

                        tabControl1.SelectedTab = tabControl1.TabPages["tabPage2"];
                        txtSearchBox.Text = Dt_4.Rows[i].Cells[3].Value.ToString();
                        DatePicker1.Text = Dt_4.Rows[i].Cells[7].Value.ToString();
                        BtnQuery_Click(this, e);
                    }
            }
        }

        //查詢(更新資料頁面)
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            string searchQuery = "Select * From [加班時數登錄] Where 員工編號='" + txtSearchBox.Text + "' And 加班日期='" + 
                DatePicker1.Text + "' Or 員工姓名='" + txtSearchBox.Text + "' And 加班日期='" + DatePicker1.Text + "'";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("查無資料", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                grp2.Visible = false;
                grp3.Visible = false;
                return;
            }
            else
            {
                grp2.Visible = true;
                panel1.Visible = true;
                lblID.Text = table.Rows[0][1].ToString();
                lblName.Text = table.Rows[0][2].ToString();
                txtShift.Text = table.Rows[0][3].ToString();
                TimePicker1.Text = table.Rows[0][4].ToString();
                TimePicker2.Text = table.Rows[0][5].ToString();
                TimePicker3.Text = table.Rows[0][7].ToString();
                TimePicker4.Text = table.Rows[0][8].ToString();
                lblHour.Text = table.Rows[0][9].ToString();
                DatePicker2.Text = DatePicker1.Text;
            }
        }

        //重新計算(更新資料頁面)
        private void BtnExeCount_Click(object sender, EventArgs e)
        {
            try
            {
                //宣告上班時間及下班時間的值
                DateTime dt1 = TimePicker1.Value;
                DateTime dt2 = TimePicker2.Value;
                DateTime dt3 = TimePicker3.Value;
                DateTime dt4 = TimePicker4.Value;


                //宣告輸入的值並與下班時間做比對，確定加班起始時間在下班時間後
                int t1 = dt2.TimeOfDay.CompareTo(dt3.TimeOfDay);

                if (t1 > 0)
                {
                    MessageBox.Show("加班時間必須在下班時間後", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                int t2 = dt1.TimeOfDay.CompareTo(dt4.TimeOfDay);
                int t3 = dt2.TimeOfDay.CompareTo(dt4.TimeOfDay);
                int t4 = dt3.TimeOfDay.CompareTo(dt4.TimeOfDay);

                if (t2 < 0 && t3 > 0)
                {
                    MessageBox.Show("加班結束時間不可在上班時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                else if (t4 == 0)
                {
                    MessageBox.Show("加班結束時間不可等於加班起始時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                //將區間時間換算成分鐘顯示加班時數
                double result = TimeDiff(dt3, dt4) / 60;
                if (result < 0) result += 24;
                double hour = Math.Round(result, 1);


                //確定修正有效
                //if (lblHour.Text == hour.ToString())
                //{
                //    MessageBox.Show("加班時數重新計算後不變", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    grp3.Visible = false;
                //    return;
                //}
                //else
                //{
                    panel1.Visible = false;
                    lblHourTotal.Text = hour.ToString();
                    TimePicker5.Text = TimePicker3.Text;
                    TimePicker6.Text = TimePicker4.Text;
                    grp3.Visible = true;
                //}
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //確認更新(更新資料頁面)
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否更新此筆資料?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string updateQuery = "Update [加班時數登錄] Set 加班起始='" + TimePicker5.Text + "',加班結束='" + TimePicker6.Text + "',加班時數='" +
                    lblHourTotal.Text + "' Where 員工編號='" + lblID.Text + "' And 加班日期='" + DatePicker2.Text + "'";

                new SqlCRUD(updateQuery, "mdb");
                MessageBox.Show("資料更新完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                txtSearchBox.Text = "";
                grp2.Visible = false;
                grp3.Visible = false;
                txtSearchBox.Focus();
            }
        }

    }
}
