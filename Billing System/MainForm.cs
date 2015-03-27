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
    
    public partial class MainForm : Form
    {
        private int childFormNumber = 0;
        public static string InvoiceOrder = "";
        public MainForm()
        {

            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void addPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Party a = new Add_Party();
            a.MdiParent = this;
            a.Show();
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItem c = new AddItem();
            c.MdiParent = this;
            c.Show();
        }

        private void allPartiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewAllParty v = new ViewAllParty();
            v.MdiParent = this;
            v.Show();
        }

        private void alllItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewAllItem i = new ViewAllItem();
            i.MdiParent = this;
            i.Show();
        }

        private void singlePartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewSingleParty p = new PreviewSingleParty();
            p.MdiParent = this;
            p.Show();
        }

        private void singleSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreSingleItem p = new PreSingleItem();
            p.MdiParent = this;
            p.Show();
        }

        void MainForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Dispose();
            }
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteParty d = new DeleteParty();
            d.MdiParent = this;
            d.Show();
          
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteItem di = new DeleteItem();
            di.MdiParent = this;
            di.Show();
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreEditParty p = new PreEditParty();
            p.MdiParent = this;
            p.Show();
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreEditItem pi = new PreEditItem();
            pi.MdiParent = this;
            pi.Show();
        }

        private void addPurchaseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPurchase a = new AddPurchase();
            a.MdiParent = this;
            a.Show();
        }

        private void sinlgePurchaseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreViewSinglePurchse p = new PreViewSinglePurchse();
            p.MdiParent = this;
            p.Show();
        }

        private void deletePurchaseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeletePurchase dp = new DeletePurchase();
            dp.MdiParent = this;
            dp.Show();
        }

        private void editPurchaseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreEditPurchase e1 = new PreEditPurchase();
            e1.MdiParent = this;
            e1.Show();
        }

        private void singleDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewPurchaseDay v = new ViewPurchaseDay();
            v.MdiParent = this;
            v.Show();
        }

        private void monthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewPurchaseMonth v1 = new ViewPurchaseMonth();
            v1.MdiParent = this;
            v1.Show();
        }

        private void addSalesEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSales a = new AddSales();
            a.MdiParent = this;
            a.Show();
        }

        private void individualSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreViewSales v = new PreViewSales();
            v.MdiParent = this;
            v.Show();
        }

        private void allSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewAllSales v = new ViewAllSales();
            v.MdiParent = this;
            v.Show();
        }

        private void deleteSalesEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSales d = new DeleteSales();
            d.MdiParent = this;
            d.Show();
        }

        private void editSalesEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreEditSales pe = new PreEditSales();
            pe.MdiParent = this;
            pe.Show();
        }

        private void partyWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewPurchaseByParty p = new ViewPurchaseByParty();
            p.MdiParent = this;
            p.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm l = new LoginForm();
            l.Show();
            this.Dispose();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void printBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBill b = new InputBill();
            b.MdiParent = this;
            b.Show();

        }

        private void partyWiseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Report r = new Report();
            r.MdiParent = this;
            r.Show();
        }

        private void partyWiseToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            PurchaseReportParty r = new PurchaseReportParty();
            r.MdiParent = this;
            r.Show();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesReportAll s = new SalesReportAll();
            s.MdiParent = this;
            s.Show();

        }

        private void allToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PurchaseReportAll p = new PurchaseReportAll();
            p.MdiParent = this;
            p.Show();

        }

        private void billToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
    }
}
