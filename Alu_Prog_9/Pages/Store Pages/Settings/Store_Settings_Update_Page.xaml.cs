using Alu_Prog_9.Classes;
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

namespace Alu_Prog_9.Pages.Store_Pages.Settings
{
    /// <summary>
    /// Логика взаимодействия для Store_Settings_Update_Page.xaml
    /// </summary>
    public partial class Store_Settings_Update_Page : Page
    {
        public Store_Settings_Update_Page()
        {
            InitializeComponent();

            Installed_Version_Al_TextBlock.Text += Properties.Settings.Default.Ver_Store;
            Installed_Version_Up_TextBlock.Text += Properties.Settings.Default.Ver_Updater;

            Server_Version_Al_TextBlock.Text += Properties.Settings.Default.New_Ver_Store;
            Server_Version_Up_TextBlock.Text += Properties.Settings.Default.New_Ver_Updater;

            FileInfo file = new System.IO.FileInfo(Properties.Settings.Default.Path_Store + "\\Al-Store.exe");
            double size = file.Length;
            size /= 1048576;

            Size_Al_TextBlock.Text += size.ToString("0.00") + " МБ";

            file = new System.IO.FileInfo(Properties.Settings.Default.Path_Updater + "\\Updater for Al-Store.exe");
            size = file.Length;
            size /= 1048576;
            Size_Up_TextBlock.Text += size.ToString("0.00") + " МБ";

            Location_Al_TextBlock.Text += Properties.Settings.Default.Path_Store;
            Location_Up_TextBlock.Text += Properties.Settings.Default.Path_Updater;

            if (Properties.Settings.Default.Ver_Store == Properties.Settings.Default.New_Ver_Store)
            {
                Al_Update.Visibility = Visibility.Hidden;
            }
            if (Properties.Settings.Default.Ver_Updater == Properties.Settings.Default.New_Ver_Updater)
            {
                Up_Update.Visibility = Visibility.Hidden;
            }
        }

        private void Start_Update_But_Click(object sender, RoutedEventArgs e)
        {
            Update_Al_Window update_Al_Window = new Update_Al_Window();
            update_Al_Window.Show();
            StaticVars.MainWindow.Hide();
        }
    }
}
