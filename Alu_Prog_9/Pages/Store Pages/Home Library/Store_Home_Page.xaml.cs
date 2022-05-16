using Alu_Prog_9.Classes;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Store_Home_Page.xaml
    /// </summary>
    public partial class Store_Home_Page : Page
    {
        string Type = "Programs";

        public Store_Home_Page()
        {
            InitializeComponent();

            if (Properties.Settings.Default.User_Login.ToLower() == "admin")
            {
                Store_Home_Admin_But.Visibility = Visibility.Visible;
            }
            if (Properties.Settings.Default.User_Login.ToLower() == "admin" || Properties.Settings.Default.User_Role == "Tester")
            {
                Store_Home_Test_But.Visibility = Visibility.Visible;
            }

            StaticVars.Store_Home_Frame = Store_Home_Frame;
            Store_Home_Frame.Content = new Store_Home_Application_Page(Type);
        }

        private void Store_Home_Programs_But_Click(object sender, RoutedEventArgs e)
        {
            Type = "Programs";
            Store_Home_Frame.NavigationService.Navigate(new Store_Home_Application_Page(Type));
        }

        private void Store_Home_Games_But_Click(object sender, RoutedEventArgs e)
        {
            Type = "Games";
            Store_Home_Frame.NavigationService.Navigate(new Store_Home_Application_Page(Type));
        }

        private void Store_Home_Test_But_Click(object sender, RoutedEventArgs e)
        {
            Type = "Tests";
            Store_Home_Frame.NavigationService.Navigate(new Store_Home_Application_Page(Type));
        }

        private void Store_Home_Admin_But_Click(object sender, RoutedEventArgs e)
        {
            Type = "Admins";
            Store_Home_Frame.NavigationService.Navigate(new Store_Home_Application_Page(Type));
        }
    }
}
