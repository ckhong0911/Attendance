using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Attendance
{
    public partial class TakeOff : Form
    {
        public TakeOff()
        {
            InitializeComponent();
        }

        //初始化DateTimePicker
        private void TakeOff_Load(object sender, EventArgs e)
        {           
            dTimePicker1.Format = DateTimePickerFormat.Custom;
            dTimePicker1.TextChanged += new EventHandler(dTimePicker1_TextChanged);

            dTimePicker2.Format = DateTimePickerFormat.Custom;
            dTimePicker2.TextChanged += new EventHandler(dTimePicker2_TextChanged);

            
            //除假別外，其他欄位設定為Readonly
            Dt.ReadOnly = false;
            for (int i = 0; i < Dt.ColumnCount; i++)
            {
                Dt.Columns[i].ReadOnly = true;
            }
            Dt.Columns[3].ReadOnly = false;
        }

        //把dTimePicker的Text值賦給dataGridView選中的單元格值
        private void dTimePicker1_TextChanged(object sender, EventArgs e)
        {
            Dt.CurrentCell.Value = dTimePicker1.Text;
            Dt.Rows[Dt.CurrentCell.RowIndex].Cells[5].Value = "";
            Dt.Rows[Dt.CurrentCell.RowIndex].Cells[6].Value = "";
        }

        //把dTimePicker的Text值賦給dataGridView選中的單元格值
        private void dTimePicker2_TextChanged(object sender, EventArgs e)
        {
            Dt.CurrentCell.Value = dTimePicker2.Text;
        }

        //選擇日期後的動作
        private void dTimePicker1_CloseUp(object sender, EventArgs e)
        {
            //dTimePicker選取的日期轉入dataGridView1選中儲存格
            Dt.Rows[Dt.CurrentRow.Index].Cells[4].Value = dTimePicker1.Value.ToString("yyyy/MM/dd");
            dTimePicker1.Visible = false;
        }

        //選擇日期後的動作
        private void dTimePicker2_CloseUp(object sender, EventArgs e)
        {
            //dTimePicker選取的日期轉入dataGridView1選中儲存格
            int rowIndex = Dt.CurrentCell.RowIndex;

            Dt.Rows[rowIndex].Cells[5].Value = dTimePicker2.Value.ToString("yyyy/MM/dd");
            dTimePicker2.Visible = false;

            DateTime date1 = Convert.ToDateTime(Dt.Rows[rowIndex].Cells[4].Value);
            DateTime date2 = Convert.ToDateTime(Dt.Rows[rowIndex].Cells[5].Value);
            int days = new TimeSpan(date2.Ticks - date1.Ticks).Days;

            //計算計假天數
            if (days >= 0 && days <= 366)
            {
                string[] arr1 = date1.ToString("yyyy/MM/dd").Split('/');
                string[] arr2 = date2.ToString("yyyy/MM/dd").Split('/');

                if (arr1[0] != arr2[0])
                {
                    MessageBox.Show("請假期間必須為同年份", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                //僅計算行事曆中上班日(旗標0)
                string dateStart = arr1[0] + arr1[1] + arr1[2];
                string dateEnd = arr2[0] + arr2[1] + arr2[2];

                string searchQuery = $"Select 旗標 From [{arr1[0]}行事曆] Where 西元日期 Between '{dateStart}' And '{dateEnd}' And 旗標 = 0";
                DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

                Dt.Rows[rowIndex].Cells[6].Value = table.Rows.Count.ToString();
                table.Clear();
            }
            else if (days < 0)
            {
                MessageBox.Show("結束日期不可小於起始日期", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Dt.Rows[rowIndex].Cells[5].Value = "";
                Dt.Rows[rowIndex].Cells[6].Selected = true;
            }
            else
            {
                MessageBox.Show("請先選擇請假起始日期", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                Dt.Rows[rowIndex].Cells[5].Value = "";
            }
            
        }

        //點選儲存格後顯示dTimePicker
        private void Dt_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                //獲得dataGridView1選中單元格顯示區域的矩形
                DataGridViewCell currentCell = Dt.CurrentCell;
                Rectangle rect = Dt.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, true);

                if (currentCell != null && currentCell.OwningColumn.Name == "StartDate")
                {
                    getDateTimePickerSize(dTimePicker1, rect);
                }
                else if (currentCell != null && currentCell.OwningColumn.Name == "EndDate")
                {
                    getDateTimePickerSize(dTimePicker2, rect);
                }
                else
                {
                    this.dTimePicker1.Visible = false;
                    this.dTimePicker2.Visible = false;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        //調整dTimePicker尺寸並顯示在dataGridView1選中儲存格內
        private void getDateTimePickerSize(DateTimePicker dateBox, Rectangle rect)
        {
            int x = Dt.Location.X;
            int y = Dt.Location.Y;
            dateBox.Visible = true;
            dateBox.Top = rect.Top + y;
            dateBox.Left = rect.Left + x;
            dateBox.Height = rect.Height;
            dateBox.Width = rect.Width;
        }

        //呼叫員工基本資料選取表單
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            FilterQuery f = new FilterQuery(this);
            f.ShowDialog();
        }

        //垃圾桶移除功能
        private void IconBox_Click(object sender, EventArgs e)
        {
            if (Dt.RowCount > 0)
            {
                if (MessageBox.Show("是否移除勾選項目?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    for (int i = Dt.Rows.Count - 1; i >= 0; i--)
                        if (Dt.Rows[i].Cells[0].Value != null)
                            if (Dt.Rows[i].Cells[0].Value.Equals("Yes"))
                                Dt.Rows.RemoveAt(i);
                }
            }
        }

        //點選儲存格，首欄變化
        private void Dt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            int colIndex = e.ColumnIndex;

            if (colIndex == 0)
            {
                if (Dt.Rows[index].Cells[0].Value == null)
                    Dt.Rows[index].Cells[0].Value = "Yes";
                else if (Dt.Rows[index].Cells[0].Value.ToString() == "Yes")
                    Dt.Rows[index].Cells[0].Value = "No";
                else
                    Dt.Rows[index].Cells[0].Value = "Yes";
            }
        }

        //儲存(新增頁面)
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (Dt.Rows.Count == 0)
            {
                MessageBox.Show("無資料可新增", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {
                Dt.Rows[0].Cells[0].Selected = true;
            }

            bool b = false;

            if (MessageBox.Show("是否儲存表格資料?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                for (int i = Dt.Rows.Count - 1; i >= 0; i--)
                {
                    string empID = Dt.Rows[i].Cells[1].Value.ToString();
                    string name = Dt.Rows[i].Cells[2].Value.ToString();
                    string type = Dt.Rows[i].Cells[3].Value.ToString();
                    string start = Dt.Rows[i].Cells[4].Value.ToString();
                    string end = Dt.Rows[i].Cells[5].Value.ToString();
                    int total = Convert.ToInt32(Dt.Rows[i].Cells[6].Value);
                    bool a = false;

                    if (type == "特休假")
                    {
                        //員工特休假取出
                        string searchQuery = $"Select 特休天數 From [員工基本資料] WHERE 員工編號='{empID}'";
                        DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");
                        int annualLeave = Convert.ToInt32(table.Rows[0][0]);

                        //請假登錄特休假總計
                        string offQuery = $"Select 員工編號 From [請假登錄] WHERE 員工編號='{empID}' And 假別='特休假'";
                        DataTable table_off = SqlCRUD.SqlQuery(offQuery, "mdb");
                        int sum = table_off.Rows.Count;

                        if ((total + sum) > annualLeave)
                        {
                            MessageBox.Show("已超過當年度特休總數", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            break;
                        }
                    }

                    //假日不可請假
                    if (total == 0)
                    {
                        MessageBox.Show("計假天數不可為 0", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        continue;
                    }

                    //int rowIndex = Dt.CurrentCell.RowIndex;
                    DateTime date1 = Convert.ToDateTime(Dt.Rows[i].Cells[4].Value);
                    DateTime date2 = Convert.ToDateTime(Dt.Rows[i].Cells[5].Value);

                    string[] arr1 = date1.ToString("yyyy/MM/dd").Split('/');
                    string reptQuery = $"Select 起始日期,結束日期,假別 From[請假登錄] WHERE 員工編號 = '{empID}' And 起始日期 Like '{arr1[0]}%'";
                    DataTable rept = SqlCRUD.SqlQuery(reptQuery, "mdb");

                    for (int j = 0; j < rept.Rows.Count; j++)
                    {
                        DateTime date3 = Convert.ToDateTime(rept.Rows[j][0]);
                        DateTime date4 = Convert.ToDateTime(rept.Rows[j][1]);

                        int t1 = date1.CompareTo(date3);
                        int t2 = date1.CompareTo(date4);
                        int t3 = date2.CompareTo(date3);
                        int t4 = date2.CompareTo(date4);

                        if (t1 >= 0 && t2 <= 0 || t3 >= 0 && t4 <= 0)
                        {
                            MessageBox.Show("該日期區間已有登錄休假" + "\r\n" + "員工編號: " + empID + "\r\n" + "員工姓名: " + name + "\r\n" + "假別: " +
                                rept.Rows[j][2].ToString() + "\r\n" + "起始日期: " + date3.ToString("yyyy/MM/dd") + "\r\n" + "結束日期: " + date4.ToString("yyyy/MM/dd"),
                                "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            a = true;
                            break;
                        }
                    }

                    if (a) continue;

                    //新增至請假登錄資料表
                    string insertQuery = $"Insert into [請假登錄] (員工編號,員工姓名,假別,起始日期,結束日期,計假天數) Values" +
                        $"('{empID}','{name}','{type}','{start}','{end}','{total}')";
                    new SqlCRUD(insertQuery, "mdb");

                    Dt.Rows.RemoveAt(i);
                    b = true;
                }

                if (b)
                    MessageBox.Show("新增完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }  
        }

        //Datagridview初始化method
        private void InitialData(DataGridView grid)
        {
            string searchQuery = "Select * From [請假登錄] Order by 起始日期 Desc,員工編號 Asc";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            grid.DataSource = table;

            for (int i = 1; i <= grid.ColumnCount - 1; i++)
                grid.Columns[i].Width = 115;

            grid.Columns[1].Visible = false;
            grid.Columns[4].Width = 150;

            //除首欄，其他欄位設定為Readonly
            grid.ReadOnly = false;
            for (int i = 1; i < grid.ColumnCount; i++)
            {
                grid.Columns[i].ReadOnly = true;
            }
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
                DatePicker3.Format = DateTimePickerFormat.Custom;
                DatePicker4.Format = DateTimePickerFormat.Custom;
                DatePicker5.Format = DateTimePickerFormat.Custom;
                BtnClear_Click(this, e);
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

        //資料篩選method
        public void Filter(string keyword, DataGridView grid)
        {
            string searchQuery = $"Select * From [請假登錄] WHERE 員工編號 Like '%{keyword}%' Or 員工姓名 Like '%{keyword}%' Or" +
                $" 假別 Like '%{keyword}%' Or 起始日期 Like '%{keyword}%' Or 結束日期 Like '%{keyword}%'";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            grid.DataSource = table;
        }

        //關鍵字搜尋(查詢資料頁面)
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Filter(txtSearch.Text, Dt_4);
        }
        
        //關鍵字搜尋(刪除資料頁面)
        private void TbSearch_TextChanged(object sender, EventArgs e)
        {
            Filter(TbSearch.Text, Dt_3);
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
                        DatePicker1.Text = Dt_4.Rows[i].Cells[5].Value.ToString();
                        BtnQuery_Click(this, e);
                    }
            }
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
                        string type = Dt_3.Rows[i].Cells[4].Value.ToString();
                        string start = Dt_3.Rows[i].Cells[5].Value.ToString();
                        string end = Dt_3.Rows[i].Cells[6].Value.ToString();

                        if (MessageBox.Show("是否刪除以下此筆資料?" + "\r\n" + "員工編號:" + empid + "\r\n" + "員工姓名:" + name +
                            "\r\n" + "假別:" + type + "\r\n" + "起始日期:" + start + "\r\n" + "結束日期:" +
                            end, "System",MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            string deleteQuery = $"Delete From [請假登錄] WHERE 員工編號='{empid}' And 假別='{type}' And 起始日期='{start}' And 結束日期='{end}'";
                            new SqlCRUD(deleteQuery, "mdb");
                            MessageBox.Show("刪除完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            tabControl1_SelectedIndexChanged(this, e);
                        }
                    }
            }
        }

        //查詢(更新資料頁面)
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            string searchQuery = "Select * From [請假登錄] Where 員工編號='" + txtSearchBox.Text + "' And 起始日期='" +
                DatePicker1.Text + "' Or 員工姓名='" + txtSearchBox.Text + "' And 起始日期='" + DatePicker1.Text + "'";
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
                grp3.Visible = false;
                panel1.Visible = true;
                lblID.Text = table.Rows[0][1].ToString();
                lblName.Text = table.Rows[0][2].ToString();
                CboType.Text = table.Rows[0][3].ToString();
                DatePicker2.Text = table.Rows[0][4].ToString();
                DatePicker3.Text = table.Rows[0][5].ToString();
                lblDays.Text = table.Rows[0][6].ToString();
                txtSearchBox.Enabled = false;
                DatePicker1.Enabled = false;
            }
        }

        //重新計算(更新資料頁面)
        private void BtnExeCount_Click(object sender, EventArgs e)
        {
            try
            {
                string empID = lblID.Text;
                string name = lblName.Text;
                string type = CboType.Text;
                string start = DatePicker2.Text;
                string end = DatePicker3.Text;

                string[] arr1 = start.Split('/');
                string[] arr2 = end.Split('/');

                if (arr1[0] != arr2[0])
                {
                    MessageBox.Show("請假期間必須為同年份", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                //僅計算行事曆中上班日(旗標0)
                string dateStart = arr1[0] + arr1[1] + arr1[2];
                string dateEnd = arr2[0] + arr2[1] + arr2[2];

                string searchQuery = $"Select 旗標 From [{arr1[0]}行事曆] Where 西元日期 Between '{dateStart}' And '{dateEnd}' And 旗標 = 0";
                DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

                lblDayTotal.Text = table.Rows.Count.ToString();
                table.Clear();

                int total = Convert.ToInt32(lblDayTotal.Text);

                if (type == "特休假")
                {
                    //員工特休假取出
                    string vacationQuery = $"Select 特休天數 From [員工基本資料] WHERE 員工編號='{empID}'";
                    DataTable table_vacation = SqlCRUD.SqlQuery(vacationQuery, "mdb");
                    int annualLeave = Convert.ToInt32(table_vacation.Rows[0][0]);

                    //請假登錄特休假總計
                    string offQuery = $"Select 員工編號 From [請假登錄] WHERE 員工編號='{empID}' And 假別='特休假'";
                    DataTable table_off = SqlCRUD.SqlQuery(offQuery, "mdb");
                    int sum = table_off.Rows.Count;

                    if ((total + sum) > annualLeave)
                    {
                        MessageBox.Show("已超過當年度特休總數", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                //假日不可請假
                if (total == 0)
                {
                    MessageBox.Show("請假日期為假日", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                //結束日期必須大於起始日期
                DateTime date1 = Convert.ToDateTime(start);
                DateTime date2 = Convert.ToDateTime(end);
                int days = new TimeSpan(date2.Ticks - date1.Ticks).Days;

                if (days < 0)
                {
                    MessageBox.Show("結束日期不可小於起始日期", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                string reptQuery = $"Select 起始日期,結束日期,假別 From[請假登錄] WHERE 員工編號 = '{empID}' And 起始日期 Like '{arr1[0]}%'";
                DataTable rept = SqlCRUD.SqlQuery(reptQuery, "mdb");

                for (int j = 0; j < rept.Rows.Count; j++)
                {
                    DateTime date3 = Convert.ToDateTime(rept.Rows[j][0]);
                    DateTime date4 = Convert.ToDateTime(rept.Rows[j][1]);

                    int t1 = date1.CompareTo(date3);
                    int t2 = date1.CompareTo(date4);
                    int t3 = date2.CompareTo(date3);
                    int t4 = date2.CompareTo(date4);

                    if (t1 >= 0 && t2 <= 0 || t3 >= 0 && t4 <= 0)
                    {
                        MessageBox.Show("該日期區間已有登錄休假" + "\r\n" + "員工編號: " + empID + "\r\n" + "員工姓名: " + name + "\r\n" + "假別: " +
                            rept.Rows[j][2].ToString() + "\r\n" + "起始日期: " + date3.ToString("yyyy/MM/dd") + "\r\n" + "結束日期: " + date4.ToString("yyyy/MM/dd"),
                            "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        grp3.Visible = false;
                        return;
                    }
                }

                panel1.Visible = false;
                DatePicker4.Text = DatePicker2.Text;
                DatePicker5.Text = DatePicker3.Text;
                grp3.Visible = true;
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //確認更新(更新頁面)
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否更新此筆資料?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                //更新至請假登錄資料表
                string updateQuery = $"Update [請假登錄] Set 假別='{CboType.Text}',起始日期='{DatePicker4.Text}',結束日期='{DatePicker5.Text}',計假天數='{lblDayTotal.Text}'" +
                    $" Where 起始日期='{DatePicker1.Text}'";
                new SqlCRUD(updateQuery, "mdb");

                BtnClear_Click(this, e);

                MessageBox.Show("更新完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                BtnClear_Click(this, e);
            }
        }

        //清除(更新頁面)
        private void BtnClear_Click(object sender, EventArgs e)
        {
            grp2.Visible = false;
            grp3.Visible = false;
            txtSearchBox.Enabled = true;
            DatePicker1.Enabled = true;
            txtSearchBox.Text = "";
            txtSearchBox.Focus();
        }

    }
}
