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
    /// Логика взаимодействия для Registration_Page.xaml
    /// </summary>
    public partial class Registration_Page : Page
    {
        MySql_Handler My_Hand;

        string Pass_Reg;

        public Registration_Page()
        {
            InitializeComponent();
        }

        public Registration_Page( string Email, string Name, string SurName)
        {
            InitializeComponent();
            Email_TextBox.Text = Email; Name_TextBox.Text = Name; SurName_TextBox.Text = SurName;
        }

        private void Enter_But_Click(object sender, RoutedEventArgs e)
        {
            if (Email_TextBox.Text == "" || Name_TextBox.Text == "" || SurName_TextBox.Text == "")
            {
                MessageBox.Show("Необходимо заполнить все поля");
                return;
            }
            if (Email_TextBox.Text.IndexOf("@") == -1 || Email_TextBox.Text.IndexOf(".") == -1 || Email_TextBox.Text.Length <= 8)
            {
                MessageBox.Show("Поле 'Email' заполнено некорректно");
                return;
            }

            My_Hand = new MySql_Handler();
            My_Hand.Registration_Code(Email_TextBox.Text, Name_TextBox.Text, SurName_TextBox.Text, out Pass_Reg);
            if (Pass_Reg == "@")
            {
                MessageBox.Show(Pass_Reg);
                NavigationService.Navigate(new Login_Page());
                return;
            }
            NavigationService.Navigate(new Registration_Code_Page(Pass_Reg, Email_TextBox.Text, Name_TextBox.Text, SurName_TextBox.Text));
        }

        private void Back_But_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login_Page());
        }
    }
}
