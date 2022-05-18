using Alu_Prog_9.Classes;
using Alu_Prog_9.Services;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Alu_Prog_9.MySql_Services
{
    public class MySql_Handler
    {
        MySql_Connector My_Con;
        MySqlCommand command;
        DataTable Tab_Accounts_Db; DataTable Tab_Programs_Db; DataTable Tab_Al_Store_Properties_Db;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        Handler handler;

        string Name, SurName, News_new_Users;
        int app_id, app_have_application;
        string app_type, app_name, app_program_name, app_version;

        int have_application;
        string program_name, version, reference;
        BitmapImage image, image_1, image_2, image_3, image_4;


        public void Login(string Login_User, string Password_User, out string Login_Bool)
        {
            My_Con = new MySql_Connector(); /*My_Hand = new MySql_Handler();*/
            adapter = new MySqlDataAdapter();

            Tab_Accounts_Db = new DataTable();

            command = new MySqlCommand("SELECT * FROM `Tab_Accounts_Db` WHERE `login` = @login AND `pass` = @pass", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Login_User; command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = Password_User;

            adapter.SelectCommand = command;
            adapter.Fill(Tab_Accounts_Db);

            if (Tab_Accounts_Db.Rows.Count > 0)
            {
                Properties.Settings.Default.User_Login = Login_User;
                Properties.Settings.Default.User_Password = Password_User;

                Properties.Settings.Default.Save();

                Login_Bool = "True";
            }
            else
            {
                Login_Bool = "False";
            }
        }

        public void Restoring_Authorization(out string Restoring_Authorization_Bool)
        {
            My_Con = new MySql_Connector();
            adapter = new MySqlDataAdapter();

            Tab_Accounts_Db = new DataTable();

            command = new MySqlCommand("SELECT * FROM `Tab_Accounts_Db` WHERE `login` = @login AND `pass` = @pass", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login; command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Password;

            adapter.SelectCommand = command;
            adapter.Fill(Tab_Accounts_Db);

            if (Tab_Accounts_Db.Rows.Count > 0)
            {
                Tab_Programs_Db = new DataTable();
                adapter = new MySqlDataAdapter();

                command = new MySqlCommand("SELECT * FROM `Tab_Programs_Db` WHERE `login` = @login AND `The_15_Puzzle` = 1", My_Con.getConnection()); //TODO: Что это тут делает `The_15_Puzzle`
                command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

                adapter.SelectCommand = command;
                adapter.Fill(Tab_Programs_Db);

                if (Tab_Programs_Db.Rows.Count > 0)
                {
                    Properties.Settings.Default.Authorization = 1; Restoring_Authorization_Bool = "True";

                    Properties.Settings.Default.Save();
                }
                else
                {
                    Restoring_Authorization_Bool = "False";
                }
            }
            else
            {
                Restoring_Authorization_Bool = "False";
            }
        }

        public void Download_DB()
        {
            My_Con = new MySql_Connector();

            Tab_Accounts_Db = new DataTable(); Tab_Programs_Db = new DataTable();
            adapter = new MySqlDataAdapter();
            command = new MySqlCommand("SELECT * FROM `Tab_Applications_Db`, `Tab_Programs_Db` WHERE Tab_Programs_Db.login = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                byte[] blob = (byte[])(reader["image_main"]);
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(blob))
                {
                    BitmapImage imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = ms;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();
                    image = imageSource;
                }
                for (int i = 1; i < 5; i++)
                {
                    blob = (byte[])(reader["image_" + i]);

                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(blob))
                    {
                        BitmapImage imageSource = new BitmapImage();
                        imageSource.BeginInit();
                        imageSource.StreamSource = ms;
                        imageSource.CacheOption = BitmapCacheOption.OnLoad;
                        imageSource.EndInit();
                        if (i == 1)
                        {
                            image_1 = imageSource;
                        }
                        else if (i == 2)
                        {
                            image_2 = imageSource;
                        }
                        else if (i == 3)
                        {
                            image_3 = imageSource;
                        }
                        else if (i == 4)
                        {
                            image_4 = imageSource;
                        }
                    }
                }
                if (Properties.Settings.Default.Edition == "'Standart'")
                {
                    version = reader["version"].ToString();
                    reference = reader["reference"].ToString();

                }
                else if (Properties.Settings.Default.Edition == "'TPK'")
                {
                    version = reader["version_TPK_Ed"].ToString();
                    reference = reader["reference_TPK_Ed"].ToString();
                }
                program_name = reader["program_name"].ToString();

                try
                { have_application = Convert.ToInt32(reader[program_name]); }
                catch
                { have_application = 0; }
                StaticVars.Application.Add(new StaticVars.DataBase()
                {
                    id = Convert.ToInt32(reader["id"]),
                    type = reader["type"].ToString(),
                    size = Convert.ToDouble(reader["size"]),
                    name = reader["name"].ToString(),
                    program_name = reader["program_name"].ToString(),
                    image = this.image,
                    image_1 = this.image_1,
                    image_2 = this.image_2,
                    image_3 = this.image_3,
                    image_4 = this.image_4,
                    version = this.version,
                    reference = this.reference,
                    have_application = this.have_application,
                    description = reader["description"].ToString(),
                    shortcut_description = reader["shortcut_description"].ToString(),
                    hot_key = reader["hot_key"].ToString(),
                    price = Convert.ToDouble(reader["price"]),
                });
            }
            My_Con.closeConnection();

            My_Con = new MySql_Connector();

            adapter = new MySqlDataAdapter();
            command = new MySqlCommand("SELECT * FROM `Tab_Soft_Db` ORDER BY soft_name asc", My_Con.getConnection());

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                byte[] blob = (Byte[])reader["soft_image"];
                using (MemoryStream ms = new MemoryStream(blob))
                {
                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = ms;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();
                    image = imageSource;
                }
                StaticVars.Soft.Add(new StaticVars.DataBase()
                {
                    id = Convert.ToInt32(reader["soft_id"]),
                    file_type = reader["soft_file_type"].ToString(),
                    file_size = Convert.ToDouble(reader["soft_file_size"]),
                    name = reader["soft_program_name"].ToString(),
                    program_name = reader["soft_program_name"].ToString(),
                    pass = reader["soft_pass"].ToString(),
                    image = this.image,
                    reference = reader["soft_reference"].ToString()
                });
                StaticVars.Count_Soft++;
            }
            My_Con.closeConnection();
        }

        public void Getting_Data()
        {
            My_Con = new MySql_Connector();

            Tab_Accounts_Db = new DataTable();
            adapter = new MySqlDataAdapter();

            command = new MySqlCommand("SELECT * FROM `Tab_Accounts_Db` WHERE `id` = 1", My_Con.getConnection());

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Properties.Settings.Default.Pass_for_Email = reader["pass_for_email"].ToString();
            }

            My_Con.closeConnection();

            Properties.Settings.Default.Save();
        }

        public void Getting_User_Data()
        {
            My_Con = new MySql_Connector(); /*My_Hand = new MySql_Handler();*/
            adapter = new MySqlDataAdapter();

            Tab_Accounts_Db = new DataTable();

            command = new MySqlCommand("SELECT * FROM `Tab_Accounts_Db` WHERE `login` = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

            adapter.SelectCommand = command;
            adapter.Fill(Tab_Accounts_Db);

            if (Tab_Accounts_Db.Rows.Count > 0)
            { }
            else if (Tab_Accounts_Db.Rows.Count == 0)
            {
                MessageBox.Show(" Ваш аккаунт был удалён или произошла внутренняя ошибка.", "Авторизация");
                Properties.Settings.Default.Authorization = 0;
                Properties.Settings.Default.Save();
                if (File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated\\Activated_File.txt"))
                {
                    File.Delete("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated\\Activated_File.txt");
                }
                return;
            }

            command = new MySqlCommand("SELECT * FROM `Tab_Accounts_Db` WHERE `login` = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Properties.Settings.Default.User_Email = reader["email"].ToString();
                Properties.Settings.Default.User_Id = Convert.ToInt32(reader["id"]);
                Properties.Settings.Default.User_Money = Convert.ToDouble(reader["money"]);
                Properties.Settings.Default.User_Role = reader["role"].ToString();
                StaticVars.Soft_License = Convert.ToInt32(reader["soft_license"]);
                StaticVars.Bot_License = Convert.ToInt32(reader["bot_license"]);
                Properties.Settings.Default.User_Name = reader["name"].ToString();
                Properties.Settings.Default.User_SurName = reader["surname"].ToString();
                Properties.Settings.Default.User_Phone = reader["phone"].ToString();
                Properties.Settings.Default.User_Gender = Convert.ToInt32(reader["gender"]);
                Properties.Settings.Default.User_Birthday_Day = Convert.ToInt32(reader["birthday_day"]);
                Properties.Settings.Default.User_Birthday_Month = reader["birthday_month"].ToString();
                Properties.Settings.Default.User_Birthday_Year = Convert.ToInt32(reader["birthday_year"]);
                Properties.Settings.Default.User_Mailing = Convert.ToInt32(reader["mailing"]);
            }

            My_Con.closeConnection();
            Properties.Settings.Default.Save();

            if (!Directory.Exists(Properties.Settings.Default.Path_Programs))
            {
                Directory.CreateDirectory(Properties.Settings.Default.Path_Programs);
            }
            if (!Directory.Exists(Properties.Settings.Default.Path_Games))
            {
                Directory.CreateDirectory(Properties.Settings.Default.Path_Games);
            }
            if (!Directory.Exists(Properties.Settings.Default.Path_Soft))
            {
                Directory.CreateDirectory(Properties.Settings.Default.Path_Soft);
            }

            if (!Directory.Exists(Properties.Settings.Default.Path_Tests) && (Properties.Settings.Default.User_Login.ToLower() == "admin" || Properties.Settings.Default.User_Role == "Tester"))
            {
                Directory.CreateDirectory(Properties.Settings.Default.Path_Tests);
            }
        }

        public void Getting_User_Bot_License()
        {
            My_Con = new MySql_Connector(); /*My_Hand = new MySql_Handler();*/
            adapter = new MySqlDataAdapter();

            Tab_Accounts_Db = new DataTable();

            command = new MySqlCommand("SELECT * FROM `Tab_Accounts_Db` WHERE `login` = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                StaticVars.Bot_License = Convert.ToInt32(reader["bot_license"]);
            }

            My_Con.closeConnection();
        }

        public void Get_Update_Information_Application()
        {
            My_Con = new MySql_Connector();

            Tab_Accounts_Db = new DataTable(); Tab_Programs_Db = new DataTable();
            adapter = new MySqlDataAdapter();
            command = new MySqlCommand("SELECT * FROM `Tab_Applications_Db`, `Tab_Programs_Db` WHERE Tab_Programs_Db.login = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                app_id = Convert.ToInt32(reader["id"]);
                app_type = reader["type"].ToString();
                app_name = reader["name"].ToString();
                app_program_name = reader["program_name"].ToString();
                if (Properties.Settings.Default.Edition == "'Standart'")
                {
                    app_version = reader["version"].ToString();
                }
                else if (Properties.Settings.Default.Edition == "'TPK'")
                {
                    app_version = reader["version_TPK_Ed"].ToString();
                }

                try
                { app_have_application = Convert.ToInt32(reader[app_program_name]); }
                catch
                { app_have_application = 0; }

                if (app_have_application == 1)
                {
                    handler = new Handler();
                    handler.Get_Update_Information_Loaded(app_id, app_type, app_name, app_program_name, app_version);
                }
            }
            My_Con.closeConnection();
        }

        public void Get_Update_Information_Al_Store()
        {
            My_Con = new MySql_Connector();

            Tab_Accounts_Db = new DataTable(); Tab_Programs_Db = new DataTable();
            adapter = new MySqlDataAdapter();

            command = new MySqlCommand("SELECT * FROM `Tab_Al_Store_Db` WHERE `id` = 1", My_Con.getConnection());

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (Properties.Settings.Default.Edition == "'Standart'")
                {
                    Properties.Settings.Default.New_Ver_Store = reader["version_store"].ToString();
                    Properties.Settings.Default.New_Ver_Updater = reader["version_updater"].ToString();
                }
                else if (Properties.Settings.Default.Edition == "'TPK'")
                {
                    Properties.Settings.Default.New_Ver_Store = reader["version_store_TPK_Ed"].ToString();
                    Properties.Settings.Default.New_Ver_Updater = reader["version_updater_TPK_Ed"].ToString();
                }
            }
            My_Con.closeConnection();

            if (Properties.Settings.Default.Ver_Store != Properties.Settings.Default.New_Ver_Store)  //TODO: Добавить проверку на то, новее ли на сервере версия
            {
                StaticVars.Count_Update_Al++;
            }
            else if (Properties.Settings.Default.Ver_Updater != Properties.Settings.Default.New_Ver_Updater)
            {
                StaticVars.Count_Update_Al++;
            }

        }

        public void Get_Application(string program_name, out string Bool)
        {
            My_Con = new MySql_Connector();

            Tab_Programs_Db = new DataTable();
            adapter = new MySqlDataAdapter();

            command = new MySqlCommand("UPDATE `Tab_Programs_Db` SET `" + program_name + "` = @program_name WHERE `Tab_Programs_Db`.`login` = @login", My_Con.getConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login; command.Parameters.Add("@program_name", MySqlDbType.Int32).Value = 1;
            My_Con.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                Bool = "True";
            }
            else
            {
                Bool = "False";
                MessageBox.Show(" Не удалось добавить программу в вашу библиотеку. Проверьте подключение к интернету или обратитесь к разработчику за помощью. \n Aluminum.Company163@gmail.com или Aluminum.Company163.reserve@gmail.com", "Ошибка!");
            }

            My_Con.closeConnection();
        }

        public void Checking_For_Account_Creation()
        {
            Tab_Al_Store_Properties_Db = new DataTable();

            command = new MySqlCommand("SELECT * FROM `Tab_Al_Store_Properties_Db` WHERE `login` = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

            adapter.SelectCommand = command;
            adapter.Fill(Tab_Al_Store_Properties_Db);

            if (Tab_Al_Store_Properties_Db.Rows.Count == 0)
            {
                command = new MySqlCommand("INSERT INTO `Tab_Al_Store_Properties_Db` (`id`, `login`) " +
                    "VALUES (@id, @login)", My_Con.getConnection());

                command.Parameters.Add("@id", MySqlDbType.Int32).Value = Properties.Settings.Default.User_Id;
                command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;
                My_Con.openConnection();

                if (command.ExecuteNonQuery() == 1)
                { }
                else
                { }

                My_Con.closeConnection();
            }

            Properties.Settings.Default.Save();

            Tab_Programs_Db = new DataTable();

            command = new MySqlCommand("SELECT * FROM `Tab_Programs_Db` WHERE `login` = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

            adapter.SelectCommand = command;
            adapter.Fill(Tab_Programs_Db);

            if (Tab_Programs_Db.Rows.Count == 0)
            {
                command = new MySqlCommand("INSERT INTO `Tab_Programs_Db` (`id`, `login`, `pass`) " +
                    "VALUES (@id, @login, @password)", My_Con.getConnection());

                command.Parameters.Add("@id", MySqlDbType.Int32).Value = Properties.Settings.Default.User_Id;
                command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Password;
                My_Con.openConnection();

                if (command.ExecuteNonQuery() == 1)
                { }
                else
                { }

                My_Con.closeConnection();
            }
        }

        public void Getting_User_Properties()
        {
            My_Con = new MySql_Connector();
            adapter = new MySqlDataAdapter();

            Tab_Al_Store_Properties_Db = new DataTable();

            command = new MySqlCommand("SELECT * FROM `Tab_Al_Store_Properties_Db` WHERE `login` = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Properties.Settings.Default.Start_Creating_Shortcut = Convert.ToInt32(reader["Start_Creating_Shortcut"]);
            }

            My_Con.closeConnection();
        }

        public void Getting_Information_of_Update_Al(out double size, out string reference)
        {
            My_Con = new MySql_Connector();
            adapter = new MySqlDataAdapter();

            Tab_Al_Store_Properties_Db = new DataTable();
            command = new MySqlCommand("SELECT * FROM `Tab_Al_Store_Db` WHERE `id` = 1", My_Con.getConnection());
            size = 0;
            reference = "";

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                size = Convert.ToDouble(reader["size_updater"]);
                if (Properties.Settings.Default.Edition == "'Standart'")
                {
                    reference = reader["reference_updater"].ToString();
                }
                else if (Properties.Settings.Default.Edition == "'TPK'")
                {
                    reference = reader["reference_updater_TPK_Ed"].ToString();
                }
            }
            My_Con.closeConnection();
        }

        public void Save_Start_Creating_Shortcut_Properties()
        {
            My_Con = new MySql_Connector();
            command = new MySqlCommand("UPDATE `Tab_Al_Store_Properties_Db` SET `Start_Creating_Shortcut` = @Start_Creating_Shortcut" +
                " WHERE `Tab_Al_Store_Properties_Db`.`login` = @login",
                My_Con.getConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;
            command.Parameters.Add("@Start_Creating_Shortcut", MySqlDbType.Int32).Value = Properties.Settings.Default.Start_Creating_Shortcut;

            My_Con.openConnection();

            if (command.ExecuteNonQuery() == 1)
            { }
            else
            {
                MessageBox.Show(" Не удалось обновить данные. Проверьте подключение к интернету или обратитесь к разработчику за помощью. \n Aluminum.Company163@gmail.com или Aluminum.Company163.reserve@gmail.com", "Ошибка!");
            }

            My_Con.closeConnection();
        }

        public void Send_Comment(int application_id, int user_id, string name, string surname, string comment_text)
        {
            My_Con = new MySql_Connector(); /*My_Hand = new MySql_Handler();*/
            adapter = new MySqlDataAdapter();

            command = new MySqlCommand("INSERT INTO `Tab_Applications_Comments_Db` (`application_id`, `user_id`, `name`, `surname`, `comment_text`, `time_send`) VALUES (@application_id, @user_id, @name, @surname, @comment_text, @time_send)", My_Con.getConnection());

            command.Parameters.Add("@application_id", MySqlDbType.Int32).Value = application_id;
            command.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surname;
            command.Parameters.Add("@comment_text", MySqlDbType.VarChar).Value = comment_text;
            command.Parameters.Add("@time_send", MySqlDbType.DateTime).Value = DateTime.Now;

            My_Con.openConnection();

            if (command.ExecuteNonQuery() == 1)
            { }
            else
            { MessageBox.Show(" Не удалось отправить комментарий. Проверьте подключение к интернету или обратитесь к разработчику за помощью. \n Aluminum.Company163@gmail.com или Aluminum.Company163.reserve@gmail.com", "Ошибка!"); }

            My_Con.closeConnection();
        }

        public void Get_Bot_Data(out int id, out string type, out double size, out string name, out string program_name, out BitmapImage image, out string description, out string clicker_path, out double price, out string version, out string reference)
        {
            My_Con = new MySql_Connector();

            Tab_Accounts_Db = new DataTable(); Tab_Programs_Db = new DataTable();
            adapter = new MySqlDataAdapter();
            command = new MySqlCommand("SELECT * FROM `Tab_Applications_Db`, `Tab_Programs_Db` WHERE Tab_Programs_Db.login = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            id = 0; type = ""; size = 0; name = ""; program_name = ""; image = new BitmapImage(); description = ""; clicker_path = ""; price = 0; version = ""; reference = "";

            while (reader.Read())
            {
                id = Convert.ToInt32(reader["id"]);
                type = reader["type"].ToString();
                size = Convert.ToDouble(reader["size"]);
                name = reader["name"].ToString();
                program_name = reader["program_name"].ToString();
                byte[] blob = (Byte[])(reader["image_main"]);
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(blob))
                {
                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = ms;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();
                    image = imageSource;
                }
                description = reader["description"].ToString();
                clicker_path = reader["shortcut_description"].ToString();
                price = Convert.ToDouble(reader["price"]);
                if (Properties.Settings.Default.Edition == "'Standart'")
                {
                    version = reader["version"].ToString();
                    reference = reader["reference"].ToString();
                }
                else if (Properties.Settings.Default.Edition == "'TPK'")
                {
                    version = reader["version_TPK_Ed"].ToString();
                    reference = reader["reference_TPK_Ed"].ToString();
                }
            }
            My_Con.closeConnection();
        }

        public void Registration_Code(string Email_User, string Name_User, string SurName_User, out string Pass)
        {
            if (IsEmailExists(Email_User))
            {
                MessageBoxResult result = MessageBox.Show(" Пользователь с такой почтой уже существует. Нажмите 'Ок', для перехода в окно авторизации, или 'Отмена'", "Al-Store", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    Pass = "@";
                }
                else
                { Pass = ""; }

                return;
            }

            Pass = "";
            int Pass_;
            Random Code = new Random();
            for (int i = 0; i <= 3; i++)
            {
                Pass_ = Code.Next(0, 10);
                Pass += Pass_.ToString();
            }

            MailAddress fromAdress1 = new MailAddress("Aluminum.Company163@gmail.com", "Al-Store");
            MailAddress fromAdress2 = new MailAddress("Aluminum.Company163.reserve@gmail.com", "Al-Store");
            string From = Name_User + " " + SurName_User;
            MailAddress toAdress = new MailAddress(Email_User, From);
            MailAddress toAdress2 = new MailAddress("Aluminum.Company163@gmail.com", "Админ");
            MailMessage message1 = new MailMessage(fromAdress1, toAdress)
            {
                Subject = "Подтверждение почты",
                Body = "     Здравствуйте " + From + ", спасибо, что регистрируетесь в сети 'Aluminum-Company'.\n" +
                " Если это не вы, то не сообщайте никому этот код.\n" +
                "    Подтвердите свою почту, введя вот этот код: " + Pass
            };

            MailMessage message2 = new MailMessage(fromAdress2, toAdress)
            {
                Subject = "Подтверждение почты",
                Body = "     Здравствуйте " + From + ", спасибо, что регистрируетесь в сети 'Aluminum-Company'.\n" +
                " Если это не вы, то не сообщайте никому этот код.\n" +
                "    Подтвердите свою почту, введя вот этот код: " + Pass
            };

            MailMessage message3 = new MailMessage(fromAdress2, toAdress2)
            {
                Subject = "Ошибка на почте",
                Body = " Основная почта перестала работать, необходимо возобновить её работоспособность."
            };

            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAdress1.Address, Properties.Settings.Default.Pass_for_Email)
            };

            try
            {
                smtpClient.Send(message1);
            }
            catch (SmtpException)
            {
                smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAdress2.Address, Properties.Settings.Default.Pass_for_Email)
                };
                try
                {
                    smtpClient.Send(message2);
                    smtpClient.Send(message3);
                }
                catch
                {
                    MessageBox.Show(" Не удалось отправить код подтверждения. Проверьте подключение к интернету или обратитесь к разработчику за помощью.", "Ошибка!");
                }
            }
        }

        public void Registration(string Email, string Name, string SurName, int Gender, string Login, string Password, int Mailing, out string Result)
        {
            if (IsLoginExists(Login))
            {
                Result = "Login";
                return;
            }

            My_Con = new MySql_Connector();
            command = new MySqlCommand("INSERT INTO `Tab_Accounts_Db` (`login`, `pass`, `money`, `email`, `email_small`, `name`, `surname`, " +
                "`phone`, `gender`, `birthday_day`, `birthday_month`, `birthday_year`, `mailing`, `pass_for_email`) " +
                "VALUES (@login, @password, @money, @email, @email_small, @name, @surname, @phone, @gender, @bir_d, @bir_m, @bir_y, @mailing, NULL)", My_Con.getConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Login;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = Password;
            command.Parameters.Add("@money", MySqlDbType.Int32).Value = 0;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = Email;
            command.Parameters.Add("@email_small", MySqlDbType.VarChar).Value = Email.ToLower();
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = Name;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = SurName;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = "Не указано";
            command.Parameters.Add("@gender", MySqlDbType.Int32).Value = Gender;
            command.Parameters.Add("@bir_d", MySqlDbType.VarChar).Value = 0;
            command.Parameters.Add("@bir_m", MySqlDbType.VarChar).Value = "Не указано";
            command.Parameters.Add("@bir_y", MySqlDbType.VarChar).Value = 0;
            command.Parameters.Add("@mailing", MySqlDbType.Int32).Value = Mailing;

            My_Con.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                Properties.Settings.Default.User_Login = Login;
                Properties.Settings.Default.User_Password = Password;

                My_Con = new MySql_Connector();
                Tab_Accounts_Db = new DataTable();
                adapter = new MySqlDataAdapter();

                command = new MySqlCommand("SELECT * FROM `Tab_Al_Store_Db` WHERE `id` = @id", My_Con.getConnection());
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = 1;

                My_Con.openConnection();

                reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    News_new_Users = reader["news_new_users"].ToString();
                }

                My_Con.closeConnection();

                My_Con = new MySql_Connector();
                command = new MySqlCommand("UPDATE `Tab_Al_Store_Db` SET `news_new_users` = @news_new_users WHERE `Tab_Al_Store_Db`.`id` = 1", My_Con.getConnection());
                command.Parameters.Add("@news_new_users", MySqlDbType.VarChar).Value = " " + Name + " " + SurName + "\n" + News_new_Users;

                My_Con.openConnection();
                if (command.ExecuteNonQuery() == 1)
                { }
                else
                { }
                My_Con.closeConnection();

                Properties.Settings.Default.Save();

                Result = "True";
            }
            else
            { Result = "False"; }

            My_Con.closeConnection();

        }

        public bool IsEmailExists(string Email_)
        {
            My_Con = new MySql_Connector();
            Tab_Accounts_Db = new DataTable();
            adapter = new MySqlDataAdapter();

            command = new MySqlCommand("SELECT * FROM `Tab_Accounts_Db` WHERE `email_small` = @email_small", My_Con.getConnection());
            command.Parameters.Add("@email_small", MySqlDbType.VarChar).Value = Email_.ToLower();

            adapter.SelectCommand = command;
            adapter.Fill(Tab_Accounts_Db);

            if (Tab_Accounts_Db.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool IsLoginExists(string Login)
        {
            My_Con = new MySql_Connector();
            Tab_Accounts_Db = new DataTable();
            adapter = new MySqlDataAdapter();

            command = new MySqlCommand("SELECT * FROM `Tab_Accounts_Db` WHERE `login` = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Login;

            adapter.SelectCommand = command;
            adapter.Fill(Tab_Accounts_Db);

            if (Tab_Accounts_Db.Rows.Count > 0)
            {

                return true;
            }
            else
            {
                return false;
            }

        }

        public void Recovery_Pass(string Email_User, out string Pass, out string Login)
        {
            My_Con = new MySql_Connector();

            Pass = "";
            Login = "";

            Tab_Accounts_Db = new DataTable();
            adapter = new MySqlDataAdapter();

            command = new MySqlCommand("SELECT * FROM `Tab_Accounts_Db` WHERE `email` = @email", My_Con.getConnection());
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = Email_User;

            adapter.SelectCommand = command;
            adapter.Fill(Tab_Accounts_Db);

            if (Tab_Accounts_Db.Rows.Count > 0)
            {
                My_Con.openConnection();

                reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Name = reader["name"].ToString();
                    SurName = reader["surname"].ToString();
                    Login = reader["login"].ToString();
                }

                My_Con.closeConnection();

                int Pass_;

                int i;
                Random Code = new Random();
                for (i = 0; i <= 3; i++)
                {
                    Pass_ = Code.Next(0, 10);
                    Pass += Pass_.ToString();
                }

                MailAddress fromAdress1 = new MailAddress("Aluminum.Company163@gmail.com", "Al-Store");
                MailAddress fromAdress2 = new MailAddress("Aluminum.Company163.reserve@gmail.com", "Al-Store");
                string From = Name + " " + SurName;
                MailAddress toAdress = new MailAddress(Email_User, From);
                MailAddress toAdress2 = new MailAddress("Aluminum.Company163@gmail.com", "Админ");
                MailMessage message1 = new MailMessage(fromAdress1, toAdress)
                {
                    Subject = "Восстановление пароля",
                    Body = "     Здравствуйте " + From + ", кто-то восстанавливает пароль от 'Al-Store'.\n" +
                    " Если это не вы, то не сообщайте никому этот код.\n" +
                    "    Подтвердите что это вы, введя вот этот код: " + Pass
                };

                MailMessage message2 = new MailMessage(fromAdress2, toAdress)
                {
                    Subject = "Восстановление пароля",
                    Body = "     Здравствуйте " + From + ", кто-то восстанавливает пароль от 'Al-Store'.\n" +
                    " Если это не вы, то не сообщайте никому этот код.\n" +
                    "    Подтвердите что это вы, введя вот этот код: " + Pass
                };

                MailMessage message3 = new MailMessage(fromAdress2, toAdress2)
                {
                    Subject = "Ошибка на почте",
                    Body = " Основная почта перестала работать, необходимо возобновить её работоспособность."
                };

                SmtpClient smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAdress1.Address, Properties.Settings.Default.Pass_for_Email)
                };

                try
                {
                    smtpClient.Send(message1);
                }
                catch (SmtpException)
                {
                    smtpClient = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAdress2.Address, Properties.Settings.Default.Pass_for_Email)
                    };
                    try
                    {
                        smtpClient.Send(message2);
                        smtpClient.Send(message3);
                    }
                    catch
                    { Pass = "Err"; return; }
                }
            }
            else
            { Pass = "NoEmail"; return; }

            My_Con.closeConnection();
        }

        public void Recovery_New_Pass(string Email, string Pass, string Login, out string Result)
        {
            My_Con = new MySql_Connector();
            Tab_Accounts_Db = new DataTable();
            adapter = new MySqlDataAdapter();

            command = new MySqlCommand("UPDATE `Tab_Accounts_Db` SET `pass` = @pass WHERE `Tab_Accounts_Db`.`email` = @email", My_Con.getConnection());

            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = Email;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = Pass;

            My_Con.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                Properties.Settings.Default.User_Login = Login;
                Properties.Settings.Default.User_Password = Pass;
                Properties.Settings.Default.Save();

                Result = "True";
            }
            else
            {
                Result = "False";
            }

            My_Con.closeConnection();
        }
    }
}
