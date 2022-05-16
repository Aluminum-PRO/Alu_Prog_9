using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.Pages.Login_Pages;
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

namespace Alu_Prog_9.Pages.Registration_Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration_User_Page.xaml
    /// </summary>
    public partial class Registration_User_Page : Page
    {
        MySql_Handler My_Hand;

        string _Email;
        int Gender, Mailing;

        public Registration_User_Page(string Email, string Name, string SurName)
        {
            InitializeComponent();

            _Email = Email;
            Name_TextBox.Text = Name; SurName_TextBox.Text = SurName;
            Login_TextBox.Text = StaticVars.Login;
            if (StaticVars.Gender != 0)
            { 
                if (StaticVars.Gender == 1)
                {
                    Men_CheckBox.IsChecked = true;
                }
                else if (StaticVars.Gender == 2)
                {
                    WoMen_CheckBox.IsChecked = true;
                }
            }
        }

        private void Back_But_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration_Page(_Email, Name_TextBox.Text, SurName_TextBox.Text));
            StaticVars.Login = Login_TextBox.Text;

            if (Men_CheckBox.IsChecked == true)
            {
                StaticVars.Gender = 1;
            }
            else if (WoMen_CheckBox.IsChecked == true)
            {
                StaticVars.Gender = 2;
            }
        }

        private void Enter_But_Click(object sender, RoutedEventArgs e)
        {
            if (Name_TextBox.Text == "" || SurName_TextBox.Text == "")
            {
                MessageBox.Show("Поля 'Имя' и 'Фамилия' не могут быть пустыми");
                return;
            }

            if (Men_CheckBox.IsChecked == false && WoMen_CheckBox.IsChecked == false)
            {
                MessageBox.Show("Вы не указали свой пол");
                return;
            }

            if (Login_TextBox.Text == "")
            {
                MessageBox.Show("Поле 'Логин' не может быть пустым");
                return;
            }

            if (Pass_TextBox.Password == "")
            {
                MessageBox.Show(" Поле 'Пароль' не может быть пустым");
                return;
            }
            else if ( Pass_TextBox.Password != Pass_Repeat_TextBox.Password )
            {
                MessageBox.Show(" Пароли не совпадают");
                return;
            }

            if (Men_CheckBox.IsChecked == true)
            {
                Gender = 1;
            }
            else if (WoMen_CheckBox.IsChecked == true)
            {
                Gender = 2;
            }

            if (Mailing_CheckBox.IsChecked == true)
            {
                Mailing = 0;
            }
            else if (Mailing_CheckBox.IsChecked == false)
            {
                Mailing = 1;
            }

            My_Hand = new MySql_Handler();
            My_Hand.Registration(_Email, Name_TextBox.Text, SurName_TextBox.Text, Gender, Login_TextBox.Text, Pass_TextBox.Password, Mailing, out string Result);
            if (Result == "Login")
            {
                MessageBox.Show(" Такой логин уже существует в сети 'Aluminum-Company', придумайте другой.", "Регистрация");
                return;
            }
            else if (Result == "True")
            {
                MessageBox.Show(" Вы успешно зарегистрировали свой аккаунт в сети 'Aluminum-Company'.", "Регистрация");
                NavigationService.Navigate(new Login_Page());
            }
            else if (Result == "False")
            {
                MessageBox.Show(" Не удалось зарегистрировать ваш аккаунт. Проверьте подключение к интернету или обратитесь к разработчику за помощью. \n Aluminum.Company163@gmail.com или Aluminum.Company163.reserve@gmail.com", "Ошибка!");
            }
        }
    }
}
