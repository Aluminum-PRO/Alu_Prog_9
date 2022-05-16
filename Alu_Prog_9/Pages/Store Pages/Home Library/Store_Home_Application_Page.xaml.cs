using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.User_Control;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Alu_Prog_9.Pages.Store_Pages.Home_Library
{
    /// <summary>
    /// Логика взаимодействия для Store_Home_Application_Page.xaml
    /// </summary>
    public partial class Store_Home_Application_Page : Page
    {
        private int id, have_application;
        private double price, size;
        private string type, name, program_name, description, shortcut_description, hot_key, version, reference;
        private BitmapImage image, image_1, image_2, image_3, image_4;
        private MySql_Connector My_Con;
        private MySqlCommand command;
        private DataTable Tab_Accounts_Db;
        private DataTable Tab_Programs_Db;
        private MySqlDataAdapter adapter;
        private MySqlDataReader reader;

        public Store_Home_Application_Page(string Type)
        {
            InitializeComponent();
            Spawn_Applications(Type);
        }

        private async void Spawn_Applications(string Type)
        {
            await Task.Run(() =>
            {
                My_Con = new MySql_Connector();

                Tab_Accounts_Db = new DataTable(); Tab_Programs_Db = new DataTable();
                adapter = new MySqlDataAdapter();
                command = new MySqlCommand("SELECT * FROM `Tab_Applications_Db`, `Tab_Programs_Db` WHERE Tab_Applications_Db.type = @type AND Tab_Programs_Db.login = @login", My_Con.getConnection());
                command.Parameters.Add("@type", MySqlDbType.VarChar).Value = Type; command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

                // Другие варианты запроса Command
                {
                    //command = new MySqlCommand("SELECT `name`, `program_name`, `image`, `description`, `price`, `version`, `version_TPK_Ed`, `reference`, `reference_TPK_Ed`, `Off_PC`, `The_15_Puzzle` FROM `Tab_Applications_Db`, `Tab_Programs_Db` WHERE Tab_Applications_Db.type = @type AND Tab_Programs_Db.login = @login", My_Con.getConnection());

                    //command = new MySqlCommand("SELECT Tab_Applications_Db.name, Tab_Applications_Db.program_name, Tab_Applications_Db.image, " +
                    //    "Tab_Applications_Db.description, Tab_Applications_Db.price, Tab_Applications_Db.version, Tab_Applications_Db.version_TPK_Ed, " +
                    //    "Tab_Applications_Db.reference, Tab_Applications_Db.reference_TPK_Ed, Tab_Programs_Db.Off_PC FROM Tab_Applications_Db, Tab_Programs_Db WHERE Tab_Applications_Db.type = @type AND Tab_Programs_Db.login = @login", My_Con.getConnection());
                }

                My_Con.openConnection();

                reader = null;
                reader = command.ExecuteReader();

                name = ""; type = ""; program_name = ""; image = new BitmapImage(); image_1 = new BitmapImage(); image_2 = new BitmapImage(); image_3 = new BitmapImage(); image_4 = new BitmapImage(); description = ""; have_application = 0; price = 0; version = ""; reference = "";

                while (reader.Read())
                {

                    id = Convert.ToInt32(reader["id"]);
                //type = reader["type"].ToString();
                //size = Convert.ToDouble(reader["size"]);
                //name = reader["name"].ToString();
                //program_name = reader["program_name"].ToString();
                //byte[] blob = (Byte[])(reader["image_main"]);
                //using (System.IO.MemoryStream ms = new System.IO.MemoryStream(blob))
                //{
                //    var imageSource = new BitmapImage();
                //    imageSource.BeginInit();
                //    imageSource.StreamSource = ms;
                //    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                //    imageSource.EndInit();
                //    image = imageSource;
                //}
                //for (int i = 1; i < 5; i++)
                //{
                //    blob = (Byte[])(reader["image_" + i]);

                //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(blob))
                //    {
                //        var imageSource = new BitmapImage();
                //        imageSource.BeginInit();
                //        imageSource.StreamSource = ms;
                //        imageSource.CacheOption = BitmapCacheOption.OnLoad;
                //        imageSource.EndInit();
                //        if (i == 1)
                //        {
                //            image_1 = imageSource;
                //        }
                //        else if (i == 2)
                //        {
                //            image_2 = imageSource;
                //        }
                //        else if (i == 3)
                //        {
                //            image_3 = imageSource;
                //        }
                //        else if (i == 4)
                //        {
                //            image_4 = imageSource;
                //        }
                //    }
                //}
                //description = reader["description"].ToString();
                //shortcut_description = reader["shortcut_description"].ToString();
                //hot_key = reader["hot_key"].ToString(); ;
                //price = Convert.ToDouble(reader["price"]);
                //if (Properties.Settings.Default.Edition == "'Standart'")
                //{
                //    version = reader["version"].ToString();
                //    reference = reader["reference"].ToString();

                //}
                //else if (Properties.Settings.Default.Edition == "'TPK'")
                //{
                //    version = reader["version_TPK_Ed"].ToString();
                //    reference = reader["reference_TPK_Ed"].ToString();
                //}

                //try
                //{ have_application = Convert.ToInt32(reader[program_name]); }
                //catch
                //{ have_application = 0; }
                //Application_Box_UC application_Box_UC = new Application_Box_UC(id/*, type, size, name, program_name, image, image_1, image_2, image_3, image_4, description, shortcut_description, hot_key, have_application, price, version, reference*/);
                //Area_for_Applicatoin_Box.Children.Add(application_Box_UC);
                if (!Dispatcher.CheckAccess())
                {
                    Dispatcher.Invoke(() =>
                    {
                        Application_Box_UC application_Box_UC = new Application_Box_UC(id/*, type, size, name, program_name, image, image_1, image_2, image_3, image_4, description, shortcut_description, hot_key, have_application, price, version, reference*/);
                        Area_for_Applicatoin_Box.Children.Add(application_Box_UC);
                    }, DispatcherPriority.Normal);
                }
                else
                {
                    Application_Box_UC application_Box_UC = new Application_Box_UC(id/*, type, size, name, program_name, image, image_1, image_2, image_3, image_4, description, shortcut_description, hot_key, have_application, price, version, reference*/);
                    Area_for_Applicatoin_Box.Children.Add(application_Box_UC);
                }
            }
                My_Con.closeConnection();
            });
        }
    }
}
