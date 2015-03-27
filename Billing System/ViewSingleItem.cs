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
    public partial class ViewSingleItem : Form
    {
        public ViewSingleItem()
        {
            InitializeComponent();
        }

        private void ViewSingleItem_Load(object sender, EventArgs e)
        {
            String series = PreSingleItem.series;
            String code = PreSingleItem.code;

            string strProvider = Utility.con;
            string strSql = "Select * from Item where Series='" + series + "' and Code ='"+code+"'";
            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                label4.Text = reader[0].ToString();
                label5.Text = reader[1].ToString();
                label6.Text = reader[2].ToString();
              

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
