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
    /// Логика взаимодействия для Store_Library_Page.xaml
    /// </summary>
    public partial class Store_Library_Page : Page
    {
        string install;

        public Store_Library_Page()
        {
            InitializeComponent();
            install = "True";

            StaticVars.Store_Library_Installed_But = Store_Library_Installed_But;
            StaticVars.Store_Library_Not_Installed_But = Store_Library_Not_Installed_But;

            StaticVars.Store_Library_Frame = Store_Library_Frame;
            Store_Library_Frame.Content = new Store_Library_Application_Page(install);
        }

        private void Store_Library_Installed_But_Click(object sender, RoutedEventArgs e)
        {
            install = "True";
            Store_Library_Frame.NavigationService.Navigate(new Store_Library_Application_Page(install));
        }

        private void Store_Library_Not_Installed_But_Click(object sender, RoutedEventArgs e)
        {
            install = "False";
            Store_Library_Frame.NavigationService.Navigate(new Store_Library_Application_Page(install));
        }
    }
}
