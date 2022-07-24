using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Avto_Run_Al_Store.MySql_Services
{
    public class MySql_Connector
    {
        private readonly MySqlConnection connection = new MySqlConnection("server=remotemysql.com;port=3306;username=wlPQk3C8bk;password=kdchouwNzR;database=wlPQk3C8bk");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                }
                catch
                { Environment.Exit(0); }
            }
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    connection.Close();
                }
                catch
                { Environment.Exit(0); }
            }
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
