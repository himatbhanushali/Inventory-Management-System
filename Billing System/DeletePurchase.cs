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
    public partial class DeletePurchase : Form
    {
        public DeletePurchase()
        {
            InitializeComponent();
        }

        private void DeletePurchase_Load(object sender, EventArgs e)
        {
            string strProvider = Utility.con;
            string strSql = "Select * from Purchase_Bill";
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
                int billno = int.Parse(comboBox1.SelectedItem.ToString());
                OleDbConnection cnon = new System.Data.OleDb.OleDbConnection();
                cnon.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.CommandText = "DELETE FROM Purchase_Bill where Bill_No=" + billno;
                cnon.Open();
                command.Connection = cnon;
                command.ExecuteNonQuery();


                OleDbConnection cnon1 = new System.Data.OleDb.OleDbConnection();
                cnon1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
                System.Data.OleDb.OleDbCommand command1 = new System.Data.OleDb.OleDbCommand();
                command1.CommandText = "DELETE FROM Purchase_Inventory where Bill_No=" + billno;
                cnon1.Open();
                command1.Connection = cnon1;
                command1.ExecuteNonQuery();

                MessageBox.Show("Purchase Entry Deleted");
                cnon.Close();
                this.Dispose();
            }
        }
    }
}
