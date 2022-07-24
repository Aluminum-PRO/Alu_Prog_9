using Avto_Run_Al_Store.MySql_Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
//using forms = System.Windows.Forms;

namespace Auto_Run_Al_Store
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MySql_Handler my_Handler;
        private string Source = "";
        public MainWindow()
        {
            InitializeComponent();

            string source = Assembly.GetExecutingAssembly().Location;
            int counts = source.Count(f => f == '\\'); counts -= 2;
            for (int i = 1; i <= counts; i++)
            {
                if (i != counts)
                {
                    Source += source.Split('\\')[i - 1] + '\\';
                }
                else if (i == counts)
                {
                    Source += source.Split('\\')[i - 1];
                }
            }
            Source += "\\Al-Store.exe";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            my_Handler = new MySql_Handler();
            my_Handler.Getting_Update_Data(Source);

            try
            {
                Process process = Process.Start(new ProcessStartInfo
                {
                    FileName = Source += "\\Al-Store\\Al-Store.exe",
                    //Arguments = "/Hi"
                    Arguments = "/AutoRun_Update"
                });
            }
            catch (Exception Ex) { MessageBox.Show("    Авто обновление Al-Store не запущено по причине ниже. Если вам мешает окно подтверждения, вы можете отключить функцию автообновления и уведомдения об обновлении в настройках обновления Al-Store.\n\n    Причина: " + Ex.Message, "Auto Update Al-Store", MessageBoxButton.OK, MessageBoxImage.Error); }
            Close();
        }
    }
}
