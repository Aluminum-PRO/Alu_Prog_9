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

namespace Alu_Prog_9.Pages.Login_Pages
{
    /// <summary>
    /// Логика взаимодействия для Recovery_Code_Page.xaml
    /// </summary>
    public partial class Recovery_Code_Page : Page
    {
        string Pass, _Login;
        int Err = 3;

        public Recovery_Code_Page(string Pass_Reg, string Login)
        {
            InitializeComponent();

            Pass = Pass_Reg;
            _Login = Login;
        }

        private void Enter_But_Click(object sender, RoutedEventArgs e)
        {
            if (Email_Code_TextBox.Text == "")
            {
                MessageBox.Show("Поле 'Код' не может быть пустым");
                return;
            }
            else if (Email_Code_TextBox.Text.Length != 4)
            {
                MessageBox.Show("Поле 'Код' должно содержать 4 цифры");
                return;
            }
            if (Email_Code_TextBox.Text != Pass)
            {
                Err--;
                if (Err > 0)
                { MessageBox.Show(" Введёный код не верен.\n    Попыток осталось " + Err, "Al-Store", MessageBoxButton.OK, MessageBoxImage.Error); }
                else if (Err == 0)
                {
                    MessageBox.Show(" Вы три раза ввели не верный код, вы будите направлены на страницу отправки кода.", "Al-Store", MessageBoxButton.OK, MessageBoxImage.Error);
                    NavigationService.Navigate(new Recovery_Pass_Page());
                }
                return;
            }
            NavigationService.Navigate(new Recovery_New_Pass_Page(_Login));
        }
        
        private void Email_Code_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.Text, 0);
        }

        private void Back_But_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Recovery_Pass_Page());
        }
    }
}
