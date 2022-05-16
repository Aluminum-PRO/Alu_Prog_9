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

namespace Alu_Prog_9.Pages.Store_Pages.Softs_Pages
{
    /// <summary>
    /// Логика взаимодействия для Store_Soft_Page.xaml
    /// </summary>
    public partial class Store_Soft_Page : Page
    {
        string downloaded;

        public Store_Soft_Page()
        {
            InitializeComponent();
            downloaded = "False";

            StaticVars.Store_Soft_Downloaded_But = Store_Soft_Downloaded_But;
            StaticVars.Store_Soft_Not_Downloaded_But = Store_Soft_Not_Downloaded_But;

            StaticVars.Search_Text_Box = Search_Soft_Box;
            StaticVars.Count_Soft_Text_box = Count_Soft_Text_box;
            StaticVars.Store_Soft_Frame = Store_Soft_Frame;
            Store_Soft_Frame.Content = new Store_Soft_Application_Page(downloaded);
        }

        private void Store_Soft_Downloaded_But_Click(object sender, RoutedEventArgs e)
        {
            downloaded = "True";
            Store_Soft_Frame.NavigationService.Navigate(new Store_Soft_Application_Page(downloaded));
        }

        private void Store_Soft_Not_Downloaded_But_Click(object sender, RoutedEventArgs e)
        {
            downloaded = "False";
            Store_Soft_Frame.NavigationService.Navigate(new Store_Soft_Application_Page(downloaded));
        }
    }
}
