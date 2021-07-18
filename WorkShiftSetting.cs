﻿using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Attendance
{
    public partial class WorkShiftSetting : Form
    {
        public WorkShiftSetting()
        {
            InitializeComponent();
            getReset();
        }

        public void getReset()
        {
            TimePicker_1.Format = DateTimePickerFormat.Custom;
            TimePicker_2.Format = DateTimePickerFormat.Custom;
            TimePicker_3.Format = DateTimePickerFormat.Custom;
            TimePicker_4.Format = DateTimePickerFormat.Custom;

            TimePicker_1.Value = Convert.ToDateTime("00:00");
            TimePicker_2.Value = Convert.ToDateTime("00:00");
            TimePicker_3.Value = Convert.ToDateTime("00:00");
            TimePicker_4.Value = Convert.ToDateTime("00:00");

            groupBox2.Visible = false;
            groupBox3.Visible = false;
            BtnOK_1.Visible = true;
            BtnReset.Visible = false;
            txtWorkTime.Text = "0";
            txtRestTime.Text = "0";
            lblSum.Text = "0";
        }

        //Step1:上下班時間設定起始--------------------------------
        private void BtnOK_1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(TimePicker_1.Text);
            DateTime dt2 = Convert.ToDateTime(TimePicker_2.Text);
            int t = dt1.TimeOfDay.CompareTo(dt2.TimeOfDay);

            //判斷1:結束時間是否大於開始時間
            if (t >= 0)
            {
                MessageBox.Show("結束時間不可大於或等於起始時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
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
            txtWorkTime.Text = result.ToString();

            double x = (result - Convert.ToDouble(txtRestTime.Text)) / 60;
            double Result = Math.Round(x,2);
            lblSum.Text = Result.ToString();

            //儲存上下班時間設定
            string addQuery = "Select * From [班別時間設定] Where 班別='" + cboX.Text + "' And 工作時數 = 0";
            DataTable tableData = SqlCRUD.SqlQuery(addQuery, "mdb");

            if (tableData.Rows.Count == 1)
            {
                string insertQuery = "Update [班別時間設定] Set 上班時間='" + TimePicker_1.Text + "',下班時間='" +
                    TimePicker_2.Text + "',工作時數='" + lblSum.Text + "' Where 班別='" + cboX.Text + "'";

                new SqlCRUD(insertQuery, "mdb");
                tableData.Clear();
            }

            groupBox2.Visible = true;
            groupBox3.Visible = true;
        }
        //Step1:上下班時間設定結束--------------------------------

        //Step2:休息時間設定開始----------------------------------
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string shift = cboX.Text;
            string start = TimePicker_3.Text;
            string end = TimePicker_4.Text;
            DateTime dt1 = Convert.ToDateTime(TimePicker_1.Text);
            DateTime dt2 = Convert.ToDateTime(TimePicker_2.Text);
            DateTime dt3 = Convert.ToDateTime(TimePicker_3.Text);
            DateTime dt4 = Convert.ToDateTime(TimePicker_4.Text);

            //判斷1:休息時間必須在班別區間內
            int t = dt1.TimeOfDay.CompareTo(dt3.TimeOfDay);
            int t2 = dt2.TimeOfDay.CompareTo(dt4.TimeOfDay);
            if (t > 0 || t2 < 0)
            {
                MessageBox.Show("設定時間不在班別區間內", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            //判斷2:結束時間是否大於開始時間
            int tt = dt3.TimeOfDay.CompareTo(dt4.TimeOfDay);
            if (tt >= 0)
            {
                MessageBox.Show("結束時間不可小於或等於起始時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
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
            txtWorkTime.Text = result2.ToString();

            double x = (result2 - Convert.ToDouble(txtRestTime.Text)) / 60;
            double Result = Math.Round(x, 2);
            lblSum.Text = Result.ToString();

            //更新班別時間設定工作時數欄位
            string updateQuery = "Update [班別時間設定] Set 工作時數='" + Result + "' Where 班別='" + cboX.Text + "'";
            new SqlCRUD(updateQuery, "mdb");

        }
        //Step2:休息時間設定結束----------------------------------


        //時間運算method
        public double TimeDiff(DateTime start, DateTime end)
        {
            TimeSpan span = end - start;
            double totalMinutes = span.TotalMinutes;
            return totalMinutes;
        }

        private void cboX_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchQuery = "Select * From [班別時間設定] Where 班別='" + cboX.Text + "' And 工作時數 > 0";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");
            Dt.Rows.Clear();
            lblTotal.Text = "0";

            if (table.Rows.Count != 0)
            {
                TimePicker_1.Text = table.Rows[0][2].ToString();
                TimePicker_2.Text = table.Rows[0][3].ToString();
      
                getData(cboX.Text);

                BtnOK_1.Visible = false;
                groupBox2.Visible = true;
                groupBox3.Visible = true;
                BtnReset.Visible = true;

                ////Step3: 自動運算
                string countQuery = "Select Sum(小計) as total From [休息時間設定] Where 班別='" + cboX.Text + "'";
                DataTable countTable = SqlCRUD.SqlQuery(countQuery, "mdb");

                lblTotal.Text = countTable.Rows[0][0].ToString();
                txtRestTime.Text = countTable.Rows[0][0].ToString();
                countTable.Clear();

                double result = TimeDiff(TimePicker_1.Value, TimePicker_2.Value);
                txtWorkTime.Text = result.ToString();

                double x = (result - Convert.ToDouble(txtRestTime.Text)) / 60;
                double Result = Math.Round(x, 2);
                lblSum.Text = Result.ToString();
            }
            else
            {              
                getReset();
            }

            groupBox1.Visible = true;            
            table.Clear();
        }

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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //首欄有打勾的列會進行資料刪除
            for (int i = 0; i <= Dt.Rows.Count - 1; i++)
            {
                if (Dt.Rows[i].Cells[0].Value != null)
                    if (Dt.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        string deleteQuery = "Delete From [休息時間設定] Where 班別='" + cboX.Text + "' And 起始時間='" +
                            Dt.Rows[i].Cells[2].Value.ToString() + "' And 結束時間='" + Dt.Rows[i].Cells[3].Value.ToString() + "'";
                        new SqlCRUD(deleteQuery, "mdb");

                        MessageBox.Show("刪除完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        cboX_SelectedIndexChanged(this, e);
                        BtnDelete.Visible = false;
                        return;
                    }
            }
        }


    }
}