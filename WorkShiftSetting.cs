using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Attendance
{
    public partial class WorkShiftSetting : Form
    {
        public WorkShiftSetting()
        {
            InitializeComponent();
            getReset();
        }

        //初始化控制項
        public void getReset()
        {
            TimePicker_1.Format = DateTimePickerFormat.Custom;
            TimePicker_2.Format = DateTimePickerFormat.Custom;
            TimePicker_3.Format = DateTimePickerFormat.Custom;
            TimePicker_4.Format = DateTimePickerFormat.Custom;
            TimePicker_5.Format = DateTimePickerFormat.Custom;
            TimePicker_6.Format = DateTimePickerFormat.Custom;
            TimePicker_7.Format = DateTimePickerFormat.Custom;
            TimePicker_8.Format = DateTimePickerFormat.Custom;

            TimePicker_1.Value = Convert.ToDateTime("00:00");
            TimePicker_2.Value = Convert.ToDateTime("00:00");
            TimePicker_3.Value = Convert.ToDateTime("00:00");
            TimePicker_4.Value = Convert.ToDateTime("00:00");
            TimePicker_5.Value = Convert.ToDateTime("00:00");
            TimePicker_6.Value = Convert.ToDateTime("00:00");
            TimePicker_7.Value = Convert.ToDateTime("00:00");
            TimePicker_8.Value = Convert.ToDateTime("00:00");

            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;
            groupBox6.Visible = false;

            Dt.Rows.Clear();
            Dtt.Rows.Clear();

            txtWorkTime.Text = "0"; //上班時間
            txtRestTime.Text = "0"; //休息時間
            lblSum.Text = "0";   //工作時數
            txtWorkTime2.Text = "0"; //上班時間
            txtRestTime2.Text = "0"; //休息時間
            lblSum2.Text = "0";   //工作時數
            cboX.Text = "";  //班別選擇
            cboY.Text = "";  //班別選擇
            chk.Checked = false; //跨日班別

            //客戶別加入下拉式選單
            if (cboX.Items.Count == 0 && cboY.Items.Count == 0)
            {
                OleDbConnection oleCon = SqlCRUD.OleRead("mdb");
                string readQuery = "Select 班別 From [班別時間設定] Where 工作時數 = 0 Order by 班別";
                OleDbCommand oleCmd = new OleDbCommand(readQuery, oleCon);
                oleCon.Open();
                OleDbDataReader oleReader = oleCmd.ExecuteReader();

                while (oleReader.Read())
                    cboX.Items.Add(oleReader.GetString(0));

                string readQuery2 = "Select 班別 From [班別時間設定] Where 工作時數 > 0 Order by 班別";
                OleDbCommand oleCmd2 = new OleDbCommand(readQuery2, oleCon);
                OleDbDataReader oleReader2 = oleCmd2.ExecuteReader();

                while (oleReader2.Read())
                    cboY.Items.Add(oleReader2.GetString(0));

                oleReader.Close();
                oleCon.Close();
            }        
        }


        //Step1:上下班時間設定起始--------------------------------
        private void BtnOK_1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(TimePicker_1.Text);
            DateTime dt2 = Convert.ToDateTime(TimePicker_2.Text);
            int t = dt1.TimeOfDay.CompareTo(dt2.TimeOfDay);

            if (MessageBox.Show("是否新增此筆班別資料?" + "\r\n" + "上班時間:" + Convert.ToDateTime(TimePicker_1.Text).ToString("HH:mm") + "\r\n" + "下班時間:" +
                Convert.ToDateTime(TimePicker_2.Text).ToString("HH:mm"), "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //判斷1:結束時間是否大於開始時間
                if (t > 0)
                {
                    //if (MessageBox.Show("是否為跨日班別?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    //{
                    //    lblSum.Text = "24";
                    //    chk.Checked = true;
                    //}
                    //else
                    //{
                        MessageBox.Show("結束時間不可大於或等於起始時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    //}
                }
                else if (t == 0)
                {
                    MessageBox.Show("起始時間不可等於結束時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                } 


                //判斷2:資料庫組數中是否存在同樣時段設定
                string searchQuery = "Select 上班時間,下班時間,班別 From [班別時間設定] Where 工作時數 > 0";
                DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

                for (int i = 0; i <= table.Rows.Count - 1; i++)
                {
                    DateTime shiftstart = Convert.ToDateTime(table.Rows[i][0]);
                    DateTime shiftend = Convert.ToDateTime(table.Rows[i][1]);

                    double shiftStart = dt1.TimeOfDay.CompareTo(shiftstart.TimeOfDay);
                    double shiftEnd = dt2.TimeOfDay.CompareTo(shiftend.TimeOfDay);

                    if (shiftStart == 0 && shiftEnd == 0)
                    {
                        MessageBox.Show("與 " + table.Rows[0][2].ToString() + " 組時間設定相同", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }


                //將區間時間換算成分鐘填入Step3對應欄位
                double result = TimeDiff(TimePicker_1.Value, TimePicker_2.Value);
                txtWorkTime.Text = Math.Abs(result).ToString();

                double x = (result - Convert.ToDouble(txtRestTime.Text)) / 60;
                double Result = Math.Round(x, 2);
                //double final = Convert.ToDouble(lblSum.Text) + Result;
                lblSum.Text = Result.ToString();


                //儲存上下班時間設定
                string addQuery = "Select * From [班別時間設定] Where 班別='" + cboX.Text + "' And 工作時數 = 0";
                DataTable tableData = SqlCRUD.SqlQuery(addQuery, "mdb");

                if (tableData.Rows.Count == 1)
                {
                    string insertQuery="";
                    if (chk.Checked)
                    {
                        insertQuery = "Update [班別時間設定] Set 上班時間='" + TimePicker_1.Text + "',下班時間='" +
                        TimePicker_2.Text + "',工作時數='" + lblSum.Text + "',跨日班別='true' Where 班別='" + cboX.Text + "'";
                    }
                    else
                    {
                        insertQuery = "Update [班別時間設定] Set 上班時間='" + TimePicker_1.Text + "',下班時間='" +
                        TimePicker_2.Text + "',工作時數='" + lblSum.Text + "',跨日班別='false' Where 班別='" + cboX.Text + "'";
                    }

                    new SqlCRUD(insertQuery, "mdb");
                    tableData.Clear();
                }

                MessageBox.Show("班別新增完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


                //更新班別下拉式選單內容
                OleDbConnection oleCon = SqlCRUD.OleRead("mdb");
                string readQuery = "Select 班別 From [班別時間設定] Where 工作時數 = 0 Order by 班別";
                OleDbCommand oleCmd = new OleDbCommand(readQuery, oleCon);
                oleCon.Open();
                OleDbDataReader oleReader = oleCmd.ExecuteReader();
                cboX.Items.Clear();

                while (oleReader.Read())
                    cboX.Items.Add(oleReader.GetString(0));

                oleReader.Close();
                oleCon.Close();
            }
        }

        private void BtnOK_2_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(TimePicker_5.Text);
            DateTime dt2 = Convert.ToDateTime(TimePicker_6.Text);
            int t = dt1.TimeOfDay.CompareTo(dt2.TimeOfDay);

            if (MessageBox.Show("是否更新此筆班別資料?" + "\r\n" + "上班時間:" + Convert.ToDateTime(TimePicker_5.Text).ToString("HH:mm") + "\r\n" + "下班時間:" +
                Convert.ToDateTime(TimePicker_6.Text).ToString("HH:mm"), "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //判斷1:結束時間是否大於開始時間
                if (t > 0)
                {
                    //if (MessageBox.Show("是否為跨日班別?", "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    //{
                    //    lblSum.Text = "24";
                    //    chk.Checked = true;
                    //}
                    //else
                    //{
                    MessageBox.Show("結束時間不可大於或等於起始時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                    //}
                }
                else if (t == 0)
                {
                    MessageBox.Show("起始時間不可等於結束時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }


                //判斷2:資料庫組數中是否存在同樣時段設定
                string searchQuery = "Select 上班時間,下班時間,班別 From [班別時間設定] Where 工作時數 > 0";
                DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

                for (int i = 0; i <= table.Rows.Count - 1; i++)
                {
                    DateTime shiftstart = Convert.ToDateTime(table.Rows[i][0]);
                    DateTime shiftend = Convert.ToDateTime(table.Rows[i][1]);

                    double shiftStart = dt1.TimeOfDay.CompareTo(shiftstart.TimeOfDay);
                    double shiftEnd = dt2.TimeOfDay.CompareTo(shiftend.TimeOfDay);

                    if (shiftStart == 0 && shiftEnd == 0)
                    {
                        MessageBox.Show("與 " + table.Rows[0][2].ToString() + " 組時間設定相同", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                //將區間時間換算成分鐘填入Step3對應欄位
                double result = TimeDiff(TimePicker_5.Value, TimePicker_6.Value);
                txtWorkTime2.Text = Math.Abs(result).ToString();

                double x = (Math.Abs(result) - Convert.ToDouble(txtRestTime2.Text)) / 60;
                double Result = Math.Round(x, 2);
                //double final = Convert.ToDouble(lblSum2.Text) + Result;
                lblSum2.Text = Result.ToString();

                //儲存班別時間(更新班別頁面)
                string updateQuery = "";
                if (chk.Checked)
                {
                    updateQuery = "Update [班別時間設定] Set 上班時間='" + TimePicker_5.Text + "',下班時間='" +
                       TimePicker_6.Text + "',工作時數='" + lblSum2.Text + "',跨日班別='true' Where 班別='" + cboY.Text + "'";
                }
                else
                {
                    updateQuery = "Update [班別時間設定] Set 上班時間='" + TimePicker_5.Text + "',下班時間='" +
                       TimePicker_6.Text + "',工作時數='" + lblSum2.Text + "',跨日班別='false' Where 班別='" + cboY.Text + "'";
                }

                new SqlCRUD(updateQuery, "mdb");
                MessageBox.Show("更新完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
        //Step1:上下班時間設定結束--------------------------------


        //Step2:休息時間設定開始----------------------------------
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string shift = cboX.Text;
            string start = TimePicker_3.Text;
            string end = TimePicker_4.Text;

            DateTime dt = Convert.ToDateTime("00:00");
            DateTime dt1 = Convert.ToDateTime(TimePicker_1.Text);
            DateTime dt2 = Convert.ToDateTime(TimePicker_2.Text);
            DateTime dt3 = Convert.ToDateTime(TimePicker_3.Text);
            DateTime dt4 = Convert.ToDateTime(TimePicker_4.Text);

            int t1 = dt1.TimeOfDay.CompareTo(dt3.TimeOfDay);
            int t2 = dt2.TimeOfDay.CompareTo(dt4.TimeOfDay);
            int t3 = dt3.TimeOfDay.CompareTo(dt.TimeOfDay);
            int t4 = dt4.TimeOfDay.CompareTo(dt.TimeOfDay);
            int t5 = dt3.TimeOfDay.CompareTo(dt4.TimeOfDay);

            if (MessageBox.Show("是否新增此筆休息時間?" + "\r\n" + "休息時間起始:" + Convert.ToDateTime(TimePicker_3.Text).ToString("HH:mm") + "\r\n" + "休息時間結束:" +
                Convert.ToDateTime(TimePicker_4.Text).ToString("HH:mm"), "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //判斷1:休息時間必須在班別區間內
                if (chk.Checked)
                {   //跨日班別
                    MessageBox.Show("尚未編寫跨班休息時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
                else
                {   //非跨日班別              
                    if (t1 > 0 || t2 < 0)
                    {
                        MessageBox.Show("設定時間不在班別區間內", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    //判斷2:結束時間是否大於開始時間
                    if (t5 >= 0)
                    {
                        MessageBox.Show("結束時間不可小於或等於起始時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                //判斷3:新增的值是否涵蓋下表設定範圍
                string searchQuery = "Select 起始時間,結束時間 From [休息時間設定] Where 班別='" + shift + "'";
                DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string timeCheck_start = "Select 起始時間 From [休息時間設定] Where 班別='" + shift + "' And" +
                                       " 起始時間 Between '" + start + "' And '" + end + "' Or 班別='" + shift + "' And" +
                                       " 結束時間 Between '" + start + "' And '" + end + "'";
                    DataTable timetable_start = SqlCRUD.SqlQuery(timeCheck_start, "mdb");

                    if (timetable_start.Rows.Count > 0)
                    {
                        MessageBox.Show("設定區間涵蓋下表設定範圍", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    //判斷4:新增的值是否在下表設定範圍之中
                    DateTime shiftstart = Convert.ToDateTime(table.Rows[i][0]);
                    DateTime shiftend = Convert.ToDateTime(table.Rows[i][1]);

                    double shiftStart = dt3.TimeOfDay.CompareTo(shiftstart.TimeOfDay);
                    double shiftEnd = dt4.TimeOfDay.CompareTo(shiftend.TimeOfDay);

                    if (shiftStart > 0 && shiftEnd < 0)
                    {
                        MessageBox.Show("設定區間在下表第 " + (i + 1) + " 列範圍內", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                double result = TimeDiff(TimePicker_3.Value, TimePicker_4.Value);

                string insertQuery = "Insert into [休息時間設定] (班別,起始時間,結束時間,小計) values " +
                        "('" + shift + "','" + start + "','" + end + "','" + result + "')";
                new SqlCRUD(insertQuery, "mdb");

                MessageBox.Show("新增完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                getData(cboX.Text);

                //Step3: 自動運算
                string countQuery = "Select Sum(小計) as total From [休息時間設定] Where 班別='" + cboX.Text + "'";
                DataTable countTable = SqlCRUD.SqlQuery(countQuery, "mdb");

                lblTotal.Text = countTable.Rows[0][0].ToString();
                txtRestTime.Text = countTable.Rows[0][0].ToString();
                countTable.Clear();

                double result2 = TimeDiff(TimePicker_1.Value, TimePicker_2.Value);
                txtWorkTime.Text = Math.Abs(result2).ToString();

                double x = (Math.Abs(result2) - Convert.ToDouble(txtRestTime.Text)) / 60;
                double Result = Math.Round(x, 2);
                lblSum.Text = Result.ToString();

                //更新班別時間設定工作時數欄位
                string updateQuery = "Update [班別時間設定] Set 工作時數='" + Result + "' Where 班別='" + cboX.Text + "'";
                new SqlCRUD(updateQuery, "mdb");
            }
        }

        private void BtnAdd2_Click(object sender, EventArgs e)
        {
            string shift = cboY.Text;
            string start = TimePicker_7.Text;
            string end = TimePicker_8.Text;

            DateTime dt = Convert.ToDateTime("00:00");
            DateTime dt1 = Convert.ToDateTime(TimePicker_5.Text);
            DateTime dt2 = Convert.ToDateTime(TimePicker_6.Text);
            DateTime dt3 = Convert.ToDateTime(TimePicker_7.Text);
            DateTime dt4 = Convert.ToDateTime(TimePicker_8.Text);

            int t1 = dt1.TimeOfDay.CompareTo(dt3.TimeOfDay);
            int t2 = dt2.TimeOfDay.CompareTo(dt4.TimeOfDay);
            int t3 = dt3.TimeOfDay.CompareTo(dt.TimeOfDay);
            int t4 = dt4.TimeOfDay.CompareTo(dt.TimeOfDay);
            int t5 = dt3.TimeOfDay.CompareTo(dt4.TimeOfDay);

            if (MessageBox.Show("是否新增此筆休息時間?" + "\r\n" + "休息時間起始:" + Convert.ToDateTime(TimePicker_7.Text).ToString("HH:mm") + "\r\n" + "休息時間結束:" +
                Convert.ToDateTime(TimePicker_8.Text).ToString("HH:mm"), "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //判斷1:休息時間必須在班別區間內
                if (chk.Checked)
                {   //跨日班別
                    MessageBox.Show("尚未編寫跨班休息時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
                else
                {   //非跨日班別              
                    if (t1 > 0 || t2 < 0)
                    {
                        MessageBox.Show("設定時間不在班別區間內", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    //判斷2:結束時間是否大於開始時間
                    if (t5 >= 0)
                    {
                        MessageBox.Show("結束時間不可小於或等於起始時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                //判斷3:新增的值是否涵蓋下表設定範圍
                string searchQuery = "Select 起始時間,結束時間 From [休息時間設定] Where 班別='" + shift + "'";
                DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string timeCheck_start = "Select 起始時間 From [休息時間設定] Where 班別='" + shift + "' And" +
                                       " 起始時間 Between '" + start + "' And '" + end + "' Or 班別='" + shift + "' And" +
                                       " 結束時間 Between '" + start + "' And '" + end + "'";
                    DataTable timetable_start = SqlCRUD.SqlQuery(timeCheck_start, "mdb");

                    if (timetable_start.Rows.Count > 0)
                    {
                        MessageBox.Show("設定區間涵蓋下表設定範圍", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    //判斷4:新增的值是否在下表設定範圍之中
                    DateTime shiftstart = Convert.ToDateTime(table.Rows[i][0]);
                    DateTime shiftend = Convert.ToDateTime(table.Rows[i][1]);

                    double shiftStart = dt3.TimeOfDay.CompareTo(shiftstart.TimeOfDay);
                    double shiftEnd = dt4.TimeOfDay.CompareTo(shiftend.TimeOfDay);

                    if (shiftStart > 0 && shiftEnd < 0)
                    {
                        MessageBox.Show("設定區間在下表第 " + (i + 1) + " 列範圍內", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                double result = TimeDiff(TimePicker_7.Value, TimePicker_8.Value);

                string insertQuery = "Insert into [休息時間設定] (班別,起始時間,結束時間,小計) values " +
                        "('" + shift + "','" + start + "','" + end + "','" + result + "')";
                new SqlCRUD(insertQuery, "mdb");

                MessageBox.Show("新增完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                getData2(cboY.Text);

                //Step3: 自動運算
                string countQuery = "Select Sum(小計) as total From [休息時間設定] Where 班別='" + cboY.Text + "'";
                DataTable countTable = SqlCRUD.SqlQuery(countQuery, "mdb");

                lblTotal2.Text = countTable.Rows[0][0].ToString();
                txtRestTime2.Text = countTable.Rows[0][0].ToString();
                countTable.Clear();

                double result2 = TimeDiff(TimePicker_5.Value, TimePicker_6.Value);
                txtWorkTime2.Text = Math.Abs(result2).ToString();

                double x = (Math.Abs(result2) - Convert.ToDouble(txtRestTime2.Text)) / 60;
                double Result = Math.Round(x, 2);
                lblSum2.Text = Result.ToString();

                //更新班別時間設定工作時數欄位
                string updateQuery = "Update [班別時間設定] Set 工作時數='" + Result + "' Where 班別='" + cboY.Text + "'";
                new SqlCRUD(updateQuery, "mdb");
            }
        }
        //Step2:休息時間設定結束----------------------------------


        //時間運算method
        public double TimeDiff(DateTime start, DateTime end)
        {
            TimeSpan span = end - start;
            double totalMinutes = span.TotalMinutes;
            return totalMinutes;
        }

        
        //班別選擇下拉式選單(新增班別)
        private void cboX_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchQuery = "Select * From [班別時間設定] Where 班別='" + cboX.Text + "' And 工作時數 > 0";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");
            Dt.Rows.Clear();
            lblTotal.Text = "0";
            txtRestTime.Text = "0";

            if (table.Rows.Count != 0)
            {
                TimePicker_1.Text = table.Rows[0][2].ToString();
                TimePicker_2.Text = table.Rows[0][3].ToString();

                getData(cboX.Text);

                ////Step3: 自動運算
                string countQuery = "Select Sum(小計) as total From [休息時間設定] Where 班別='" + cboX.Text + "'";
                DataTable countTable = SqlCRUD.SqlQuery(countQuery, "mdb");

                if (countTable.Rows[0][0].ToString() != "")
                {
                    lblTotal.Text = countTable.Rows[0][0].ToString();
                    txtRestTime.Text = countTable.Rows[0][0].ToString();
                    countTable.Clear();
                }
                
                double result = TimeDiff(TimePicker_1.Value, TimePicker_2.Value);
                txtWorkTime.Text = Math.Abs(result).ToString();

                double x = (Math.Abs(result) - Convert.ToDouble(txtRestTime.Text)) / 60;
                double Result = Math.Round(x, 2);
                lblSum.Text = Result.ToString();
            }
            else
            {              
                getReset();
            }

            groupBox1.Visible = true;
            groupBox2.Visible = true;
            groupBox3.Visible = true;
            table.Clear();
        }

        //班別選擇下拉式選單(更新班別)
        private void cboY_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchQuery = "Select * From [班別時間設定] Where 班別='" + cboY.Text + "' And 工作時數 > 0";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");
            Dtt.Rows.Clear();
            lblTotal2.Text = "0";
            txtRestTime2.Text = "0";

            if (table.Rows.Count != 0)
            {
                TimePicker_5.Text = table.Rows[0][2].ToString();
                TimePicker_6.Text = table.Rows[0][3].ToString();

                if (table.Rows[0][5].ToString() == "true")
                    chk.Checked = true;

                getData2(cboY.Text);

                //Step3: 自動運算
                string countQuery = "Select Sum(小計) as total From [休息時間設定] Where 班別='" + cboY.Text + "'";
                DataTable countTable = SqlCRUD.SqlQuery(countQuery, "mdb");

                if (countTable.Rows[0][0].ToString() != "")
                {
                    lblTotal2.Text = countTable.Rows[0][0].ToString();
                    txtRestTime2.Text = countTable.Rows[0][0].ToString();
                    countTable.Clear();
                }

                double result = TimeDiff(TimePicker_5.Value, TimePicker_6.Value);
                txtWorkTime2.Text = Math.Abs(result).ToString();

                double x = (Math.Abs(result) - Convert.ToDouble(txtRestTime2.Text)) / 60;
                double Result = Math.Round(x, 2);
                lblSum2.Text = Result.ToString();
            }
            else
            {
                getReset();
            }

            table.Clear();
        }


        //表格勾選(新增班別)
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

            BtnDelete.Visible = true;
        }

        //表格勾選(更新班別)
        private void Dtt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            for (int i = 0; i <= Dtt.RowCount - 1; i++)
            {
                if ((bool)(Dtt.Rows[i].Cells[0].Value = true))
                    if (i == index)
                        continue;
                    else
                        Dtt.Rows[i].Cells[0].Value = false;
            }

            BtnDelete2.Visible = true;
        }


        //新增班別資料帶入
        public void getData(string shift)
        {
            string settingQuery = "Select * From [休息時間設定] Where 班別='" + shift + "'";
            DataTable table_list = SqlCRUD.SqlQuery(settingQuery, "mdb");

            int rowCount = table_list.Rows.Count;
            int colCount = table_list.Columns.Count;

            if (rowCount != 0)
            {
                Dt.Rows.Clear();
                Dt.Rows.Add(rowCount);

                for (int i = 0; i < rowCount; i++)
                    for (int j = 1; j < colCount; j++)
                        Dt.Rows[i].Cells[j].Value = table_list.Rows[i][j];

            }

            Dt.ReadOnly = false;
            for (int i = 1; i < colCount; i++)
                Dt.Columns[i].ReadOnly = true;

        }

        //更新班別資料帶入
        public void getData2(string shift)
        {
            string settingQuery = "Select * From [休息時間設定] Where 班別='" + shift + "'";
            DataTable table_list = SqlCRUD.SqlQuery(settingQuery, "mdb");

            int rowCount = table_list.Rows.Count;
            int colCount = table_list.Columns.Count;

            if (rowCount != 0)
            {
                Dtt.Rows.Clear();
                Dtt.Rows.Add(rowCount);

                for (int i = 0; i < rowCount; i++)
                    for (int j = 1; j < colCount; j++)
                        Dtt.Rows[i].Cells[j].Value = table_list.Rows[i][j];

            }

            Dtt.ReadOnly = false;
            for (int i = 1; i < colCount; i++)
                Dtt.Columns[i].ReadOnly = true;

            groupBox4.Visible = true;
            groupBox5.Visible = true;
            groupBox6.Visible = true;
        }


        //刪除休息時間(新增班別)
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //首欄有打勾的列會進行資料刪除
            for (int i = 0; i <= Dt.Rows.Count - 1; i++)
            {
                if (Dt.Rows[i].Cells[0].Value != null)
                    if (Dt.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        if (MessageBox.Show("是否刪除此筆休息時間?" + "\r\n" + "休息時間起始:" + Convert.ToDateTime(Dt.Rows[i].Cells[2].Value).ToString("HH:mm") + "\r\n" + "休息時間結束:" +
                            Convert.ToDateTime(Dt.Rows[i].Cells[3].Value).ToString("HH:mm"), "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            string deleteQuery = "Delete From [休息時間設定] Where 班別='" + cboX.Text + "' And 起始時間='" +
                            Dt.Rows[i].Cells[2].Value.ToString() + "' And 結束時間='" + Dt.Rows[i].Cells[3].Value.ToString() + "'";
                            new SqlCRUD(deleteQuery, "mdb");

                            MessageBox.Show("刪除完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            cboX_SelectedIndexChanged(this, e);
                            BtnDelete.Visible = false;
                            return;
                        }
                        else
                            return;
                    }
            }
        }

        //刪除休息時間(更新班別)
        private void BtnDelete2_Click(object sender, EventArgs e)
        {
            //首欄有打勾的列會進行資料刪除
            for (int i = 0; i <= Dtt.Rows.Count - 1; i++)
            {
                if (Dtt.Rows[i].Cells[0].Value != null)
                    if (Dtt.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        if (MessageBox.Show("是否刪除此筆休息時間?" + "\r\n" + "休息時間起始:" + Convert.ToDateTime(Dtt.Rows[i].Cells[2].Value).ToString("HH:mm") + "\r\n" + "休息時間結束:" +
                            Convert.ToDateTime(Dtt.Rows[i].Cells[3].Value).ToString("HH:mm"), "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            string deleteQuery = "Delete From [休息時間設定] Where 班別='" + cboY.Text + "' And 起始時間='" +
                            Dtt.Rows[i].Cells[2].Value.ToString() + "' And 結束時間='" + Dtt.Rows[i].Cells[3].Value.ToString() + "'";
                            new SqlCRUD(deleteQuery, "mdb");

                            MessageBox.Show("刪除完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            cboY_SelectedIndexChanged(this, e);
                            BtnDelete2.Visible = false;
                            return;
                        }
                        else
                            return;
                    }
            }
        }


        //索引書籤切換
        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])        //新增班別
            {
                getReset();

                //更新班別下拉式選單內容
                OleDbConnection oleCon = SqlCRUD.OleRead("mdb");
                string readQuery = "Select 班別 From [班別時間設定] Where 工作時數 = 0 Order by 班別";
                OleDbCommand oleCmd = new OleDbCommand(readQuery, oleCon);
                oleCon.Open();
                OleDbDataReader oleReader = oleCmd.ExecuteReader();
                cboX.Items.Clear();

                while (oleReader.Read())
                    cboX.Items.Add(oleReader.GetString(0));

                oleReader.Close();
                oleCon.Close();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])   //更新班別
            {
                getReset();

                //更新班別下拉式選單內容
                OleDbConnection oleCon = SqlCRUD.OleRead("mdb");
                string readQuery = "Select 班別 From [班別時間設定] Where 工作時數 > 0 Order by 班別";
                OleDbCommand oleCmd = new OleDbCommand(readQuery, oleCon);
                oleCon.Open();
                OleDbDataReader oleReader = oleCmd.ExecuteReader();
                cboY.Items.Clear();

                while (oleReader.Read())
                    cboY.Items.Add(oleReader.GetString(0));

                oleReader.Close();
                oleCon.Close();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage3"])   //刪除資料
            {
                string searchQuery = "Select * From [班別時間設定] Where 工作時數 > 0 Order by 班別";
                DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

                TbSearch.Text = "";
                Dt1.DataSource = table;
                Dt1.Columns[1].Visible = false;
                Dt1.Columns[6].Visible = false;
                Dt1.Columns[2].Width = 70;

                Dt1.Columns["上班時間"].DefaultCellStyle.Format = "HH:mm";
                Dt1.Columns["下班時間"].DefaultCellStyle.Format = "HH:mm";

                //除首欄，其他欄位設定為Readonly
                Dt1.ReadOnly = false;
                for (int i = 1; i < Dt1.ColumnCount; i++)
                {
                    Dt1.Columns[i].ReadOnly = true;
                }
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])   //查詢班別
            {
                string searchQuery = "Select * From [班別時間設定] Where 工作時數 > 0 Order by 班別";
                DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

                txtSearch.Text = "";
                Dt2.DataSource = table;
                Dt2.Columns[1].Visible = false;
                Dt2.Columns[6].Visible = false;
                Dt2.Columns[2].Width = 70;

                Dt2.Columns["上班時間"].DefaultCellStyle.Format = "HH:mm";
                Dt2.Columns["下班時間"].DefaultCellStyle.Format = "HH:mm";

                //除首欄，其他欄位設定為Readonly
                Dt2.ReadOnly = false;
                for (int i = 1; i < Dt2.ColumnCount; i++)
                {
                    Dt2.Columns[i].ReadOnly = true;
                }
            }
        }

        //資料篩選(刪除資料頁面)
        public void Filter(string keyword)
        {
            string searchQuery = "Select * From [班別時間設定] WHERE 班別 Like '%" + keyword + "%' Or 上班時間 Like '%" + keyword +
                "%' Or 下班時間 Like '%" + keyword + "%' Or 工作時數 Like '%" + keyword + "%'";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            Dt1.DataSource = table;
        }

        //資料篩選(刪除資料頁面)
        public void Filter2(string keyword)
        {
            string searchQuery = "Select * From [班別時間設定] WHERE 班別 Like '%" + keyword + "%' Or 上班時間 Like '%" + keyword +
                "%' Or 下班時間 Like '%" + keyword + "%' Or 工作時數 Like '%" + keyword + "%'";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            Dt2.DataSource = table;
        }


        //關鍵字搜尋(刪除班別)
        private void TbSearch_TextChanged(object sender, EventArgs e)
        {
            Filter(TbSearch.Text);
        }

        //關鍵字搜尋(查詢班別)
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Filter2(txtSearch.Text);
        }


        //Dt1表格首欄僅允許勾選一個
        private void Dt1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            for (int i = 0; i <= Dt1.RowCount - 1; i++)
            {
                if ((bool)(Dt1.Rows[i].Cells[0].Value = true))
                    if (i == index)
                        continue;
                    else
                        Dt1.Rows[i].Cells[0].Value = false;
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


        //刪除班別資料
        private void BtnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Dt1.Rows.Count - 1; i++)
            {
                if (Dt1.Rows[i].Cells[0].Value != null)
                    if (Dt1.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        string shift = Dt1.Rows[i].Cells[2].Value.ToString();
                        string first = Convert.ToDateTime(Dt1.Rows[i].Cells[3].Value).ToString("HH:mm");
                        string end = Convert.ToDateTime(Dt1.Rows[i].Cells[4].Value).ToString("HH:mm");

                        if (MessageBox.Show("是否刪除以下此筆資料?" + "\r\n" + "班別代號:" + shift + "\r\n" + "上班時間:" + first +
                            "\r\n" + "下班時間:" + end, "System", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            string updateQuery = "Update [班別時間設定] Set 上班時間='00:00',下班時間='00:00',工作時數='0',跨日班別='' Where 班別='" + shift + "'";
                            new SqlCRUD(updateQuery, "mdb");

                            string deleteQuery = "Delete From [休息時間設定] Where 班別='" + shift + "'";
                            new SqlCRUD(deleteQuery, "mdb");

                            MessageBox.Show("刪除完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            tabControl1_SelectedIndexChanged_1(this, e);
                            TbSearch.Text = "";
                        }

                    }
            }
        }

        //查詢班別資料(更新勾選資料)
        private void BtnUpdating_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Dt2.Rows.Count - 1; i++)
            {
                if (Dt2.Rows[i].Cells[0].Value != null)
                    if (Dt2.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        string shift = Dt2.Rows[i].Cells[2].Value.ToString();

                        tabControl1.SelectedTab = tabControl1.TabPages["tabPage2"];
                        cboY.Text = shift;
                        cboY_SelectedIndexChanged(this, e);
                    }
            }
        }


    }
}
