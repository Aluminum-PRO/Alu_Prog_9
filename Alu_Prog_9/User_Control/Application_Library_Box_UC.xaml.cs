using Alu_Prog_9.Classes;
using Alu_Prog_9.Pages;
using Alu_Prog_9.Pages.Store_Pages.Applications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Alu_Prog_9.User_Control
{
    /// <summary>
    /// Логика взаимодействия для Application_Library_Box_UC.xaml
    /// </summary>
    public partial class Application_Library_Box_UC : UserControl
    {
        private readonly int _id = 0;
        private string _install = "";
        string _type;
        double _size;
        private string _name = "";
        private string _program_name = "";
        private string _shortcut_description, _hot_key;
        private readonly BitmapImage _image;
        private int _have_application = 0;
        private string _version = "", app_version;
        private string _reference = "";

        public Application_Library_Box_UC()
        {
            InitializeComponent();
        }

        public Application_Library_Box_UC(int id, string install, string type, double size, string name, string program_name, BitmapImage image, int have_application, string shortcut_description, string hot_key, string version, string reference)
        {
            InitializeComponent();
            _id = id;
            _install = install;
            _type = type;
            _size = size;
            _name = name;
            _program_name = program_name;
            _image = image;
            _have_application = have_application;
            _shortcut_description = shortcut_description;
            _hot_key = hot_key;
            _version = version;
            _reference = reference;

            Name_Application.Text = name.ToString()/*.Replace(@"_", " ")*/;
            Application_Image.Source = image;
            if (install == "True")
            {
                FileVersionInfo myFileVersionInfo_Store = FileVersionInfo.GetVersionInfo(Properties.Settings.Default.Full_Path + "\\" + type + "\\" + name + "\\" + name + ".exe");
                app_version = myFileVersionInfo_Store.FileVersion;
                if (Convert.ToInt32(app_version.Split('.')[0]) != 0)
                { app_version += ".Release"; }
                else if (Convert.ToInt32(app_version.Split('.')[1]) != 0)
                { app_version += ".Beta"; }
                else if (Convert.ToInt32(app_version.Split('.')[2]) != 0)
                { app_version += ".Alpha"; }
                else if (Convert.ToInt32(app_version.Split('.')[3]) != 0)
                { app_version += ".Pre-Alpha"; }

                if (app_version == version)
                {
                    Update_Application.Text = "Установлено";
                    Update_Effect.BlurRadius = 0;
                }
            }
            else if (install == "False")
            {
                Update_Application.Text = "Не установлено";
                Update_Effect.BlurRadius = 0;
            }

        }

        private void Open_Library_Application_Box_But_Click(object sender, RoutedEventArgs e)
        {
            Application_Library_Box_Page application_Library_Box_Page = new Application_Library_Box_Page(_id, _install, _type, _size, _name, _program_name, _image, _have_application, _shortcut_description, _hot_key, app_version, _version, _reference);
            StaticVars.Store_Library_Frame.Content = application_Library_Box_Page;
        }
    }
}
