using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.Pages.Registration_Pages;
using Alu_Prog_9.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Alu_Prog_9.Pages.Login_Pages
{
    /// <summary>
    /// Логика взаимодействия для Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Page
    {
        Telegram_Bot_Send_Activity telegram_Bot_Send_Activity = new Telegram_Bot_Send_Activity();
        MySql_Handler My_Hand;

        string Login_User, Password_User;

        public Login_Page()
        {
            InitializeComponent();

            KeyDown += (s, e) => { if (e.Key == Key.Return) Login_But_Click(Login_But, null); };

            Login_TextBox.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));
            Password_TextBox.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));

        }

        public void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        { }

        private void Recovery_Pass_But_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Recovery_Pass_Page());
        }

        private void Registration_But_Click(object sender, RoutedEventArgs e)
        {
            if (StaticVars.Count_Update_Al != 0)
            {
                MessageBoxResult result = MessageBox.Show(" Для регистрации необходимо обновление.\n    Обновиться?", "Al-Store", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Update_Al_Window update_Al_Window = new Update_Al_Window(false);
                    update_Al_Window.Show();
                    StaticVars.MainWindow.Hide();
                }
                else if (result == MessageBoxResult.No)
                { return; }
            }
            else
            { NavigationService.Navigate(new Registration_Page()); }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Login_TextBox.Text = Properties.Settings.Default.User_Login;
            Password_TextBox.Password = Properties.Settings.Default.User_Password;
        }

        private void Login_TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsLetterOrDigit(e.Text, 0) && !Char.IsPunctuation(e.Text, 0);
        }

        private void Login_But_Click(object sender, RoutedEventArgs e)
        {
            Login_User = Login_TextBox.Text;
            Password_User = Password_TextBox.Password;

            if (Login_User == "" || Password_User == "")
            {
                MessageBox.Show(" Поле 'Логина' или 'Пароля' не может быть пустым!", " Al-Store");
                return;
            }

            My_Hand = new MySql_Handler();
            My_Hand.Login(Login_User, Password_User, out string Login_Bool);

            if (Login_Bool == "True")
            {
                telegram_Bot_Send_Activity.Al_Store_Started();
                MessageBox.Show(" Вы успешно авторизовались.", " Al-Store");

                Properties.Settings.Default.Authorization = 1;
                if (Directory.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated"))
                { }
                else
                {
                    Directory.CreateDirectory("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated");
                }
                File.WriteAllText("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated\\Activated_File.txt", Properties.Settings.Default.User_Login + " " + Properties.Settings.Default.User_Password);

                My_Hand.Getting_User_Data();

                Properties.Settings.Default.Save();

                NavigationService.Navigate(new Main_Page());

                //Process[] processList_Al_Bot = Process.GetProcessesByName("Al-Bot");
                //if (processList_Al_Bot.Length == 0)
                //{
                //    //TODO: Добавить аргументы к запуску Бота (Login_Page)
                //    try { Process.Start(Properties.Settings.Default.Full_Path + "\\Al-Store\\Al-Bot\\Al-Bot.exe"); }
                //    catch { }

                //}
            }
            else if (Login_Bool == "False")
            { MessageBox.Show(" Вы не верно указали логин или пароль.", " Al-Store"); }
        }
    }
}
