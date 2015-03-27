using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Billing_System
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            String id;
            String pass;

            id = textBox1.Text;
            pass = textBox2.Text;
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                if (id.Equals("fine") && pass.Equals("1001"))
                {
                    MainForm m = new MainForm();
                    m.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Please provide valid Login Id or password");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
