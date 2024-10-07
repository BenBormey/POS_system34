using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data;
using System.Runtime.CompilerServices;

namespace POS.Data
{
    internal class Sale : Product
    {
        public long SaleId { get; set; }
        public int CustomerId { get; set; }
        public string SqlSale { get; set; }
        public string SqlSaleDetail { get; set; }
        public string SqlUpdateStock { get; set; }



        public int QTY { get; set; }
        public double Amount()
        {
            return this.QTY * this.SellPrice;

        }
        public double TotableAmount(DataGridView dgsale)
        {
                double sum   = 0;  
           foreach(DataGridViewRow dgv in dgsale.Rows)
            {

                sum += double.Parse(dgv.Cells[5].Value.ToString());
            }
           return sum;  
        }
        public void ScanBarcode(DataGridView dgvSale, DataGridView dgvproduct,TextBox txtBarcode, Label lblTotalAmont)
        {
            try
            {
                this.SQL = "select * from  tbProduct where Barcode = @Barcode";
                Database.cmd= new SqlCommand(this.SQL, Database.con);
                this.Barcode = long.Parse(txtBarcode.Text.Trim());
                Database.cmd.Parameters.AddWithValue("@Barcode", this.Barcode);
               
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);
                if (Database.tbl.Rows.Count < 1)
                {

                    MessageBox.Show("Barcode Notfound!");
                    return;

                }

                foreach (DataRow dataRow in Database.tbl.Rows)
                {
                    this.QtyInstock =int.Parse(dataRow["QtyStock"].ToString());

                  //  r["name"].ToString()
                }
                if (this.QtyInstock <= 1)
                {
                    MessageBox.Show("QtyINstock Dotn't Enough");
                    return;
                }
                
               
                Database.cmd.ExecuteNonQuery();
                //foreach (DataGridViewRow dgv in dgvproduct.Rows)
                //{

                //    if (int.Parse(dgv.Cells[4].Value.ToString()) == 0)
                //    {
                //        MessageBox.Show("QTY in Stock Insufficient product");
                //        return;
                //    }
                //}
            
                
               
                this.Barcode = long.Parse(Database.tbl.Rows[0]["Barcode"].ToString());
                this.Id = int.Parse(Database.tbl.Rows[0]["id"].ToString());
                this.Name = Database.tbl.Rows[0]["Name"].ToString();
                this.QTY = 1;
                this.SellPrice = long.Parse(Database.tbl.Rows[0]["sellPrice"].ToString());
              

                object[] rows = { this.Barcode, this.Id, this.Name, this.QTY, this.SellPrice, this.Amount() };
                foreach (DataGridViewRow dgv in dgvSale.Rows)
                {
                    if (this.QtyInstock <= int.Parse(dgv.Cells[3].Value.ToString()))
                    {
                        MessageBox.Show("QTY in Stock Insufficient product");
                        return;
                    }
                    long checkBarcode = long.Parse(dgv.Cells[0].Value.ToString());
                    if(checkBarcode == this.Barcode)
                    {

                     
                        this.QTY += int.Parse(dgv.Cells[3].Value.ToString());
                        dgv.Cells[3].Value = this.QTY;
                        dgv.Cells[5].Value = this.Amount();
                        lblTotalAmont.Text = this.TotableAmount(dgvSale).ToString();


                        return ;
                      
                    }
                    
                }
                
                dgvSale.Rows.Add(rows);
                lblTotalAmont.Text = this.TotableAmount(dgvSale).ToString();




            }
            catch (Exception ex)
            {

                MessageBox.Show("Erorr:" + ex.Message);
            }
        }
        public void  CommitData(DataGridView dgSale, Label lblAmount, TextBox txtCashRecieve, Label lblCashReturn)
        {
                SqlTransaction sqlTransaction = null;
            try
            {
                sqlTransaction = Database.con.BeginTransaction();
                this.SqlSale = "insert into tbSale values(11,GETDATE(),@UserId,@TotalAmount)select SCOPE_IDENTITY();";
    //            Database.cmd.Parameters.AddWithValue("@customerId", this.CustomerId);
                Database.cmd= new SqlCommand(this.SqlSale, Database.con,sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@UserId", User.Id);
                Database.cmd.Parameters.AddWithValue("@TotalAmount", double.Parse(lblAmount.Text));
                this.SaleId = Convert.ToInt64(Database.cmd.ExecuteScalar());

                foreach (DataGridViewRow dgv in dgSale.Rows) {

                    this.Id = int.Parse(dgv.Cells[1].Value.ToString());
                    this.QTY = int.Parse(dgv.Cells[3].Value.ToString());
                    this.SellPrice = double.Parse(dgv.Cells[4].Value.ToString());
                    this.SqlSaleDetail = "insert into tbSaleDetail values (@SaleId,@ProductId,@QTY,@SellPrice,@Amount);"; 
                    Database.cmd= new SqlCommand(
                        this.SqlSaleDetail,Database.con,sqlTransaction
                        );

                    Database.cmd.Parameters.AddWithValue("@SaleId", this.SaleId);
                    Database.cmd.Parameters.AddWithValue("@ProductId", this.Id);
                    Database.cmd.Parameters.AddWithValue("@QTY", this.QTY);
                    Database.cmd.Parameters.AddWithValue("@SellPrice", this.SellPrice);
                    Database.cmd.Parameters.AddWithValue("@Amount", this.Amount());
                    Database.cmd.ExecuteNonQuery();


                    this.SqlUpdateStock = "update tbProduct set QtyStock = QtyStock - @QTY  where id= @id";
                    Database.cmd = new SqlCommand(this.SqlUpdateStock, Database.con, sqlTransaction);
                    Database.cmd.Parameters.AddWithValue("@QTY" , this.QTY);
                    Database.cmd.Parameters.AddWithValue("@id", this.Id);
                    Database.cmd.ExecuteNonQuery();

                
                }

                sqlTransaction.Commit();
              PrintReportSale(SaleId,lblAmount,txtCashRecieve,lblCashReturn);
                dgSale.Rows.Clear();
                txtCashRecieve.Text = "";
                lblCashReturn.Text = "";
                lblAmount.Text = " ";
                


                MessageBox.Show("Sale Commit"+ this.SaleId);
                
            }catch(Exception ex)
            {
                MessageBox.Show($"error {ex.Message}");

            }

        }
        public static void PrintReportSale(long SaleId, Label lblAmount, TextBox txtCashRecieve, Label lblCashReturn)
        {
            Microsoft.Office.Interop.Excel.Application Xa = new
            Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook Wb;
            Microsoft.Office.Interop.Excel.Worksheet Ws;
            try
            {
                string sqlPrint = "select * from VIEW_SALE_TO_Customer where id =@id";
                Database.cmd = new SqlCommand(sqlPrint, Database.con);
                Database.cmd.Parameters.AddWithValue("@id", SaleId);
                Database.cmd.ExecuteNonQuery();
                Database.ad = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ad.Fill(Database.tbl);

                string pathFileReport = Application.StartupPath + @"\reports\C#.xlsx";
                Wb = Xa.Workbooks.Open(pathFileReport, false, false, true);
                //get excel sheet
                Ws = Wb.Worksheets["Sheet1"];
                int row = 11;
                int i = 1;

                Ws.Cells[7, 3] = SaleId;
                Ws.Cells[8, 3] = Database.tbl.Rows[0]["userName"];
                Ws.Cells[7, 6] = Database.tbl.Rows[0]["SaleDate"];
                foreach (DataRow r in Database.tbl.Rows)
                {
                    Ws.Cells[row, 1] = i;
                    Ws.Cells[row, 2] = r["Name"].ToString();
                    Ws.Cells[row, 4] = r["Qty"].ToString();
                    Ws.Cells[row, 5] = r["Price"].ToString();
                    i++;
                    row++;
                }

                //set hide row excel
                for (int j = 12; j <= 35; j++)
                {
                    string check = Convert.ToString(Ws.Cells[j, 2].Text);
                    if (check.Equals(""))
                    {
                        Ws.Rows[j].Hidden = true;
                    }


                }
                Ws.Cells[36, 6] = lblAmount.Text;
                Ws.Cells[37, 6] = txtCashRecieve.Text;
                Ws.Cells[38, 6] = lblCashReturn.Text;
                //autofit column in worksheet
                Ws.Columns.AutoFit();
                //show excel application
                Xa.Visible = true;
                //print preivew excel sheet
                // Ws.PrintPreview();
                //print out doc from worksheet
                int pageFrom = 1, pageTo = 1, noCopy = 2;
                Ws.PrintOutEx(pageFrom, pageTo, noCopy);

                // Wb.Close(false); //false mean close ingore save
                //quit application
                // Xa.Quit();
                //clear all excel object
                Ws = null; Wb = null; Xa = null;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }


        ////  public  int GetCustomerId(TextBox txtCustmerIds)
        //  {
        //      try
        //      {
        //          this.SQL = "select id from tbCustomer";
        //          Database.cmd = new SqlCommand(this.SQL, Database.con);
        //          Database.cmd.ExecuteNonQuery();
        //          Database.ad = new SqlDataAdapter(Database.cmd);
        //          Database.tbl = new DataTable();
        //          Database.ad.Fill(Database.tbl);
        //          foreach (Database i in Database.tbl.Rows) {

        //              this.CustomerId = int.Parse(Database.tbl.Rows[0]["id"].ToString());



        //          }



        //        }

        //        catch (Exception e)
        //        {

        //            MessageBox.Show("Erorr :" + e.Message);

        //        }
        //        return this.CustomerId; 


        //    }
    }
}
