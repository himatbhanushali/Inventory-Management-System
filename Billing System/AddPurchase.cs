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
    public partial class AddPurchase : Form
    {
        public AddPurchase()
        {
            InitializeComponent();
        }

        private void AddPurchase_Load(object sender, EventArgs e)
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
            if (comboBox2.SelectedIndex != -1 && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                dataGridView1.Rows.Add(comboBox2.SelectedItem.ToString(), textBox3.Text, textBox4.Text, textBox5.Text);
                comboBox2.SelectedIndex = -1;
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox2.Focus();
            }
            else
            {
                MessageBox.Show("Please provide all details of the item purchased");
            }
        }

        public void clear_data()
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Focus();
            dataGridView1.Rows.Clear();
        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            clear_data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
              int billno;
              String pname, date;
              float amount;
            
              String iname, size;
              int qty;
              float price;
              int count = dataGridView1.Rows.Count;
                if(textBox2.Text=="")
                {
                    MessageBox.Show("Please provide bill no");
                    textBox2.Focus();
                    

                }
                else if(textBox1.Text == "")
                {
                    MessageBox.Show("Amount Cannot be blank");
                    textBox1.Focus();
                }
                else if(comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select Party Name");
                    comboBox1.Focus();
                }
                else if(textBox1.Text != "" && textBox2.Text != "" && comboBox1.SelectedIndex != -1 && count>1)
                {
                    billno = int.Parse(textBox2.Text.ToString());
                    pname = comboBox1.SelectedItem.ToString();
                    amount = float.Parse(textBox1.Text.ToString());
                    date = dateTimePicker1.Value.ToShortDateString();

                    OleDbConnection cnon = new System.Data.OleDb.OleDbConnection();
                    cnon.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                    command.CommandText = "INSERT INTO Purchase_Bill(Bill_No,Party_Name,Purchase_Date,Amount)VALUES(@bno,@pname,@pdate,@rate)";
                    command.Parameters.AddWithValue("@bno", billno);
                    command.Parameters.AddWithValue("@pname",pname);
                    command.Parameters.AddWithValue("@pdate", date);
                    command.Parameters.AddWithValue("@rate", amount);
                    cnon.Open();
                    command.Connection = cnon;
                    command.ExecuteNonQuery();
                    cnon.Close();
                    
                    int j = dataGridView1.RowCount;
                    int i = 1;
                    foreach (DataGridViewRow row in dataGridView1.Rows) {

                       
                        if (i < j)
                        {
                            i++;
                            iname = row.Cells[0].Value.ToString();
                            size = row.Cells[1].Value.ToString();
                            qty = int.Parse(row.Cells[2].Value.ToString());
                            price = float.Parse(row.Cells[3].Value.ToString());

                           // MessageBox.Show(billno + " " + iname + " " + size + " " + qty + " " + price);

                            OleDbConnection cnon1 = new System.Data.OleDb.OleDbConnection();
                            cnon1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
                            System.Data.OleDb.OleDbCommand command1 = new System.Data.OleDb.OleDbCommand();
                            command1.CommandText = "INSERT INTO Purchase_Inventory VALUES(@bno,@iname,@sz,@qty,@rate)";
                            command1.Parameters.AddWithValue("@bno", billno);
                            command1.Parameters.AddWithValue("@iname",iname);
                            command1.Parameters.AddWithValue("@sz", size);
                            command1.Parameters.AddWithValue("@qty", qty);
                            command1.Parameters.AddWithValue("@rate", price);
                            cnon1.Open();
                            command1.Connection = cnon1;
                            command1.ExecuteNonQuery();
                            cnon1.Close();
                            
                           
                        }
                        
                //...
                 }
                    clear_data();
                    MessageBox.Show("Purchase Entry Added");
                }

                
           
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
