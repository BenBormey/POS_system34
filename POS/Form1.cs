﻿
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

namespace POS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Database.connection();
            User user = new User();
            user.email=this.txtEmail.Text;
            user.password=this.txtPassword.Text;
            user.Login(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
