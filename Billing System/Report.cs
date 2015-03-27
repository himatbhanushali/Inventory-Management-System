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
    public partial class Report : Form
    {
        

        private System.Windows.Forms.PrintDialog prnDialog;
        private System.Windows.Forms.PrintPreviewDialog prnPreview;
        private System.Drawing.Printing.PrintDocument prnDocument;
       

        public Report()
        {
            InitializeComponent();
          
            this.prnDialog = new System.Windows.Forms.PrintDialog();
            this.prnPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.prnDocument = new System.Drawing.Printing.PrintDocument();
            // The Event of 'PrintPage'
            prnDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(prnDocument_PrintPage);
		
        
        }
        private void prnDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            leftMargin = 10;
            rightMargin = 501;
            topMargin = 10;
            bottomMargin = 805;
            InvoiceWidth = 501;
            InvoiceHeight = 791;
            InvoiceWidth = 491;
           

           /* if (!ReadInvoice)
                ReadInvoiceData();*/

            SetInvoiceHead(e.Graphics); // Draw Invoice Head
            SetOrderData(e.Graphics); // Draw Order Data
            SetInvoiceData(e.Graphics, e); // Draw Invoice Data

            ReadInvoice = true;
        }

        
        // for Invoice Head:
        private string InvTitle;
        private string InvSubTitle1;
        private string InvSubTitle2;
        private string InvSubTitle3;
        private string InvImage;

        // for Database:
        private OleDbConnection cnn;
        private OleDbCommand cmd;
        private OleDbDataReader rdrInvoice;
        private string strCon;
        private string InvSql;

        // for Report:
        private static int firstvy;
        private static int secondvy;
        private static int firstvline;
        private static int secondvline;
        private static int thirdvline;
        private static int fourthvline;

        private int CurrentY;
        private int CurrentX;
        private int leftMargin;
        private int rightMargin;
        private int topMargin;
        private int bottomMargin;
        private int InvoiceWidth;
        private int InvoiceHeight;
        private string CustomerName;
        private string CustomerCity;
        private string SellerName;
        private string SaleID;
        private string SaleDate;
        private decimal SaleFreight;
        private decimal SubTotal=0;
        private decimal InvoiceTotal;
        private bool ReadInvoice;
        private int AmountPosition;
        private decimal subtotal;
        private decimal ftotal;

        private Font InvTitleFont = new Font("Arial", 14, FontStyle.Regular);
        // Title Font height
        private int InvTitleHeight;
        // SubTitle Font
        private Font InvSubTitleFonthindi = new Font("DevLys 010", 16, FontStyle.Bold);
        private Font InvSubTitleFont = new Font("Arial", 14, FontStyle.Regular);
        // SubTitle Font height
        private int InvSubTitleHeight;
        // Invoice Font
        private Font InvoiceFont = new Font("Arial", 12, FontStyle.Regular);
        private Font InvoiceFontContact = new Font("Arial", 10, FontStyle.Regular);
        // Invoice Font height
        private int InvoiceFontHeight;
        // Blue Color
        private SolidBrush BlueBrush = new SolidBrush(Color.Black);
        // Red Color
        private SolidBrush RedBrush = new SolidBrush(Color.Black);
        // Black Color
        private SolidBrush BlackBrush = new SolidBrush(Color.Black);


        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text=="")
            {
                MessageBox.Show("Please Select Party Name");
            }
            else
            {

            var d1 = dateTimePicker1.Value;
            var d2 = dateTimePicker2.Value;
            String min = d1.Date.ToString("MM/dd/yyyy");
            String max = d2.Date.ToString("MM/dd/yyyy");
            if (dateTimePicker1.Value > dateTimePicker2.Value || dateTimePicker2.Value > DateTime.Now)
            {
                MessageBox.Show("Select appropriate Date");
            }
            else
            {
                OleDbConnection con = new OleDbConnection(Utility.con);

                String dat = "select * from Bill where Bill_Date between #" + d1.Date.ToString("MM/dd/yyyy") + "# and #" + d2.Date.ToString("MM/dd/yyyy") + "# and Party_Name='" + comboBox1.Text + "'";

                //  dat = "select * from Bill where Bill_Date between #02/02/2014# and #02/02/2015#";
                // string strProvider = Utility.con;
                OleDbDataAdapter dataadapter = new OleDbDataAdapter(dat, con);
                DataSet ds = new DataSet();

                con.Open();
                dataadapter.Fill(ds, "Bill_Date");
                // dataadapter.Fill(ds);
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Bill_Date";
                con.Close();
            }

            }
        }

        private void Report_Load(object sender, EventArgs e)
        {
            
            string strSql = "Select distinct Party_Name from Bill";
            OleDbConnection con = new OleDbConnection(Utility.con);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataReader reader = cmd.ExecuteReader();
           

            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Challan_No"].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*Bill b = new Bill();
            Utility.reportno = 1;
            this.Hide();
            b.Show();*/

            //Print

            ReadInvoice = false;
            PrintReport(); // Print Invoice


        }

        //PRINTING REPORT DESIGN
        private void SetInvoiceHead(Graphics g)
        {
            ReadInvoiceHead();

            CurrentY = topMargin;
            CurrentX = leftMargin;
            int ImageHeight = 0;

            // Draw Invoice image:
            if (System.IO.File.Exists(InvImage))
            {
                Bitmap oInvImage = new Bitmap(InvImage);
                // Set Image Left to center Image:
                int xImage = CurrentX + (InvoiceWidth - oInvImage.Width) / 2;
                ImageHeight = oInvImage.Height; // Get Image Height
                g.DrawImage(oInvImage, xImage, CurrentY);
            }

            InvTitleHeight = (int)(InvTitleFont.GetHeight(g));
            InvSubTitleHeight = (int)(InvSubTitleFont.GetHeight(g));

            // Get Titles Length:
            int lenInvTitle = (int)g.MeasureString(InvTitle, InvTitleFont).Width;
            int lenInvSubTitle1 = (int)g.MeasureString(InvSubTitle1, InvSubTitleFont).Width;
            int lenInvSubTitle2 = (int)g.MeasureString(InvSubTitle2, InvSubTitleFont).Width;
            int lenInvSubTitle3 = (int)g.MeasureString(InvSubTitle3, InvSubTitleFont).Width;

            // Set Titles Left:
            int xInvTitle = CurrentX + (InvoiceWidth - lenInvTitle) / 2;
            int xInvSubTitle1 = CurrentX + (InvoiceWidth - lenInvSubTitle1) / 2;
            int xInvSubTitle2 = CurrentX + (InvoiceWidth - lenInvSubTitle2) / 2;
            int xInvSubTitle3 = CurrentX + (InvoiceWidth - lenInvSubTitle3) / 2;

            // Draw Invoice Head:
            if (InvTitle != "")
            {
                CurrentY = CurrentY + ImageHeight;
                g.DrawString(InvTitle, InvTitleFont, BlueBrush, xInvTitle, CurrentY+ 1);
                g.DrawLine(new Pen(Brushes.Black, 2), CurrentX, CurrentY + InvTitleHeight + 5, rightMargin, CurrentY + InvTitleHeight + 5);

            }
            if (InvSubTitle1 != "")
            {
                CurrentY = CurrentY + InvTitleHeight + 10;
                g.DrawString(InvSubTitle1, InvSubTitleFonthindi, BlueBrush, xInvSubTitle1, CurrentY);
            }
            if (InvSubTitle2 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight;
                g.DrawString(InvSubTitle2, InvSubTitleFont, BlueBrush, xInvSubTitle2, CurrentY);
            }
            if (InvSubTitle3 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight;
                g.DrawString(InvSubTitle3, InvSubTitleFont, BlueBrush, xInvSubTitle3, CurrentY);
               String FieldValue = "Contact: " + 8655337765;
                g.DrawString(FieldValue, InvoiceFontContact, BlackBrush, rightMargin - 150, CurrentY+10);
            
            }

            // Draw line:
            CurrentY = CurrentY + InvSubTitleHeight + 8;
            g.DrawLine(new Pen(Brushes.Black, 2), CurrentX, CurrentY, rightMargin, CurrentY);



        }

        private void SetOrderData(Graphics g)
        {

            g.DrawLine(new Pen(Brushes.Black, 2), leftMargin, leftMargin, rightMargin, leftMargin);
            g.DrawLine(new Pen(Brushes.Black, 2), leftMargin, leftMargin, leftMargin, bottomMargin);
            g.DrawLine(new Pen(Brushes.Black, 2), leftMargin, bottomMargin, rightMargin, bottomMargin);
            g.DrawLine(new Pen(Brushes.Black, 2), rightMargin, bottomMargin, rightMargin, leftMargin);

            // Set Company Name, City, Salesperson, Order ID and Order Date
            string FieldValue = "";
            InvoiceFontHeight = (int)(InvoiceFont.GetHeight(g));
            // Set Company Name:
            CurrentX = leftMargin;
            CurrentY = CurrentY + 8;
            FieldValue = "Party Name: " + dataGridView1.Rows[0].Cells[1].Value.ToString();
            g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX + 5, CurrentY);
            // Set City:
            CurrentX = CurrentX + (int)g.MeasureString(FieldValue, InvoiceFont).Width + 16;
           // Set Salesperson:
           /* CurrentX = leftMargin;
            CurrentY = CurrentY + InvoiceFontHeight;
            FieldValue = "To: " + dataGridView1.Rows[0].Cells[1].Value.ToString();
            
           */ // Set Order ID:
            CurrentX = leftMargin;
            /*CurrentY = CurrentY + InvoiceFontHeight;
               FieldValue = "Challan No: " + SaleID;
               g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY + 7);
               g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY + 2, rightMargin, CurrentY + 2);

               // Set Order Date:
               CurrentX = rightMargin - 170;
               FieldValue = "Order Date: " + SaleDate;
               g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX, CurrentY + 7);
   */
            // Draw line:
            CurrentY = CurrentY + InvoiceFontHeight + 8;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY + 2, rightMargin, CurrentY + 2);
        }

        private void SetInvoiceData(Graphics g, System.Drawing.Printing.PrintPageEventArgs e)
        {// Set Invoice Table:
            string FieldValue = "";
            int CurrentRecord = 0;
            int RecordsPerPage = 20; // twenty items in a page
            decimal Amount = 0;
            bool StopReading = false;

            // Set Table Head:
            int xProductID = leftMargin;
            firstvline = leftMargin + 60;
            secondvline = firstvline + 170;
            thirdvline = secondvline + 130;
            fourthvline = thirdvline + 100;

            firstvy = CurrentY;
            CurrentY = CurrentY + InvoiceFontHeight;
            g.DrawString("Challan No", InvoiceFont, BlueBrush, xProductID + 5, CurrentY);
            // g.DrawLine(new Pen(Brushes.Black), firstvline, firstvy, firstvline, firstvy + bottomMargin);

            int xProductName = xProductID + (int)g.MeasureString("Product ID", InvoiceFont).Width + 4;
          /*  g.DrawString("Product Name", InvoiceFont, BlueBrush, xProductName, CurrentY);
              g.DrawLine(new Pen(Brushes.Black), secondvline, firstvy, secondvline, firstvy + bottomMargin);
            */
            int xUnitPrice = xProductName + (int)g.MeasureString("Product Name", InvoiceFont).Width + 72;
            /*g.DrawString("Party Name", InvoiceFont, BlueBrush, xProductName, CurrentY);
              g.DrawLine(new Pen(Brushes.Black), thirdvline, firstvy, thirdvline, firstvy + bottomMargin);
            */

            int xQuantity = (rightMargin-leftMargin)/2;
            g.DrawString("Date", InvoiceFont, BlueBrush, xQuantity-30, CurrentY);

            int xDiscount = xQuantity + (int)g.MeasureString("Quantity", InvoiceFont).Width + 4;
            /*   g.DrawString("Discount", InvoiceFont, BlueBrush, xDiscount, CurrentY);  */

            AmountPosition = rightMargin - 120;
            g.DrawString("Total Amount", InvoiceFont, BlueBrush, AmountPosition, CurrentY);

            // Set Invoice Table:
/*
            int i = 0;
            while (CurrentRecord < RecordsPerPage)
            {
                for (i=0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    CurrentY = CurrentY + InvoiceFontHeight + 8;
                    g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY - 4, rightMargin, CurrentY - 4);


                     FieldValue = rdrInvoice["ProductID"].ToString();
                       g.DrawString(FieldValue, InvoiceFont, BlackBrush, xProductID, CurrentY);
                      
                    FieldValue = dataGridView1.Rows[i].Cells["Party_Name"].Value.ToString(); ;
                    // if Length of (Product Name) > 20, Draw 20 character only
                    if (FieldValue.Length > 20)
                        FieldValue = FieldValue.Remove(20, FieldValue.Length - 20);
                    g.DrawString(FieldValue, InvoiceFont, BlackBrush, xProductName, CurrentY);
                    FieldValue = String.Format("{0:0.00}", dataGridView1.Rows[i].Cells["Bill_Date"].Value.ToString());
                    g.DrawString(FieldValue, InvoiceFont, BlackBrush, xUnitPrice, CurrentY);
                    FieldValue = rdrInvoice["Piece"].ToString();
                      g.DrawString(FieldValue, InvoiceFont, BlackBrush, xQuantity, CurrentY);
                      FieldValue = String.Format("{0:0.00%}", rdrInvoice["Discount"]);
                       g.DrawString(FieldValue, InvoiceFont, BlackBrush, xDiscount, CurrentY);
                       
                    Amount = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Total_Amount"].Value.ToString());
                    // Format Extended Price and Align to Right:
                    FieldValue = String.Format("{0:0.00}", Amount);
                    int xAmount = AmountPosition + (int)g.MeasureString("Extended Price", InvoiceFont).Width;
                    xAmount = xAmount - (int)g.MeasureString(FieldValue, InvoiceFont).Width;
                    g.DrawString(FieldValue, InvoiceFont, BlackBrush, xAmount, CurrentY);
                    CurrentY = CurrentY + InvoiceFontHeight;



                    if (i < dataGridView1.Rows.Count - 1)
                    {
                        StopReading = true;
                        break;
                    }
                    
                    CurrentRecord++;
                    
                }
            }
    */

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                 CurrentY = CurrentY + InvoiceFontHeight + 8;
                    g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY - 4, rightMargin, CurrentY - 4);





                    FieldValue = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    g.DrawString(FieldValue, InvoiceFont, BlackBrush, xProductID + 5, CurrentY);
                
                    FieldValue = dataGridView1.Rows[i].Cells[2].Value.ToString();
                 FieldValue=DateTime.Parse(FieldValue).ToString("dd/MM/yyyy");
                    g.DrawString(FieldValue, InvoiceFont, BlackBrush, xQuantity-30, CurrentY);

                    FieldValue = dataGridView1.Rows[i].Cells[3].Value.ToString(); ;
                    g.DrawString(FieldValue, InvoiceFont, BlackBrush, AmountPosition, CurrentY);
                    SubTotal = SubTotal + Decimal.Parse(FieldValue);
                

            }
            SetInvoiceTotal(g);


            if (CurrentRecord < RecordsPerPage)
                e.HasMorePages = false;
            else
                e.HasMorePages = true;

            if (StopReading)
            {
                rdrInvoice.Close();
                cnn.Close();
                SetInvoiceTotal(g);
            }

            g.Dispose();
        }

        private void SetInvoiceTotal(Graphics g)
        {// Set Invoice Total:



            // Draw line:
            CurrentY = bottomMargin - 80;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY);
            // Get Right Edge of Invoice:
            int xRightEdg = AmountPosition + (int)g.MeasureString("Extended Price", InvoiceFont).Width;

            // Write Sub Total:
            int xSubTotal = AmountPosition - (int)g.MeasureString("Invoice Total", InvoiceFont).Width;
            secondvy = CurrentY;
            CurrentY = CurrentY + 8;

            g.DrawString("Invoice Total", InvoiceFont, RedBrush, xSubTotal, CurrentY);


            string TotalValue = String.Format("{0:0.00}", SubTotal);
            int xTotalValue = xRightEdg - (int)g.MeasureString(TotalValue, InvoiceFont).Width;

            g.DrawString(TotalValue, InvoiceFont, BlackBrush, xTotalValue-10, CurrentY);

            // Write Order Freight:
            /*    int xOrderFreight = AmountPosition - (int)g.MeasureString("Piece", InvoiceFont).Width;
                CurrentY = CurrentY + InvoiceFontHeight;
                g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY - 1, rightMargin, CurrentY - 1);

                g.DrawString("Piece", InvoiceFont, RedBrush, xOrderFreight, CurrentY);
                string FreightValue = String.Format("{0:0.00}", SaleFreight);
                int xFreight = xRightEdg - (int)g.MeasureString(FreightValue, InvoiceFont).Width;
                g.DrawString(FreightValue, InvoiceFont, BlackBrush, xFreight, CurrentY);
    
            // Write Invoice Total:
            int xInvoiceTotal = AmountPosition - (int)g.MeasureString("Invoice Total", InvoiceFont).Width;
            CurrentY = CurrentY + InvoiceFontHeight;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY - 1, rightMargin, CurrentY - 1);

            g.DrawString("Invoice Total", InvoiceFont, RedBrush, leftMargin, CurrentY);
            string InvoiceValue = String.Format("{0:0.00}", InvoiceTotal);
            int xInvoiceValue = xRightEdg - (int)g.MeasureString(InvoiceValue, InvoiceFont).Width;
            g.DrawString(InvoiceValue, InvoiceFont, BlackBrush, xInvoiceValue, CurrentY);
            CurrentY = CurrentY + InvoiceFontHeight + 3;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY);
            */
           
            CurrentY = CurrentY + InvoiceFontHeight + 6;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY);
            g.DrawString("All Goods are at Net Cost", InvoiceFont, BlackBrush, leftMargin, bottomMargin-40);
            g.DrawLine(new Pen(Brushes.Black), leftMargin, bottomMargin - 22, rightMargin, bottomMargin - 22);
            int midlemargin = (rightMargin - leftMargin) / 2;
            g.DrawString(" !! THANK YOU !!", InvoiceFont, BlackBrush, midlemargin - 35, bottomMargin - 20);
            DateTime today = DateTime.Today;
            g.DrawString(today.ToString("dd/MM/yyyy"), InvoiceFont, BlackBrush, rightMargin - 100, topMargin + 5);


            //verticle line

           /* g.DrawLine(new Pen(Brushes.Black), firstvline, firstvy, firstvline, secondvy);

            g.DrawLine(new Pen(Brushes.Black), secondvline, firstvy, secondvline, secondvy);

            g.DrawLine(new Pen(Brushes.Black), thirdvline, firstvy, thirdvline, secondvy);
            g.DrawLine(new Pen(Brushes.Black), fourthvline, firstvy, fourthvline, secondvy);
            */
        }

        private void DisplayDialog()
        {
            try
            {
                prnDialog.Document = this.prnDocument;
                DialogResult ButtonPressed = prnDialog.ShowDialog();
                // If user Click 'OK', Print Invoice
                if (ButtonPressed == DialogResult.OK)
                    prnDocument.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void DisplayInvoice()
        {
            prnPreview.Document = this.prnDocument;

            try
            {
                prnPreview.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void PrintReport()
        {
            try
            {
                prnDocument.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDialog_Click(object sender, EventArgs e)
        {
            ReadInvoice = false;
            DisplayDialog(); // Print Dialog
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please Select Party Name");
            }
            else if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Please Press Search");
            }
            else
            {
                ReadInvoice = false;
                DisplayInvoice(); // Print Preview
            }
        }

        private void ReadInvoiceHead()
        {
            //Titles and Image of invoice:
            InvTitle = "REPORT";
            InvSubTitle1 = "AA  Jh x.sk’kk; ue% AA  ";
            InvSubTitle2 = "NAIVEDH JEWELLERS";
            InvSubTitle3 = "( RAJU BHAI )";
            InvImage = Application.StartupPath + @"\Images\" + "InvPic.jpg";
        }
    
        















    }
}
