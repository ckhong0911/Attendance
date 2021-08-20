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
<<<<<<< HEAD
        DateTime date;

        FilterQuery() { }

        public FilterQuery(OverTime grid, DateTime date)
        {
            InitializeComponent();
            this.grid = grid;
            this.date = date;
=======

        FilterQuery() { }

        public FilterQuery(OverTime grid)
        {
            InitializeComponent();
            this.grid = grid;
>>>>>>> 5c66bde0f1701b8080dad176d8df9347aa0cef95

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
<<<<<<< HEAD
                        int r = grid.Dt_1.Rows.Count - 1;
                        string empID = Dt.Rows[i].Cells[1].Value.ToString();
                        string name = Dt.Rows[i].Cells[2].Value.ToString();
                        bool b = true;

                        //重複加入防呆設定
                        for (int k = 0; k <= r; k++)
                        {
                            if (grid.Dt_1.Rows[k].Cells[1].Value.ToString() == empID)
                            {
                                if (grid.Dt_1.Rows[k].Cells[6].Value.ToString() == date.ToString("yyyy/MM/dd"))
                                {
                                    MessageBox.Show("員工編號:" + empID + "\r\n" + "員工姓名:" + name + "\r\n" +  "日期:" + date.ToString("yyyy/MM/dd") +
                                        "\r\n" + "已存在清單中", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                    b = false;
                                    break;
                                }
                            }
                        }

                        if (b)
                        {
                            grid.Dt_1.Rows.Add();

                            //關聯資料表
                            string searchQuery = "Select a.員工編號,a.員工姓名,b.班別,b.上班時間,b.下班時間 From [員工基本資料] as a, [班別時間設定] as b " +
                                "Where a.班別 = b.班別 And 員工編號='" + empID + "'";
                            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

                            for (int j = 1; j <= 5; j++)
                                grid.Dt_1.Rows[grid.Dt_1.Rows.Count - 1].Cells[j].Value = table.Rows[0][j - 1];

                            grid.Dt_1.Rows[grid.Dt_1.Rows.Count - 1].Cells[6].Value = date.ToString("yyyy/MM/dd");
                        }
 
                    }
                }
            }

            grid.Dt_1.Columns["Column6"].DefaultCellStyle.Format = "HH:mm";
            grid.Dt_1.Columns["Column7"].DefaultCellStyle.Format = "HH:mm";
            Close();
=======
                        grid.Dt_1.Rows.Add();
                        int r = grid.Dt_1.Rows.Count - 1;
                        string empID = Dt.Rows[i].Cells[1].Value.ToString();

                        //2021/8/14 陳sir 要先做特休
                        //string searchQuery = "Select a.員工編號,a.員工姓名,b.起始時間,b.結束時間 From [員工基本資料] as a, 班別設定資料 as b Where 員工編號='" + empID + "'";
                    }
                }

                if (i == Dt.Rows.Count - 1) Close();
            }
>>>>>>> 5c66bde0f1701b8080dad176d8df9347aa0cef95
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
