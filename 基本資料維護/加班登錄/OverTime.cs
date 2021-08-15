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
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            FilterQuery f = new FilterQuery(this);
            f.ShowDialog();
        }
    }
}
