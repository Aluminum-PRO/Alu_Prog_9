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
            Store_Admin_Frame.Content = new Store_Admin_Update_Page();
        }

        private void Store_Admin_Profiles_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Admin_Frame.NavigationService.Navigate(new Store_Admin_Profiles_Page());
        }

        private void Store_Admin_Mailing_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Admin_Frame.NavigationService.Navigate(new Store_Admin_Mailing_Page());
        }

        private void Store_Admin_Update_But_Click(object sender, RoutedEventArgs e)
        {
            Store_Admin_Frame.NavigationService.Navigate(new Store_Admin_Update_Page());
        }
    }
}
