using Alu_Prog_9.Classes;
using Alu_Prog_9.Pages;
using Alu_Prog_9.Pages.Login_Pages;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Alu_Prog_9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker _bw;

        public MainWindow()
        {
            InitializeComponent();

            WindowStyle = WindowStyle.None; Main_Border.CornerRadius = new CornerRadius(20); AllowsTransparency = true;

            StaticVars.MainWindow = this;
            StaticVars.Main_Frame = Main_Frame;

            if (Properties.Settings.Default.Authorization == 0)
            {
                Main_Frame.Content = new Login_Page();
            }
            else if (Properties.Settings.Default.Authorization == 1)
            {
                Main_Frame.Content = new Main_Page();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Activate();

            //_bw = new BackgroundWorker();
            //_bw.DoWork += new DoWorkEventHandler((o, args) =>
            //{
            //    //Long stuff here       
            //    this.Dispatcher.Invoke((Action)(() =>
            //    {
            //        while (!IsActive)
            //        {
            //            Process[] processList = Process.GetProcessesByName("Al-Store");
            //            if (processList.Length > 1)
            //            {
            //                if (WindowState == WindowState.Minimized)
            //                {
            //                    WindowState = WindowState.Normal;
            //                }
            //                Activate();
            //                break;
            //            }
            //            Task.Delay(600).Wait();
            //        }
            //        if (_bw.CancellationPending)
            //        {
            //            args.Cancel = true;
            //            return;
            //        }
            //        MessageBox.Show("");
            //    }));
            //});

            //_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler((o, args) =>
            //{
            //    //End long stuff here
            //    this.Dispatcher.Invoke((Action)(() => this.button1.IsEnabled = true));
            //});
        }

        private void Main_Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Forward || e.NavigationMode == NavigationMode.Back)
            {
                e.Cancel = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }

        private void Close_But_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ReSize_Max_But_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                //if (Location.X == 0 && Location.Y == 0)
                //{
                //    CenterToScreen();
                //}
            }
        }

        public void ReSize_Min_But_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public void Wake_Up_But_Click()
        {
            if (!IsActive)
            {
                Show();
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            //try { Only_One_window(); }
            //catch { }
        }

        public async void Only_One_window()
        {
            await Task.Run(() =>
            {
                while (!IsActive)
                {
                    Process[] processList = Process.GetProcessesByName("Al-Store");
                    if (processList.Length > 1)
                    {
                        if (WindowState == WindowState.Minimized)
                        {
                            WindowState = WindowState.Normal;
                        }
                        Activate();
                        break;
                    }
                    Task.Delay(500).Wait();
                }
            });
        }

        //TODO: Сделать активацию окна при открытии дубликата
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    
        //}

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    this.button1.IsEnabled = false;

        //    _bw.RunWorkerAsync();
        //}
        //        Если бы вы хотели читать из txtLog, а не изменять его, код был бы таким же:

        ////Long stuff here       
        //this.Dispatcher.Invoke((Action)(() => 
        //{
        //    string myLogText = txtLog.Text;
        //        myLogText = myLogText + Environment.NewLine + "Blabla";
        //    txtLog.Text = myLogText;
        //}));
    }
}
