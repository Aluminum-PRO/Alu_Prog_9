using Alu_Prog_9.Services;
using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Alu_Prog_9.MySql_Services
{
    public class MySql_Connector
    {
        Errors_Saves_and_Sending errors_Saves_And_Sending = new Errors_Saves_and_Sending();
        private readonly MySqlConnection connection = new MySqlConnection("server=remotemysql.com;port=3306;username=wlPQk3C8bk;password=kdchouwNzR;database=wlPQk3C8bk");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    errors_Saves_And_Sending.Recording_Errors(ex);
                    if (ex.HResult == -2147467259)
                        MessageBox.Show(" Нет интернет соединения. Проверьте ваше подключение к интернету или подождите несколько минут и повторите попытку.\n\n " + ex.Message + "\n Код ошибки: " + ex.HResult, "Al-Store", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                        MessageBox.Show(" Не удалось открыть подключение к серверам 'Aluminum-Company'. Проверьте ваше подключение к интернету или подождите несколько минут и повторите попытку.\n\n " + ex.Message + "\n Код ошибки: " + ex.HResult, "Al-Store", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(0);
                }
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
                catch (Exception ex)
                {
                    errors_Saves_And_Sending.Recording_Errors(ex);
                    if (ex.HResult == -2147467259)
                        MessageBox.Show(" Нет интернет соединения. Проверьте ваше подключение к интернету или подождите несколько минут и повторите попытку.\n\n " + ex.Message + "\n Код ошибки: " + ex.HResult, "Al-Store", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else
                        MessageBox.Show(" Не удалось закрыть подключение к серверам 'Aluminum-Company'. Проверьте ваше подключение к интернету или подождите несколько минут и повторите попытку.\n\n " + ex.Message + "\n Код ошибки: " + ex.HResult, "Al-Store", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
