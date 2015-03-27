using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Billing_System
{
    public partial class PreEditSales : Form
    {
        public static int challanno;
        public PreEditSales()
        {
            InitializeComponent();
        }

        private void PreEditSales_Load(object sender, EventArgs e)
        {
            string strProvider = Utility.con;
            string strSql = "Select * from Bill";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            challanno = int.Parse(comboBox1.SelectedItem.ToString());

            EditSales s = new EditSales();
            s.MdiParent = this.MdiParent;
            s.Show();
            this.Dispose();

        }
    }
}
