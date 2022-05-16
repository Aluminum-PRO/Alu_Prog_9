using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.Pages.Store_Pages.Home_Library;
using Alu_Prog_9.User_Control;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

namespace Alu_Prog_9.Pages.Store_Pages.Applications
{
    /// <summary>
    /// Логика взаимодействия для Application_Box_Page.xaml
    /// </summary>
    public partial class Application_Box_Page : Page
    {
        MySql_Handler My_Hand = new MySql_Handler(); 
        //TODO: Добавить ограничение на ввод символов в комментарий 
        //TODO: Добавить комментарии в библиотеке приложений

        MySql_Connector My_Con;
        MySqlCommand command;
        DataTable Tab_Accounts_Db;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        int Count_Comments = 0;
        private int application_id, app_have_application;
        private double app_size;
        private string app_type, app_install, app_name, app_program_name, app_shortcut_description, app_hot_key, installed_app_version, app_version, app_reference, name, surname, comment_text, send_comment_text, time_send;
        BitmapImage app_image, image_1, image_2, image_3, image_4;

        public Application_Box_Page(int _id, string _type, double _size, string _name, string _program_name, BitmapImage _image, BitmapImage _image_1, BitmapImage _image_2, BitmapImage _image_3, BitmapImage _image_4, string _description, string _shortcut_description, string _hot_key, int _have_application, double _price, string _version, string _reference)
        {
            InitializeComponent();
            KeyDown += (s, e) => { if (e.Key == Key.Enter) Send_Comment_But_Click(Send_Comment_But, null); };

            application_id = _id;
            app_type = _type;
            app_size = _size;
            app_name = _name;
            app_program_name = _program_name;
            Name_Application.Text = _name.ToString()/*.Replace(@"_", " ")*/;
            app_image = _image; image_1 = _image_1; image_2 = _image_2; image_3 = _image_3; image_4 = _image_4;
            Application_Image_Main.Source = _image; Application_Image_1.Source = _image_1; Application_Image_2.Source = _image_2; Application_Image_3.Source = _image_3; Application_Image_4.Source = _image_4;
            Description_Application.Text = _description;
            app_shortcut_description = _shortcut_description;
            app_hot_key = _hot_key;
            // Метод для поля "Have_Application"
            {
                app_have_application = _have_application;
                if (_have_application == 1)
                {
                    Have_Application.Text = "Имеется"; 
                    Get_Application_But.Visibility = Visibility.Hidden;
                }
                else if (_have_application == 0)
                {
                    Open_Application_But.Visibility = Visibility.Hidden;
                    if (_price == 0)
                    {
                        Have_Application.Text = "Бесплатно";
                    }
                    else if (_price > 0)
                    {
                        Have_Application.Text = "Цена: " + _price.ToString() + " ₽";
                    }
                }
            }
            app_version = _version;
            app_reference = _reference;

            Refrech_Comments();
        }
        private void Send_Comment_But_Click(object sender, RoutedEventArgs e)
        {
            if (Send_Comment_Text.Text != "")
            {
                send_comment_text = Send_Comment_Text.Text;
                Send_Comment_Text.Text = "";

                My_Hand.Send_Comment(application_id, Properties.Settings.Default.User_Id, Properties.Settings.Default.User_Name, Properties.Settings.Default.User_SurName, send_comment_text);

                Comments_Application.Children.Clear();
                Refrech_Comments();
            }
        }

        public void Refrech_Comments()
        {
            My_Con = new MySql_Connector();

            Tab_Accounts_Db = new DataTable();
            adapter = new MySqlDataAdapter();
            command = new MySqlCommand("SELECT `user_id`, `name`, `surname`, `comment_text`, `time_send` FROM `Tab_Applications_Comments_Db` WHERE application_id = @application_id  ORDER BY comment_id desc", My_Con.getConnection());
            command.Parameters.Add("@application_id", MySqlDbType.Int32).Value = application_id;

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Count_Comments++;
                name = reader["name"].ToString();
                surname = reader["surname"].ToString();
                comment_text = reader["comment_text"].ToString();
                time_send = reader["time_send"].ToString();
                Comment_UC comment_UC = new Comment_UC(name, surname, comment_text, time_send);
                Comments_Application.Children.Add(comment_UC);
            }
            if (Count_Comments == 0)
            {
                Have_Comments.Text = "Комментариев нет.";
            }

            My_Con.closeConnection();
        }

        private void Get_Application_But_Click(object sender, RoutedEventArgs e)
        {
            My_Hand.Get_Application(app_program_name, out string Bool);
            if (Bool == "True")
            {
                Have_Application.Text = "Имеется";
                Get_Application_But.Visibility = Visibility.Hidden;
                Open_Application_But.Visibility = Visibility.Visible;
            }
            else if (Bool == "False")
            { }
        }

        private void Open_Application_But_Click(object sender, RoutedEventArgs e)
        {
            StaticVars.Main_Frame.NavigationService.Navigate(new Main_Page());
            StaticVars.Store_Home_But.IsChecked = false;
            StaticVars.Store_Library_But.IsChecked = true;
            StaticVars.Store_Frame.NavigationService.Navigate(new Store_Library_Page());
            if (File.Exists(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + "\\" + app_name + ".exe"))
            {
                app_install = "True";
                FileVersionInfo myFileVersionInfo_Store = FileVersionInfo.GetVersionInfo(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + "\\" + app_name + ".exe");
                installed_app_version = myFileVersionInfo_Store.FileVersion;
                if (Convert.ToInt32(installed_app_version.Split('.')[0]) != 0)
                { installed_app_version += ".Release"; }
                else if (Convert.ToInt32(installed_app_version.Split('.')[1]) != 0)
                { installed_app_version += ".Beta"; }
                else if (Convert.ToInt32(installed_app_version.Split('.')[2]) != 0)
                { installed_app_version += ".Alpha"; }
                else if (Convert.ToInt32(installed_app_version.Split('.')[3]) != 0)
                { installed_app_version += ".Pre-Alpha"; }
            }
            else if (!File.Exists(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + "\\" + app_name + ".exe"))
            {
                app_install = "False";
                StaticVars.Store_Library_Installed_But.IsChecked = false;
                StaticVars.Store_Library_Not_Installed_But.IsChecked = true;
            }
            Application_Library_Box_Page application_Library_Box_Page = new Application_Library_Box_Page(application_id, app_install, app_type, app_size, app_name, app_program_name, app_image, app_have_application, app_shortcut_description, app_hot_key, installed_app_version, app_version, app_reference);
            StaticVars.Store_Library_Frame.Content = application_Library_Box_Page;
        }

        private void Go_Library_But_Click(object sender, RoutedEventArgs e)
        {
            StaticVars.Main_Frame.NavigationService.Navigate(new Main_Page());
            StaticVars.Store_Home_But.IsChecked = false;
            StaticVars.Store_Library_But.IsChecked = true;
            StaticVars.Store_Frame.NavigationService.Navigate(new Store_Library_Page());
        }

        private void Open_Application_Image_1_But_Click(object sender, RoutedEventArgs e)
        {
            Application_Image_Main.Source = image_1;
        }

        private void Open_Application_Image_2_But_Click(object sender, RoutedEventArgs e)
        {
            Application_Image_Main.Source = image_2;
        }

        private void Open_Application_Image_3_But_Click(object sender, RoutedEventArgs e)
        {
            Application_Image_Main.Source = image_3;
        }

        private void Open_Application_Image_4_But_Click(object sender, RoutedEventArgs e)
        {
            Application_Image_Main.Source = image_4;
        }
    }
}
