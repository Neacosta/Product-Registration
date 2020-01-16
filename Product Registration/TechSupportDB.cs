using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
namespace Product_Registration
{
    public static class TechSupportDB
    {

        public static SqlConnection GetConnection()
        {
            string connectionS = ConfigurationManager.ConnectionStrings["TechConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionS);
            return conn;
        }
    }
}
