using Alu_Prog_9.Classes;
using Alu_Prog_9.User_Control;
using System.Windows.Controls;

namespace Alu_Prog_9.Pages.Store_Pages.Home_Library
{
    /// <summary>
    /// Логика взаимодействия для Store_Home_Application_Page.xaml
    /// </summary>
    public partial class Store_Home_Application_Page : Page
    {
        public Store_Home_Application_Page(string Type)
        {
            InitializeComponent();
            Spawn_Applications(Type);
        }

        private void Spawn_Applications(string Type)
        {
            foreach (StaticVars.DataBase i in StaticVars.Application)
            {
                if (i.type == Type)
                {
                    Application_Box_UC application_Box_UC = new Application_Box_UC(i.id, i.type, i.size, i.name, i.program_name, i.image, i.image_1, i.image_2, i.image_3, i.image_4, i.description, i.shortcut_description, i.hot_key, i.have_application, i.price, i.version, i.reference);
                    Area_for_Applicatoin_Box.Children.Add(application_Box_UC);
                }
            }
        }
    }
}
