using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.Services;
using System;
using System.Collections.Generic;
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

namespace Alu_Prog_9.Pages.Store_Pages.Settings
{
    /// <summary>
    /// Логика взаимодействия для Store_Settings_Update_Page.xaml
    /// </summary>
    public partial class Store_Settings_Update_Page : Page
    {
        MySql_Handler My_Hand;
        Handler handler;

        public Store_Settings_Update_Page()
        {
            InitializeComponent();

            Installed_Version_Al_TextBlock.Text += Properties.Settings.Default.Ver_Store;
            Installed_Version_Up_TextBlock.Text += Properties.Settings.Default.Ver_Updater;

            Server_Version_Al_TextBlock.Text += Properties.Settings.Default.New_Ver_Store;
            Server_Version_Up_TextBlock.Text += Properties.Settings.Default.New_Ver_Updater;

            FileInfo file = new System.IO.FileInfo(Properties.Settings.Default.Path_Store + "\\Al-Store.exe");
            double size = file.Length;
            size /= 1048576;

            Size_Al_TextBlock.Text += size.ToString("0.00") + " МБ";

            file = new System.IO.FileInfo(Properties.Settings.Default.Path_Updater + "\\Updater for Al-Store.exe");
            size = file.Length;
            size /= 1048576;
            Size_Up_TextBlock.Text += size.ToString("0.00") + " МБ";

            Location_Al_TextBlock.Text += Properties.Settings.Default.Path_Store;
            Location_Up_TextBlock.Text += Properties.Settings.Default.Path_Updater;

            if (Properties.Settings.Default.Ver_Store == Properties.Settings.Default.New_Ver_Store)
            {
                Al_Update.Visibility = Visibility.Hidden; What_New_But.Visibility = Visibility.Hidden;
            }
            if (Properties.Settings.Default.Ver_Updater == Properties.Settings.Default.New_Ver_Updater)
            {
                Up_Update.Visibility = Visibility.Hidden;
            }

            if (StaticVars.Auto_Update == 1)
            {
                Auto_Update_But.IsChecked = true;
                Update_Msg_StackPanel.Visibility = Visibility.Collapsed;
            }
            else if (StaticVars.Auto_Update == 0)
            {
                Auto_Update_But.IsChecked = false;
                Update_Msg_StackPanel.Visibility = Visibility.Visible;
            }

            if (StaticVars.Update_Msg == 1)
            {
                Update_Msg_But.IsChecked = true;
            }
            else if (StaticVars.Update_Msg == 0)
            {
                Update_Msg_But.IsChecked = false;
            }
        }

        private void Start_Update_But_Click(object sender, RoutedEventArgs e)
        {
            Telegram_Bot_Send_Activity telegram_Bot_Send_Activity = new Telegram_Bot_Send_Activity();
            telegram_Bot_Send_Activity.Al_Store_Updating(false);

            Update_Al_Window update_Al_Window = new Update_Al_Window(false);
            update_Al_Window.Show();
            StaticVars.MainWindow.Hide();
        }

        private void Auto_Update_But_Click(object sender, RoutedEventArgs e)
        {
            My_Hand = new MySql_Handler();
            handler = new Handler();
            
            if (Auto_Update_But.IsChecked == false)
                StaticVars.Auto_Update = 0;
            else if (Auto_Update_But.IsChecked == true)
                StaticVars.Auto_Update = 1;
            My_Hand.Set_Properties("Auto_Update", StaticVars.Auto_Update, out bool Result);
            if (Result == true)
            { }
            else if (Result == false)
            {
                if (StaticVars.Auto_Update == 0)
                { StaticVars.Auto_Update = 1; Auto_Update_But.IsChecked = true; }
                else if (StaticVars.Auto_Update == 1)
                { StaticVars.Auto_Update = 0; Auto_Update_But.IsChecked = false; }
                MessageBox.Show(" Не удалось обновить данные. Проверьте подключение к интернету или обратитесь к разработчику за помощью. \n Aluminum.Company163@gmail.com или Aluminum.Company163.reserve@gmail.com", "Ошибка!");
            }
            
            if (Auto_Update_But.IsChecked == true)
                handler.SetAutorunValue(true);


            if (StaticVars.Auto_Update == 0)
            { Update_Msg_StackPanel.Visibility = Visibility.Visible; }
            else if (StaticVars.Auto_Update == 1)
            { Update_Msg_StackPanel.Visibility = Visibility.Collapsed; }
        }

        private void Update_Msg_But_Click(object sender, RoutedEventArgs e)
        {
            My_Hand = new MySql_Handler();
            handler = new Handler();

            if (Update_Msg_But.IsChecked == false)
                StaticVars.Update_Msg = 0;
            else if (Update_Msg_But.IsChecked == true)
                StaticVars.Update_Msg = 1;
            My_Hand.Set_Properties("Update_Msg", StaticVars.Update_Msg, out bool Result);
            if (Result == true)
            { }
            else if (Result == false)
            {
                if (StaticVars.Update_Msg == 0)
                { StaticVars.Update_Msg = 1; Update_Msg_But.IsChecked = true; }
                else if (StaticVars.Update_Msg == 1)
                { StaticVars.Update_Msg = 0; Update_Msg_But.IsChecked = false; }
                MessageBox.Show(" Не удалось обновить данные. Проверьте подключение к интернету или обратитесь к разработчику за помощью. \n Aluminum.Company163@gmail.com или Aluminum.Company163.reserve@gmail.com", "Ошибка!");
            }
            if (Update_Msg_But.IsChecked == true)
                handler.SetAutorunValue(true);
            else if (Update_Msg_But.IsChecked == false)
                handler.SetAutorunValue(false);
        }

        private void What_New_But_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(StaticVars.what_news, "Что нового в обновлении Al-Store", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
