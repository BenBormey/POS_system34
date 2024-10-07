using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Data
{
    internal class Database
    {
        public static SqlConnection con= new SqlConnection(@"Data Source = DESKTOP-KAJJLGQ\BORMEYBEN; Initial Catalog =Group2_Su34_c#1; User ID = sa; Password=123");

      //  public static SqlConnection con = new SqlConnection(@"Data Source = namesevar; Initial Catalog =Group2_Su34_c#1; User ID = name; Password=password);
        public static SqlDataAdapter ad;
        public static DataTable tbl;
        public static SqlCommand cmd;
        public static void connection()
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Closed) { 
                
                    con.Open();
                  
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr:" + ex.Message);
            }
        }


    }
}
