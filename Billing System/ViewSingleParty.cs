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
    public partial class ViewSingleParty : Form
    {
        public ViewSingleParty()
        {
            InitializeComponent();
        }

        private void ViewSingleParty_Load(object sender, EventArgs e)
        {
            String pname = PreviewSingleParty.pname;

            string strProvider = Utility.con;
            string strSql = "Select * from PartyDetails where Party_Name='" + pname+"'";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                label2.Text = reader[0].ToString();
                label4.Text = reader[1].ToString() + " / " + reader[2].ToString();
                label6.Text = reader[3].ToString();
                label8.Text = reader[4].ToString();
                label10.Text = reader[5].ToString();
                label12.Text = reader[6].ToString();
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
