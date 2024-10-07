
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace POS.Data
{
    internal class Supplier:Action
    {
        public int Id {  get; set; }    
        public string Name { get; set; }    
        public double Tell { get; set; }
        public string Address { get; set; }
        
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string SQL { get; set; }
        public int RowEffercted {  get; set; }
        public DataGridViewRow DGV;
        public override void create(DataGridView dg)
        {
            try
            {
                this.SQL = "insert into tblSupplier(Name,Tel,Address,CreateAt,CreateBy) values(@Name,@Tell,@Address,getDate(),@createBy)";
                Database.cmd= new SqlCommand(this.SQL,Database.con);
                Database.cmd.Parameters.AddWithValue("@Name",this.Name);
                Database.cmd.Parameters.AddWithValue("@Tell", this.Tell);
                Database.cmd.Parameters.AddWithValue("@Address",this.Address);
                Database.cmd.Parameters.AddWithValue("@createBy", User.Id);
                this.RowEffercted= Database.cmd.ExecuteNonQuery();

                if (RowEffercted == 1) {

                    MessageBox.Show("Creating");
            
                
                
                }
                dg.Rows.Clear();
                this.loadData(dg);



            }
            catch (Exception ex)
            {

               MessageBox.Show("Error:"+ex.Message);
            }
        }
        public override void loadData(DataGridView dg)
        {
            try
            {
                this.SQL = "select * from tblSupplier";
                Database.cmd=new SqlCommand(this.SQL,Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
               // dg.DataSource = Database.tbl;
                foreach (DataRow r in Database.tbl.Rows) {
                   object[] row = { r["Id"], r["Name"], r["tel"], r["Address"], r["CreateAt"], r["CreateBy"], r["UpdateAt"], r["UpdateBy"] };
                  dg.Rows.Add(row);
                
               }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:"+ ex.Message);
            }
        }
        public void  tranformData(DataGridView dg,TextBox txtname,TextBox txtTel,TextBox txtAddress)
        {
            try
            {
                this.DGV=dg.SelectedRows[0];
                txtname.Text = DGV.Cells[1].Value.ToString();
                txtTel.Text =DGV.Cells[2].Value.ToString();
                txtAddress.Text = DGV.Cells[3].Value.ToString();

                


            }catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            
        }
        public override void delete(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count < 0) {

                    return;
                
                }
                this.DGV=dg.SelectedRows[0];
                this.Id =int.Parse(DGV.Cells[0].Value.ToString());
                this.SQL = "delete from tblSupplier where id=@id";
                Database.cmd=new SqlCommand(this.SQL,Database.con);
                Database.cmd.Parameters.AddWithValue("@id", this.Id);
                if (MessageBox.Show("Do you want to delete!", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes)) {
                    Database.cmd.ExecuteNonQuery();
                        dg.Rows.Remove(DGV);
                }
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
                this.SQL = "select * from tblSupplier where Name like concat('%',@name,'%')";
                Database.cmd=new SqlCommand(this.SQL,Database.con);
                Database.cmd.Parameters.AddWithValue("@name", this.Name);
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow r in Database.tbl.Rows) {
                    object[] row = { r["Id"], r["Name"], r["tel"], r["Address"], r["CreateAt"], r["CreateBy"], r["UpdateAt"], r["UpdateBy"] };
                    dg.Rows.Add(row);


                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }
        public override void update(DataGridView dg)
        {
            try
            {
                this.DGV = dg.SelectedRows[0];
                Id = int.Parse(DGV.Cells[0].Value.ToString());
                this.SQL = "update tblSupplier set Name=@userName,Tel=@Tell,Address=@address,UpdateAt=GETDATE(),UpdateBy=@UpdateBy where id=@id;";
                Database.cmd = new SqlCommand(this.SQL, Database.con);
                Database.cmd.Parameters.AddWithValue("@id", Id);
                Database.cmd.Parameters.AddWithValue("@userName",this.Name );
                Database.cmd.Parameters.AddWithValue("@address", this.Address);
                Database.cmd.Parameters.AddWithValue("@Tell", this.Tell);
                Database.cmd.Parameters.AddWithValue("@updateBy", User.Id);

                this.RowEffercted = Database.cmd.ExecuteNonQuery();
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                if (RowEffercted == 1)
                {
                    MessageBox.Show("Updated!");
                    dg.Rows.Clear();
                    this.loadData(dg);

                }
                else
                {
                    MessageBox.Show("Update! false");
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show("Error:"+ex.Message);
            }
        }
    }
}
