using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Product_Registration
{
   public static class RegistrationDB
   {

        public static bool AddRegistration(Registration registration)
        {
            SqlConnection connection = TechSupportDB.GetConnection();
            string insertStatement =
                "INSERT Registrations " +
                "(CustomerID, ProductCode, RegistrationDate) " +
                "VALUES (@CustomerID, @ProductCode, @RegistrationDate)";
            SqlCommand insertCommand =
                new SqlCommand(insertStatement, connection);


            insertCommand.Parameters.AddWithValue(
                "@CustomerID", registration.CustomerID);

            insertCommand.Parameters.AddWithValue(
                "@ProductCode", registration.ProductCode);
            insertCommand.Parameters.AddWithValue(
                "@RegistrationDate", registration.RegistrationDate);
            try
            {
                connection.Open();
                int count = insertCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();

            }


        }
    }
}  
