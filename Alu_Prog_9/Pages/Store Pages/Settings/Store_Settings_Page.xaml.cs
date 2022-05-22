using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Alu_Prog_9.Pages.Store_Pages.Settings
{
    /// <summary>
    /// Логика взаимодействия для Store_Settings_Page.xaml
    /// </summary>
    public partial class Store_Settings_Page : Page
    {
        public Store_Settings_Page()
        {
            InitializeComponent();
            Store_Settings_Frame.Content = new Store_Settings_Update_Page();
        }

        private void Store_Settings_Update_But_Click(object sender, RoutedEventArgs e)
        {
            //if (Store_Settings_Update_But.IsChecked == false)
            Store_Settings_Frame.NavigationService.Navigate(new Store_Settings_Update_Page());
        }

        private void Store_Settings_Data_Loaded_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Settings_Frame.NavigationService.Navigate(new Store_Settings_Data_Loaded_Page());
        }

        private void Store_Settings_Al_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            //if (Store_Settings_Al_Bot_But.IsChecked == false)
            MessageBox.Show("Привязку к боту пока что не завезли!");
        }

        private void Store_Settings_Errors_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Settings_Frame.NavigationService.Navigate(new Store_Settings_Errors_Page());
        }

        private void Store_Settings_Frame_Navigated(object sender, NavigationEventArgs e)
        {
            while (Store_Settings_Frame.NavigationService.CanGoBack)
            {
                Store_Settings_Frame.NavigationService.RemoveBackEntry();
            }
            Store_Settings_Frame.NavigationService.RemoveBackEntry();
        }
    }
}
