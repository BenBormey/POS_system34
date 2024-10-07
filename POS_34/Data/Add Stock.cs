using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace POS.Data
{
    internal class Add_Stock : Product
    {
        public int SupplierId { get; set; }
        public int  Qty { get; set; }
        public double cost {  get; set; }
        public double Amount=>Qty*cost;
        public string  SqlUpdate { get; set; }
        public void SetSubplier(ComboBox cboSubpleirName)
        {
            try
            {
                this.SQL = "select Name from tblSupplier";
                Database.cmd = new SqlCommand(this.SQL, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                foreach (DataRow r in Database.tbl.Rows)
                {
                    cboSubpleirName.Items.Add(r["Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);

            }

        }
        public int GetSuplierId(ComboBox cboSubpleirName)
        {
            int id = 0;
            try
            {
                this.SQL = "select * from tblSupplier where Name = @name";
                Database.cmd = new SqlCommand(this.SQL, Database.con);
                Database.cmd.Parameters.AddWithValue("@name", cboSubpleirName.Text);
                Database.cmd.ExecuteNonQuery();
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                if (Database.tbl.Rows.Count > 0)
                {
                    id = int.Parse(Database.tbl.Rows[0]["id"].ToString());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);

            }
            return id;
        }
        public void TranferData(DataGridView dg, TextBox txtname, TextBox txtid, PictureBox pcxpic)
        {
            try
            {
                if (dg.Rows.Count > 0)
                {
                    DGV = new DataGridViewRow();
                    DGV = dg.SelectedRows[0];
                    this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                    this.SQL = "select * from View_ProductWithCategory where id=@id ";
                    Database.cmd = new SqlCommand(this.SQL, Database.con);
                    Database.cmd.Parameters.AddWithValue("@id", this.Id);
                    Database.cmd.ExecuteNonQuery();
                    Database.ad = new SqlDataAdapter(Database.cmd);
                    Database.tbl = new DataTable();
                    Database.ad.Fill(Database.tbl);
                    if (dg.Rows.Count > 0)
                    {

                        txtid.Text = Database.tbl.Rows[0]["id"].ToString();
                        txtname.Text = Database.tbl.Rows[0]["Name"].ToString();
                        if (Database.tbl.Rows[0]["Photo"].Equals(" "))
                        {

                            pcxpic.Image = null;
                        }
                        else
                        {
                            pcxpic.Image = Image.FromFile(Database.tbl.Rows[0]["Photo"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);
            }
        }
        public  void create()
        {
            try
            {
                this.SQL = "insert into tbAdd_Stock values( @ProductId,@SupplierId,@qty,@cost,@amount,Getdate(),@UserId)";
                Database.cmd= new SqlCommand(this.SQL, Database.con);
                Database.cmd.Parameters.AddWithValue("@UserId", User.Id);
                Database.cmd.Parameters.AddWithValue("@ProductId", this.Id);
                Database.cmd.Parameters.AddWithValue("@SupplierId", this.SupplierId);
                Database.cmd.Parameters.AddWithValue("@qty", this.Qty);
                Database.cmd.Parameters.AddWithValue("@cost", this.cost);
                Database.cmd.Parameters.AddWithValue("@amount", this.Amount);
                this.ROwEffercted = Database.cmd.ExecuteNonQuery();


                this.SqlUpdate = "update tbProduct set QtyStock = QtyStock + @qty  where id =@ProductId;";
                Database.cmd = new SqlCommand(this.SqlUpdate, Database.con);
                Database.cmd.Parameters.AddWithValue("@qty", this.Qty);
                Database.cmd.Parameters.AddWithValue("@ProductId", this.Id);
                Database.cmd.ExecuteNonQuery();
                if (ROwEffercted > 0)
                {
                    MessageBox.Show("Stock Update"); 
                }

            }
            catch (Exception ex) {
                MessageBox.Show("Error:"+ ex.Message);
            
            }
        }
    }
}
