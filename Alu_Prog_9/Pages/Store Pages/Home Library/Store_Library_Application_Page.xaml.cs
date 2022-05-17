using Alu_Prog_9.Classes;
using Alu_Prog_9.User_Control;
using System.IO;
using System.Windows.Controls;

namespace Alu_Prog_9.Pages.Store_Pages.Home_Library
{
    /// <summary>
    /// Логика взаимодействия для Store_Library_Application_Page.xaml
    /// </summary>
    public partial class Store_Library_Application_Page : Page
    {
        public Store_Library_Application_Page(string install)
        {
            InitializeComponent();

            foreach (StaticVars.DataBase i in StaticVars.Application)
            {
                if (i.have_application == 1)
                {
                    if (install == "True")
                    {
                        if (File.Exists(Properties.Settings.Default.Full_Path + "\\" + i.type + "\\" + i.name + "\\" + i.name + ".exe"))
                        {
                            Application_Library_Box_UC application_Library_Box_UC = new Application_Library_Box_UC(i.id, install, i.type, i.size, i.name, i.program_name, i.image, i.have_application, i.shortcut_description, i.hot_key, i.version, i.reference);
                            Area_for_Applicatoin_Box.Children.Add(application_Library_Box_UC);
                        }
                    }
                    if (install == "False")
                    {
                        if (!File.Exists(Properties.Settings.Default.Full_Path + "\\" + i.type + "\\" + i.name + "\\" + i.name + ".exe"))
                        {
                            Application_Library_Box_UC application_Library_Box_UC = new Application_Library_Box_UC(i.id, install, i.type, i.size, i.name, i.program_name, i.image, i.have_application, i.shortcut_description, i.hot_key, i.version, i.reference);
                            Area_for_Applicatoin_Box.Children.Add(application_Library_Box_UC);
                        }
                    }
                }
            }
        }
    }
}
