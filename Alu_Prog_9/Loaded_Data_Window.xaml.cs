using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Alu_Prog_9
{
    /// <summary>
    /// Логика взаимодействия для Loaded_Data_Window.xaml
    /// </summary>
    public partial class Loaded_Data_Window : Window
    {
        private Telegram_Bot_Send_Activity telegram_Bot_Send_Activity = new Telegram_Bot_Send_Activity();

        private MySql_Handler My_Hand;
        private Handler handler;
        private string Activated_File_Text;
        private bool Copy_Check = false, AutoRun_Update = false;

        public Loaded_Data_Window()
        {
            InitializeComponent();
            telegram_Bot_Send_Activity.Al_Store_Started();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1)
            {
                Copy_Check = true;
            }
            else
            {
                foreach (string element in args)
                {
                    if (element == "/Hi")
                    {
                        MessageBox.Show("Hi, Admin!", "Al-Store", MessageBoxButton.OK, MessageBoxImage.Information);
                        Environment.Exit(0);
                    }
                    if (element == "/AutoRun_Update")
                    {
                        AutoRun_Update = true;
                    }
                }
            }

            if (Copy_Check)
            {
                Process[] processList = Process.GetProcessesByName("Al-Store");
                if (processList.Length > 1)
                { Environment.Exit(0); }
            }
            Load();
        }

        private void Load()
        {
            Thread thread = new Thread(() =>
            {
                Logo_Window logo_Window = new Logo_Window();
                logo_Window.Show();

                logo_Window.Closed += (sender2, e2) =>
                    logo_Window.Dispatcher.InvokeShutdown();

                System.Windows.Threading.Dispatcher.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            if (!AutoRun_Update)
                thread.Start();

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

            Properties.Settings.Default.Full_Path = Source;
            Properties.Settings.Default.Path_Store = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Al-Store");
            Properties.Settings.Default.Path_Programs = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Programs");
            Properties.Settings.Default.Path_Games = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Games");
            Properties.Settings.Default.Path_Tests = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Tests");
            Properties.Settings.Default.Path_Bot_MTAProvince = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Bot by MaxNeck");
            Properties.Settings.Default.Path_Soft = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Soft");
            Properties.Settings.Default.Path_Updater = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Updater");
            Properties.Settings.Default.Path_Shortcut = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Al-Store\\Иконки ярлыков");
            Properties.Settings.Default.Path_AutoRun = Properties.Settings.Default.Path_Store + "\\Auto Update\\Auto Update.exe";
            Properties.Settings.Default.User_Identyty = Environment.UserName;

            //handler = new Handler();
            //handler.Check_Shortcut_AutoRun();

            Properties.Settings.Default.Path_Errors_Log = $@"C:\Users\{Properties.Settings.Default.User_Identyty}\AppData\Roaming\Aluminum-Company\Al-Store\Errors Log";
            if (!Directory.Exists(Properties.Settings.Default.Path_Errors_Log))
            {
                Directory.CreateDirectory(Properties.Settings.Default.Path_Errors_Log);
            }


            bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

            if (!isAdmin && Source.Remove(1) == "C")
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

            My_Hand = new MySql_Handler();
            My_Hand.Getting_Data();
            if (StaticVars.Count_Update_Al == 0 && StaticVars.Auto_Update == 1)
            {
                AutoRun_Update = false;
                Environment.Exit(0);
            }
                

            if (Properties.Settings.Default.Authorization == 1)
            {
                My_Hand.Getting_User_Data();
                if (AutoRun_Update && StaticVars.Auto_Update == 1)
                { Auto_Update(); return; }
                else if (AutoRun_Update && StaticVars.Update_Msg == 1)
                {
                    Auto_Update_Msg();
                    return;
                }
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
                        telegram_Bot_Send_Activity.Al_Store_Auto_Logined();
                        My_Hand.Getting_User_Data();
                        if (AutoRun_Update && StaticVars.Auto_Update == 1)
                        { Auto_Update(); return; }
                        else if (AutoRun_Update && StaticVars.Update_Msg == 1)
                        {
                            Auto_Update_Msg();
                            return;
                        }

                        //TODO: Переделать окно уведомления о восстановлении активации
                        System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
                        notifyIcon.Visible = true;
                        notifyIcon.Text = "Al-Store";
                        notifyIcon.Icon = Properties.Resources.Al_Store;
                        notifyIcon.ShowBalloonTip(5000, "Al-Store", " Сработала функция восстановления активации.\n Добро пожаловать, " + Properties.Settings.Default.User_Name + " " + Properties.Settings.Default.User_SurName + "!", System.Windows.Forms.ToolTipIcon.Info);
                        notifyIcon.Visible = false;
                        //notifyIcon.Click += NotifyIcon_Click;
                    }
                }
            }

            if (!File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\Al-Store.lnk") && Properties.Settings.Default.Start_Creating_Shortcut == 0)
            {
                MessageBoxResult result = MessageBox.Show(" На вашем рабочем столе нет ярлыка 'Al-Store'.\n\n   Создать?", "Al-Store", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    handler.Create_Shortcut("Al-Store", "Al-Store", "", "Al-Store - Магазин приложений", Properties.Settings.Default.Ver_Store, "Ctrl+Shift+A");
                }
                else if (result == MessageBoxResult.No)
                {
                    MessageBoxResult _result = MessageBox.Show(" Не предлагать больше этот вопрос?", "Al-Store", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (_result == MessageBoxResult.Yes)
                    {
                        Properties.Settings.Default.Start_Creating_Shortcut = 1;
                        My_Hand.Set_Properties("Start_Creating_Shortcut", Properties.Settings.Default.Start_Creating_Shortcut, out bool Result);
                        if (Result == true)
                        { }
                        else if (Result == false)
                        {
                            if (Properties.Settings.Default.Start_Creating_Shortcut == 0)
                            { Properties.Settings.Default.Start_Creating_Shortcut = 1; }
                            else if (Properties.Settings.Default.Start_Creating_Shortcut == 1)
                            { Properties.Settings.Default.Start_Creating_Shortcut = 0; }
                            MessageBox.Show(" Не удалось обновить данные. Проверьте подключение к интернету или обратитесь к разработчику за помощью. \n Aluminum.Company163@gmail.com или Aluminum.Company163.reserve@gmail.com", "Ошибка!");
                        }
                        //TODO: Есть вопросы к функции "не спаршивать больше про создание ярлыка"
                    }
                }
            }
            else if (File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\Al-Store.lnk") && Properties.Settings.Default.First_Started == 0)
            {
                Properties.Settings.Default.First_Started = 1;

                handler = new Handler();
                handler.Create_Shortcut("Al-Store", "Al-Store", "", "Al-Store - Магазин приложений", Properties.Settings.Default.Ver_Store, "Ctrl+Shift+A");
            }
            StaticVars.Loading_Data = false;
            Check_Loaded();
        }

        private void Auto_Update()
        {
            System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Text = "Al-Store";
            notifyIcon.Icon = Properties.Resources.Al_Store;
            notifyIcon.ShowBalloonTip(10000, "Al-Store", " Запущено автоматическое обновление Al-Store.\n\nДанную функцию можно отключить в настройках обновления Al-Store.", System.Windows.Forms.ToolTipIcon.Info);
            notifyIcon.Visible = false;

            Telegram_Bot_Send_Activity telegram_Bot_Send_Activity = new Telegram_Bot_Send_Activity();
            telegram_Bot_Send_Activity.Al_Store_Updating(true);

            Update_Al_Window update_Al_Window = new Update_Al_Window(true);
            update_Al_Window.Show();
        }

        private async void Auto_Update_Msg()
        {
            System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Text = "Al-Store";
            notifyIcon.Icon = Properties.Resources.Al_Store;
            notifyIcon.ShowBalloonTip(10000, "Al-Store", " Доступно обновление Al-Store.\n\nДанную функцию можно отключить в настройках обновления Al-Store.", System.Windows.Forms.ToolTipIcon.Info);
            notifyIcon.Visible = false;

            await Task.Delay(10000);
            Environment.Exit(0);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void Check_Loaded()
        {
            while (true)
            {
                if (!StaticVars.Logo_Animation)
                { break; }
            }
            Thread.Sleep(800);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Telegram_Chating();
            Close();
        }

        private void Telegram_Chating()
        {
            Thread Telegram_Chating_thread = new Thread(() =>
            {
                Telegram_Chating_Window telegram_Chating_Window = new Telegram_Chating_Window();
                telegram_Chating_Window.Show();

                telegram_Chating_Window.Closed += (sender2, e2) =>
                    telegram_Chating_Window.Dispatcher.InvokeShutdown();

                System.Windows.Threading.Dispatcher.Run();
            });

            Telegram_Chating_thread.SetApartmentState(ApartmentState.STA);
            Telegram_Chating_thread.Start();
        }
    }
}
