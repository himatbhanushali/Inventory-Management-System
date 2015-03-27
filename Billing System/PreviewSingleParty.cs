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
    public partial class PreviewSingleParty : Form
    {
       
        public static string pname;
        public PreviewSingleParty()
        {
            InitializeComponent();
        }

        private void PreviewSingleParty_Load(object sender, EventArgs e)
        {
            string strProvider = Utility.con;
            string strSql = "Select * from PartyDetails";
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                pname = comboBox1.SelectedItem.ToString();
                ViewSingleParty v = new ViewSingleParty();
                v.MdiParent = this.MdiParent;
                v.Show();
                this.Dispose();
            }
            else {
                MessageBox.Show("Please select Party Name");
            }
        }
    }
}
