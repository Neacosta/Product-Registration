using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Product_Registration
{
   public static class CustomerDB
   {
        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            SqlConnection connection = TechSupportDB.GetConnection();

            string select = "SELECT CustomerID, Name " +
                            "FROM Customers " +
                            "ORDER BY Name";
            SqlCommand SelectCommand = new SqlCommand(select, connection);


            try
            {
                connection.Open();
                SqlDataReader reader = SelectCommand.ExecuteReader();
                
                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.CustomerID = (int)reader["CustomerID"];
                    customer.Name = reader["Name"].ToString();
                    customers.Add(customer);
                }
                reader.Close();
            }   
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return customers;

        

        }
    }
}
