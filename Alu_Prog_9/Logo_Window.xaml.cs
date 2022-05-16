using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.Services;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;

namespace Alu_Prog_9
{
    /// <summary>
    /// Логика взаимодействия для Logo_Window.xaml
    /// </summary>
    public partial class Logo_Window : Window
    {
        MySql_Handler My_Hand;
        Handler handler;

        string Activated_File_Text;

        public Logo_Window()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string Source = "";
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

            Process[] processList = Process.GetProcessesByName("Al-Store");
            if (processList.Length > 1)
            {
                Opacity = 0;
                Environment.Exit(0);
            }
            else
            {
                Properties.Settings.Default.Full_Path = Source;
                Properties.Settings.Default.Path_Store = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Al-Store");
                Properties.Settings.Default.Path_Programs = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Programs");
                Properties.Settings.Default.Path_Games = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Games");
                Properties.Settings.Default.Path_Tests = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Tests");
                Properties.Settings.Default.Path_Bot_MTAProvince = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Bot by MaxNeck");
                Properties.Settings.Default.Path_Soft = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Soft");
                Properties.Settings.Default.Path_Updater = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Updater");
                Properties.Settings.Default.Path_Shortcut = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Al-Store\\Иконки ярлыков");

                Properties.Settings.Default.User_Identyty = Environment.UserName;

                bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
                
                if (isAdmin == false && Source.Remove(1) == "C")
                {
                    Opacity = 0;
                    MessageBox.Show(" Вашу версию программы нельзя использовать на диске 'C'. Перенесите её на другой диск, или установите 'Standart Edition' версию.\n После нажатия кнопки 'Ок', будет открыта страница разработчика, попросите его помочь вам с этой проблемой.", " Al-Store", MessageBoxButton.OK, MessageBoxImage.Error);
                    Process.Start("https://vk.com/aluminum343");
                    Environment.Exit(0);
                }

                if (File.Exists(Properties.Settings.Default.Full_Path + "\\Edition\\TPK_Edition.txt"))
                {
                    Properties.Settings.Default.Edition = "'TPK'";
                }
                else if (!File.Exists(Properties.Settings.Default.Full_Path + "\\Edition\\TPK_Edition.txt"))
                {
                    Properties.Settings.Default.Edition = "'Standart'";
                }

                if (File.Exists(Properties.Settings.Default.Path_Store + "\\Al-Store.exe"))
                {
                    FileVersionInfo myFileVersionInfo_Store = FileVersionInfo.GetVersionInfo(Properties.Settings.Default.Path_Store + "\\Al-Store.exe");
                    Properties.Settings.Default.Ver_Store = myFileVersionInfo_Store.FileVersion;
                    if (Convert.ToInt32(Properties.Settings.Default.Ver_Store.Split('.')[0]) != 0)
                    { Properties.Settings.Default.Ver_Store += ".Release"; }
                    else if (Convert.ToInt32(Properties.Settings.Default.Ver_Store.Split('.')[1]) != 0)
                    { Properties.Settings.Default.Ver_Store += ".Beta"; }
                    else if (Convert.ToInt32(Properties.Settings.Default.Ver_Store.Split('.')[2]) != 0)
                    { Properties.Settings.Default.Ver_Store += ".Alpha"; }
                    else if (Convert.ToInt32(Properties.Settings.Default.Ver_Store.Split('.')[3]) != 0)
                    { Properties.Settings.Default.Ver_Store += ".Pre-Alpha"; }
                }
                if (File.Exists(Properties.Settings.Default.Path_Updater + "\\Updater for Al-Store.exe"))
                {
                    FileVersionInfo myFileVersionInfo_Store = FileVersionInfo.GetVersionInfo(Properties.Settings.Default.Path_Updater + "\\Updater for Al-Store.exe");
                    Properties.Settings.Default.Ver_Updater = myFileVersionInfo_Store.FileVersion;
                    if (Convert.ToInt32(Properties.Settings.Default.Ver_Updater.Split('.')[0]) != 0)
                    { Properties.Settings.Default.Ver_Updater += ".Release"; }
                    else if (Convert.ToInt32(Properties.Settings.Default.Ver_Updater.Split('.')[1]) != 0)
                    { Properties.Settings.Default.Ver_Updater += ".Beta"; }
                    else if (Convert.ToInt32(Properties.Settings.Default.Ver_Updater.Split('.')[2]) != 0)
                    { Properties.Settings.Default.Ver_Updater += ".Alpha"; }
                    else if (Convert.ToInt32(Properties.Settings.Default.Ver_Updater.Split('.')[3]) != 0)
                    { Properties.Settings.Default.Ver_Updater += ".Pre-Alpha"; }
                }

                Properties.Settings.Default.Save();
            }

            _ = Opacity_LoadAsync();
        }

        public async Task Opacity_LoadAsync()
        {
            await Task.Run(() => { }); //TODO: Доделать асинхронную заставку лого!
            for (Opacity = 0; Opacity <= 1;)
            {
                Opacity += 0.05;
                await Task.Delay(30);
            }

            for (Opacity = 1; Opacity >= 0.4;)
            {
                Opacity -= 0.05;
                await Task.Delay(30);
            }

            await Task.Delay(50);

            for (Opacity = 0.4; Opacity <= 1;)
            {
                Opacity += 0.05;
                await Task.Delay(30);
            }

            await Task.Delay(1000);

            for (Opacity = 1; Opacity >= 0;)
            {
                Opacity -= 0.05;
                await Task.Delay(30);
            }

            My_Hand = new MySql_Handler();
            My_Hand.Getting_Data();
            My_Hand.Get_Update_Information_Al_Store();
            if (Properties.Settings.Default.Authorization == 1)
            {
                My_Hand.Getting_User_Data();
                My_Hand.Checking_For_Account_Creation();
                My_Hand.Getting_User_Properties();
                My_Hand.Get_Update_Information_Application();
            }
            else if (Properties.Settings.Default.Authorization == 0)
            {
                if (File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated\\Activated_File.txt"))
                {
                    Activated_File_Text = File.ReadAllText("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\Al-Store\\Activated\\Activated_File.txt");
                    Properties.Settings.Default.User_Login = Activated_File_Text.Split(' ')[0]; Properties.Settings.Default.User_Password = Activated_File_Text.Split(' ')[1];

                    My_Hand.Restoring_Authorization(out string Restoring_Authorization_Bool);

                    if (Restoring_Authorization_Bool == "True")
                    {
                        My_Hand.Getting_User_Data();
                        My_Hand.Checking_For_Account_Creation();
                        My_Hand.Getting_User_Properties();
                        My_Hand.Get_Update_Information_Application();

                        //TODO: Переделать окно уведомления о восстановлении активации
                        MessageBox.Show(" Сработала функция восстановления активации.\n Добро пожаловать, " + Properties.Settings.Default.User_Name + " " + Properties.Settings.Default.User_SurName + "!", "Al-Store");
                        //MyShortNotification.ShowDialog("Сработала функция восстановления активации.\n Добро пожаловать, " + Properties.Settings.Default.User_Name + " " + Properties.Settings.Default.User_SurName + "!");
                    }
                }
            }
            

            if (!File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\Al-Store.lnk") && Properties.Settings.Default.Start_Creating_Shortcut == 0)
            {
                MessageBoxResult result = MessageBox.Show(" На вашем рабочем столе нет ярлыка 'Al-Store'.\n\n   Создать?", "Al-Store", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    handler = new Handler();
                    handler.Create_Shortcut("Al-Store", "Al-Store", "", "Al-Store - Магазин приложений", Properties.Settings.Default.Ver_Store, "Ctrl+Shift+A");
                }
                else if (result == MessageBoxResult.No)
                {
                    MessageBoxResult _result = MessageBox.Show(" Не предлагать больше этот вопрос?", "Al-Store", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (_result == MessageBoxResult.Yes)
                    {
                        Properties.Settings.Default.Start_Creating_Shortcut = 1;
                        Properties.Settings.Default.Save();
                        My_Hand.Save_Start_Creating_Shortcut_Properties();
                        //TODO: Есть вопросы к функции "не спаршивать больше про создание ярлыка"
                    }
                }
            }
            else if (File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\Al-Store.lnk") && Properties.Settings.Default.First_Started == 0)
            {
                Properties.Settings.Default.First_Started = 1;
                Properties.Settings.Default.Save();

                handler = new Handler();
                handler.Create_Shortcut("Al-Store", "Al-Store", "", "Al-Store - Магазин приложений", Properties.Settings.Default.Ver_Store, "Ctrl+Shift+A");
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
