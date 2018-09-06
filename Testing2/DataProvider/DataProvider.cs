using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Testing2.Hash_salt;
using Testing2.Model;


namespace Testing2.DataProvider
{
    public class DataProvider : IDataProvider
    {
        String detail;
        private String check_mail;
        private String check_pass;
        private readonly String connectionString;
        public DataProvider()
        {
            connectionString = "Server=DESKTOP-ALMQ9QA\\SQLEXPRESS;Database=handallo;Trusted_Connection=True;MultipleActiveResultSets=true";
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
                string sQuery = "SELECT * FROM Customer"
                                + " WHERE CustomerId = @Id";
                dbConnection.Open();
               
                return dbConnection.Query<User>(sQuery, new {Id = CustomerId}).FirstOrDefault();
            }
        }

        public Boolean AddUser(User user)
        {
            var email = user.Email;

            user.Passw = HashSalt.Hash_Salt(user.Passw);

            using (IDbConnection dbConnection = Connection)
            {
                string sQuery0 = "SELECT FirstName FROM Customer WHERE Email = @email";

                dbConnection.Open();
                string result = dbConnection.QueryFirstOrDefault<String>(sQuery0, new { @Email = email });
                dbConnection.Close();

                if (String.IsNullOrEmpty(result))
                {
                    string sQuery = "INSERT INTO Customer(FirstName,LastName,Passw,Email)" +
                                    "VALUES(@firstName,@lastName,@passw,@email)";

                    dbConnection.Open();
                    dbConnection.Execute(sQuery, user);
                    return true;

                }

                else
                {
                    return false;
                }


            }
        }

        public Boolean UserLogin(Login login)
        {


            login.Password = HashSalt.Hash_Salt(login.Password);

            var o = login.Email;
            var i = login.Password;
            
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT FirstName FROM Customer WHERE Email = @Email AND Passw = @Password";
                dbConnection.Open();
                detail = dbConnection.QueryFirstOrDefault<String>(sQuery,new{@Email = o , @Password = i});


            }

            if (String.IsNullOrEmpty(detail))
            {
                return false;
            }
            else
            {
                return true;
            }
        } 


    }
}
