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
    public partial class ViewSingleSales : Form
    {
        int challanno;
        public ViewSingleSales()
        {
            InitializeComponent();
        }

        private void ViewSingleSales_Load(object sender, EventArgs e)
        {
            challanno = PreViewSales.challanno;



            string strProvider = Utility.con;
            string strSql = "Select * from Bill where Challan_No=" + challanno + "";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                textBox1.Text = challanno.ToString();
                textBox3.Text = reader[1].ToString();
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
            String item,box,piece,amt,particular;
            float tot;
            while (reader2.Read())
            {

                item = reader2[1].ToString();
                box = reader2[2].ToString();
                particular = reader2[3].ToString();
                piece = reader2[4].ToString();
                amt = reader2[5].ToString();

                tot = float.Parse(piece) * float.Parse(amt);
                dataGridView1.Rows.Add(item,box,particular,piece,amt,tot);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
