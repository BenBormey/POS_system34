using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.view;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Threading;
using System.IO;
using POS;
using System.Drawing;

namespace POS.Data
{
    internal class Product:Action
    {
       // public byte[] photo {  get; set; }   
        public static string pathPhoto { get; set; }
        public string Photo { get; set; }
        public int Id { get; set; } 
        public string Name { get; set; }    
        public long Barcode {  get; set; }
        public double Price { get; set; }   
     
        public double SellPrice { get; set; }
        public int QtyInstock { get; set; }
        public int CategoryId {  get; set; }    
        public DateTime CreateAt { get; set; }
       
        public int CreateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UpdateBy { get; set; }
        public string SQL { get; set; }
        public int ROwEffercted { get; set; }
        public DataGridViewRow DGV;


        public override void create(DataGridView dg)
        {
            try
            {
                this.SQL = "insert into tbProduct(Name,Barcode,SellPrice,QtyStock,Photo,CateogoryId,CreateAt,CreateBy)Values(@Name,@Barcode,@SellPrice,@QtyInStock,@Photo,@CategoryId,getDate(),@CreateBy)";
                Database.cmd = new SqlCommand(this.SQL, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Barcode", this.Barcode);
                Database.cmd.Parameters.AddWithValue("@SellPrice", this.SellPrice);
                Database.cmd.Parameters.AddWithValue("@QtyInStock", this.QtyInstock);
                Database.cmd.Parameters.AddWithValue("@CategoryId", this.CategoryId);
                Database.cmd.Parameters.AddWithValue("@CreateBy", User.Id);
                Database.cmd.Parameters.AddWithValue("@Photo", this.Photo);
                
                this.ROwEffercted = Database.cmd.ExecuteNonQuery();
                if (this.ROwEffercted == 1)
                {
                    MessageBox.Show("User Created");

                    Product.pathPhoto = " ";
                    
                  
                    

                }
             


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);


            }
        }
        public override void loadData(DataGridView dg)
        {
            try
            {

                this.SQL = "select * from View_ProductWithCategory";
                Database.cmd = new SqlCommand(this.SQL, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                DataGridViewImageColumn img = new DataGridViewImageColumn();

                // dg.DataSource = Database.tbl;
                foreach (DataRow r in Database.tbl.Rows)
                {

                    object[] row = { r["Id"], r["Name"], r["Barcode"], r["SellPrice"], r["QtyStock"], r["Photo"], r["Category"], r["CreateAt"], r["CreateBy"], r["upadate"], r["UpdateBy"] };
                    dg.Rows.Add(row);

                }




            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);

            }


        }

        public void SetCategolryId(ComboBox cboname)
        {
            try
            {
                this.SQL = "select * from tblCatefory";
                Database.cmd= new SqlCommand(this.SQL,Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ad= new SqlDataAdapter(Database.cmd);
                Database.tbl= new DataTable();
                Database.ad.Fill(Database.tbl);
                foreach (DataRow r in Database.tbl.Rows) {

                    cboname.Items.Add(r["name"].ToString());
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Erorr:"+ ex.Message);
            
            }

        }
        public int GetCategolryId(ComboBox cboname)
        {
            int id= 0;  
            try
            {
                this.SQL = "select * from tblCatefory where name=@name";
                Database.cmd= new SqlCommand (this.SQL,Database.con);
                Database.cmd.Parameters.AddWithValue("@name", cboname.Text);
                Database.cmd.ExecuteNonQuery();
                Database.ad= new SqlDataAdapter( Database.cmd);
                Database.tbl= new DataTable();
                Database.ad.Fill(Database.tbl);
                if(Database.tbl.Rows.Count> 0)
                {
                    id = int.Parse(Database.tbl.Rows[0]["Id"].ToString());
                }
                

            }
            catch (Exception ex) {
                MessageBox.Show("Erorr:"+ ex.Message);
            
            }
            return id;
}
        public override void delete(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count < 0) {
                    return;
                }
                this.DGV = dg.SelectedRows[0];
                this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                this.SQL = "delete from tbProduct where id=@id";
                Database.cmd= new SqlCommand(this.SQL,Database.con);
                Database.cmd.Parameters.AddWithValue("@id", this.Id);
                if(MessageBox.Show("Do you want to delete?", "Delete", MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    this.ROwEffercted = Database.cmd.ExecuteNonQuery();
                    if (this.ROwEffercted == 1)
                    {
                        MessageBox.Show("Uer deleted");
                        dg.Rows.Remove(DGV);
                    }
                }
              

               
            }
            catch (Exception ex) {
                MessageBox.Show("Erorr:" + ex.Message);

            }
        }
    

        public void transferDa(DataGridView dg, ComboBox cboCategory, TextBox txtName, TextBox txtBarcode, TextBox txtSellPrice, TextBox txtQtyinstock,PictureBox pcx)
        {
            this.DGV = dg.SelectedRows[0];
            txtName.Text = DGV.Cells[1].Value.ToString();
            txtBarcode.Text = DGV.Cells[2].Value.ToString();
            txtSellPrice.Text = DGV.Cells[3].Value.ToString();
            txtQtyinstock.Text = DGV.Cells[4].Value.ToString();
            if (DGV.Cells[5].Value.ToString() == null) {

                pcx.Image = null;
            }
            else
            {
                Product.pathPhoto = DGV.Cells[5].Value.ToString();
                pcx.Image = Image.FromFile(Product.pathPhoto);
            }
            cboCategory.Text = DGV.Cells[6].Value.ToString();
       

        }
        public override void update(DataGridView dg)
        {

            try
            {
                this.DGV=dg.SelectedRows[0];
                this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                this.SQL = "update tbProduct " +
                    "set Name=@Name,Barcode=@Barcode,SellPrice=@SellPrice,QtyStock=@Qty,Photo=@Photo," +
                    "CateogoryId=@Cid,Upadate=getDate(),UpdateBy=@UpdateBy where id=@id";
                Database.cmd=new SqlCommand(this.SQL,Database.con);
                Database.cmd.Parameters.AddWithValue("@id", this.Id);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Barcode", this.Barcode);
                Database.cmd.Parameters.AddWithValue("@SellPrice", this.Price);
                Database.cmd.Parameters.AddWithValue("@Qty", this.QtyInstock);
                Database.cmd.Parameters.AddWithValue("@Cid", this.CategoryId);
                Database.cmd.Parameters.AddWithValue("@UpdateBy", User.Id);
                Database.cmd.Parameters.AddWithValue("@Photo", this.Photo);

                this.ROwEffercted=Database.cmd.ExecuteNonQuery();
                Database.ad= new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                if (ROwEffercted == 1)
                {
                    MessageBox.Show("Update!");
                    dg.Rows.Clear();
                    this.loadData(dg);
                }

               

            } catch (Exception ex) {
                MessageBox.Show("Error:" + ex.Message);
            }
        }
        public override void search(DataGridView dg)
        {
            try
            {
             
               // this.SQL = "select * from View_ProductWithCategory where Name like CONCAT('%',@name,'%')";
               this.SQL= "select * from View_ProductWithCategory where  Barcode = @Barcode";
                Database.cmd= new SqlCommand (this.SQL,Database.con);
                Database.cmd.Parameters.AddWithValue("@Barcode", this.Barcode);
                Database.cmd.ExecuteNonQuery();
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);

                dg.Rows.Clear();
                foreach (DataRow r in Database.tbl.Rows)
                {



                    object[] row = { r["Id"], r["Name"], r["Barcode"], r["SellPrice"], r["QtyStock"], r["Photo"], r["Category"], r["CreateAt"], r["CreateBy"], r["upadate"], r["UpdateBy"] };
                    dg.Rows.Add(row);

                }
         
              

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }

        }
      
    }
    
}
