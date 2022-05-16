using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.User_Control;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Alu_Prog_9.Pages.Store_Pages.Softs_Pages
{
    /// <summary>
    /// Логика взаимодействия для Store_Soft_Application_Page.xaml
    /// </summary>
    public partial class Store_Soft_Application_Page : Page
    {
        int id;
        string file_type, name, program_name, pass , reference;
        double file_size;
        BitmapImage image;


        MySql_Connector My_Con;
        MySqlCommand command; 
        DataTable Tab_Accounts_Db; DataTable Tab_Programs_Db;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        public Store_Soft_Application_Page()
        {
            InitializeComponent();
        }

        public Store_Soft_Application_Page(string download)
        {
            InitializeComponent();

            My_Con = new MySql_Connector();

            Tab_Accounts_Db = new DataTable(); Tab_Programs_Db = new DataTable();
            adapter = new MySqlDataAdapter();
            command = new MySqlCommand("SELECT * FROM `Tab_Soft_Db` ORDER BY soft_name asc", My_Con.getConnection());

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            name = ""; program_name = ""; image = new BitmapImage(); pass = ""; reference = ""; StaticVars.Count_Soft = 0;

            while (reader.Read())
            {
                id = Convert.ToInt32(reader["soft_id"]);
                file_type = reader["soft_file_type"].ToString();
                file_size = Convert.ToDouble(reader["soft_file_size"]);
                name = reader["soft_name"].ToString();
                program_name = reader["soft_program_name"].ToString();
                pass = reader["soft_pass"].ToString();
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
                reference = reader["soft_reference"].ToString();
                if (download == "True")
                {
                    if (File.Exists(Properties.Settings.Default.Full_Path + "\\Soft\\" + name + "\\" + name + file_type))
                    {
                        Application_Soft_Box_UC application_Soft_Box_UC = new Application_Soft_Box_UC(id, download, file_type, file_size, name, program_name, pass, image, reference);
                        Area_for_Applicatoin_Box.Children.Add(application_Soft_Box_UC);
                    }
                }
                if (download == "False")
                {
                    if (!File.Exists(Properties.Settings.Default.Full_Path + "\\Soft\\" + name + "\\" + name + file_type))
                    {
                        Application_Soft_Box_UC application_Soft_Box_UC = new Application_Soft_Box_UC(id, download, file_type, file_size, name, program_name, pass, image, reference);
                        Area_for_Applicatoin_Box.Children.Add(application_Soft_Box_UC);
                    }
                }
                StaticVars.Count_Soft++;
            }
            StaticVars.Count_Soft_Text_box.Text += StaticVars.Count_Soft.ToString();
            My_Con.closeConnection();
        }
    }
}
