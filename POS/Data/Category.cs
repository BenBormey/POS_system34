using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Data;

namespace POS.Data
{
    internal class Category:Action
    {
        public int Id { get; set; }
        public string Categoryname { get; set; }
        public bool Status { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
        public int UpdateBy { get; set; }
        public string sql { get; set; }
        public int ROwEffercted { get; set; }
        public DataGridViewRow DGV;
     

        public override void create(DataGridView dg)
        {
      
            try
            {
                this.sql = "insert into tblCatefory (name,status,CreateAt,CreateBy) values(@username,@status,getDate(),@createBy)";
                Database.cmd = new SqlCommand(this.sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@username", this.Categoryname);
                Database.cmd.Parameters.AddWithValue("@status", this.Status);
                Database.cmd.Parameters.AddWithValue("@createBy",User.Id);
                this.ROwEffercted = Database.cmd.ExecuteNonQuery();
           
                if (ROwEffercted == 1)
                {
                    MessageBox.Show("Createing!");
                    
                }
                dg.Rows.Clear();
                this.loadData(dg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);

            }



        }
        public override void delete(DataGridView dg)
        {
            try
            {
                if(dg.Rows.Count < 0)
                {
                    return;  
                }
                this.DGV= dg.SelectedRows[0];
                this.Id =int.Parse(DGV.Cells[0].Value.ToString());
                this.sql = "delete from tblCatefory where id= @id";
                Database.cmd= new SqlCommand(this.sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@id",this.Id);
                Database.ad= new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                if (MessageBox.Show("Does anyone agree to delete?", "Delete!", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    Database.cmd.ExecuteNonQuery();
                    dg.Rows.Remove(DGV);
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
                this.sql = "select* from tblCatefory";
                Database.cmd= new SqlCommand(this.sql,Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ad= new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                dg.DataSource = Database.tbl;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);

            }

        }
        public override void search(DataGridView dg)
        {
            try
            {
                this.sql = "select * from tblCatefory where id=@id ;";
                Database.cmd= new SqlCommand(this.sql,Database.con);
                Database.cmd.Parameters.AddWithValue("@id", this.Id);
     
                Database.cmd.ExecuteNonQuery();
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                dg.Rows.Clear();
              //  dg.DataSource = Database.tbl;


                foreach (DataRow dr in Database.tbl.Rows) {
                    object[] row =
                    {
                        dr["Id"],dr["Name"],dr["status"],dr["CreateAt"],dr["CreateBy"],dr["UpdateAt"],dr["UpdateBy"]
                    };
                    dg.Rows.Add(row);


                }

            }
            catch (Exception ex) { MessageBox.Show("Erorr:" + ex.Message); }
        }
   
        
    }

}
