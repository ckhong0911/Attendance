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
    public partial class ShiftChoose : Form
    {
        EmployDataCreate employ;
        int flag;

        ShiftChoose() { }

        public ShiftChoose(EmployDataCreate employ,int flag)
        {
            InitializeComponent();
            this.employ = employ;
            this.flag = flag;

            string searchQuery = "Select 班別,上班時間,下班時間 From [班別時間設定] Where 工作時數 > 0";
            DataTable table = SqlCRUD.SqlQuery(searchQuery, "mdb");

            Dt.DataSource = table;
            Dt.Columns[1].Width = 80;
            Dt.Columns[2].Width = 120;
            Dt.Columns[3].Width = 120;

            Dt.Columns["上班時間"].DefaultCellStyle.Format = "HH:mm";
            Dt.Columns["下班時間"].DefaultCellStyle.Format = "HH:mm";


            //除首欄，其他欄位設定為Readonly
            Dt.ReadOnly = false;
            for (int i = 1; i < Dt.ColumnCount; i++)
            {
                Dt.Columns[i].ReadOnly = true;
            }
        }

        private void Dt_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //僅允許勾選一個
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

        private void BtnOK_Click(object sender, EventArgs e)
        {
            //勾選判斷式
            for (int i = 0; i < Dt.RowCount; i++)
            {
                if (Dt.Rows[i].Cells[0].Value != null)
                    if (Dt.Rows[i].Cells[0].Value.Equals("Yes"))
                    {
                        if (flag == 1)
                            employ.txtShift.Text = Dt.Rows[i].Cells[1].Value.ToString();
                        else
                            employ.txtShiftUpdate.Text = Dt.Rows[i].Cells[1].Value.ToString();

                        Close();
                    }
            }
        }
    }
}
