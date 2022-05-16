using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater_For_Al_Store.MySql_Services
{
    public class MySql_Connector
    {
        readonly MySqlConnection connection = new MySqlConnection("server=remotemysql.com;port=3306;username=wlPQk3C8bk;password=kdchouwNzR;database=wlPQk3C8bk");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                }
                catch
                {
                    //MyMessageBox.ShowDialog(" Не удалось подключиться к серверам 'Aluminum-Company'. Проверьте ваше подключение к интернету или подождите несколько минут и повторите попытку. Подробнее об ошибке:\n" + ex.ToString(), MyMessageBox.Buttons.OK);
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
                catch
                {
                    //MyMessageBox.ShowDialog(" Не удалось закрыть подключение к серверам 'Aluminum-Company'. Проверьте ваше подключение к интернету или подождите несколько минут и повторите попытку. Подробнее об ошибке:\n" + ex.ToString(), MyMessageBox.Buttons.OK);
                }
            }
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
