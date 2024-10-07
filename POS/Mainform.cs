using POS.Data;
using POS.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class Mainform : Form
    {
        private int childFormNumber = 0;
    


        public Mainform()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm =new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
             
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
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
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
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

        private void UserToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Userform1  userform1 = new Userform1();
            userform1.MdiParent = this;
            userform1.Show();
            
           

        }

        
        private void SubpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Categoryform categoryform = new Categoryform(); 
            categoryform.MdiParent = this;
            categoryform.Show();
            
        }

        private void ProductToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProductForm1 productForm1 = new ProductForm1();
            productForm1.MdiParent = this;
            productForm1.Show();
        }

        private void Supplier1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupplierForm supplierForm = new SupplierForm();
            supplierForm.MdiParent = this;
            supplierForm.Show();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {

        }

        private void customerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            customerForm.MdiParent = this;
            customerForm.Show();
            
        }

        private void securityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want Logout ", "LogOut!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information).Equals(DialogResult.OK))
            {

            Application.Exit();
            }
        }

        private void saleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaleForm saleForm = new SaleForm();
            saleForm.MdiParent = this;
            saleForm.Show();
        }
    }
}
