using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Testing2.Model;

namespace Testing2.DataProvider
{
    public class DataProvider : IDataProvider
    {
        private readonly String connectionString;
        public DataProvider()
        {
            connectionString = "Server=DESKTOP-ALMQ9QA\\SQLEXPRESS;Database=handallo;Trusted_Connection=True;MultipleActiveResultSets=true"; ;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public User GetUser(int CustomerId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM Customers"
                                + " WHERE CustomerId = @Id";
                dbConnection.Open();
                return dbConnection.Query<User>(sQuery, new {Id = CustomerId}).FirstOrDefault();
            }
        }
    }
}
