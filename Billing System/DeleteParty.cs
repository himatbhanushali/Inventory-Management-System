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
    public partial class DeleteParty : Form
    {
        public DeleteParty()
        {
            InitializeComponent();
        }

        private void DeleteParty_Load(object sender, EventArgs e)
        {
            string strProvider = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
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
                String pname = comboBox1.SelectedItem.ToString();
                OleDbConnection cnon = new System.Data.OleDb.OleDbConnection();
                cnon.ConnectionString = Utility.con;
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.CommandText = "DELETE FROM PartyDetails where Party_Name='" + pname + "'";
                cnon.Open();
                command.Connection = cnon;
                command.ExecuteNonQuery();

                MessageBox.Show("Party Deleted");
                cnon.Close();

                comboBox1.Text = "";
                comboBox1.Items.Clear();
                DeleteParty_Load(sender, e);
            }
            else {
                MessageBox.Show("Please select Party Name");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
