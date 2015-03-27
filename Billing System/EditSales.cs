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
    public partial class EditSales : Form
    {
        public EditSales()
        {
            InitializeComponent();
        }

        private void EditSales_Load(object sender, EventArgs e)
        {
            string strProvider1 = Utility.con;
            string strSql1 = "Select * from PartyDetails";
            OleDbConnection con1 = new OleDbConnection(strProvider1);
            OleDbCommand cmd1 = new OleDbCommand(strSql1, con1);
            con1.Open();
            cmd1.CommandType = CommandType.Text;
            OleDbDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                comboBox1.Items.Add(reader1[0].ToString());
            }
            int challanno = PreEditSales.challanno;

            string strProvider = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql = "Select * from Bill where Challan_No=" + challanno + "";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                textBox1.Text = challanno.ToString();
                comboBox1.SelectedItem = reader[1].ToString();
                dateTimePicker1.Text = reader[2].ToString();
                textBox2.Text = reader[3].ToString();

            }


            string strProvider2 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql2 = "Select * from Sales where Challan_No=" + challanno + "";
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



            string strProvider6 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql6 = "Select * from PartyDetails";
            OleDbConnection con6 = new OleDbConnection(strProvider6);
            OleDbCommand cmd6 = new OleDbCommand(strSql6, con6);
            con6.Open();
            cmd6.CommandType = CommandType.Text;
            OleDbDataReader reader6 = cmd6.ExecuteReader();

            while (reader6.Read())
            {
                comboBox1.Items.Add(reader6[0].ToString());
            }



            string strProvider5 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql5 = "Select * from Item";
            OleDbConnection con5 = new OleDbConnection(strProvider5);
            OleDbCommand cmd5 = new OleDbCommand(strSql5, con5);
            con5.Open();
            cmd5.CommandType = CommandType.Text;
            OleDbDataReader reader5 = cmd5.ExecuteReader();

            while (reader5.Read())
            {
                comboBox3.Items.Add(reader5[0].ToString());
            }


            string strProvider7 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql7 = "Select * from Item";
            OleDbConnection con7 = new OleDbConnection(strProvider7);
            OleDbCommand cmd7 = new OleDbCommand(strSql7, con7);
            con7.Open();
            cmd7.CommandType = CommandType.Text;
            OleDbDataReader reader7 = cmd7.ExecuteReader();

            while (reader7.Read())
            {
                comboBox4.Items.Add(reader7[1].ToString());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            float irate = 0;

            string strProvider1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql1 = "Select * from Item where Series='" + comboBox3.SelectedItem.ToString() + "' and Code ='" + comboBox4.SelectedItem.ToString() + "'";
            OleDbConnection con1 = new OleDbConnection(strProvider1);
            OleDbCommand cmd1 = new OleDbCommand(strSql1, con1);
            con1.Open();
            cmd1.CommandType = CommandType.Text;
            OleDbDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {

                irate = float.Parse(reader1[2].ToString());


            }

            float disrate = 0;
            String itemname = comboBox3.SelectedItem.ToString() + "" + comboBox4.SelectedItem.ToString();
            int box = int.Parse(textBox3.Text);

            String otype = comboBox2.SelectedItem.ToString();
            int qty = 0;
            if (otype.Equals("Ring"))
            {
                qty = 12 * box;

            }
            else
            {
                qty = 6 * box;
            }

            String pname = comboBox1.SelectedItem.ToString();


            string strProvider = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Database.accdb";
            string strSql = "Select * from PartyDetails where Party_Name='" + pname + "'";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                String ra = reader[6].ToString();
                if (ra.Equals("0"))
                    disrate = 0;
                else if (ra.Equals("12"))
                    disrate = 12;
                else
                    disrate = 15;

            }
            float price = qty * irate;
            price = ((disrate / 100) * price) + price;



            dataGridView1.Rows.Add(itemname, box, qty, irate, price);
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            textBox3.Text = "";
        }

        public void clear_data()
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            dataGridView1.Rows.Clear();
            comboBox1.Focus();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            clear_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            int challano = int.Parse(textBox1.Text);
            OleDbConnection cnon = new System.Data.OleDb.OleDbConnection();
            cnon.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
            command.CommandText = "DELETE FROM Bill where Challan_No=" + challano;
            cnon.Open();
            command.Connection = cnon;
            command.ExecuteNonQuery();


            OleDbConnection cnon1 = new System.Data.OleDb.OleDbConnection();
            cnon1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
            System.Data.OleDb.OleDbCommand command1 = new System.Data.OleDb.OleDbCommand();
            command1.CommandText = "DELETE FROM Sales where Challan_No=" + challano;
            cnon1.Open();
            command1.Connection = cnon1;
            command1.ExecuteNonQuery();
            cnon.Close();


            int challanno;
            String pname, date;
            float amount;

            if (textBox1.Text == "")
            {
                MessageBox.Show("Please check challan No");
                textBox1.Focus();
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please calculate total");
                textBox2.Focus();
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Party Name");
                comboBox1.Focus();
            }
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.SelectedIndex != -1 && (dataGridView1.Rows.Count >= 1))
            {
                challanno = int.Parse(textBox1.Text.ToString());
                pname = comboBox1.SelectedItem.ToString();
                amount = float.Parse(textBox2.Text.ToString());
                date = dateTimePicker1.Value.ToShortDateString();

                OleDbConnection cnon3 = new System.Data.OleDb.OleDbConnection();
                cnon3.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
                System.Data.OleDb.OleDbCommand command3 = new System.Data.OleDb.OleDbCommand();
                command3.CommandText = "INSERT INTO Bill VALUES(@cno,@pname,@bdate,@rate)";
                command3.Parameters.AddWithValue("@cno", challanno);
                command3.Parameters.AddWithValue("@pname", pname);
                command3.Parameters.AddWithValue("@bdate", date);
                command3.Parameters.AddWithValue("@rate", amount);
                cnon3.Open();
                command3.Connection = cnon3;
                command3.ExecuteNonQuery();
                cnon3.Close();

                int j = dataGridView1.RowCount;
                int i = 0;

                String item;
                int box, piece;
                float amt;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    i++;
                    if (i < j)
                    {
                        item = row.Cells[0].Value.ToString();
                        box = int.Parse(row.Cells[1].Value.ToString());
                        piece = int.Parse(row.Cells[2].Value.ToString());
                        amt = float.Parse(row.Cells[3].Value.ToString());

                        // MessageBox.Show(billno + " " + iname + " " + size + " " + qty + " " + price);

                        OleDbConnection cnon4 = new System.Data.OleDb.OleDbConnection();
                        cnon4.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Database.accdb";
                        System.Data.OleDb.OleDbCommand command4 = new System.Data.OleDb.OleDbCommand();
                        command4.CommandText = "INSERT INTO sales VALUES(@cno,@item,@bx,@pi,@rate)";
                        command4.Parameters.AddWithValue("@cno", challanno);
                        command4.Parameters.AddWithValue("@item", item);
                        command4.Parameters.AddWithValue("@bx", box);
                        command4.Parameters.AddWithValue("@pi", piece);
                        command4.Parameters.AddWithValue("@rate", amt);
                        cnon4.Open();
                        command4.Connection = cnon4;
                        command4.ExecuteNonQuery();
                        cnon4.Close();


                    }
                    else
                    {

                        break;

                    }
                    MessageBox.Show("Sales entry Altered");
                    clear_data();
                }
            }
        }
    }
}
