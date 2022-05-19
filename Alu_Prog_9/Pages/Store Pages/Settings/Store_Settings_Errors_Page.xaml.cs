using Alu_Prog_9.Services;
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
    /// Логика взаимодействия для Store_Settings_Errors_Page.xaml
    /// </summary>
    public partial class Store_Settings_Errors_Page : Page
    {
        Errors_Saves_and_Sending errors_Saves_And_Sending = new Errors_Saves_and_Sending();
        public Store_Settings_Errors_Page()
        {
            InitializeComponent();
        }

        private void Send_Errors_But_Click(object sender, RoutedEventArgs e)
        {
            try { errors_Saves_And_Sending.Send_Recording_Errors(); MessageBox.Show(" Отчёт об ошибках отправлен", "Al-Store", MessageBoxButton.OK, MessageBoxImage.Information); }
            catch (Exception ex)
            { 
                errors_Saves_And_Sending.Recording_Errors(ex);
                MessageBox.Show(" Отчёт об ошибках не отправлен.\n\n " + ex.Message + "\n Код ошибки: " + ex.HResult, "Al-Store", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
