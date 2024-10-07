using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient ;
using System.Data;
using System.Xml.Linq;
using System.Drawing;
using POS.view;
namespace POS.Data
{
    internal class User:Action
    {

        public DataGridViewRow DGV;
        public static  int Id { get; set; } 
        public  static string username { get; set; }
        public string gender { get; set; }  
        public string password { get; set; }
        public string email { get; set; }   
        public string status { get; set; }
        public int ROwEffercted { get; set; }
        public DateTime CreateAt { get; set; }
        public int createBy { get; set; }
        public DateTime updateAt { get; set; }  
        public int updateBy { get; set; }
        public string sql { get; set; }
        public   void Login(Form from)
        {
            try
            {
                this.sql = "select * from tbUser where email = @email  and password =@password;";
                Database.cmd=new SqlCommand(this.sql,Database.con);
                Database.cmd.Parameters.AddWithValue("@email", this.email);
                Database.cmd.Parameters.AddWithValue("@password", this.password);
                Database.cmd.ExecuteNonQuery();
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);

                if(Database.tbl.Rows.Count > 0)
                {
                    User.Id =int.Parse(Database.tbl.Rows[0]["id"].ToString());

                    Mainform mainforms = new Mainform();
                    mainforms.Show();
                    Form form = new Form();
                    form.Close();
                }
                else
                {
                    MessageBox.Show("Username and Password is invalid!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERORR" + ex.Message);
            }
            
        }
        public override void create(DataGridView dg)
        {
            try
            {
                this.sql = "insert into tbUser(userName,gender,password,email,status,CreateAt,CreateBy)" +
                    " VALUES(@username,@Gender,@password,@gamil,@staus,getDate(),@createBy);";
                Database.cmd=new SqlCommand(this.sql,Database.con);
                Database.cmd.Parameters.AddWithValue("@username", username);
                Database.cmd.Parameters.AddWithValue("@Gender", this.gender);
                Database.cmd.Parameters.AddWithValue("@password", this.password);
                Database.cmd.Parameters.AddWithValue("@gamil", this.email);
                Database.cmd.Parameters.AddWithValue("@staus", this.status);
                Database.cmd.Parameters.AddWithValue("@createBy", User.Id);
              
                this.ROwEffercted = Database.cmd.ExecuteNonQuery();
                if (this.ROwEffercted == 1)
                {
                    MessageBox.Show("Create User ");
                }
                
               //dg.Rows.Clear();
               // this.loadData(dg);
                    
                
                
                
               
             
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR:"+ ex.Message);

            }
            
        }
        public override void loadData(DataGridView dg)
        {
            try
            {
                this.sql = "select * from View_User";
                Database.cmd= new SqlCommand(this.sql,Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ad= new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);

               // style1 dg.DataSource = Database.tbl;
                foreach (DataRow r in Database.tbl.Rows)//style 2
                {
                    object[] row = { r["Id"], r["UserName"], r["Gender"], r["Password"], r["email"], r["status"],r["CreateAt"],r["CreateBy"],r["UpdateAt"],r["UpdateBy"]
                                };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);
            }
        }
     
        public override void delete(DataGridView dg)
        {
            if(dg.Rows.Count <0) {
                return; 
            
            }
            this.DGV= dg.SelectedRows[0];
            Id =int.Parse(DGV.Cells[0].Value.ToString());
            this.sql = "delete tbUser where id=@id";
            Database.cmd = new SqlCommand(this.sql, Database.con);
            Database.cmd.Parameters.AddWithValue("@id", Id);
            if (MessageBox.Show("Do you want to delete!", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                this.ROwEffercted =Database.cmd.ExecuteNonQuery();
                if (ROwEffercted == 1)
                {
                    MessageBox.Show("Deleted!");
                    dg.Rows.Remove(DGV);
                }


            }

            

        }
        public override void update(DataGridView dg)
        {
            this.DGV=dg.SelectedRows[0];
            Id = int.Parse(DGV.Cells[0].Value.ToString());
            this.sql = "update tbUser set userName=@userName,email=@email,gender=@gender,password=@password,status=@status,UpdateAt=getDate(),updateBy=@updateBy where id=@id;";
            Database.cmd= new SqlCommand(this.sql,Database.con);
            Database.cmd.Parameters.AddWithValue("@id", Id);
            Database.cmd.Parameters.AddWithValue("@userName", username);
            Database.cmd.Parameters.AddWithValue("@gender", this.gender);
            Database.cmd.Parameters.AddWithValue("@password", this.password);
            Database.cmd.Parameters.AddWithValue("@status", this.status);
            Database.cmd.Parameters.AddWithValue("@email", this.email);
            Database.cmd.Parameters.AddWithValue("@updateBy", User.Id);
           this.ROwEffercted= Database.cmd.ExecuteNonQuery();
            Database.ad = new SqlDataAdapter(Database.cmd);
            Database.tbl = new DataTable();
            Database.ad.Fill(Database.tbl);
            if (ROwEffercted == 1)
            {
                MessageBox.Show("Updated!");
                
            }
            else
            {
                MessageBox.Show("Update! false");
            }
            dg.Rows.Clear();
            this.loadData(dg);




        }
        public override void search(DataGridView dg)
        {
            try
            {
              
                this.sql = "select * from tbUser where [userName] like CONCAT ('%',@username,'%')";
                Database.cmd= new SqlCommand(this.sql,Database.con);
                Database.cmd.Parameters.AddWithValue("@username", username);
                Database.cmd.ExecuteNonQuery();
                Database.ad= new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow r in Database.tbl.Rows)
                {
                    object[] row = { r["Id"], r["UserName"], r["Gender"], r["Password"], r["email"], r["status"],r["CreateAt"],r["CreateBy"],r["UpdateAt"],r["UpdateBy"]
                               };
                   dg.Rows.Add(row);
                }





            }
            catch (Exception ex) {

                MessageBox.Show("Erorr:" + ex.Message);
            
            }
        }

        internal void Tranferdata(DataGridView dg, TextBox txtUserName,ComboBox cboGander, TextBox txtPassword, TextBox txtEmail, RadioButton rTrue, RadioButton rFalse)
        {
            this.DGV = dg.SelectedRows[0];
            txtUserName.Text = DGV.Cells[1].Value.ToString();
            cboGander.Text = DGV.Cells[2].Value.ToString();
            txtPassword.Text = DGV.Cells[3].Value.ToString();
            txtEmail.Text = DGV.Cells[4].Value.ToString();
            if (DGV.Cells[5].Value.ToString().Equals("True"))
            {
                rTrue.Checked = true;

            }
            else
            {
                rFalse.Checked = true;

            }
        }
    }
}
