using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.User_Control;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Alu_Prog_9.Pages.Store_Pages.Admins.Update
{
    /// <summary>
    /// Логика взаимодействия для Store_Admin_Update_Page.xaml
    /// </summary>
    public partial class Store_Admin_Update_Page : Page
    {
        private MySql_Connector My_Con;
        private MySqlCommand command;
        private MySqlDataAdapter adapter;
        private MySqlDataReader reader;
        private int id;
        private double size, price;
        private string type, name, program_name, version, TPK_version, reference, TPK_reference, what_news, TPK_what_news, description, shortcut_description, hot_key;
        private BitmapImage image, image_1, image_2, image_3, image_4;

        public Store_Admin_Update_Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            My_Con = new MySql_Connector();

            adapter = new MySqlDataAdapter();
            command = new MySqlCommand("SELECT * FROM `Tab_Al_Store_Db`; SELECT * FROM `Tab_Applications_Db`", My_Con.getConnection());

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();


            while (reader.Read())
            {
                id = Convert.ToInt32(reader["id"]);
                name = reader["name"].ToString();
                version = reader["version"].ToString();
                reference = reader["reference"].ToString().Replace("https://getfile.dokpub.com/yandex/get/", "");
                TPK_version = reader["TPK_version"].ToString();
                TPK_reference = reader["TPK_reference"].ToString().Replace("https://getfile.dokpub.com/yandex/get/", "");
                what_news = reader["what_news"].ToString();
                TPK_what_news = reader["TPK_what_news"].ToString();
                size = Convert.ToDouble(reader["size"]);
                if (reader["id"].ToString() == "2" || reader["id"].ToString() == "3")
                {
                    Update_Al_Store_But_UC update_Al_Store_But_UC = new Update_Al_Store_But_UC(id, name, version, TPK_version, reference, TPK_reference, what_news, TPK_what_news, size);
                    Al_Store_StackPanel.Children.Add(update_Al_Store_But_UC);
                }
            }

            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["id"]);
                    type = reader["type"].ToString();
                    size = Convert.ToDouble(reader["size"]);
                    name = reader["name"].ToString();
                    program_name = reader["program_name"].ToString();
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
                    version = reader["version"].ToString();
                    reference = reader["reference"].ToString().Replace("https://getfile.dokpub.com/yandex/get/", "");
                    TPK_version = reader["version_TPK_Ed"].ToString();
                    TPK_reference = reader["reference_TPK_Ed"].ToString().Replace("https://getfile.dokpub.com/yandex/get/", "");
                    description = reader["description"].ToString();
                    shortcut_description = reader["shortcut_description"].ToString();
                    hot_key = reader["hot_key"].ToString();
                    price = Convert.ToDouble(reader["price"]);
                    Update_App_But_UC update_App_But_UC = new Update_App_But_UC(id, type, size, name, program_name, image, image_1, image_2, image_3, image_4, description, shortcut_description, hot_key, price, version, TPK_version, reference, TPK_reference);
                    if (type == "Programs")
                        Programs_StackPanel.Children.Add(update_App_But_UC);
                    else if (type == "Games")
                        Games_StackPanel.Children.Add(update_App_But_UC);
                    else if (type == "Tests")
                        Tests_StackPanel.Children.Add(update_App_But_UC);
                    else if (type == "Admins")
                        Admins_StackPanel.Children.Add(update_App_But_UC);
                }
            }
            My_Con.closeConnection();
        }
    }
}
