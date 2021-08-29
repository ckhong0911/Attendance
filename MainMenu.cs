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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void 員工基本資料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployDataCreate f = new EmployDataCreate();
            f.ShowDialog();
        }

        private void 行事曆維護ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calendar f = new Calendar();
            f.ShowDialog();
        }

        private void 排班設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkShiftSetting f = new WorkShiftSetting();
            f.ShowDialog();
        }

        private void 加班登錄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OverTime f = new OverTime();
            f.ShowDialog();
        }

        private void 請假登錄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeOff f = new TakeOff();
            f.ShowDialog();
        }

        //private void BtnWorkShiftSetting_Click(object sender, EventArgs e)
        //{
        //    WorkShiftSetting f = new WorkShiftSetting();
        //    f.Show();         
        //}
    }
}
