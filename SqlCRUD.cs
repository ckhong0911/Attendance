using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Attendance
{
    class SqlCRUD
    {
        private OleDbConnection oleCon;
        private OleDbCommand oleCmd;

        public SqlCRUD() { }
        public SqlCRUD(string query, string db)
        {
            try
            {
                if (db == "mtb")
                {
                    oleCon = new OleDbConnection("provider=Microsoft.ACE.Oledb.12.0;data source=AppData\\MyDataBase.accdb");
                }
                
                oleCmd = new OleDbCommand(query, oleCon);
                oleCon.Open();
                oleCmd.ExecuteNonQuery();
                oleCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                oleCon.Close();
            }
        }

        public static DataTable SqlQuery(string query, string db)
        {
            OleDbConnection oleCon = new OleDbConnection();
            try
            {
                if (db == "mtb")
                {
                    oleCon = new OleDbConnection("provider=Microsoft.ACE.Oledb.12.0;data source=AppData\\MyDataBase.accdb");
                }

                OleDbCommand oleCmd = new OleDbCommand(query, oleCon);
                OleDbDataAdapter oda = new OleDbDataAdapter(oleCmd);
                DataTable table = new DataTable();

                oda.Fill(table);
                oda.Dispose();
                return table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return null;
            }
        }
    }
}
