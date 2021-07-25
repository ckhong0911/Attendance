﻿using System;
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
            f.Show();
        }

        private void 行事曆維護ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calendar f = new Calendar();
            f.Show();
        }

        //private void BtnWorkShiftSetting_Click(object sender, EventArgs e)
        //{
        //    WorkShiftSetting f = new WorkShiftSetting();
        //    f.Show();         
        //}
    }
}
