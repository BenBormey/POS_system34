using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using POS.Funtion;
using System.Xml.Linq;

namespace POS.Data
{
    internal class Customer:Action

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string sql { get; set; }
        public int ROwEffercted { get; set; }
        public string Tel { get; set; }
        public DataGridViewRow DGV;
  
        public override void create(DataGridView dg)
        {

            try
            {
                this.sql = "insert into tbCustomer(Name,Gender,Tel) values( @Name ,@Gender,@Tel)";
                Database.cmd = new SqlCommand(this.sql, Database.con);
                
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Gender", this.Gender);
                Database.cmd.Parameters.AddWithValue("@Tel", this.Tel);
                this.ROwEffercted = Database.cmd.ExecuteNonQuery();
        
                if (ROwEffercted == 1)
                {
                    MessageBox.Show("Creating!");
                    dg.Rows.Clear();
                    this.loadData(dg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);

            }

        }
        public override void loadData(DataGridView dg)
        {
            try
            {
                this.sql = "select* from tbCustomer";
                Database.cmd = new SqlCommand(this.sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                foreach (DataRow dr in Database.tbl.Rows)
                {

                    object[] row = { dr["Id"], dr["Name"], dr["Gender"], dr["Tel"] };
                    dg.Rows.Add(row);

                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);

            }

        }
        public void transferDa(DataGridView dg,  TextBox txtName, ComboBox cboGander,TextBox txtTelephone)
        {
            this.DGV = dg.SelectedRows[0];
            txtName.Text = DGV.Cells[1].Value.ToString();
            cboGander.Text = DGV.Cells[2].Value.ToString();
            txtTelephone.Text= DGV.Cells[3].Value.ToString();
            //if (DGV.Cells[5].Value.ToString().Equals(""))
            //{

            //}
            //else
            //{

            //}



        }

        public override void delete(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count < 0)
                {
                    return;
                }
                this.DGV = dg.SelectedRows[0];
                this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                this.sql = "delete from tbCustomer where id=@Id";
                Database.cmd = new SqlCommand(this.sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@id", this.Id);
                if (MessageBox.Show("Do you want to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.ROwEffercted = Database.cmd.ExecuteNonQuery();
                    if (this.ROwEffercted == 1)
                    {
                        MessageBox.Show("User deleted");
                        dg.Rows.Remove(DGV);
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);

            }
        }

        public override void update(DataGridView dg)
        {
            try
            {
                this.DGV=dg.SelectedRows[0];
                this.Id=int.Parse( DGV.Cells[0].Value.ToString());
                this.sql = "update tbCustomer set Name=@name, Gender=@Gender ,Tel=@Tel   where id =@id";
                Database.cmd= new SqlCommand(this.sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Gender", this.Gender);
                Database.cmd.Parameters.AddWithValue("@id", this.Id);
                Database.cmd.Parameters.AddWithValue("@Tel",this.Tel);
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                this.ROwEffercted=Database.cmd.ExecuteNonQuery();
                Database.ad.Fill(Database.tbl);
                if (ROwEffercted == 1)
                {

                MessageBox.Show("Update!");
                dg.Rows.Clear();
                this.loadData(dg);
                }

            }
            catch (Exception ex) {

                MessageBox.Show("Erorr:" + ex.Message);
            }


        }
        public override void search(DataGridView dg)
        {
            try
            {
                this.sql= "select * from tbCustomer where Name like CONCAT ('%',@name,'%')";
                Database.cmd= new SqlCommand(this.sql,Database.con);
                Database.cmd.Parameters.AddWithValue("@name", this.Name);
                Database.ad= new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow dr in Database.tbl.Rows)
                {

                    object[] row = { dr["Id"], dr["Name"], dr["Gender"] };
                    dg.Rows.Add(row);

                }



            }
            catch (Exception ex) { 
                MessageBox.Show("Erorr:"+ ex.Message);  
            }
        }

    }

}


