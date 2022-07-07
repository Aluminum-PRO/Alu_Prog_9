using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Updater_For_Al_Store.MySql_Services;

namespace Updater_For_Al_Store
{
    /// <summary>
    /// Логика взаимодействия для Update_Up_Window.xaml
    /// </summary>
    public partial class Update_Up_Window : Window
    {
        MySql_Handler My_Hand;

        string app_reference;

        int ProgressBar_Value = 0, ProgressBar_Value_ = 0, Killing = 0, Update_Process_1;

        bool AutoRun_Update;

        public Update_Up_Window()
        {
            InitializeComponent();

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1)
            { }
            else
            {
                foreach (String element in args)
                {
                    if (element == "/AutoRun_Update")
                    {
                        AutoRun_Update = true;
                        Opacity = 0;
                        ShowInTaskbar = false;
                    }
                }
            }

            WindowStyle = WindowStyle.None; Main_Border.CornerRadius = new CornerRadius(20); AllowsTransparency = true;
            
            string Source = "";
            string source = Assembly.GetExecutingAssembly().Location;
            int counts = source.Count(f => f == '\\'); counts--;
            for (int i = 1; i <= counts; i++)
            {
                if (i != counts)
                {
                    Source = Source + source.Split('\\')[i - 1] + '\\';
                }
                else if (i == counts)
                {
                    Source = Source + source.Split('\\')[i - 1];
                }
            }
            Properties.Settings.Default.Full_Path = Source;
            Properties.Settings.Default.Path_Updater = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Updater");
            Properties.Settings.Default.Path_Store = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Al-Store");
            Properties.Settings.Default.Path_Programs = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Programs");
            Properties.Settings.Default.Path_Games = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.Full_Path + "\\Games");

            if (File.Exists(Properties.Settings.Default.Full_Path + "\\Edition\\TPK_Edition.txt"))
            {
                Properties.Settings.Default.Edition = "'TPK'";
            }
            else if (!File.Exists(Properties.Settings.Default.Full_Path + "\\Edition\\TPK_Edition.txt"))
            {
                Properties.Settings.Default.Edition = "'Standart'";
            }

            foreach (Process process in Process.GetProcessesByName("Al-Store"))
            {
                process.Kill();
            }
            foreach (Process process in Process.GetProcessesByName("MySql.Data"))
            {
                process.Kill();
            }
            _ = Killing_Process();
            _ = Start_Process();
            _ = Start_Process_2();
        }

        public async Task Killing_Process()
        {
            for (Killing = 0; Killing != 1;)
            {
                Process[] processList = Process.GetProcessesByName("Al-Store");
                if (processList.Length > 0)
                { }
                else
                {
                    Process[] processList_ = Process.GetProcessesByName("MySql.Data");
                    if (processList_.Length > 0)
                    { }
                    else
                    {
                        Killing = 1;
                    }
                }
                await Task.Delay(30);
            }
        }

        public async Task Start_Process()
        {
            for (int _ProgressBar = 0; _ProgressBar != 1;)
            {
                if (ProgressBar_Value < 100)
                {
                    ProgressBar_Value++;
                }
                else if (Killing == 1 && ProgressBar_Value == 100)
                {
                    ProgressBar_Value = 0;
                    Status_TextBlock.Text = "Скачивание файлов";
                    Process_TextBlock.Text = "Скачивание";
                    _ProgressBar = 1;
                    Update_Process__1();
                }
                ProgressBar.Value = ProgressBar_Value;
                await Task.Delay(10);
            }
        }

        public async Task Start_Process_2()
        {
            for (ProgressBar_Value_ = 50; ProgressBar_Value_ != 100;)
            {
                if (ProgressBar_Value_ < 100)
                {
                    ProgressBar_Value_++;
                }
                ProgressBar_2.Value = ProgressBar_Value_;
                await Task.Delay(200);
            }
        }

        public void Update_Process__1()
        {
            My_Hand = new MySql_Handler();
            My_Hand.Getting_Information_of_Update_Al(out double size, out app_reference);
            WebClient webClient = new WebClient();
            try
            {
                webClient.DownloadProgressChanged += (s, e_) =>
                {
                    if (Process_TextBlock.Text == "Скачивание")
                    { Process_TextBlock.Text = "Скачивание."; }
                    else if (Process_TextBlock.Text == "Скачивание.")
                    { Process_TextBlock.Text = "Скачивание.."; }
                    else if (Process_TextBlock.Text == "Скачивание..")
                    { Process_TextBlock.Text = "Скачивание..."; }
                    else if (Process_TextBlock.Text == "Скачивание...")
                    { Process_TextBlock.Text = "Скачивание"; }
                    ProgressBar.Value = (double)e_.BytesReceived / 1048576 * 100 / size;
                };
                webClient.DownloadFileAsync(new Uri(app_reference), Properties.Settings.Default.Full_Path + "\\Al-Store.zip");
                webClient.DownloadFileCompleted += (s, e_) =>
                {
                    _ = Update_Process__2Async();
                };
            }
            catch
            {
                MessageBox.Show("No");
                return;
            }
        }
        public async Task Update_Process__2Async()
        {
            Status_TextBlock.Text = "Обработка файлов";
            Process_TextBlock.Text = "Удаление старого Al-Store";
            string zipPath = Properties.Settings.Default.Full_Path + "\\Al-Store.zip";
            string extractPath = Properties.Settings.Default.Full_Path + "\\";
            try
            {
                try
                {
                    Directory.Delete(Properties.Settings.Default.Full_Path + "\\Al-Store", true);
                }
                catch { }
                Process_TextBlock.Text = "Разархивация файлов";
                await Task.Run(() =>
                {
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                });
            }
            catch
            {
                try
                {
                    using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                    {
                        foreach (var archiveEntry in archive.Entries)
                        {
                            ZipFile.ExtractToDirectory(zipPath, extractPath);
                        }
                    }
                }
                catch (Exception _ex)
                {
                    MessageBox.Show(_ex.ToString());
                    //Update_Process__2();
                    //return;
                }
            }
            Process_TextBlock.Text = "Удаление остаточных файлов";
            try
            {
                File.Delete(Properties.Settings.Default.Full_Path + "\\Al-Store.zip");
            }
            catch { }
            Update_Process_1 = 1;
            ProgressBar_2.Value = ProgressBar_Value_;
            Status_TextBlock.Text = "Завершение работы Updater-а";
            Process_TextBlock.Text = "Закрытие";
            if (File.Exists(Properties.Settings.Default.Full_Path + "\\Al-Store\\Al-Store.exe") && !AutoRun_Update)
            {
                Process.Start(Properties.Settings.Default.Full_Path + "\\Al-Store\\Al-Store.exe");
            }
            else if (!File.Exists(Properties.Settings.Default.Full_Path + "\\Al-Store\\Al-Store.exe") && !AutoRun_Update)
            { MessageBox.Show(" Неизвестная ошибка обновления. Повторите попытку, или не тратьте время до исправления разработчиком ошибок.", "Ошибка!"); }
            Environment.Exit(0);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }

        private void ReSize_Min_But_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Close_But_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
