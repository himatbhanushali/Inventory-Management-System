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
    public partial class DeleteItem : Form
    {
        public DeleteItem()
        {
            InitializeComponent();
        }

        private void DeleteItem_Load(object sender, EventArgs e)
        {
            string strProvider = Utility.con;
            string strSql = "Select distinct Series from Item";
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string strProvider = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql = "Select Code from Item where Series='" + comboBox1.SelectedItem.ToString() + "'";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox2.Items.Add(reader[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
            {
                String series = comboBox1.SelectedItem.ToString();
                String code = comboBox2.SelectedItem.ToString();

                OleDbConnection cnon = new System.Data.OleDb.OleDbConnection();
                cnon.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.CommandText = "DELETE FROM Item where Series='" + series + "' and Code = '" + code + "'";
                cnon.Open();
                command.Connection = cnon;
                command.ExecuteNonQuery();

                MessageBox.Show("Item Deleted");
                cnon.Close();

                comboBox1.Text = "";
                comboBox1.Items.Clear();
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                DeleteItem_Load(sender, e);
            }
            else {

                MessageBox.Show("Please select valid Item to delete");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
