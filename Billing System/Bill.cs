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
    public partial class Bill : Form
    {

        private System.Windows.Forms.PrintDialog prnDialog;
        private System.Windows.Forms.PrintPreviewDialog prnPreview;
        private System.Drawing.Printing.PrintDocument prnDocument;
       // private System.ComponentModel.Container components;
        public Bill()
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

            /*
            leftMargin = (int)e.MarginBounds.Left;
            rightMargin = (int)e.MarginBounds.Right;
            topMargin = (int)e.MarginBounds.Top;
            bottomMargin = (int)e.MarginBounds.Bottom;
            InvoiceWidth = (int)e.MarginBounds.Width;
            InvoiceHeight = (int)e.MarginBounds.Height;
             * */


            leftMargin = 10;
            rightMargin = 501;
            topMargin = 10;
            bottomMargin = 805;
            InvoiceWidth = 501;
            InvoiceHeight = 805;


            if (!ReadInvoice)
                ReadInvoiceData();

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
        private static int fifthvline;
         private static int sixthvline;


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
        private decimal SubTotal;
        private decimal InvoiceTotal;
        private bool ReadInvoice;
        private int AmountPosition;
        private decimal subtotal;
        private decimal ftotal;

        private Font InvTitleFont = new Font("Arial", 12, FontStyle.Regular);
        private Font InvoiceFontpoc = new Font("Arial", 13, FontStyle.Bold);
        // Title Font height
        private int InvTitleHeight;
        // SubTitle Font
        private Font InvSubTitleFont = new Font("Arial", 10, FontStyle.Regular);
        private Font InvSubTitleFontFINE = new Font("Arial", 10, FontStyle.Bold);
        private Font ContactFont = new Font("Arial", 10, FontStyle.Regular);
        //invoice total font

        private Font InvoiceFonttotal = new Font("Arial", 12, FontStyle.Bold);
        // SubTitle Font height
        private Font InvSubTitleFonthindi = new Font("DevLys 010",16,FontStyle.Bold);
        private int InvSubTitleHeight;
        // Invoice Font
        private Font InvoiceFont = new Font("Arial", 9, FontStyle.Regular);
        private Font InvoiceFontvalue = new Font("Arial", 8, FontStyle.Regular);

        // Invoice Font height
        private int InvoiceFontHeight;
        // Blue Color
        private SolidBrush BlueBrush = new SolidBrush(Color.Black);
        // Red Color
        private SolidBrush RedBrush = new SolidBrush(Color.Black);
        // Black Color
        private SolidBrush BlackBrush = new SolidBrush(Color.Black);

        private void lblID_Click(object sender, EventArgs e)
        {

        }

        private void Bill_Load(object sender, EventArgs e)
        {
           
            ordGrid.CaptionText = "Invoice...";
            LoadOrder();
            FindOrderData();

             
            lblSeller.Text = SellerName;
            lblID.Text = SaleID;
            lblDate.Text = SaleDate;
            lblSubTotal.Text = SubTotal.ToString();
            lblFreight.Text = SaleFreight.ToString();
            lblInvoiceTotal.Text = InvoiceTotal.ToString();
            
          

        }


        private void LoadOrder()
        {
            int intOrder = int.Parse(MainForm.InvoiceOrder);

            // following lines to connect with access database file 'Northwind.mdb' 
            string MyDataFile = Application.StartupPath + @"\DataFile\Database.accdb";
            string MyPass = "";
            strCon = Utility.con;
                
                /* If you are using SQL Server, please replace previous lines with following
            strCon = @"provider=sqloledb;Data Source=PC;Initial Catalog=" +
                "Northwind;Integrated Security=SSPI" + ";";
            and replace 'Data Source=PC' with the name of your system */

            try
            {
                // Get Invoice Data:
                InvSql = "Select DISTINCT S.Challan_No,S.Particular,S.Item,S.Piece,S.Box,S.Amount,B.Party_Name,B.Bill_Date,S.Amount * S.Piece as 'Final Amount' from Sales S INNER JOIN Bill B ON S.Challan_No=B.Challan_No where S.Challan_No=" + intOrder;

                //create an OleDbDataAdapter
                OleDbDataAdapter datAdp = new OleDbDataAdapter(InvSql, strCon);

                //create a command builder
                OleDbCommandBuilder cBuilder = new OleDbCommandBuilder(datAdp);

                //create a DataTable to hold the query results
                DataTable dTable = new DataTable();

                //fill the DataTable
                datAdp.Fill(dTable);

                if (dTable.Rows.Count == 0)
                {
                    MessageBox.Show("This Order not found, Please enter another order.");
                   this.Close();
                }

                // Create a TableStyle to format Datagrid columns.
                ordGrid.TableStyles.Clear();
                DataGridTableStyle tableStyle = new DataGridTableStyle();

                foreach (DataColumn dc in dTable.Columns)
                {
                    DataGridTextBoxColumn txtColumn = new DataGridTextBoxColumn();
                    txtColumn.MappingName = dc.ColumnName;
                    txtColumn.HeaderText = dc.Caption;
                    switch (dc.ColumnName.ToString())
                    {
                        case "ProductID":   // Product ID 
                            txtColumn.HeaderText = "Product ID";
                            txtColumn.Width = 60;
                            break;
                        case "ProductName":   // Product Name 
                            txtColumn.HeaderText = "Product Name";
                            txtColumn.Width = 110;
                            break;
                        case "UnitPrice":   // Unit Price 
                            txtColumn.HeaderText = "Unit Price";
                            txtColumn.Format = "0.00";
                            txtColumn.Alignment = HorizontalAlignment.Right;
                            txtColumn.Width = 60;
                            break;
                        case "Discount":   // Discount 
                            txtColumn.HeaderText = "Discount";
                            txtColumn.Format = "p"; // Percent
                            txtColumn.Alignment = HorizontalAlignment.Right;
                            txtColumn.Width = 60;
                            break;
                        case "Quantity":   // Quantity 
                            txtColumn.HeaderText = "Quantity";
                            txtColumn.Alignment = HorizontalAlignment.Right;
                            txtColumn.Width = 50;
                            break;
                        case "Total Amount":   // Extended Price 
                            txtColumn.HeaderText = "Total Amount";
                            txtColumn.Format = "0.00";
                            txtColumn.Alignment = HorizontalAlignment.Right;
                            txtColumn.Width = 90;
                            break;
                    }
                    tableStyle.GridColumnStyles.Add(txtColumn);
                }

                tableStyle.MappingName = dTable.TableName;
                ordGrid.TableStyles.Add(tableStyle);
                //set DataSource of DataGrid 
                ordGrid.DataSource = dTable.DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void FindOrderData()
        {
            int intOrder = int.Parse(MainForm.InvoiceOrder);
            

            InvSql = "Select S.Challan_No,S.Item,S.Particular,S.Piece,S.Box,S.Amount,B.Party_Name,B.Bill_Date,B.Total_Amount from Sales S INNER JOIN Bill B ON S.Challan_No=B.Challan_No where S.Challan_No=" + intOrder;

            OleDbConnection cnn = new OleDbConnection(strCon);
            OleDbCommand cmdOrder = new OleDbCommand(InvSql, cnn);
            cnn.Open();
            OleDbDataReader rdrOrder = cmdOrder.ExecuteReader();

            // Get CompanyName, City, Salesperson, OrderID, OrderDate and Freight
            rdrOrder.Read();
            
            CustomerName = "ABC COMPANY";
            CustomerCity = "Mumbai";
            SellerName = rdrOrder["Party_Name"].ToString();
            SaleID = rdrOrder["Challan_No"].ToString();
            System.DateTime dtOrder = Convert.ToDateTime(rdrOrder["Bill_Date"]);
            SaleDate = dtOrder.ToString("dd/MM/yyyy");
            //SaleDate = dtOrder.ToShortDateString();
           // subtotal = Decimal.Parse("rdrOrder['Amount']");
            string amt = rdrOrder["Amount"].ToString();
            subtotal=Decimal.Parse(amt);

            amt = rdrOrder["Amount"].ToString();
            ftotal = Decimal.Parse(amt) * Decimal.Parse(rdrOrder["Piece"].ToString());
            amt=rdrOrder["Piece"].ToString();
            SaleFreight = Decimal.Parse(amt);
            // Get invoice total
           GetInvoiceTotal();

            rdrOrder.Close();
            cnn.Close();
        }

        private void ReadInvoiceHead()
        {
            //Titles and Image of invoice:
            InvTitle = "DELIVERY CHALAN";
            InvSubTitle1 = "AA  Jh x.sk’kk; ue% AA  ";
            InvSubTitle2 = "FINE JEWELLERS";
            InvSubTitle3 = "( RAJU BHAI )";
            InvImage = Application.StartupPath + @"\Images\" + "InvPic.jpg";
        }

        private void GetInvoiceTotal()
        {
            SubTotal = 0;

            cnn = new OleDbConnection(strCon);
            cmd = new OleDbCommand(InvSql, cnn);
            cnn.Open();
            rdrInvoice = cmd.ExecuteReader();
            
            while (rdrInvoice.Read())
            {
                SubTotal = SubTotal + Convert.ToDecimal(rdrInvoice["Amount"])* Convert.ToDecimal(rdrInvoice["Piece"]);
            }

            rdrInvoice.Close();
            cnn.Close();

            // Get Total
            InvoiceTotal = SubTotal;
            // Set Total
            
        }

        private void ReadInvoiceData()
        {
            cnn = new OleDbConnection(strCon);
            cmd = new OleDbCommand(InvSql, cnn);
            cnn.Open();
            rdrInvoice = cmd.ExecuteReader();
            rdrInvoice.Read();
        }

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
                g.DrawString(InvTitle, InvTitleFont, BlueBrush, xInvTitle-10, CurrentY);
                g.DrawLine(new Pen(Brushes.Black, 2), CurrentX, CurrentY + InvTitleHeight + 5, rightMargin, CurrentY + InvTitleHeight + 5);
        
            }
            if (InvSubTitle1 != "")
            {
                CurrentY = CurrentY + InvTitleHeight + 10;
                g.DrawString(InvSubTitle1, InvSubTitleFonthindi, BlueBrush, xInvSubTitle1-40, CurrentY);
            }
            if (InvSubTitle2 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight + 4;
                g.DrawString(InvSubTitle2, InvSubTitleFontFINE, BlueBrush, xInvSubTitle2-20, CurrentY);
            }
            if (InvSubTitle3 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight  + 4;
                g.DrawString(InvSubTitle3, InvSubTitleFont, BlueBrush, xInvSubTitle3-20, CurrentY);
            }

            // Draw line:
            CurrentY = CurrentY + InvSubTitleHeight + 8;
            g.DrawLine(new Pen(Brushes.Black, 2), CurrentX, CurrentY, rightMargin, CurrentY);
       
   
        
        }

        private void SetOrderData(Graphics g)
        {
            int middlemargin = (bottomMargin - leftMargin) / 2;
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
            FieldValue = "Party Name: " + SellerName;
            g.DrawString(FieldValue, InvoiceFontpoc, BlackBrush, CurrentX+ 10, CurrentY);
            // Set City:
            CurrentX = CurrentX + (int)g.MeasureString(FieldValue, InvoiceFont).Width + 16;
            FieldValue = "Mobile: " + 8655337765;
            g.DrawString(FieldValue, ContactFont, BlackBrush, rightMargin -135 , CurrentY-40);
            // Set Salesperson:
            CurrentX = leftMargin;
            CurrentY = CurrentY + InvoiceFontHeight;
           // FieldValue = "To: " + SellerName;
           // g.DrawString(FieldValue, InvoiceFont, BlackBrush, CurrentX+10, CurrentY);

            FieldValue = "Office: 28802590";
            g.DrawString(FieldValue, ContactFont, BlackBrush, rightMargin - 133, CurrentY - 38);
            
            // Set Order ID:
            CurrentX = leftMargin;
            CurrentY = CurrentY + InvoiceFontHeight;
            FieldValue = "Challan No: " + SaleID;
            g.DrawString(FieldValue, InvoiceFontpoc, BlackBrush, CurrentX + 10, CurrentY + 3);
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY + 2 , rightMargin, CurrentY + 2);
       
            // Set Order Date:
            CurrentX = rightMargin - 170;
            FieldValue = "Order Date: " + SaleDate;
            g.DrawString(FieldValue, InvoiceFontpoc, BlackBrush, CurrentX-28, CurrentY + 4);

            // Draw line:
            CurrentY = CurrentY + InvoiceFontHeight + 8;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY + 2, rightMargin, CurrentY+ 2);
        }

        private void SetInvoiceData(Graphics g, System.Drawing.Printing.PrintPageEventArgs e)
        {// Set Invoice Table:
            string FieldValue = "";
            int CurrentRecord = 0;
            int RecordsPerPage = 20; // twenty items in a page
            decimal Amount = 0;
            bool StopReading = false;

            // Set Table Head:
            int xProductID = leftMargin+15;
             

             firstvy=CurrentY;
            CurrentY = CurrentY + InvoiceFontHeight;
            g.DrawString("Sr", InvoiceFont, BlueBrush, xProductID -10, CurrentY);
            
            int xProductName = xProductID + (int)g.MeasureString("Sr", InvoiceFont).Width;
            g.DrawString("Art", InvoiceFont, BlueBrush, xProductName, CurrentY);
         //  g.DrawLine(new Pen(Brushes.Black), secondvline, firstvy, secondvline, firstvy + bottomMargin);
       
            int xUnitPrice = xProductName + (int)g.MeasureString("Art", InvoiceFont).Width + 30;
            g.DrawString("Box", InvoiceFont, BlueBrush, xUnitPrice-2, CurrentY);
          //  g.DrawLine(new Pen(Brushes.Black), thirdvline, firstvy, thirdvline, firstvy + bottomMargin);
       

            int xQuantity = xUnitPrice + (int)g.MeasureString("Box", InvoiceFont).Width +10;
            g.DrawString("Particular", InvoiceFont, BlueBrush, xQuantity+70, CurrentY);

              int xDiscount = xQuantity + (int)g.MeasureString("Pieces", InvoiceFont).Width + 164;
              g.DrawString("Pieces", InvoiceFont, BlueBrush, xDiscount, CurrentY);

             int xPrice = xDiscount + (int)g.MeasureString("Pieces", InvoiceFont).Width + 5;
             
             g.DrawString("Price", InvoiceFont, BlueBrush, xPrice, CurrentY);
      

             AmountPosition = rightMargin - (int)g.MeasureString("Total Amount", InvoiceFont).Width - 5;
            g.DrawString("Total Amount", InvoiceFont, BlueBrush, AmountPosition, CurrentY);


            firstvline = xProductName - 4;
            secondvline = xUnitPrice - 4;
            thirdvline = xQuantity + 2;
            fourthvline = xDiscount -4;
            fifthvline = AmountPosition-4;
            sixthvline = fourthvline + 48;
            // Set Invoice Table:

            int i = 1;
            while (CurrentRecord < RecordsPerPage)
            {
                CurrentY = CurrentY + InvoiceFontHeight ;
                g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY , rightMargin, CurrentY );


                /*   FieldValue = rdrInvoice["ProductID"].ToString();*/
                g.DrawString(i + "", InvoiceFont, BlackBrush, xProductID -10, CurrentY+5);
                FieldValue = rdrInvoice["Item"].ToString();
                // if Length of (Product Name) > 20, Draw 20 character only
                if (FieldValue.Length > 20)
                    FieldValue = FieldValue.Remove(20, FieldValue.Length - 20);
                g.DrawString(FieldValue, InvoiceFontvalue, BlackBrush, xProductName, CurrentY+5);
                FieldValue =  rdrInvoice["Piece"].ToString();
                g.DrawString(FieldValue, InvoiceFontvalue, BlackBrush, xDiscount, CurrentY + 5);
                FieldValue = rdrInvoice["Box"] + "";
                g.DrawString(FieldValue, InvoiceFontvalue, BlackBrush, xUnitPrice, CurrentY + 5);
                FieldValue = rdrInvoice["Particular"] + "";
                g.DrawString(FieldValue, InvoiceFontvalue, BlackBrush, xQuantity + 70, CurrentY + 5);

               /* FieldValue = rdrInvoice["Piece"].ToString();
                g.DrawString(FieldValue, InvoiceFont, BlackBrush, xUnitPrice, CurrentY); */
                 FieldValue = String.Format("{0:0.00}", rdrInvoice["Amount"]);
                 g.DrawString(FieldValue, InvoiceFontvalue, BlackBrush, xPrice, CurrentY + 5);
                 
                Amount = Convert.ToDecimal(rdrInvoice["Amount"]) * Convert.ToDecimal(rdrInvoice["Piece"]);
                // Format Extended Price and Align to Right:
                FieldValue = String.Format("{0:0.00}", Amount);
                int xAmount = AmountPosition;
                //xAmount = xAmount - (int)g.MeasureString(FieldValue, InvoiceFont).Width;
                g.DrawString(FieldValue, InvoiceFontvalue, BlackBrush, xAmount, CurrentY + 5);
                CurrentY = CurrentY + InvoiceFontHeight;



                if (!rdrInvoice.Read())
                {
                    StopReading = true;
                    break;
                }

                CurrentRecord++;
                i++;
            }

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
            CurrentY = CurrentY + 8;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY);
            // Get Right Edge of Invoice:
            int xRightEdg = AmountPosition + (int)g.MeasureString("Extended Price", InvoiceFont).Width;

            // Write Sub Total:
            int xSubTotal = AmountPosition - (int)g.MeasureString("Sub Total", InvoiceFont).Width;
            secondvy = CurrentY;
            CurrentY = CurrentY + 8;
            
           // g.DrawString("Sub Total", InvoiceFont, RedBrush, xSubTotal, CurrentY);

           
            string TotalValue = String.Format("{0:0.00}", SubTotal);
            int xTotalValue = xRightEdg - (int)g.MeasureString(TotalValue, InvoiceFont).Width;
      
        //    g.DrawString(TotalValue, InvoiceFont, BlackBrush, xTotalValue, CurrentY);

            // Write Order Freight:
        /*    int xOrderFreight = AmountPosition - (int)g.MeasureString("Piece", InvoiceFont).Width;
            CurrentY = CurrentY + InvoiceFontHeight;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY - 1, rightMargin, CurrentY - 1);

            g.DrawString("Piece", InvoiceFont, RedBrush, xOrderFreight, CurrentY);
            string FreightValue = String.Format("{0:0.00}", SaleFreight);
            int xFreight = xRightEdg - (int)g.MeasureString(FreightValue, InvoiceFont).Width;
            g.DrawString(FreightValue, InvoiceFont, BlackBrush, xFreight, CurrentY);
*/
            // Write Invoice Total:
            int xInvoiceTotal = AmountPosition - (int)g.MeasureString("Invoice Total", InvoiceFont).Width;
            CurrentY = 795 - 75;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY - 1, rightMargin, CurrentY - 1);

            g.DrawString("Invoice Total", InvoiceFonttotal, RedBrush, leftMargin + 10, CurrentY);
            string InvoiceValue = String.Format("{0:0.00}", InvoiceTotal);
            int xInvoiceValue = xRightEdg - (int)g.MeasureString(InvoiceValue, InvoiceFont).Width;
            g.DrawString(InvoiceValue, InvoiceFonttotal, BlackBrush, AmountPosition + 10, CurrentY);
            CurrentY = CurrentY + 25;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY , rightMargin, CurrentY );

            g.DrawString("All Goods are at Net Cost", InvoiceFont, BlackBrush, leftMargin +10 , CurrentY +3);

            CurrentY = CurrentY + 25;
            g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, CurrentY);
            int midlemargin = (rightMargin - leftMargin) / 2;
            g.DrawString(" !! THANK YOU !!", InvoiceFont, BlackBrush, midlemargin-40, CurrentY +3);
            


            //verticle line
           
           
            g.DrawLine(new Pen(Brushes.Black), firstvline, firstvy, firstvline, secondvy);

            g.DrawLine(new Pen(Brushes.Black), secondvline, firstvy, secondvline, secondvy);

            g.DrawLine(new Pen(Brushes.Black), thirdvline, firstvy, thirdvline, secondvy);
            g.DrawLine(new Pen(Brushes.Black), fourthvline, firstvy, fourthvline, secondvy);
            g.DrawLine(new Pen(Brushes.Black), fifthvline, firstvy, fifthvline, secondvy);
            g.DrawLine(new Pen(Brushes.Black), sixthvline, firstvy, sixthvline, secondvy); 
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
            ReadInvoice = false;
            PrintReport(); // Print Invoice
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            ReadInvoice = false;
            DisplayInvoice(); // Print Preview
        }

        private void btnDialog_Click(object sender, EventArgs e)
        {
            ReadInvoice = false;
            DisplayDialog(); // Print Dialog
        }

        private void lblInvoiceTotal_Click(object sender, EventArgs e)
        {

        }

        private void ordGrid_Navigate(object sender, NavigateEventArgs ne)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
