using POS.Data;
using System;
using System.Windows.Forms;

namespace POS.view
{
    public partial class AddStockForm : Form
    {
        public AddStockForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Add_Stock = new Add_Stock();
            Add_Stock.Id = int.Parse(txtProductId.Text);
            Add_Stock.Qty = int.Parse(txtQTY.Text);
            Add_Stock.cost = double.Parse(txtCostPrice.Text);
            Add_Stock.SupplierId = Add_Stock.GetSuplierId(cboSubplier);
            Add_Stock.create();
        }
        Add_Stock Add_Stock;
        private void AddStock_Load(object sender, EventArgs e)
        {
            Add_Stock = new Add_Stock();
            Add_Stock.SetSubplier(cboSubplier);


        }
    }
}
