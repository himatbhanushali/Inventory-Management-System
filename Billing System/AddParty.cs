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
    public partial class Add_Party : Form
    {
        public Add_Party()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void clear_data()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            richTextBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            textBox1.Focus();

        
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter Party name");
                textBox1.Focus();
            }
            else if (textBox2.Text == "" && textBox3.Text == "")
            {
                MessageBox.Show("Please Enter at least one contact no");
                textBox2.Focus();
            }
            else if (richTextBox1.Text == "")
            {
                MessageBox.Show("Please enter party address");
                richTextBox1.Focus();
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select party type");
                comboBox1.Focus();
            }

            else if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Please select discount rate");
                comboBox2.Focus();
            }

            if (textBox1.Text != "" && (textBox2.Text != "" || textBox3.Text != "")  && richTextBox1.Text != "" && comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
            {
                String pname, mob, off, email, address, ptype,diss;
                float rate=0;


                pname = textBox1.Text;
                mob = textBox2.Text;
                off = textBox3.Text;
                email = textBox4.Text;
                address = richTextBox1.Text;
                ptype = comboBox1.SelectedItem.ToString();
                diss = comboBox2.SelectedItem.ToString();


                if (mob == "")
                {
                    mob = "Not available";
                }

                if (off == "")
                {
                    off = "Not available";
                }

                if (diss.Equals("Net"))
                {
                    rate = 0;
                }
                else if (diss.Equals("12%"))
                {
                    rate = 12;
                }
                else if (diss.Equals("15%"))
                {
                    rate = 15;
                }
                OleDbConnection cnon = new System.Data.OleDb.OleDbConnection();
                cnon.ConnectionString = Utility.con;
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.CommandText = "INSERT INTO PartyDetails(Party_Name,Contact_No,Contact_No1,Email_Id,Address,Party_Type,Discount_Rate)VALUES(@pname,@c1,@c2,@email,@address,@ptype,@rate)";
                command.Parameters.AddWithValue("@pname", pname);
                command.Parameters.AddWithValue("@c1", mob);
                command.Parameters.AddWithValue("@c2", off);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@address", address);
                command.Parameters.AddWithValue("@ptype", ptype);
                command.Parameters.AddWithValue("@rate", rate);
                cnon.Open();
                command.Connection = cnon;
                command.ExecuteNonQuery();

                MessageBox.Show("Party Added");
                cnon.Close();



                clear_data();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Add_Party_Load(object sender, EventArgs e)
        {

        }
    }
}
