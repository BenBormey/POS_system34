using POS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.view
{
    public partial class SaleForm : Form
    {
        public SaleForm()
        {
            InitializeComponent();
        }

        

        private void txtScanBarcode_KeyPress(object sender, KeyPressEventArgs e)
        
        
        
        {
            if (txtScanBarcode.Equals(""))
            {
                txtScanBarcode.Focus();
                return;
            }
            Sale sale = new Sale();
            if (e.KeyChar == (char)13)
            {
                ProductForm1 productForm = new ProductForm1();
                sale = new Sale();
                sale.ScanBarcode(dgvSale,productForm.dgproduct,txtScanBarcode,lblTotalAmount);
            }
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {

        }

        private void txtChasRecieve_KeyPress(object sender, KeyPressEventArgs e)
        {
            Sale sale;
            
           if(e.KeyChar == (char)13)
            {

                if (dgvSale.Rows.Count > 0)
                {
                  double cashRecieve, cashReturn, totalAmount;
                    totalAmount = double.Parse(lblTotalAmount.Text);
                    cashRecieve = double.Parse(txtChasRecieve.Text);
                    sale = new Sale();
                //    sale.CustomerId = sale.GetCustomerId(txtCustomerId);


                    if (cashRecieve >= totalAmount)
                    {
                       
                       cashReturn = -totalAmount+ cashRecieve;
                        lblCashReturn.Text= cashReturn.ToString();

                       
                        sale.CommitData(dgvSale, lblTotalAmount, txtChasRecieve, lblCashReturn);
                    }
                    
                }
                
            }
        }
    }
}
