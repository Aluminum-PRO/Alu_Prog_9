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
            Store_Settings_Frame.NavigationService.Navigate(new Store_Settings_Update_Page());
        }

        private void Store_Settings_Al_Bot_But_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
