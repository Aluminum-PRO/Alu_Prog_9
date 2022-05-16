using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
using System;
using System.Collections.Generic;
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
using Alu_Prog_9.Pages;
using Alu_Prog_9.Pages.Store_Pages;
using Alu_Prog_9.Pages.Store_Pages.Home_Library;
using Alu_Prog_9.Pages.Store_Pages.News;
using Alu_Prog_9.Pages.Store_Pages.Settings;
using Alu_Prog_9.Pages.Store_Pages.Baskets;
using Alu_Prog_9.Pages.Store_Pages.Admins;
using Alu_Prog_9.Pages.Store_Pages.Softs_Pages;
using Alu_Prog_9.Pages.Store_Pages.Accounts;
using Alu_Prog_9.Pages.Bot_Pages;

namespace Alu_Prog_9.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main_Page.xaml
    /// </summary>
    public partial class Main_Page : Page
    {
        MySql_Handler My_Hand;

        public Main_Page()
        {
            InitializeComponent();

            StaticVars.Store_Frame = Store_Frame;

            StaticVars.Ellepse_Grid = Ellepse_Grid;
            StaticVars.Ellipse_text = Ellipse_Text;

            StaticVars.Ellepse_Grid_Al = Ellepse_Grid_Al;
            StaticVars.Ellipse_Al_text = Ellipse_Al_Text;

            StaticVars.Store_Home_But = Store_Home_But;
            StaticVars.Store_Library_But = Store_Library_But;

            Store_Frame.Content = new Store_Home_Page();
            if (Properties.Settings.Default.User_Login.ToLower() == "admin")
            {
                Store_Admin_But.Visibility = Visibility.Visible;
            }

            if (StaticVars.Count_Update == 0)
            {
                Ellepse_Grid.Visibility = Visibility.Hidden;
            }
            else
            {
                Ellipse_Text.Text = StaticVars.Count_Update.ToString();
            }

            if (StaticVars.Count_Update_Al == 0)
            {
                Ellepse_Grid_Al.Visibility = Visibility.Hidden;
            }
            else
            {
                Ellipse_Al_Text.Text = StaticVars.Count_Update_Al.ToString();
            }

            if (StaticVars.Bot_License == 1)
            {
                Store_Bot_Soft_But.Visibility = Visibility.Visible;
            }
            Login_But.Content = Properties.Settings.Default.User_Login;
        }

        private void Store_Home_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Frame.NavigationService.Navigate(new Store_Home_Page());
        }

        private void Store_News_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Frame.NavigationService.Navigate(new Store_News_Page());
        }

        private void Store_Library_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Frame.NavigationService.Navigate(new Store_Library_Page());
        }

        private void Store_Settings_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Frame.NavigationService.Navigate(new Store_Settings_Page());
        }

        private void Store_Basket_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Frame.NavigationService.Navigate(new Store_Basket_Page());
        }

        private void Store_Admin_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Frame.NavigationService.Navigate(new Store_Admin_Page());
        }

        private void Store_Soft_But_Click(object sender, RoutedEventArgs e)
        {
            if (StaticVars.Soft_License == 0)
            {
                My_Hand = new MySql_Handler();
                My_Hand.Getting_User_Data();
            }
            if (StaticVars.Soft_License == 1)
            {
                Store_Frame.NavigationService.Navigate(new Store_Soft_Page());
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(" У вас нет лицензии на использование стороннего софта.\nНажмите 'Ок', для связи с разработчиком. \nЕщё связь с разработчиком:\n Aluminum.Company163@gmail.com или Aluminum.Company163.reserve@gmail.com", "Al-Store", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.OK)
                { Process.Start("https://vk.com/aluminum343/"); }
            }
        }

        private void Login_But_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Переделать кнопку профиля, а так же доделать профиль
            Store_Frame.NavigationService.Navigate(new Store_Account_Page());
        }

        private void Store_Bot_Soft_But_Click(object sender, RoutedEventArgs e)
        {
            My_Hand = new MySql_Handler();
            My_Hand.Get_Bot_Data(out int id, out string type, out double size, out string name, out string program_name, out BitmapImage image, out string description, out string clicker_path, out double price, out string version, out string reference);
            if (StaticVars.Bot_License == 0)
            {
                My_Hand.Getting_User_Data();
            }
            if (StaticVars.Bot_License == 1)
            {
                Store_Frame.NavigationService.Navigate(new Bot_Library_Box_Page(id, type, size, name, program_name, image, description, clicker_path, StaticVars.Bot_License, price, version, reference));
            }
            else
            {
                Store_Frame.NavigationService.Navigate(new Bot_Box_Page(id, type, size, name, program_name, image, description, clicker_path, StaticVars.Bot_License, price, version, reference));
            }
        }
    }
}
