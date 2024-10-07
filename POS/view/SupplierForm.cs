using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Data;
using POS.Funtion;

namespace POS.view
{
    public partial class SupplierForm : Form
    {
        public SupplierForm()
        {
            InitializeComponent();
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            supplier = new Supplier();
           // dgSupplier.Columns.Clear();
            supplier.loadData(dgSupplier);
        }
        Supplier supplier;

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (funtion.startBox(txtname, txtTel, txtAddress) == 0)
            {
                return;
            }
            supplier = new Supplier();
            supplier.Name=this.txtname.Text;
            supplier.Tell=int.Parse(this.txtTel.Text);
            supplier.Address=this.txtAddress.Text;

            supplier.create(dgSupplier);
            funtion.ClearBox(txtname, txtTel, txtAddress);




        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
           if( funtion.startBox(txtname, txtTel, txtAddress)==0)
            {
                return ;
            }
            supplier = new Supplier();
            supplier.Name = this.txtname.Text;
            supplier.Tell = double.Parse(this.txtTel.Text);
            supplier.Address = this.txtAddress.Text;
            supplier.update(dgSupplier);
           funtion.ClearBox(txtname,txtTel,txtAddress);
        }

        private void dgSupplier_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            supplier = new Supplier();
            supplier.tranformData(dgSupplier, txtname, txtTel, txtAddress);
        }

        private void dgSupplier_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            supplier = new Supplier();
            supplier.delete(dgSupplier);
            funtion.ClearBox(txtname, txtTel, txtAddress);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            supplier = new Supplier();
            supplier.Name=this.txtSearch.Text;
            supplier.search(dgSupplier);
        }

        private void dgSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
