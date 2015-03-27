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
    public partial class ViewFullParty : Form
    {
        public ViewFullParty()
        {
            InitializeComponent();
        }

        private void ViewFullParty_Load(object sender, EventArgs e)
        {
            String pname = ViewPurchaseByParty.pname;

            string strProvider = Utility.con;
            string strSql = "Select * from Purchase_Bill where Party_Name='"+pname+"'";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable scores = new DataTable();
            da.Fill(scores);
            dataGridView1.DataSource = scores;

        }
    }
}
