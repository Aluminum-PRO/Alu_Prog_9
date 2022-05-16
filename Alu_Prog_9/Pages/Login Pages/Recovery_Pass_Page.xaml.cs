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
    /// Логика взаимодействия для Recovery_Pass_Page.xaml
    /// </summary>
    public partial class Recovery_Pass_Page : Page
    {
        MySql_Handler My_Hand;

        string Pass_Reg;

        public Recovery_Pass_Page()
        {
            InitializeComponent();

            Email_TextBox.Text = StaticVars.Rec_Email;
        }

        private void Back_But_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login_Page());
        }

        private void Enter_But_Click(object sender, RoutedEventArgs e)
        {
            if (Email_TextBox.Text.IndexOf("@") == -1 || Email_TextBox.Text.IndexOf(".") == -1 || Email_TextBox.Text.Length <= 8)
            {
                MessageBox.Show("Поле 'Email' заполнено некорректно");
                return;
            }

            My_Hand = new MySql_Handler();
            My_Hand.Recovery_Pass(Email_TextBox.Text, out Pass_Reg, out string Login);
            if (Pass_Reg == "Err")
            { MessageBox.Show(" Не удалось отправить код подтверждения. Проверьте подключение к интернету или обратитесь к разработчику за помощью.", "Ошибка!"); return; }
            if (Pass_Reg == "NoEmail")
            { MessageBox.Show(" Аккаунта с такой почтой не существует.", "Восстановление"); return; }
            StaticVars.Rec_Email = Email_TextBox.Text;

            NavigationService.Navigate(new Recovery_Code_Page(Pass_Reg, Login));
        }
    }
}
