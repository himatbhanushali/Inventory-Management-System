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
    public partial class InputBill : Form
    {
        public InputBill()
        {
            InitializeComponent();
        }


        

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                MainForm.InvoiceOrder =comboBox1.SelectedItem.ToString();
                this.Hide();
                Bill b = new Bill();
                b.MdiParent = this.MdiParent;
                b.Show();
            }
        }

        private void InputBill_Load(object sender, EventArgs e)
        {
            string strProvider = Utility.con;
            string strSql = "Select * from Bill";
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
