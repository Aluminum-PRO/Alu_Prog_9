using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
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
    /// Логика взаимодействия для Recovery_New_Pass_Page.xaml
    /// </summary>
    public partial class Recovery_New_Pass_Page : Page
    {
        MySql_Handler My_Hand;

        string Email = StaticVars.Rec_Email, _Login;

        public Recovery_New_Pass_Page(string Login)
        {
            InitializeComponent();
            _Login = Login;
        }

        private void Back_But_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Recovery_Pass_Page());
        }

        private void Enter_But_Click(object sender, RoutedEventArgs e)
        {
            if (Password_TextBox_1.Password == "")
            {
                MessageBox.Show(" Поле 'Пароль' не может быть пустым");
                return;
            }
            else if (Password_TextBox_1.Password != Password_TextBox_2.Password)
            {
                MessageBox.Show(" Пароли не совпадают");
                return;
            }

            My_Hand = new MySql_Handler();
            My_Hand.Recovery_New_Pass(Email, Password_TextBox_1.Password, _Login, out string Result);
            if (Result == "True")
            {
                MessageBox.Show(" Ваш пароль изменён.", "Восстановление");
                NavigationService.Navigate(new Login_Page());
            }
            else if (Result == "False")
            {
                MessageBox.Show(" Не удалось сменить пароль. Проверьте подключение к интернету или обратитесь к разработчику за помощью. \n Aluminum.Company163@gmail.com или Aluminum.Company163.reserve@gmail.com", "Ошибка!");
                return;
            }
        }
    }
}
