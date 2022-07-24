using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Avto_Run_Al_Store.MySql_Services
{
    public class MySql_Handler
    {
        //TODO: Оптимизировать запросы, сделать получение данных за один запрос
        private MySql_Connector My_Con;
        private MySqlCommand command;
        private DataTable Tab_Accounts_Db;
        private DataTable Tab_Programs_Db;
        private DataTable Tab_Al_Store_Properties_Db;
        private MySqlDataAdapter adapter;
        private MySqlDataReader reader;
        private string Activated_File_Text, Edition, User_Identyty, User_Login, User_Password, New_Ver_Store, Ver_Store, New_Ver_Updater, Ver_Updater;
        private int Count_Update_Al, Auto_Update, Update_Msg;

        public void Getting_Update_Data(string Source)
        {
            My_Con = new MySql_Connector();

            adapter = new MySqlDataAdapter();

            command = new MySqlCommand("SELECT * FROM `Tab_Al_Store_Db` WHERE `id` = 2; SELECT * FROM `Tab_Al_Store_Db` WHERE `id` = 3", My_Con.getConnection());

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (Edition == "'Standart'")
                {
                    New_Ver_Store = reader["version"].ToString();
                }
                else if (Edition == "'TPK'")
                {
                    New_Ver_Store = reader["TPK_version"].ToString();
                }
            }
            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    if (Edition == "'Standart'")
                    {
                        New_Ver_Updater = reader["version"].ToString();
                    }
                    else if (Edition == "'TPK'")
                    {
                        New_Ver_Updater = reader["TPK_version"].ToString();
                    }
                }
            }
            Getting_Version_Data(Source);
            if (Ver_Store != New_Ver_Store)  //TODO: Добавить проверку на то, новее ли на сервере версия
            {
                Count_Update_Al++;
            }
            else if (Ver_Updater != New_Ver_Updater)
            {
                Count_Update_Al++;
            }
            if (Count_Update_Al == 0)
                Environment.Exit(0);
            My_Con.closeConnection();
            Restoring_Authorization(Source);
        }

        private void Getting_Version_Data(string Source)
        {
            if (File.Exists(Source + "\\Al-Store\\Al-Store.exe"))
            {
                FileVersionInfo myFileVersionInfo_Store = FileVersionInfo.GetVersionInfo(Source + "\\Al-Store\\Al-Store.exe");
                Ver_Store = myFileVersionInfo_Store.FileVersion;
                if (Convert.ToInt32(Ver_Store.Split('.')[0]) != 0)
                { Ver_Store += ".Release"; }
                else if (Convert.ToInt32(Ver_Store.Split('.')[1]) != 0)
                { Ver_Store += ".Beta"; }
                else if (Convert.ToInt32(Ver_Store.Split('.')[2]) != 0)
                { Ver_Store += ".Alpha"; }
                else if (Convert.ToInt32(Ver_Store.Split('.')[3]) != 0)
                { Ver_Store += ".Pre-Alpha"; }
            }
            if (File.Exists(Source + "\\Updater\\Updater for Al-Store.exe"))
            {
                FileVersionInfo myFileVersionInfo_Store = FileVersionInfo.GetVersionInfo(Source + "\\Updater\\Updater for Al-Store.exe");
                Ver_Updater = myFileVersionInfo_Store.FileVersion;
                if (Convert.ToInt32(Ver_Updater.Split('.')[0]) != 0)
                { Ver_Updater += ".Release"; }
                else if (Convert.ToInt32(Ver_Updater.Split('.')[1]) != 0)
                { Ver_Updater += ".Beta"; }
                else if (Convert.ToInt32(Ver_Updater.Split('.')[2]) != 0)
                { Ver_Updater += ".Alpha"; }
                else if (Convert.ToInt32(Ver_Updater.Split('.')[3]) != 0)
                { Ver_Updater += ".Pre-Alpha"; }
            }
        }

        private void Restoring_Authorization(string Source)
        {
            if (File.Exists("C:\\Users\\" + User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated\\Activated_File.txt"))
            {
                Activated_File_Text = File.ReadAllText("C:\\Users\\" + User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated\\Activated_File.txt");
                User_Login = Activated_File_Text.Split(' ')[0]; User_Password = Activated_File_Text.Split(' ')[1];
            }
            else
            { Environment.Exit(0); }

            if (File.Exists(Source + "\\Edition\\TPK_Edition.txt"))
            {
                Edition = "'TPK'";
            }
            else if (!File.Exists(Source + "\\Edition\\TPK_Edition.txt"))
            {
                Edition = "'Standart'";
            }

            My_Con = new MySql_Connector();
            adapter = new MySqlDataAdapter();

            Tab_Accounts_Db = new DataTable();

            command = new MySqlCommand("SELECT * FROM `Tab_Accounts_Db` WHERE `login` = @login AND `pass` = @pass", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = User_Login; command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = User_Password;

            adapter.SelectCommand = command;
            adapter.Fill(Tab_Accounts_Db);

            if (Tab_Accounts_Db.Rows.Count == 0)
            {
                Getting_User_Data();
            }
            if (Tab_Accounts_Db.Rows.Count > 0)
            {
                Environment.Exit(0);
            }
        }

        public void Getting_User_Data()
        {
            My_Con = new MySql_Connector();
            adapter = new MySqlDataAdapter();

            Tab_Al_Store_Properties_Db = new DataTable(); Tab_Programs_Db = new DataTable();

            command = new MySqlCommand("SELECT * FROM `Tab_Al_Store_Properties_Db` WHERE `login` = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = User_Login;

            adapter.SelectCommand = command;
            adapter.Fill(Tab_Al_Store_Properties_Db);

            if (Tab_Al_Store_Properties_Db.Rows.Count == 0)
            {
                Environment.Exit(0);
            }
            else
            {
                reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Auto_Update = Convert.ToInt32(reader["Auto_Update"]);
                    Update_Msg = Convert.ToInt32(reader["Update_Msg"]);
                }
            }
            My_Con.closeConnection();

            if (Auto_Update == 1 && Update_Msg == 1)
            {
                Environment.Exit(0);
            }
        }
    }
}
