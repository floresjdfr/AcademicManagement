using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GestionAcademica.API.DataAccess
{
    public class Service
    {
        protected SqlConnection connection = null;
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=GestionAcademica;Persist Security Info=True;User ID=GestionAcademica;Password=123";
        
        public Service() { }
        protected bool Connect()
        {
            if (connection != null && connection.State == ConnectionState.Open) return true;
            else
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                return true;
            }
        }
        protected void Disconnect()
        {
            if (connection != null)
                connection.Close();
        }
        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
