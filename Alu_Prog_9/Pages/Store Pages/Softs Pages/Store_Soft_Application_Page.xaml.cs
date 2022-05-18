using Alu_Prog_9.Classes;
using Alu_Prog_9.User_Control;
using System.IO;
using System.Windows.Controls;

namespace Alu_Prog_9.Pages.Store_Pages.Softs_Pages
{
    /// <summary>
    /// Логика взаимодействия для Store_Soft_Application_Page.xaml
    /// </summary>
    public partial class Store_Soft_Application_Page : Page
    {
        public Store_Soft_Application_Page()
        {
            InitializeComponent();
        }

        public Store_Soft_Application_Page(string download)
        {
            InitializeComponent();
            Spawn_Softs(download);
        }

        private void Spawn_Softs(string download)
        {
            foreach (StaticVars.DataBase i in StaticVars.Soft)
            {
                if (download == "True")
                {
                    if (File.Exists(Properties.Settings.Default.Full_Path + "\\Soft\\" + i.name + "\\" + i.name + i.file_type))
                    {
                        Application_Soft_Box_UC application_Soft_Box_UC = new Application_Soft_Box_UC(i.id, download, i.file_type, i.file_size, i.name, i.program_name, i.pass, i.image, i.reference);
                        Area_for_Applicatoin_Box.Children.Add(application_Soft_Box_UC);
                    }
                }
                if (download == "False")
                {
                    if (!File.Exists(Properties.Settings.Default.Full_Path + "\\Soft\\" + i.name + "\\" + i.name + i.file_type))
                    {
                        Application_Soft_Box_UC application_Soft_Box_UC = new Application_Soft_Box_UC(i.id, download, i.file_type, i.file_size, i.name, i.program_name, i.pass, i.image, i.reference);
                        Area_for_Applicatoin_Box.Children.Add(application_Soft_Box_UC);
                    }
                }
            }
            StaticVars.Count_Soft_Text_box.Text = "Софт не от Aluminum-Company: " + StaticVars.Count_Soft.ToString();
        }
    }
}
