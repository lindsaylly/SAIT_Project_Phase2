using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsPackages
{
    public static class TravelExpertsDB
    {
        public static SqlConnection GetConnection()
        {
            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\northwnd.mdf;Integrated Security=True;Connect Timeout=30";
            return new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = TravelExperts; Integrated Security = True");
        }
    }
}
