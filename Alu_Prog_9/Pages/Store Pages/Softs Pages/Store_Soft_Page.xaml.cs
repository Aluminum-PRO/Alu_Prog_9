using Alu_Prog_9.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Alu_Prog_9.Pages.Store_Pages.Softs_Pages
{
    /// <summary>
    /// Логика взаимодействия для Store_Soft_Page.xaml
    /// </summary>
    public partial class Store_Soft_Page : Page
    {
        private string downloaded;

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
            //if (Store_Soft_Downloaded_But.IsChecked == false)
                Store_Soft_Frame.NavigationService.Navigate(new Store_Soft_Application_Page(downloaded));
        }

        private void Store_Soft_Not_Downloaded_But_Click(object sender, RoutedEventArgs e)
        {
            downloaded = "False";
            //if (Store_Soft_Not_Downloaded_But.IsChecked == false)
                Store_Soft_Frame.NavigationService.Navigate(new Store_Soft_Application_Page(downloaded));
        }

        private void Store_Soft_Frame_Navigated(object sender, NavigationEventArgs e)
        {
            while (Store_Soft_Frame.NavigationService.CanGoBack)
            {
                Store_Soft_Frame.NavigationService.RemoveBackEntry();
            }
            Store_Soft_Frame.NavigationService.RemoveBackEntry();
        }
    }
}
