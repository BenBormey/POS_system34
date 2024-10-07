using POS.Data;
using POS.Funtion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS.view
{
    public partial class ProductForm1 : Form
    {
        public ProductForm1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (funtion.startBox(txtBarcode, txtName, txtQtyinstock) == 0)
            {
                return;
            }
            product=new Product();
            product.Name = this.txtName.Text;
            product.CategoryId = product.GetCategolryId(cboCategory);
            product.Barcode=int.Parse(this.txtBarcode.Text);
            product.SellPrice=double.Parse(this.txtsellPrice.Text);
            product.QtyInstock=int.Parse(this.txtQtyinstock.Text);

            if(pcxPic == null)
            {
                product.Photo = " ";
            }
            else
            {
                product.Photo = Product.pathPhoto;
            }
          
            dgproduct.Rows.Clear();
            product.loadData(dgproduct);
            product.create(dgproduct);
            funtion.ClearBox(txtName,txtBarcode,txtsellPrice,txtQtyinstock);
            pcxPic.Image = null;
           
        }
        Product product;

        private void ProductForm1_Load(object sender, EventArgs e)
        {
            product = new Product();
            product.SetCategolryId(cboCategory);
            product.loadData(dgproduct);

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
            product = new Product();
            product.Barcode = int.Parse(this.txtsearch.Text);
            }
            product.search(dgproduct);
          
            
        }

        private void dgproduct_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dgproduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            product= new Product();
            product.transferDa(dgproduct, cboCategory, txtName, txtBarcode, txtQtyinstock, txtsellPrice,pcxPic);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            product = new Product();

            product.CategoryId = product.GetCategolryId(cboCategory);
            product.Name = txtName.Text;
            product.Barcode = int.Parse(txtBarcode.Text);
            product.Price = double.Parse(txtsellPrice.Text);
            product.QtyInstock = int.Parse(txtQtyinstock.Text);
            if (pcxPic.Image == null)
            {
                product.Photo = " ";
            }
            else
            {
                product.Photo = Product.pathPhoto;
            }
            product.update(dgproduct);
            funtion.ClearBox(txtBarcode, txtName, txtQtyinstock, txtsellPrice);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            product = new Product();
            product.delete(dgproduct);
            funtion.ClearBox(txtBarcode, txtName, txtQtyinstock, txtsellPrice);
        }

     

        private void label6_Click(object sender, EventArgs e)
        {
            txtsearch.Text=" ";
            if (txtsearch.Text == " ")
            {
                dgproduct.Rows.Clear();
                product.loadData(dgproduct);
            }
            else
            {

            }
           
            


        }

        private void addStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStockForm addStockForm = new AddStockForm();
            Add_Stock add_Stock = new Add_Stock();
            add_Stock.TranferData(dgproduct, addStockForm.txtProductName, addStockForm.txtProductId,addStockForm.pcxPic);
            addStockForm.ShowDialog();
        }

        private void btnBrown_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                Product.pathPhoto = op.FileName;
                pcxPic.Image = Image.FromFile(Product.pathPhoto);
            }
        }
    }
}
