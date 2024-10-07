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
    public partial class Categoryform : Form
    {
        public Categoryform()
        {
            InitializeComponent();
        }
        Category category;

        private void btncreate_Click(object sender, EventArgs e)
        {
            category = new Category();
            category.Categoryname = this.txtName.Text;
            if (rFalse.Checked) {
                category.Status = this.rFalse.Checked;
            }
            else
            {
                category.Status = this.rTrue.Checked;

            }
          
           

            category.create(dgCategory);

            
        }

        private void Categoryform_Load(object sender, EventArgs e)
        {
            category = new Category();
            dgCategory.Columns.Clear();
            category.loadData(dgCategory);
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            category = new Category();
            category.delete(dgCategory);
        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                category = new Category();
                category.Id = int.Parse(txtsearch.Text);
    
                category.search(dgCategory);
            }
        }

        private void dgCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnuldate_Click(object sender, EventArgs e)
        {

        }
    }
}
