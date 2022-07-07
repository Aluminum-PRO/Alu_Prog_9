using Alu_Prog_9.MySql_Services;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Alu_Prog_9
{
    /// <summary>
    /// Логика взаимодействия для Update_Al_Window.xaml
    /// </summary>
    public partial class Update_Al_Window : Window
    {
        MySql_Handler My_Hand;

        string app_reference;
        int ProgressBar_Value = 0, ProgressBar_Value_ = 0, Killing = 0;
        bool AutoRun_Update;

        public Update_Al_Window(bool AutoRun_Update)
        {
            InitializeComponent();
            this.AutoRun_Update = AutoRun_Update;
            if (AutoRun_Update)
            { Opacity = 0; ShowInTaskbar = false; }
            WindowStyle = WindowStyle.None; Main_Border.CornerRadius = new CornerRadius(20); AllowsTransparency = true;

            foreach (Process process in Process.GetProcessesByName("Updater for Al-Store"))
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
                Process[] processList = Process.GetProcessesByName("Updater for Al-Store");
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
                await Task.Delay(20);
            }
        }

        public async Task Start_Process_2()
        {
            for (ProgressBar_Value_ = 0; ProgressBar_Value_ != 50;)
            {
                if (ProgressBar_Value_ < 50)
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
                webClient.DownloadFileAsync(new Uri(app_reference), Properties.Settings.Default.Full_Path + "\\Updater.zip");
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
            Process_TextBlock.Text = "Удаление старого Updater-а";
            string zipPath = Properties.Settings.Default.Full_Path + "\\Updater.zip";
            string extractPath = Properties.Settings.Default.Full_Path + "\\";
            try
            {
                try
                {
                    Directory.Delete(Properties.Settings.Default.Full_Path + "\\Updater", true);
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
                catch
                {
                    _ = Update_Process__2Async();
                    return;
                }
                
            }
            Process_TextBlock.Text = "Удаление остаточных файлов";
            try
            {
                File.Delete(Properties.Settings.Default.Full_Path + "\\Updater.zip");
            }
            catch { }
            ProgressBar_2.Value = ProgressBar_Value_;
            Status_TextBlock.Text = "Перезапуск Updater-а";
            Process_TextBlock.Text = "Закрытие";
            if (File.Exists(Properties.Settings.Default.Full_Path + "\\Updater\\Updater for Al-Store.exe"))
            {
                string Arg = "";
                if (AutoRun_Update)
                { Arg = "/AutoRun_Update"; }
                Process process = Process.Start(new ProcessStartInfo
                {
                    FileName = Properties.Settings.Default.Full_Path + "\\Updater\\Updater for Al-Store.exe",
                    Arguments = Arg
                });
            }
            else MessageBox.Show(" Недостаточно файлов для обновления, просьба переустановить Al-Store", "Ошибка!");
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
