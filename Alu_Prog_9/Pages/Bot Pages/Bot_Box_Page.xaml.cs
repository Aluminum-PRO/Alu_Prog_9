using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.User_Control;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

namespace Alu_Prog_9.Pages.Bot_Pages
{
    /// <summary>
    /// Логика взаимодействия для Bot_Box_Page.xaml
    /// </summary>
    public partial class Bot_Box_Page : Page
    {
        MySql_Handler My_Hand = new MySql_Handler();

        MySql_Connector My_Con;
        MySqlCommand command;
        DataTable Tab_Accounts_Db;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        int Count_Comments = 0;
        private int bot_id, bot_have_license;
        private double bot_size, bot_price;
        private string bot_type, bot_name, bot_program_name, bot_version, bot_reference, name, surname, comment_text, send_comment_text, time_send, bot_description, bot_clicker_path;
        BitmapImage bot_image;

        public Bot_Box_Page(int _id, string _type, double _size, string _name, string _program_name, BitmapImage _image, string _description, string _clicker_path, int _have_license, double _price, string _version, string _reference)
        {
            InitializeComponent();
            KeyDown += (s, e) => { if (e.Key == Key.Enter) Send_Comment_But_Click(Send_Comment_But, null); };

            bot_id = _id;
            bot_type = _type;
            bot_size = _size;
            bot_name = _name;
            bot_program_name = _program_name;
            Name_Bot.Text = _name.ToString();
            bot_price = _price;
            Price_Bot.Text += _price + " ₽";
            bot_image = _image;
            bot_description = _description;
            Description_Bot.Text = _description;
            bot_clicker_path = _clicker_path;
            // Метод для поля "Have_Application"
            {
                bot_have_license = _have_license;
                if (_have_license == 1)
                {
                    Have_License.Text = "Лицензия на использование бота имеется";
                }
                else if (_have_license == 0)
                {
                    Have_License.Text = "Лицензия на использование бота отсутствует";
                }
            }
            bot_version = _version;
            bot_reference = _reference;

            Refrech_Comments();
        }

        private void Go_By_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://funpay.ru/users/296061/");
        }

        private void Check_License_But_Click(object sender, RoutedEventArgs e)
        {
            My_Hand = new MySql_Handler();
            My_Hand.Getting_User_Bot_License();
            if (StaticVars.Bot_License == 1)
            {
                StaticVars.Store_Frame.NavigationService.Navigate(new Bot_Library_Box_Page(bot_id, bot_type, bot_size, bot_name, bot_program_name, bot_image, bot_description, bot_clicker_path, StaticVars.Bot_License, bot_price, bot_version, bot_reference));
            }
            else
            {
                MessageBox.Show(" У вас всё ещё нет лиценции");
            }
        }

        private void Send_Comment_But_Click(object sender, RoutedEventArgs e)
        {
            if (Send_Comment_Text.Text != "")
            {
                send_comment_text = Send_Comment_Text.Text;
                Send_Comment_Text.Text = "";

                My_Hand.Send_Comment(bot_id, Properties.Settings.Default.User_Id, Properties.Settings.Default.User_Name, Properties.Settings.Default.User_SurName, send_comment_text);

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
            command.Parameters.Add("@application_id", MySqlDbType.Int32).Value = bot_id;

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
    }
}
