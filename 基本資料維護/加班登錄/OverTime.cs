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
    public partial class OverTime : Form
    {
        public OverTime()
        {
            InitializeComponent();
<<<<<<< HEAD
            OTdate.Format = DateTimePickerFormat.Custom;

            //欄位1~6、9設定為Readonly
            Dt_1.ReadOnly = false;
            for (int i = 0; i <= 6; i++)
            {
                Dt_1.Columns[i].ReadOnly = true;
            }
            Dt_1.Columns[9].ReadOnly = true;
=======
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
>>>>>>> 5c66bde0f1701b8080dad176d8df9347aa0cef95
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            FilterQuery f = new FilterQuery(this,OTdate.Value);
            f.ShowDialog();
        }

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

        private void Dt_1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                int x = Dt_1.CurrentCell.ColumnIndex;
                int y = Dt_1.CurrentCell.RowIndex;

                if (x == 8 && Dt_1.Rows[y].Cells[7].Value == null)
                {
                    MessageBox.Show("請先填入加班起始時間", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    Dt_1.CurrentCell.Value = "";
                    return;
                }

                DateTime dt1 = Convert.ToDateTime(Dt_1.Rows[y].Cells[4].Value.ToString());
                DateTime dt2 = Convert.ToDateTime(Dt_1.Rows[y].Cells[5].Value.ToString());
                string val = Dt_1.CurrentCell.Value.ToString();
                string[] str = val.Split(':');

                if (val.Length == 0)
                {
                    MessageBox.Show("未輸入任何資料", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (str.Length != 2)
                {
                    MessageBox.Show(val + " 非標準時間格式" + "\r\n" + "範例: 17:30(24小時制)", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Dt_1.CurrentCell.Value = "";
                    return;
                }
                else if (str[0].Length != 2 || str[1].Length != 2)
                {
                    MessageBox.Show(val + " 非標準時間格式" + "\r\n" + "範例: 17:30(24小時制)","System",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    Dt_1.CurrentCell.Value = "";
                    return;
                }
                

                DateTime dt3 = Convert.ToDateTime(val);
                int t1 = dt1.TimeOfDay.CompareTo(dt3.TimeOfDay);
                int t2 = dt2.TimeOfDay.CompareTo(dt3.TimeOfDay);

                if (x == 7)
                {
                    if (t2 > 0)
                    {
                        MessageBox.Show("加班時間必須在下班時間後", "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        Dt_1.CurrentCell.Value = "";
                        return;
                    }
                }
                else if (x == 8)
                {
                    DateTime dt4 = Convert.ToDateTime(Dt_1.Rows[y].Cells[7].Value);

                }
                
            }
        }


=======
            FilterQuery f = new FilterQuery(this);
            f.ShowDialog();
        }
>>>>>>> 5c66bde0f1701b8080dad176d8df9347aa0cef95
    }
}
