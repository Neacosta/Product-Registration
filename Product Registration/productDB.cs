using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Product_Registration
{
    public static class ProductDB
    {
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            SqlConnection connection = TechSupportDB.GetConnection();
            string selectStatement = "SELECT ProductCode, Name "
                                   + "FROM Products "
                                   + "ORDER BY Name";
            SqlCommand selectCommand =
                new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    Product p = new Product();

                    p.ProductCode = reader["ProductCode"].ToString();
                    p.Name = reader["Name"].ToString();
                    products.Add(p);

                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return products;
        }
    }
}
