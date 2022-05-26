using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Alu_Prog_9.Pages.Store_Pages.Accounts
{
    /// <summary>
    /// Логика взаимодействия для Store_Account_Page.xaml
    /// </summary>
    public partial class Store_Account_Page : Page
    {
        public Store_Account_Page()
        {
            InitializeComponent();

            Store_Account_Frame.Content = new Store_Account_Account_Page();
        }

        private void Store_Account_Account_But_Click(object sender, RoutedEventArgs e)
        {
            //if (Store_Account_Account_But.IsChecked == false)
                NavigationService.Navigate(new Store_Account_Account_Page());
        }

        private void Store_Account_Settings_But_Click(object sender, RoutedEventArgs e)
        {
            //if (Store_Account_Settings_But.IsChecked == false)
                NavigationService.Navigate(new Store_Account_Settings_Page());
        }

        private void Store_Account_Frame_Navigated(object sender, NavigationEventArgs e)
        {
            while (Store_Account_Frame.NavigationService.CanGoBack)
            {
                Store_Account_Frame.NavigationService.RemoveBackEntry();
            }
            Store_Account_Frame.NavigationService.RemoveBackEntry();
        }

        private void Store_Account_Mailing_But_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
