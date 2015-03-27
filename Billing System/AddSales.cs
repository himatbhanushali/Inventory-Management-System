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
    public partial class AddSales : Form
    {
        public AddSales()
        {
            InitializeComponent();
        }

        private void AddSales_Load(object sender, EventArgs e)
        {

            int challanno = 0;

            string strProvider4 = Utility.con;
            string strSql4 = "Select * from Bill";
            OleDbConnection con4 = new OleDbConnection(strProvider4);
            OleDbCommand cmd4 = new OleDbCommand(strSql4, con4);
            con4.Open();
            cmd4.CommandType = CommandType.Text;
            OleDbDataReader reader4 = cmd4.ExecuteReader();

            while (reader4.Read())
            {
                challanno= int.Parse(reader4[0].ToString());
            }

            if (challanno > 0)
            {
                challanno=challanno+1;
                textBox1.Text = challanno.ToString();
            }
            else
            {
                textBox1.Text = "1";
            }






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



            string strProvider1 = Utility.con;
            string strSql1 = "Select * from Item";
            OleDbConnection con1 = new OleDbConnection(strProvider1);
            OleDbCommand cmd1 = new OleDbCommand(strSql1, con1);
            con1.Open();
            cmd1.CommandType = CommandType.Text;
            OleDbDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                comboBox3.Items.Add(reader1[0].ToString());
            }


        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strProvider2 = Utility.con;
            string strSql2 = "Select * from Item where Series ='" + comboBox3.SelectedItem +"'";
            OleDbConnection con2 = new OleDbConnection(strProvider2);
            OleDbCommand cmd2 = new OleDbCommand(strSql2, con2);
            con2.Open();
            cmd2.CommandType = CommandType.Text;
            OleDbDataReader reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                comboBox4.Items.Add(reader2[1].ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (comboBox3.SelectedIndex != -1 && comboBox5.SelectedIndex != -1 && comboBox4.SelectedIndex != -1 && textBox3.Text != "")
            {
                String dabi = "";
                float irate = 0;

                if (comboBox5.SelectedIndex != -1)
                {
                    dabi = comboBox5.Text;
                }
                else
                {
                    MessageBox.Show("Please Select Packing Dabbi");
                }
                string strProvider1 = Utility.con;
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

                int box = 0;

                try
                {
                    box = Convert.ToInt32(textBox3.Text);
                }
                catch (Exception h)
                {
                    MessageBox.Show("Please provide number only in Quantity");
                    textBox3.Focus();
                    return;
                }


                String otype = comboBox2.SelectedItem.ToString();
                int qty = 0;
                if (otype.Equals("Dabi Set"))
                {
                    qty = 6 * box;
                    

                }
                else 
                {
                    qty = 12 * box;
                }

                String pname = comboBox1.SelectedItem.ToString();


                string strProvider = Utility.con;
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
                if (dabi.Equals("Planet"))
                {
                    irate--;
                }
                else
                {

                }
                float price = qty * irate;
                price = ((disrate / 100) * price) + price;



                dataGridView1.Rows.Add(itemname, box,otype,qty, irate, price);
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                textBox3.Text = "";
            }
            else
            {
                MessageBox.Show("Please provide all values");
            }

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            float total = 0;
            float price = 0;
            int j = dataGridView1.RowCount;
            int i = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                i++;
                if (i < j)
                {

                    price = float.Parse(row.Cells[4].Value.ToString());
                    total = total + price;



                }
                else
                {

                    break;

                }

            }
            textBox2.Text = total.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            
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

                OleDbConnection cnon = new System.Data.OleDb.OleDbConnection();
                cnon.ConnectionString = Utility.con;
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand();
                command.CommandText = "INSERT INTO Bill VALUES(@cno,@pname,@bdate,@rate)";
                command.Parameters.AddWithValue("@cno", challanno);
                command.Parameters.AddWithValue("@pname", pname);
                command.Parameters.AddWithValue("@bdate", date);
                command.Parameters.AddWithValue("@rate", amount);
                cnon.Open();
                command.Connection = cnon;
                command.ExecuteNonQuery();
                cnon.Close();

                int j = dataGridView1.RowCount;
                int i = 1;
                
                String item,otype;
                int box, piece;
                float amt;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    
                    if(i < j)
                    {
                        item = row.Cells[0].Value.ToString();
                        box = int.Parse(row.Cells[1].Value.ToString());
                        otype = row.Cells[2].Value.ToString();
                        piece = int.Parse(row.Cells[3].Value.ToString());
                        amt = float.Parse(row.Cells[4].Value.ToString());

                       

                        OleDbConnection cnon1 = new System.Data.OleDb.OleDbConnection();
                        cnon1.ConnectionString = Utility.con;
                        System.Data.OleDb.OleDbCommand command1 = new System.Data.OleDb.OleDbCommand();
                        command1.CommandText = "INSERT INTO sales VALUES(@cno,@item,@bx,@oty,@pi,@rate)";
                        command1.Parameters.AddWithValue("@cno",challanno);
                        command1.Parameters.AddWithValue("@item",item);
                        command1.Parameters.AddWithValue("@bx", box);
                        command1.Parameters.AddWithValue("@oty",otype);
                        command1.Parameters.AddWithValue("@pi",piece);
                        command1.Parameters.AddWithValue("@rate",amt);
                        cnon1.Open();
                        command1.Connection = cnon1;
                        command1.ExecuteNonQuery();
                        cnon1.Close();
                        i++;

                    }
         
                    
                }
                MessageBox.Show("Sales entry added");
                //clear_data();
                button4.Enabled = true;
            }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            float total = 0;
            float price = 0;
            int j = dataGridView1.RowCount;
            int i = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                i++;
                if (i < j)
                {

                    price = float.Parse(row.Cells[5].Value.ToString());
                    total = total + price;



                }
                else
                {

                    break;

                }

            }
            textBox2.Text = total.ToString();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("It works");
        
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            MainForm.InvoiceOrder = textBox1.Text;
            this.Hide();
            Bill b = new Bill();
            b.MdiParent = this.MdiParent;
            b.Show();
        }
      

        
    }
}
