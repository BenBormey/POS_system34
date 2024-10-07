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

namespace POS.view
{
    public partial class Userform1 : Form
    {
        public Userform1()
        {
            InitializeComponent();
        }
    

        private void Userform1_Load(object sender, EventArgs e)
        {
            User user = new User();
            cboGander.Items.Add("Male");
            cboGander.Items.Add("Female");

            user.loadData(dgUser);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (funtion.startBox(txtName, txtPassword, txtEmail) == 0)
            {
                return;
            }
            User user = new User();
            User.username = this.txtName.Text;
            user.password = this.txtPassword.Text;
            user.gender = this.cboGander.Text;
            user.email = this.txtEmail.Text;
            if (rTrue.Checked)
            {

                user.status = rTrue.Text; ;
            }
            else
            {
                user.status =rFalse.Text;
            }
            
            user.create(dgUser);
            dgUser.Rows.Clear();
            user.loadData(dgUser);
           // Userform1_Load(object sender, EventArgs e);
        
          
            funtion.ClearBox(txtName, txtPassword, txtEmail);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.delete(dgUser);
            funtion.ClearBox(txtName, txtPassword, txtEmail);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User user = new User();
            if (funtion.startBox(txtName, txtPassword, txtEmail) == 0)
            {
                return;
            }
            user = new User();
            User.username = this.txtName.Text;
            user.password = this.txtPassword.Text;
            user.gender = this.cboGander.Text;
            user.email = this.txtEmail.Text;
            if (rTrue.Checked)
            {

                user.status = rTrue.Text;
            }
            else
            {
                user.status = rFalse.Text;
            }


            user.update(dgUser);
            funtion.ClearBox(txtName, txtPassword, txtEmail);
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                User user = new User();
                User.username = txtsearch.Text;
                user.search(dgUser);

            }
        }

      

        private void dgUser_DoubleClick(object sender, EventArgs e)
        {
            User user = new User();
            user.Tranferdata(dgUser, txtName,cboGander, txtPassword, txtEmail, rTrue,rFalse);
        }

        private void dgUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboGander_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
