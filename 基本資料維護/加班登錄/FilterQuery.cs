using System;
using System.Data;
using System.Windows.Forms;

namespace Attendance
{
    public partial class FilterQuery : Form
    {
        OverTime grid;
        DateTime date;

        FilterQuery() { }

        public FilterQuery(OverTime grid, DateTime date)
        {
            InitializeComponent();
            this.grid = grid;
            this.date = date;

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
                        int r = grid.Dt_1.Rows.Count - 1;
                        string empID = Dt.Rows[i].Cells[1].Value.ToString();
                        string name = Dt.Rows[i].Cells[2].Value.ToString();
                        bool b = true;

                        //重複加入防呆設定
                        for (int k = 0; k <= r; k++)
                        {
                            string dataQuery = "Select 員工編號 From [加班時數登錄] Where 員工編號='" + empID + "' And 加班日期='" + grid.OTdate.Text + "'";
                            DataTable dataTable = SqlCRUD.SqlQuery(dataQuery, "mdb");

                            if (dataTable.Rows.Count > 0)
                            {
                                MessageBox.Show("員工編號:" + empID + "\r\n" + "員工姓名:" + name + "\r\n" + "日期:" + date.ToString("yyyy/MM/dd") +
                                        "\r\n" + "已存在加班資料", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                                b = false;
                                break;
                            }

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
