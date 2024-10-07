using POS.Data;
using POS.Funtion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.view
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }
        Customer customer;

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void btncreate_Click(object sender, EventArgs e)
        {
          if( funtion.startBox(txtName, txtTel) == 0){
                return;
            }
             customer = new Customer();
            customer.Name = this.txtName.Text;
            customer.Gender = this.cboGender.Text;
            customer.Tel=this.txtTel.Text;

            customer.create(dataGridView);
            funtion.ClearBox(txtName,txtTel);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            customer = new Customer();
            cboGender.Items.Add("Male");
            cboGender.Items.Add("Female");
            customer.loadData(dataGridView);

        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             customer = new Customer();
            customer.transferDa(dataGridView,txtName , cboGender,txtTel);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            customer = new Customer();
            customer.delete(dataGridView);
            funtion.ClearBox(txtName, txtTel);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            customer= new Customer();
            customer.Name= this.txtName.Text;
            customer.Gender = this.cboGender.Text;
            customer.Tel=this.txtTel.Text ;
            customer.update(dataGridView);
            funtion.ClearBox(txtName, txtTel);

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                customer = new Customer();
                customer.Name = this.textBox4.Text;
                customer.search(dataGridView);
            }
        }
    }
}
