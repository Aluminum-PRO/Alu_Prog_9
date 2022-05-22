using Alu_Prog_9.Classes;
using Alu_Prog_9.Pages.Store_Pages.Admins.Update;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Alu_Prog_9.Pages.Store_Pages.Admins
{
    /// <summary>
    /// Логика взаимодействия для Store_Admin_Page.xaml
    /// </summary>
    public partial class Store_Admin_Page : Page
    {
        public Store_Admin_Page()
        {
            InitializeComponent();
            StaticVars.Store_Admin_Frame = Store_Admin_Frame;
            Store_Admin_Frame.Content = new Store_Admin_Update_Page();
        }

        private void Store_Admin_Profiles_But_Click(object sender, RoutedEventArgs e)
        {
            //if (Store_Admin_Profiles_But.IsChecked == false)
                Store_Admin_Frame.NavigationService.Navigate(new Store_Admin_Profiles_Page());
        }

        private void Store_Admin_Mailing_But_Click(object sender, RoutedEventArgs e)
        {
            //if (Store_Admin_Mailing_But.IsChecked == false)
                Store_Admin_Frame.NavigationService.Navigate(new Store_Admin_Mailing_Page());
        }

        private void Store_Admin_Update_But_Click(object sender, RoutedEventArgs e)
        {
            //if (Store_Admin_Update_But.IsChecked == false)
                Store_Admin_Frame.NavigationService.Navigate(new Store_Admin_Update_Page());
        }

        private void Store_Admin_Frame_Navigated(object sender, NavigationEventArgs e)
        {
            while (Store_Admin_Frame.NavigationService.CanGoBack)
            {
                Store_Admin_Frame.NavigationService.RemoveBackEntry();
            }
            Store_Admin_Frame.NavigationService.RemoveBackEntry();
        }
    }
}
