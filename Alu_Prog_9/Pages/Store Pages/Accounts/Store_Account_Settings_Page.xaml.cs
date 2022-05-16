using Alu_Prog_9.Classes;
using Alu_Prog_9.Pages.Login_Pages;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Alu_Prog_9.Pages.Store_Pages.Accounts
{
    /// <summary>
    /// Логика взаимодействия для Store_Account_Settings_Page.xaml
    /// </summary>
    public partial class Store_Account_Settings_Page : Page
    {
        public Store_Account_Settings_Page()
        {
            InitializeComponent();
        }

        private void Exit_Account_But_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Authorization = 0;
            Properties.Settings.Default.Save();
            if (File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated\\Activated_File.txt"))
            {
                File.Delete("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated\\Activated_File.txt");
            }
            //Login_StackPanel.Visibility = Visibility.Hidden;
            StaticVars.Main_Frame.NavigationService.Navigate(new Login_Page());
        }
    }
}
