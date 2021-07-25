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
    public partial class Calendar : Form
    {
        public Calendar()
        {
            InitializeComponent();

            GetDateTime dt = new GetDateTime();
            getData(dt.getYear(), dt.getMonth());

            cbYear.Text = dt.getYear();
            cbMonth.Text = dt.getMonth();

            //除首欄，其他欄位設定為Readonly
            Dt.ReadOnly = false;
            for (int i = 1; i < Dt.ColumnCount; i++)
            {
                Dt.Columns[i].ReadOnly = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string[] arr = { cbYear.Text, cbMonth.Text, TbDay.Text };
            string dateTime = arr[0] + arr[1] + arr[2];
            int flag = 0;

            if (Rb1.Checked == true)
                flag = 2;

            string updateQuery = "Update [" + arr[0] + "行事曆] Set 旗標='" + flag + "',備註='" + TbRemark.Text + "' Where 西元日期='" + dateTime + "'";
            new SqlCRUD(updateQuery, "mdb");

            MessageBox.Show("儲存完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            BtnQuery_Click(this, e);
            Rb1.Checked = false;
            Rb2.Checked = false;
            TbRemark.Text = "";
        }

        public new string Right(string str, int length)
        {   //method
            string result = str.Substring(str.Length - length, length);
            return result;
        }

        public void getData(string year, string month)
        {
            string displayQuery = "Select * From [" + year + "行事曆] Where 西元日期 Like '" + year + month + "%' Order by 西元日期";
            DataTable table = SqlCRUD.SqlQuery(displayQuery, "mdb");

            if (table.Rows.Count > 0)
            {
                Dt.Rows.Add(table.Rows.Count);

                for (int i = 0; i <= table.Rows.Count - 1; i++)
                {
                    Dt.Rows[i].Cells[1].Value = Right(table.Rows[i][0].ToString(), 2);
                    Dt.Rows[i].Cells[2].Value = table.Rows[i][1].ToString();

                    if (table.Rows[i][2].ToString() == "0")
                        Dt.Rows[i].Cells[3].Value = "否";
                    else if (table.Rows[i][2].ToString() == "2")
                    {
                        Dt.Rows[i].Cells[3].Value = "是";
                        Dt.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                    }                       
                    else
                        Dt.Rows[i].Cells[3].Value = "未定義";

                    Dt.Rows[i].Cells[4].Value = table.Rows[i][3].ToString();
                }
            }

            table.Rows.Clear();
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

            TbRemark.Text = Dt.Rows[index].Cells[4].Value.ToString();
            TbDay.Text = Dt.Rows[index].Cells[1].Value.ToString();

            if (Dt.Rows[index].Cells[3].Value.ToString() == "是")
                Rb1.Checked = true;
            else
                Rb2.Checked = true;
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ExecuteQuery();
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ExecuteQuery();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            Dt.Rows.Clear();

            string year = cbYear.Text;
            string month = cbMonth.Text;

            if (month.Length == 1)
                month = "0" + month;

            GetDateTime dt = new GetDateTime();
            getData(year, month);

            Rb1.Checked = false;
            Rb2.Checked = false;
            TbRemark.Text = "";
        }
    }
}
