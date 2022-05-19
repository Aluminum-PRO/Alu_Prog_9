using Alu_Prog_9.MySql_Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alu_Prog_9.Pages.Store_Pages.Admins.Update
{
    /// <summary>
    /// Логика взаимодействия для Store_Admin_Update_Page.xaml
    /// </summary>
    public partial class Store_Admin_Update_Page : Page
    {
        MySql_Connector My_Con;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        int id, have_application;
        double size;
        string type, name, program_name, version, TPK_version, reference, TPK_reference;

        public Store_Admin_Update_Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            My_Con = new MySql_Connector();

            adapter = new MySqlDataAdapter();
            command = new MySqlCommand("SELECT * FROM `Tab_Al_Store_Db`, `Tab_Applications_Db`", My_Con.getConnection());

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                version = reader["Tab_Al_Store_Db.version"].ToString();
                version = reader["Tab_Al_Store_Db.version_TPK_Ed"].ToString();
                reference = reader["Tab_Al_Store_Db.reference"].ToString();
                reference = reader["Tab_Al_Store_Db.reference_TPK_Ed"].ToString();
                size = Convert.ToDouble(reader["size"]);
            }

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
        }
    }
}
