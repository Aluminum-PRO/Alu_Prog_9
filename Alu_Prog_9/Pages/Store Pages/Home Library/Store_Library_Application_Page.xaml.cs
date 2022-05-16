using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.User_Control;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace Alu_Prog_9.Pages.Store_Pages.Home_Library
{
    /// <summary>
    /// Логика взаимодействия для Store_Library_Application_Page.xaml
    /// </summary>
    public partial class Store_Library_Application_Page : Page
    {
        int id, have_application;
        double size;
        string type, name, program_name, shortcut_description, hot_key, version, reference;
        BitmapImage image;


        MySql_Connector My_Con;
        MySqlCommand command;
        DataTable Tab_Accounts_Db; DataTable Tab_Programs_Db;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        public Store_Library_Application_Page()
        {
            InitializeComponent();
        }

        public Store_Library_Application_Page(string install)
        {
            InitializeComponent();

            My_Con = new MySql_Connector();

            Tab_Accounts_Db = new DataTable(); Tab_Programs_Db = new DataTable();
            adapter = new MySqlDataAdapter();
            command = new MySqlCommand("SELECT * FROM `Tab_Applications_Db`, `Tab_Programs_Db` WHERE Tab_Programs_Db.login = @login", My_Con.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            name = ""; program_name = ""; image = new BitmapImage(); have_application = 0; version = ""; reference = "";

            while (reader.Read())
            {
                id = Convert.ToInt32(reader["id"]);
                type = reader["type"].ToString();
                size = Convert.ToDouble(reader["size"]);
                name = reader["name"].ToString();
                program_name = reader["program_name"].ToString();
                shortcut_description = reader["shortcut_description"].ToString();
                hot_key = reader["hot_key"].ToString();
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

                if (have_application == 1)
                {
                    if (install == "True")
                    {
                        if (File.Exists(Properties.Settings.Default.Full_Path + "\\" + type + "\\" + name + "\\" + name + ".exe"))
                        {
                            Application_Library_Box_UC application_Library_Box_UC = new Application_Library_Box_UC(id, install, type, size, name, program_name, image, have_application, shortcut_description, hot_key, version, reference);
                            Area_for_Applicatoin_Box.Children.Add(application_Library_Box_UC);
                        }
                    }
                    if (install == "False")
                    {
                        if (!File.Exists(Properties.Settings.Default.Full_Path + "\\" + type + "\\" + name + "\\" + name + ".exe"))
                        {
                            Application_Library_Box_UC application_Library_Box_UC = new Application_Library_Box_UC(id, install, type, size, name, program_name, image, have_application, shortcut_description, hot_key, version, reference);
                            Area_for_Applicatoin_Box.Children.Add(application_Library_Box_UC);
                        }
                    }

                }
            }
            My_Con.closeConnection();
        }
    }
}
