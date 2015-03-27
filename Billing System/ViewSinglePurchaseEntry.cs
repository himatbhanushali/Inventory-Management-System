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
    public partial class ViewSinglePurchaseEntry : Form
    {
        public ViewSinglePurchaseEntry()
        {
            InitializeComponent();
        }

        private void ViewSinglePurchaseEntry_Load(object sender, EventArgs e)
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
            int billno = PreViewSinglePurchse.billno;

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
            String iname,size,qty,price;
            while (reader2.Read())
            {
                
               iname= reader2[1].ToString();
                size= reader2[2].ToString();
                qty = reader2[3].ToString();
                 price = reader2[4].ToString();

                 dataGridView1.Rows.Add(iname,size,qty,price);
            }


                  
                        

                          
                      
        }
    }
}
