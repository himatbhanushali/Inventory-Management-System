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
    public partial class ViewPurchaseMonth : Form
    {
        public ViewPurchaseMonth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            String sdate, edate;

            sdate = dateTimePicker1.Value.ToShortDateString();
            edate = dateTimePicker2.Value.ToShortDateString();
            String date = dateTimePicker1.Value.ToShortDateString();
            string strProvider2 = Utility.con;
            string strSql2 = "Select * from Purchase_Bill where Purchase_Date between '" + sdate + "' and '"+edate +"'";
            OleDbConnection con2 = new OleDbConnection(strProvider2);
            OleDbCommand cmd2 = new OleDbCommand(strSql2, con2);
            con2.Open();
            cmd2.CommandType = CommandType.Text;
            OleDbDataReader reader2 = cmd2.ExecuteReader();
            String iname, size, qty, price;
            while (reader2.Read())
            {

                iname = reader2[0].ToString();
                size = reader2[1].ToString();
                qty = reader2[2].ToString();
                price = reader2[3].ToString();

                dataGridView1.Rows.Add(iname, size, qty, price);
            }
        }
    }
}
