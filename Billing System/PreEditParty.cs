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
    public partial class PreEditParty : Form
    {
        public static string pname;
        public PreEditParty()
        {
            InitializeComponent();
        }

        private void PreEditParty_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                pname = comboBox1.SelectedItem.ToString();
                EditParty e1 = new EditParty();
                e1.MdiParent = this.MdiParent;
                e1.Show();
                this.Dispose();
            }
            else {
                MessageBox.Show("Please Select Party Name");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
