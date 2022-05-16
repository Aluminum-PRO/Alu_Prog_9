using Alu_Prog_9.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alu_Prog_9.Pages.Bot_Pages
{
    /// <summary>
    /// Логика взаимодействия для Bot_Library_Box_Page.xaml
    /// </summary>
    public partial class Bot_Library_Box_Page : Page
    {
        int bot_id;
        double bot_Size;
        string bit_depth, install, bot_type, bot_name, bot_clicker_path, bot_install_version, bot_server_version, bot_reference;

        public Bot_Library_Box_Page(int id, string type, double Size, string name, string program_name, BitmapImage image, string description, string _clicker_path, int have_license, double price, string version, string reference)
        {
            InitializeComponent();

            bit_depth = GetOSBit();
            
            if (Directory.Exists(Properties.Settings.Default.Full_Path + "\\" + bot_name))
            { install = "True"; }
            else if (!Directory.Exists(Properties.Settings.Default.Full_Path + "\\" + bot_name))
            { install = "False"; }

            Download_Grid.Visibility = Visibility.Hidden;

            bot_id = id;
            bot_type = type;
            bot_Size = Size;
            bot_name = name;
            bot_clicker_path = _clicker_path;
            try { 
                string bot_install_version_txt = File.ReadAllText(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\BotVersion.txt"); 
                bot_install_version = bot_install_version_txt.Split('"')[1];
            }
            catch { install = "False"; }
            bot_reference = reference;

            Name_Bot.Text = name.ToString();
            Bot_Image_Main.Source = image;
            bot_server_version = version;
            Server_Version_TextBlock.Text += version;

            if (install == "True")
            {
                install_True();
            }
            else if (install == "False")
            {
                install_False();
            }
        }

        public void install_True()
        {
            double size = Convert.ToDouble(GetTotalSize(Properties.Settings.Default.Full_Path + "\\" + bot_name, 3));

            Install_Bot.Text = "Установлено";
            Installed_Version_TextBlock.Text = "Установленая версия: " + bot_install_version;
            Size_File_TextBlock.Text = "Размер файла: " + size.ToString("0.00") + " МБ";
            Location_File_TextBlock.Text = "Расположение файла: " + Properties.Settings.Default.Full_Path + "\\" + bot_name;
            if (bot_install_version != bot_server_version)
            {
                Update_Bot_But.IsEnabled = true;
            }
        }

        public void install_False()
        {
            Started_Bot_But.Visibility = Visibility.Hidden;
            Install_Bot_But.Visibility = Visibility.Visible;
            Update_Bot_But.Visibility = Visibility.Hidden;
            Delete_Bot_But.Visibility = Visibility.Hidden;
            Open_Location_Bot_But.Visibility = Visibility.Hidden;

            Settings_Bot_But.Visibility = Visibility.Hidden;
            Response_Database_Bot_But.Visibility = Visibility.Hidden;
            Exceptions_Chat_Bot_But.Visibility = Visibility.Hidden;
            Log_Response_Bot_But.Visibility = Visibility.Hidden;

            Install_Bot.Text = "Не установлено";
            Installed_Version_TextBlock.Text += "-";
            Size_File_TextBlock.Text += "-";
            Location_File_TextBlock.Text += "-";
        }


        public static string GetOSBit()
        {
            bool is64bit = Is64Bit();
            if (is64bit)
                return "x64";
            else
                return "x32";
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);

        public static bool Is64Bit()
        {
            bool retVal;
            IsWow64Process(Process.GetCurrentProcess().Handle, out retVal);
            return retVal;
        }

        private void Started_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var startInfo = new ProcessStartInfo(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\" + bot_clicker_path.Split('\\')[0] + "\\" + bit_depth + "\\" + bot_clicker_path.Split('\\')[1]);
                startInfo.Verb = "runas";
                Process.Start(startInfo);
            }
            catch { install_False(); }
        }

        private void Settings_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\Бот лесоруб\\Настройки бота.txt");
        }

        private void Response_Database_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\Бот лесоруб\\База ответов бота.txt");
        }

        private void Exceptions_Chat_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\Бот лесоруб\\Исключения с чата.txt");
        }

        private void Log_Response_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\Бот лесоруб\\Лог ответов.txt");
        }

        private void Install_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            Install_Bot.Text = "Устанавливается...";
            Download_Grid.Visibility = Visibility.Visible;
            ProgressBar.Value = 0;
            Install_Bot_But.IsEnabled = false;

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
                    ProgressBar.Value = (double)e_.BytesReceived / 1048576 * 100 / bot_Size;
                };
                webClient.DownloadFileAsync(new Uri(bot_reference), Properties.Settings.Default.Full_Path + "\\" + bot_name + ".zip");
                webClient.DownloadFileCompleted += async (s, e_) =>
                {
                    Process_TextBlock.Text = "Распаковка";
                    string zipPath = Properties.Settings.Default.Full_Path + "\\" + bot_name + ".zip";
                    string extractPath = Properties.Settings.Default.Full_Path + "\\";

                    try
                    {
                        try
                        {
                            Directory.Delete(Properties.Settings.Default.Full_Path + "\\" + bot_name, true);
                        }
                        catch { }
                        await Task.Run(() =>
                        {
                            ZipFile.ExtractToDirectory(zipPath, extractPath);
                        });
                    }
                    catch
                    {
                        using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                        {
                            foreach (var archiveEntry in archive.Entries)
                            {
                                ZipFile.ExtractToDirectory(zipPath, extractPath);
                            }
                        }
                    }

                    File.Delete(Properties.Settings.Default.Full_Path + "\\" + bot_name + ".zip");

                    try
                    {
                        string bot_install_version_txt = File.ReadAllText(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\BotVersion.txt");
                        bot_install_version = bot_install_version_txt.Split('"')[1];
                    }
                    catch {}

                    double size = Convert.ToDouble(GetTotalSize(Properties.Settings.Default.Full_Path + "\\" + bot_name, 3));

                    Install_Bot.Text = "Установлено";

                    Installed_Version_TextBlock.Text = "Установленная версия: " + bot_install_version;
                    Size_File_TextBlock.Text = "Размер файла: " + size.ToString("0.00") + " МБ";
                    Location_File_TextBlock.Text = "Расположение файла: " + Properties.Settings.Default.Full_Path + "\\" + bot_name;

                    Download_Grid.Visibility = Visibility.Hidden;
                    Process_TextBlock.Text = "Скачивание";

                    Started_Bot_But.Visibility = Visibility.Visible;
                    Install_Bot_But.Visibility = Visibility.Hidden;
                    Install_Bot_But.IsEnabled = true;
                    Update_Bot_But.Visibility = Visibility.Visible;
                    Delete_Bot_But.Visibility = Visibility.Visible;
                    Open_Location_Bot_But.Visibility = Visibility.Visible;

                    Settings_Bot_But.Visibility = Visibility.Visible;
                    Response_Database_Bot_But.Visibility = Visibility.Visible;
                    Exceptions_Chat_Bot_But.Visibility = Visibility.Visible;
                    Log_Response_Bot_But.Visibility = Visibility.Visible;
                };
            }
            catch
            {
                System.Windows.MessageBox.Show("No");
                return;
            }
        }

        private void Update_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            
            Install_Bot.Text = "Обновление...";
            Download_Grid.Visibility = Visibility.Visible;
            ProgressBar.Value = 0;

            string path = null;
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Куда сохранить копию настроек бота:\n\nЕсли вы не хотите сохранять, нажмите 'Отмена'";
                dialog.ShowNewFolderButton = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.SelectedPath;
                    Directory.CreateDirectory(path + "\\Копия настроек бота");
                    File.WriteAllText(path + "\\Копия настроек бота\\Настройки бота.txt", File.ReadAllText(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\Бот лесоруб\\Настройки бота.txt"));
                    File.WriteAllText(path + "\\Копия настроек бота\\База ответов бота.txt", File.ReadAllText(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\Бот лесоруб\\База ответов бота.txt"));
                    File.WriteAllText(path + "\\Копия настроек бота\\Исключения с чата.txt", File.ReadAllText(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\Бот лесоруб\\Исключения с чата.txt"));
                    File.WriteAllText(path + "\\Копия настроек бота\\Лог ответов.txt", File.ReadAllText(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\Бот лесоруб\\Лог ответов.txt"));

                    
                }
            }

            try
            { Directory.Delete(Properties.Settings.Default.Full_Path + "\\" + bot_name, true); }
            catch
            { System.Windows.MessageBox.Show("Ошибка.", "Al-Store"); Install_Bot.Text = "Установлено"; return; }
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
                    ProgressBar.Value = (double)e_.BytesReceived / 1048576 * 100 / bot_Size;
                };
                webClient.DownloadFileAsync(new Uri(bot_reference), Properties.Settings.Default.Full_Path + "\\" + bot_name + ".zip");
                webClient.DownloadFileCompleted += async (s, e_) =>
                {
                    Process_TextBlock.Text = "Распаковка";
                    string zipPath = Properties.Settings.Default.Full_Path + "\\" + bot_name + ".zip";
                    string extractPath = Properties.Settings.Default.Full_Path + "\\";

                    try
                    {
                        try
                        {
                            Directory.Delete(Properties.Settings.Default.Full_Path + "\\" + bot_name, true);
                        }
                        catch { }
                        await Task.Run(() =>
                        {
                            ZipFile.ExtractToDirectory(zipPath, extractPath);
                        });
                    }
                    catch
                    {
                        using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                        {
                            foreach (var archiveEntry in archive.Entries)
                            {
                                ZipFile.ExtractToDirectory(zipPath, extractPath);
                            }
                        }
                    }

                    File.Delete(Properties.Settings.Default.Full_Path + "\\" + bot_name + ".zip");

                    try
                    {
                        string bot_install_version_txt = File.ReadAllText(Properties.Settings.Default.Full_Path + "\\" + bot_name + "\\BotVersion.txt");
                        bot_install_version = bot_install_version_txt.Split('"')[1];
                    }
                    catch { }

                    Install_Bot.Text = "Установлено";
                    Started_Bot_But.Visibility = Visibility.Visible;
                    Install_Bot_But.Visibility = Visibility.Hidden;
                    Update_Bot_But.Visibility = Visibility.Visible;
                    Delete_Bot_But.Visibility = Visibility.Visible;
                    Open_Location_Bot_But.Visibility = Visibility.Visible;

                    Download_Grid.Visibility = Visibility.Hidden;
                    Update_Bot_But.IsEnabled = false;

                    Settings_Bot_But.Visibility = Visibility.Visible;
                    Response_Database_Bot_But.Visibility = Visibility.Visible;
                    Exceptions_Chat_Bot_But.Visibility = Visibility.Visible;
                    Log_Response_Bot_But.Visibility = Visibility.Visible;

                    double size = Convert.ToDouble(GetTotalSize(Properties.Settings.Default.Full_Path + "\\" + bot_name, 3));

                    Installed_Version_TextBlock.Text = "Установленная версия: " + bot_install_version;
                    Size_File_TextBlock.Text = "Размер файла: " + size.ToString("0.00") + " МБ";
                    Location_File_TextBlock.Text = "Расположение файла: " + Properties.Settings.Default.Full_Path + "\\" + bot_name;
                    
                    Process.Start(path + "\\Копия настроек бота\\");
                };
            }
            catch
            { System.Windows.MessageBox.Show("Ошибка.", "Al-Store"); Install_Bot.Text = "Установлено"; return; }
        }

        private void Delete_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show(" Вы уверены что хотите удалить программу '" + bot_name + "'?", "Al-Store", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Install_Bot.Text = "Удаление...";
                try
                { Directory.Delete(Properties.Settings.Default.Full_Path + "\\" + bot_name, true); }
                catch
                { System.Windows.MessageBox.Show("Ошибка.", "Al-Store"); Install_Bot.Text = "Установлено"; return; }

                Download_Grid.Visibility = Visibility.Hidden;
                Process_TextBlock.Text = "Скачивание";

                Started_Bot_But.Visibility = Visibility.Hidden;
                Install_Bot_But.Visibility = Visibility.Visible;
                Update_Bot_But.Visibility = Visibility.Hidden;
                Delete_Bot_But.Visibility = Visibility.Hidden;
                Open_Location_Bot_But.Visibility = Visibility.Hidden;

                Settings_Bot_But.Visibility = Visibility.Hidden;
                Response_Database_Bot_But.Visibility = Visibility.Hidden;
                Exceptions_Chat_Bot_But.Visibility = Visibility.Hidden;
                Log_Response_Bot_But.Visibility = Visibility.Hidden;

                Install_Bot.Text = "Не установлено";
                Installed_Version_TextBlock.Text = "Установленная версия: -";
                Size_File_TextBlock.Text = "Размер файла: -";
                Location_File_TextBlock.Text = "Расположение файла: -";
            }
        }

        private void Open_Location_Bot_But_Click(object sender, RoutedEventArgs e)
        {
            try { Process.Start(Properties.Settings.Default.Full_Path + "\\" + bot_name); }
            catch { System.Windows.MessageBox.Show(" Расположение не найдено.", "Al-Store"); }
        }

        private double totalSize = 0;

        public string GetTotalSize(string directory, int type)
        {
            //Получаем список файлов в текущей директории.
            string[] files = System.IO.Directory.GetFiles(directory);

            //Инициализируем новую переменную 
            //общего размера директории.
            string folderSize = string.Empty;

            //Проходимся по всем найденным файлам.
            foreach (string file in files)
            {
                //Складываем размер файлов текущей 
                //директории.
                GetFileSize(file);
            }

            //Вызываем метод GetDirectories с передачей в качестве параметра, пути к 
            //текущей директории. Данный метод возвращает
            //массив имен подкаталогов.
            string[] subDirs = System.IO.Directory.GetDirectories(directory);
            //Проходимся по всем полученным подкаталогам.
            foreach (string dir in subDirs)
            {
                //Вызов метода получения размера 
                //текущего подкаталога.
                GetTotalSize(dir, type);
            }

            switch (type)
            {
                case 0:
                    //Возвращение полученного размера директории в
                    //килобайтах.
                    folderSize = (totalSize / Math.Pow(1024, 1)).ToString() + " Кб.";
                    break;
                case 1:
                    //Возвращение полученного размера директории в
                    //мегабайтах.
                    folderSize = (totalSize / Math.Pow(1024, 2)).ToString() + " Мб.";
                    break;
                case 2:
                    //Возвращение полученного размера директории в
                    //гигабайтах.
                    folderSize = (totalSize / Math.Pow(1024, 3)).ToString() + " Гб.";
                    break;
                case 3:
                    //Возвращение полученного размера директории в
                    //мегабайтах.
                    folderSize = (totalSize / Math.Pow(1024, 2)).ToString();
                    break;
                default:
                    //Возвращение полученного размера директории в
                    //гигабайтах.
                    folderSize = (totalSize / Math.Pow(1024, 1)).ToString() + " Гб.";
                    break;
            }

            return folderSize;
        }

        private void GetFileSize(string path)
        {
            //Берем текущий файл в текущей директории.
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            //Получаем размер текущего файла в байтах и 
            //прибавляем его к общему размеру директории.
            totalSize += fi.Length;
        }
    }
}
