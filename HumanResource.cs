using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Attendance
{
    public partial class HumanResource : Form
    {
        public HumanResource()
        {
            InitializeComponent();
            string query = "Select * From [員工基本資料]";
            DataTable table = SqlCRUD.SqlQuery(query, "mtb");
            Dt.DataSource = table;
        }


    }
}
