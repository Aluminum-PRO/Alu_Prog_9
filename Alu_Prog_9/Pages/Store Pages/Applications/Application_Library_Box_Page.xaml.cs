using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.Services;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Alu_Prog_9.Pages.Store_Pages.Applications
{
    /// <summary>
    /// Логика взаимодействия для Application_Library_Box_Page.xaml
    /// </summary>
    public partial class Application_Library_Box_Page : Page
    {
        Handler handler;



        int app_id;
        double app_Size;
        string app_type, app_name, app_shortcut_description, app_hot_key, app_version, app_reference;

        public Application_Library_Box_Page()
        {
            InitializeComponent();
            //KeyDown += (s, e) => { if (e.Key == Key.Enter) Send_Comment_But_Click(Send_Comment_But, null); };
        }

        public Application_Library_Box_Page(int id, string install, string type, double Size, string name, string _program_name, BitmapImage image, int _have_application, string shortcut_description, string hot_key, string install_version, string _version, string _reference)
        {
            InitializeComponent();
            
            Download_Grid.Visibility = Visibility.Hidden;

            app_id = id;
            app_type = type;
            app_Size = Size;
            app_name = name;
            app_shortcut_description = shortcut_description;
            app_hot_key = hot_key;
            app_version = install_version;
            app_reference = _reference;

            Name_Application.Text = name.ToString();
            Application_Image_Main.Source = image;
            if (install == "True")
            {
                FileInfo file = new System.IO.FileInfo(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + "\\" + app_name + ".exe");
                double size = file.Length;
                size /= 1048576;

                Install_Application.Text = "Установлено";
                Installed_Version_TextBlock.Text = "Установленая версия: " + install_version;
                Size_File_TextBlock.Text = "Размер файла: " + size.ToString("0.00") + " МБ";
                Location_File_TextBlock.Text = "Расположение файла: " + Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name;
                if (install_version != _version)
                {
                    Update_Application_But.IsEnabled = true; //TODO: Добавить текст о том, что в обновлении нового
                }
            }
            else if (install == "False")
            {
                Started_Application_But.Visibility = Visibility.Hidden;
                Install_Application_But.Visibility = Visibility.Visible;
                Update_Application_But.Visibility = Visibility.Hidden;
                Delete_Application_But.Visibility = Visibility.Hidden;
                Create_Shortcut_But.Visibility = Visibility.Hidden;
                Open_Location_Application_But.Visibility = Visibility.Hidden;

                Install_Application.Text = "Не установлено";
                Installed_Version_TextBlock.Text += "-";
                Size_File_TextBlock.Text += "-";
                Location_File_TextBlock.Text += "-";
            }
            Server_Version_TextBlock.Text += _version; 
        }

        public void Get_App_Version_Info()
        {
            FileVersionInfo myFileVersionInfo_Store = FileVersionInfo.GetVersionInfo(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + "\\" + app_name + ".exe");
            app_version = myFileVersionInfo_Store.FileVersion;
            if (Convert.ToInt32(app_version.Split('.')[0]) != 0)
            { app_version += ".Release"; }
            else if (Convert.ToInt32(app_version.Split('.')[1]) != 0)
            { app_version += ".Beta"; }
            else if (Convert.ToInt32(app_version.Split('.')[2]) != 0)
            { app_version += ".Alpha"; }
            else if (Convert.ToInt32(app_version.Split('.')[3]) != 0)
            { app_version += ".Pre-Alpha"; }
        }

        private void Started_Application_But_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Directory.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\" + app_type + "\\" + app_name + "\\Activated"))
            { }
            else
            {
                Directory.CreateDirectory("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\" + app_type + "\\" + app_name + "\\Activated");
            }
            File.WriteAllText("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\" + app_type + "\\" + app_name + "\\Activated\\Activated_File.txt", "");

            Process.Start(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + "\\" + app_name + ".exe");
        }

        private void Install_Application_But_Click(object sender, RoutedEventArgs e)
        {
            Install_Application.Text = "Устанавливается...";
            Download_Grid.Visibility = Visibility.Visible;
            ProgressBar.Value = 0;
            Install_Application_But.IsEnabled = false;

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
                    ProgressBar.Value = (double)e_.BytesReceived / 1048576 * 100 / app_Size;
                    //Download_Size_TextBlock.Text = $"Загружено: {e_.ProgressPercentage}% ({((double)e_.BytesReceived / 1048576).ToString("#.# МБ")})";
                };
                webClient.DownloadFileAsync(new Uri(app_reference), Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + ".zip");
                webClient.DownloadFileCompleted += async (s, e_) =>
                {
                    Process_TextBlock.Text = "Распаковка";
                    string zipPath = Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + ".zip";
                    string extractPath = Properties.Settings.Default.Full_Path + "\\" + app_type + "\\";

                    try
                    {
                        try
                        {
                            Directory.Delete(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name, true);
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

                    File.Delete(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + ".zip");

                    if (Directory.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\" + app_type + "\\" + app_name + "\\Activated"))
                    { }
                    else
                    {
                        Directory.CreateDirectory("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\" + app_type + "\\" + app_name + "\\Activated");
                    }
                    File.WriteAllText("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\" + app_type + "\\" + app_name + "\\Activated\\Activated_File.txt", "");

                    Get_App_Version_Info();

                    if (!File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\" + app_name + ".lnk"))
                    {
                        handler = new Handler();
                        handler.Create_Shortcut(app_type, app_name, app_name + "\\", app_shortcut_description, app_version, app_hot_key);
                    }

                    FileInfo file = new System.IO.FileInfo(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + "\\" + app_name + ".exe");
                    double size = file.Length;
                    size /= 1048576;

                    Install_Application.Text = "Установлено";

                    Installed_Version_TextBlock.Text = "Установленная версия: " + app_version;
                    Size_File_TextBlock.Text = "Размер файла: " + size.ToString("0.00") + " МБ";
                    Location_File_TextBlock.Text = "Расположение файла: " + Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name;

                    Download_Grid.Visibility = Visibility.Hidden;
                    Process_TextBlock.Text = "Скачивание";

                    Started_Application_But.Visibility = Visibility.Visible;
                    Install_Application_But.Visibility = Visibility.Hidden;
                    Install_Application_But.IsEnabled = true;
                    Update_Application_But.Visibility = Visibility.Visible;
                    Delete_Application_But.Visibility = Visibility.Visible;
                    Create_Shortcut_But.Visibility = Visibility.Visible;
                    Open_Location_Application_But.Visibility = Visibility.Visible;
                    //StaticVars.Count_Update--;
                    //StaticVars.Ellipse_text.Text = StaticVars.Count_Update.ToString();
                    //if (StaticVars.Count_Update == 0)
                    //{
                    //    StaticVars.Ellepse_Grid.Visibility = Visibility.Hidden;
                    //}
                };
            }
            catch (Exception Ex) { MessageBox.Show(Ex.Message + "\n\n" + Ex.HelpLink, Ex.Source, MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void Update_Application_But_Click(object sender, RoutedEventArgs e)
        {
            Install_Application.Text = "Обновление...";
            Download_Grid.Visibility = Visibility.Visible;
            ProgressBar.Value = 0;
            try
            { Directory.Delete(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name, true); }
            catch
            { MessageBox.Show("Ошибка.", "Al-Store"); Install_Application.Text = "Установлено"; return; }
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
                    ProgressBar.Value = (double)e_.BytesReceived / 1048576 * 100 / app_Size;
                };
                webClient.DownloadFileAsync(new Uri(app_reference), Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + ".zip");
                webClient.DownloadFileCompleted += async (s, e_) =>
                {
                    Process_TextBlock.Text = "Распаковка";
                    string zipPath = Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + ".zip";
                    string extractPath = Properties.Settings.Default.Full_Path + "\\" + app_type + "\\";

                    try
                    {
                        try
                        {
                            Directory.Delete(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name, true);
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

                    File.Delete(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + ".zip");

                    if (Directory.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\" + app_type + "\\" + app_name + "\\Activated"))
                    { }
                    else
                    {
                        Directory.CreateDirectory("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\" + app_type + "\\" + app_name + "\\Activated");
                    }
                    File.WriteAllText("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\AppData\\Roaming\\Aluminum-Company\\" + app_type + "\\" + app_name + "\\Activated\\Activated_File.txt", "");

                    Get_App_Version_Info();

                    if (!File.Exists("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\" + app_name + ".lnk"))
                    {
                        handler = new Handler();
                        handler.Create_Shortcut(app_type, app_name, app_name + "\\", app_shortcut_description, app_version, app_hot_key);
                    }

                    Install_Application.Text = "Установлено";
                    Started_Application_But.Visibility = Visibility.Visible;
                    Install_Application_But.Visibility = Visibility.Hidden;
                    Update_Application_But.Visibility = Visibility.Visible;
                    Delete_Application_But.Visibility = Visibility.Visible;
                    Create_Shortcut_But.Visibility = Visibility.Visible;
                    Open_Location_Application_But.Visibility = Visibility.Visible;

                    Download_Grid.Visibility = Visibility.Hidden;
                    Update_Application_But.IsEnabled = false;

                    FileInfo file = new System.IO.FileInfo(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name + "\\" + app_name + ".exe");
                    double size = file.Length;
                    size /= 1048576;

                    Installed_Version_TextBlock.Text = "Установленная версия: " + app_version;
                    Size_File_TextBlock.Text = "Размер файла: " + size.ToString("0.00") + " МБ";
                    Location_File_TextBlock.Text = "Расположение файла: " + Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name;

                    StaticVars.Count_Update--;
                    if (StaticVars.Count_Update == 0)
                    {
                        StaticVars.Ellepse_Grid.Visibility = Visibility.Hidden;
                    }
                };
            }
            catch
            { MessageBox.Show("Ошибка.", "Al-Store"); Install_Application.Text = "Установлено"; return; }
        }

        private void Delete_Application_But_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(" Вы уверены что хотите удалить программу '" + app_name + "'?", "Al-Store", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Install_Application.Text = "Удаление...";
                try
                { Directory.Delete(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name, true); }
                catch
                { MessageBox.Show("Ошибка.", "Al-Store"); Install_Application.Text = "Установлено"; return; }
                try
                { File.Delete("C:\\Users\\" + Properties.Settings.Default.User_Identyty + "\\Desktop\\" + app_name + ".lnk"); } //TODO: Почему то не удаляется ярлык Tools & Fun
                catch { }

                Download_Grid.Visibility = Visibility.Hidden;
                Process_TextBlock.Text = "Скачивание";

                Started_Application_But.Visibility = Visibility.Hidden;
                Install_Application_But.Visibility = Visibility.Visible;
                Update_Application_But.Visibility = Visibility.Hidden;
                Delete_Application_But.Visibility = Visibility.Hidden;
                Create_Shortcut_But.Visibility = Visibility.Hidden;
                Open_Location_Application_But.Visibility = Visibility.Hidden;

                Install_Application.Text = "Не установлено";
                Installed_Version_TextBlock.Text = "Установленная версия: -";
                Size_File_TextBlock.Text = "Размер файла: -";
                Location_File_TextBlock.Text = "Расположение файла: -";
            }
        }

        private void Create_Shortcut_But_Click(object sender, RoutedEventArgs e)
        {
            handler = new Handler();
            handler.Create_Shortcut(app_type, app_name, app_name + "\\", app_shortcut_description, app_version, app_hot_key); //TODO: Добавить выключение функции быстрого запуска приложений через комбинацию клавиш
        }

        private void Open_Location_Application_But_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Directory.Exists(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name))
            {
                Process.Start(Properties.Settings.Default.Full_Path + "\\" + app_type + "\\" + app_name);
            }
            else
            {
                MessageBox.Show(" Расположение не найдено.", "Al-Store");
            }
        }
    }
}
