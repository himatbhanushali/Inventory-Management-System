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
    public partial class EditItem : Form
    {
        public EditItem()
        {
            InitializeComponent();
        }

        private void EditItem_Load(object sender, EventArgs e)
        {
            String series = PreEditItem.series;
            String code = PreEditItem.code;

            string strProvider = Utility.con;
            string strSql = "Select * from Item where Series='" + series + "' and Code ='" + code + "'";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboBox1.SelectedItem = reader[0].ToString();
                textBox1.Text = reader[1].ToString();
                textBox2.Text = reader[2].ToString();


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Series for Item");
            }
            if (textBox1.Text == "" || textBox1.Text == " ")
            {
                MessageBox.Show("Please Enter code for Item");
            }
            if (textBox2.Text == "" || textBox2.Text == " ")
            {
                MessageBox.Show("Please Enter Rate for Item");
            }

            if (comboBox1.SelectedIndex != -1 && textBox1.Text != "" && textBox2.Text != "")
            {
                String series, code;
                float price;


                series = comboBox1.SelectedItem.ToString();
                code = textBox1.Text;
                price = float.Parse(textBox2.Text);

                OleDbConnection cnon = new System.Data.OleDb.OleDbConnection();
                cnon.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.CommandText = "UPDATE Item set Series='" + series + "',Code ='" + code + "',Rate =" + price +" where Series='"+series+"' and Code ='"+ code +"'";
                cnon.Open();
                command.Connection = cnon;
                command.ExecuteNonQuery();

                MessageBox.Show("Item Edited");
                cnon.Close();

                this.Dispose();
            }
        }
    }
}
