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

namespace Alu_Prog_9.Pages.Store_Pages.News
{
    /// <summary>
    /// Логика взаимодействия для Store_News_Page.xaml
    /// </summary>
    public partial class Store_News_Page : Page
    {
        public Store_News_Page()
        {
            InitializeComponent();
            Store_News_Frame.Content = new Store_News_Update_History_Page();
        }

        private void Store_News_Update_History_But_Click(object sender, RoutedEventArgs e)
        {
            Store_News_Frame.NavigationService.Navigate(new Store_News_Update_History_Page());
        }

        private void Store_News_News_But_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Store_News_Users_But_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Store_News_Frame_Navigated(object sender, NavigationEventArgs e)
        {
            while (Store_News_Frame.NavigationService.CanGoBack)
            {
                Store_News_Frame.NavigationService.RemoveBackEntry();
            }
            Store_News_Frame.NavigationService.RemoveBackEntry();
        }
    }
}
