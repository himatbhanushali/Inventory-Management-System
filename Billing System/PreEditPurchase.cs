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
    public partial class PreEditPurchase : Form
    {
        public static int billno;
        public PreEditPurchase()
        {
            InitializeComponent();
        }

        private void PreEditPurchase_Load(object sender, EventArgs e)
        {
            string strProvider = Utility.con;
            string strSql = "Select * from Purchase_Bill";
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
            if (comboBox1.SelectedIndex != -1)
            {
                billno = int.Parse(comboBox1.SelectedItem.ToString());
                EditPurchase e1 = new EditPurchase();
                e1.MdiParent = this.MdiParent;
                e1.Show();
                this.Dispose();
            }
        }
    }
}
