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
    public partial class AddItem : Form
    {
        public AddItem()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Series for Item");
            }
            if (textBox1.Text == "" || textBox1.Text==" ")
            {
                MessageBox.Show("Please Enter code for Item");
            }
            if (textBox2.Text == "" || textBox2.Text == " ")
            {
                MessageBox.Show("Please Enter Rate for Item");
            }

            if (comboBox1.SelectedIndex != -1 && textBox1.Text != "" && textBox2.Text != "")
            {
                String series,code;
                float price;


                series = comboBox1.SelectedItem.ToString();
                code = textBox1.Text;
                price = float.Parse(textBox2.Text);

                OleDbConnection cnon = new System.Data.OleDb.OleDbConnection();
            //    cnon.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
                cnon.ConnectionString = Utility.con;
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.CommandText = "INSERT INTO Item(Series,Code,Rate)VALUES(@series,@code,@rate)";
                command.Parameters.AddWithValue("@series",series);
                command.Parameters.AddWithValue("@code", code);
                command.Parameters.AddWithValue("@rate", price);
                cnon.Open();
                command.Connection = cnon;
                command.ExecuteNonQuery();

                MessageBox.Show("Item Added");
                cnon.Close();

               
                
                clear_data();
            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear_data();
        }

        public void clear_data() {

            comboBox1.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AddItem_Load(object sender, EventArgs e)
        {

        }
    }
}
