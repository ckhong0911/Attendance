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
    public partial class FilterQuery : Form
    {
        OverTime grid;

        FilterQuery() { }

        public FilterQuery(OverTime grid)
        {
            InitializeComponent();
            this.grid = grid;

            string searchQuery = $"Select 員工編號,員工姓名 From [員工基本資料]";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            Dt.DataSource = table;


            //除首欄，其他欄位設定為Readonly
            Dt.ReadOnly = false;
            for (int i = 1; i < Dt.ColumnCount; i++)
            {
                Dt.Columns[i].ReadOnly = true;
            }

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //尋找已勾選的單號，取出傳遞至新增加班
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                if (Dt.Rows[i].Cells[0].Value != null)
                {
                    if (Dt.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        grid.Dt_1.Rows.Add();
                        int r = grid.Dt_1.Rows.Count - 1;
                        string empID = Dt.Rows[i].Cells[1].Value.ToString();

                        //2021/8/14 陳sir 要先做特休
                        //string searchQuery = "Select a.員工編號,a.員工姓名,b.起始時間,b.結束時間 From [員工基本資料] as a, 班別設定資料 as b Where 員工編號='" + empID + "'";
                    }
                }

                if (i == Dt.Rows.Count - 1) Close();
            }
        }

        private void Tb_Search_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = "Select 員工編號,員工姓名 From [員工基本資料] Where 員工編號 Like '%" +
                Tb_Search.Text + "%' Or 員工姓名 Like '%" + Tb_Search.Text + "%'";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            Dt.DataSource = table;
        }
    }
}
