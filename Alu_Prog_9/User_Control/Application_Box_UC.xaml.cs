using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.Pages;
using Alu_Prog_9.Pages.Store_Pages.Applications;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alu_Prog_9.User_Control
{
    /// <summary>
    /// Логика взаимодействия для Application_Box_UC.xaml
    /// </summary>
    public partial class Application_Box_UC : UserControl   
    {
        private MySql_Connector My_Con;
        private MySqlCommand command;
        private DataTable Tab_Accounts_Db;
        private DataTable Tab_Programs_Db;
        private MySqlDataAdapter adapter;
        private MySqlDataReader reader;

        private readonly int id = 0;
        private string type = "";
        private double size = 0;
        private string name = "";
        private string program_name = "";
        private BitmapImage image;
        private BitmapImage image_1;
        private BitmapImage image_2;
        private BitmapImage image_3;
        private BitmapImage image_4;
        private string description = "";
        private string shortcut_description = "";
        private string hot_key = "";
        private int have_application = 0;
        private double price = 0;
        private string version = "";
        private string reference = "";

        public Application_Box_UC(int id/*, string type, double size, string name, string program_name, BitmapImage image, BitmapImage image_1, BitmapImage image_2, BitmapImage image_3, BitmapImage image_4, string description, string shortcut_description, string hot_key, int have_application, double price, string version, string reference*/)
        {
            InitializeComponent();
            this.id = id;
            Uploading_Application_Data();
            //_type = type;
            //_size = size;
            //_name = name;
            //_program_name = program_name;
            //_image = image; _image_1 = image_1; _image_2 = image_2; _image_3 = image_3; _image_4 = image_4;
            //_description = description;
            //_shortcut_description = shortcut_description;
            //_hot_key = hot_key;
            //_have_application = have_application;
            //_price = price;
            //_version = version;
            //_reference = reference;

            //Name_Application.Text = name.ToString()/*.Replace(@"_", " ")*/;
            // Метод для поля "Have_Application"
            //{
            //    if (have_application == 1)
            //    {
            //        Have_Application.Text = "Имеется";
            //    }
            //    else if (have_application == 0)
            //    {
            //        if (price == 0)
            //        {
            //            Have_Application.Text = "Бесплатно";
            //        }
            //        else if (price > 0)
            //        {
            //            Have_Application.Text = "Цена: " + price.ToString() + " ₽";
            //        }
            //    }
            //}
            //Application_Image.Source = image;

        }

        private async void Uploading_Application_Data()
        {
            await Task.Run(() =>
            {
                My_Con = new MySql_Connector();

                Tab_Accounts_Db = new DataTable(); Tab_Programs_Db = new DataTable();
                adapter = new MySqlDataAdapter();
                command = new MySqlCommand("SELECT * FROM `Tab_Applications_Db`, `Tab_Programs_Db` WHERE Tab_Applications_Db.id = @id AND Tab_Programs_Db.login = @login", My_Con.getConnection());
                command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id; command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

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
                    for (int i = 1; i < 5; i++)
                    {
                        blob = (Byte[])(reader["image_" + i]);

                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream(blob))
                        {
                            var imageSource = new BitmapImage();
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
                    description = reader["description"].ToString();
                    shortcut_description = reader["shortcut_description"].ToString();
                    hot_key = reader["hot_key"].ToString(); ;
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

                    try
                    { have_application = Convert.ToInt32(reader[program_name]); }
                    catch
                    { have_application = 0; }
                }
                My_Con.closeConnection();

                Name_Application.Text = name.ToString()/*.Replace(@"_", " ")*/;
                // Метод для поля "Have_Application"
                {
                    if (have_application == 1)
                    {
                        Have_Application.Text = "Имеется";
                    }
                    else if (have_application == 0)
                    {
                        if (price == 0)
                        {
                            Have_Application.Text = "Бесплатно";
                        }
                        else if (price > 0)
                        {
                            Have_Application.Text = "Цена: " + price.ToString() + " ₽";
                        }
                    }
                }
                Application_Image.Source = image;
            });
        }

        private void Open_Application_Box_But_Click(object sender, RoutedEventArgs e)
        {
            //Application_Box_Page application_Box_Page = new Application_Box_Page(id, name, program_name, image, have_application, price, version, version_TPK_Ed, reference, reference_TPK_Ed);
            //PostPage postUC = new PostPage(sourceImg, authorId, author, sourceAuthorImg, dt, text, lks, myLks, id);
            //StaticVars.MainWnd.Content = postUC;

            Application_Box_Page application_Box_Page = new Application_Box_Page(id, type, size, name, program_name, image, image_1, image_2, image_3, image_4, description, shortcut_description, hot_key, have_application, price, version, reference);
            StaticVars.Store_Home_Frame.Content = application_Box_Page;
        }
    }
}
