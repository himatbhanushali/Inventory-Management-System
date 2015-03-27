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
    public partial class EditParty : Form
    {
        public EditParty()
        {
            InitializeComponent();
        }

        void clear_data()
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
        private void EditParty_Load(object sender, EventArgs e)
        {
            String pname = PreEditParty.pname;

            string strProvider = Utility.con;
            string strSql = "Select * from PartyDetails where Party_Name='" + pname + "'";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                textBox1.Text = reader[0].ToString();
                textBox2.Text = reader[1].ToString();
                textBox3.Text = reader[2].ToString();
                textBox4.Text = reader[3].ToString();
                richTextBox1.Text = reader[4].ToString();
                comboBox1.SelectedItem = reader[5].ToString();
                String ra = reader[6].ToString();
                if (ra.Equals("0"))
                    comboBox2.SelectedItem = "Net";
                else if (ra.Equals("12"))
                    comboBox2.SelectedItem = "12%";
                else
                    comboBox2.SelectedItem = "15%";

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear_data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter Party name");
                textBox1.Focus();
            }
            if (textBox2.Text == "" && textBox3.Text == "")
            {
                MessageBox.Show("Please Enter at least one contact no");
                textBox2.Focus();
            }
            
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("Please enter party address");
                richTextBox1.Focus();
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select party type");
                comboBox1.Focus();
            }

            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Please select discount rate");
                comboBox2.Focus();
            }

            if (textBox1.Text != "" && (textBox2.Text != "" || textBox3.Text != "")  && richTextBox1.Text != "" && comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
            {
                String pname, mob, off, email, address, ptype, diss;
                float rate = 0;


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
                cnon.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.CommandText = "update PartyDetails set Party_Name ='" + pname + "',Contact_No ='" + mob + "',Contact_No1='" + off + "',Email_Id ='" + email + "',Address ='" + address + "',Party_Type ='" + ptype + "',Discount_Rate = " + rate+" where Party_Name='"+ pname +"'";
                cnon.Open();
                command.Connection = cnon;
                command.ExecuteNonQuery();

                MessageBox.Show("Party Details Edited");
                cnon.Close();



                this.Dispose();
            }
        }
    }
}
