using Alu_Prog_9.Classes;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Alu_Prog_9
{
    /// <summary>
    /// Логика взаимодействия для Application_Soft_Box_Window.xaml
    /// </summary>
    public partial class Application_Soft_Box_Window : Window
    {
        string Soft_Name, Soft_Reference, Soft_Pass;
        double Soft_Size;

        public Application_Soft_Box_Window()
        {
            InitializeComponent();
        }

        public Application_Soft_Box_Window(int id, string download, double size, string name, string pass, BitmapImage image, string reference)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None; Main_Border.CornerRadius = new CornerRadius(20); AllowsTransparency = true;
            Download_Grid.Visibility = Visibility.Hidden;

            Soft_Size_TextBlock.Text = size.ToString() + " МБ";

            Soft_Size = size;
            Soft_Name = name;
            Soft_Pass = pass;
            Soft_Name_TextBlock.Text = name;

            Soft_Image.Source = image;

            Soft_Reference = reference;

            if (download == "True")
            {
                Soft_Code_TextBox.Visibility = Visibility.Hidden;
                Soft_Action_But.Content = "Удалить";
            }
            else if (download == "False")
            {
                Soft_Action_But.Content = "Скачать";
                Soft_Open_Action_But.Visibility = Visibility.Hidden;
                if (Soft_Pass != "0")
                {
                    Soft_Code_TextBox.Visibility = Visibility.Visible;
                }
                else
                {
                    Soft_Code_TextBox.Visibility = Visibility.Hidden;
                }
            }
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
            StaticVars.MainWindow.Hide();

            WindowState = WindowState.Minimized;
        }

        private void Close_But_Click(object sender, RoutedEventArgs e)
        {
            StaticVars.MainWindow.Wake_Up_But_Click();
            Close();
        }

        private void Soft_Action_But_Click(object sender, RoutedEventArgs e)
        {
            if (Soft_Action_But.Content.ToString() == "Скачать")
            {
                if (Soft_Pass == "0")
                {
                    Soft_Action_But.IsEnabled = false;
                    Download_Soft();
                }
                else
                {
                    if (Soft_Code_TextBox.Text == Soft_Pass)
                    {
                        Soft_Code_TextBox.Visibility = Visibility.Hidden;
                        Soft_Action_But.IsEnabled = false;
                        Download_Soft();
                    }
                    else
                    {
                        MessageBox.Show(" Для этого приложения необходим пароль администратора.");
                    }
                }
            }
            else if (Soft_Action_But.Content.ToString() == "Удалить")
            {
                Delete_Soft();
            }
        }

        public void Download_Soft()
        {
            Download_Grid.Visibility = Visibility.Visible;
            WebClient webClient = new WebClient();
            try
            {
                webClient.DownloadProgressChanged += (s, e) =>
                {
                    if (Process_TextBlock.Text == "Скачивание")
                    { Process_TextBlock.Text = "Скачивание."; }
                    else if (Process_TextBlock.Text == "Скачивание.")
                    { Process_TextBlock.Text = "Скачивание.."; }
                    else if (Process_TextBlock.Text == "Скачивание..")
                    { Process_TextBlock.Text = "Скачивание..."; }
                    else if (Process_TextBlock.Text == "Скачивание...")
                    { Process_TextBlock.Text = "Скачивание"; }
                    ProgressBar.Value = (double)e.BytesReceived / 1048576 * 100 / Soft_Size;
                };
                webClient.DownloadFileAsync(new Uri(Soft_Reference), Properties.Settings.Default.Full_Path + "\\Soft\\" + Soft_Name + ".zip");
                webClient.DownloadFileCompleted += async (s, e) =>
                {
                    Process_TextBlock.Text = "Распаковка";
                    await Task.Run(() =>
                    {
                        string zipPath = Properties.Settings.Default.Full_Path + "\\Soft\\" + Soft_Name + ".zip";
                        string extractPath = Properties.Settings.Default.Full_Path + "\\Soft\\";
                        ZipFile.ExtractToDirectory(zipPath, extractPath);
                    });

                    try
                    { File.Delete(Properties.Settings.Default.Full_Path + "\\Soft\\" + Soft_Name + ".zip"); }
                    catch { }
                    Download_Grid.Visibility = Visibility.Hidden;
                    Soft_Action_But.IsEnabled = true;
                    Soft_Action_But.Content = "Удалить";
                    Soft_Open_Action_But.Visibility = Visibility.Visible;
                };
            }
            catch
            {
                MessageBox.Show("No");
                return;
            }
        }

        private void Soft_Open_Action_But_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(Properties.Settings.Default.Full_Path + "\\Soft\\" + Soft_Name);
            }
            catch
            { MessageBox.Show(" Директроии не существует."); }
        }

        public void Delete_Soft()
        {
            try
            {
                Directory.Delete(Properties.Settings.Default.Full_Path + "\\Soft\\" + Soft_Name, true);
                Soft_Action_But.Content = "Скачать";
                Soft_Open_Action_But.Visibility = Visibility.Hidden;
            }
            catch { }
        }
    }
}
