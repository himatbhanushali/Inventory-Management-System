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
    public partial class EditPurchase : Form
    {
        public EditPurchase()
        {
            InitializeComponent();
        }

        private void EditPurchase_Load(object sender, EventArgs e)
        {
            string strProvider1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql1 = "Select distinct Party_Name from PartyDetails";
            OleDbConnection con1 = new OleDbConnection(strProvider1);
            OleDbCommand cmd1 = new OleDbCommand(strSql1, con1);
            con1.Open();
            cmd1.CommandType = CommandType.Text;
            OleDbDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                comboBox1.Items.Add(reader1[0].ToString());
            }
            int billno = PreEditPurchase.billno;

            string strProvider = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql = "Select * from Purchase_Bill where Bill_No=" + billno + "";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                textBox2.Text = billno.ToString();
                comboBox1.SelectedItem = reader[1].ToString();
                dateTimePicker1.Text = reader[2].ToString();
                textBox1.Text = reader[3].ToString();

            }


            string strProvider2 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql2 = "Select * from Purchase_Inventory where Bill_No=" + billno + "";
            OleDbConnection con2 = new OleDbConnection(strProvider2);
            OleDbCommand cmd2 = new OleDbCommand(strSql2, con2);
            con2.Open();
            cmd2.CommandType = CommandType.Text;
            OleDbDataReader reader2 = cmd2.ExecuteReader();
            String iname, size, qty, price;
            while (reader2.Read())
            {

                iname = reader2[1].ToString();
                size = reader2[2].ToString();
                qty = reader2[3].ToString();
                price = reader2[4].ToString();

                dataGridView1.Rows.Add(iname, size, qty, price);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear_data();
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
            else {
                MessageBox.Show("Please provide all details for Item");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int b = PreEditPurchase.billno;

            OleDbConnection cnon4 = new System.Data.OleDb.OleDbConnection();
            cnon4.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
            System.Data.OleDb.OleDbCommand command4 = new System.Data.OleDb.OleDbCommand();
            command4.CommandText = "DELETE FROM Purchase_Bill where Bill_No=" + b;
            cnon4.Open();
            command4.Connection = cnon4;
            command4.ExecuteNonQuery();


            OleDbConnection cnon5 = new System.Data.OleDb.OleDbConnection();
            cnon5.ConnectionString = Utility.con;
            System.Data.OleDb.OleDbCommand command5 = new System.Data.OleDb.OleDbCommand();
            command5.CommandText = "DELETE FROM Purchase_Inventory where Bill_No=" + b;
            cnon5.Open();
            command5.Connection = cnon5;
            command5.ExecuteNonQuery();
            cnon5.Close();



            int billno;
            String pname, date;
            float amount;

            String iname, size;
            int qty;
            float price;
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please provide bill no");
                textBox2.Focus();
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Amount Cannot be blank");
                textBox1.Focus();
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Party Name");
                comboBox1.Focus();
            }
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.SelectedIndex != -1 && (dataGridView1.Rows.Count >= 1))
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
                command.Parameters.AddWithValue("@pname", pname);
                command.Parameters.AddWithValue("@pdate", date);
                command.Parameters.AddWithValue("@rate", amount);
                cnon.Open();
                command.Connection = cnon;
                command.ExecuteNonQuery();
                cnon.Close();

                int j = dataGridView1.RowCount;
                int i = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    i++;
                    if (i < j)
                    {
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
                        command1.Parameters.AddWithValue("@iname", iname);
                        command1.Parameters.AddWithValue("@sz", size);
                        command1.Parameters.AddWithValue("@qty", qty);
                        command1.Parameters.AddWithValue("@rate", price);
                        cnon1.Open();
                        command1.Connection = cnon1;
                        command1.ExecuteNonQuery();
                        cnon1.Close();


                    }
                    else
                    {

                        break;

                    }
                    //...
                }
            }

            clear_data();
            MessageBox.Show("Purchase Entry Altered");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
