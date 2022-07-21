using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

namespace Auto_Run_Al_Store
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Source = "";
        public MainWindow()
        {
            InitializeComponent();
            
            string source = Assembly.GetExecutingAssembly().Location;
            int counts = source.Count(f => f == '\\'); counts--;
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
            Process process = Process.Start(new ProcessStartInfo
            {
                FileName = Source,
                Arguments = "/AutoRun_Update"
                //Arguments = "/Hi"
            });
            Close();
        }
    }
}
