using Alu_Prog_9.Classes;
using Alu_Prog_9.Pages.Store_Pages.Admins.Update;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Update_App_But_UC.xaml
    /// </summary>
    public partial class Update_App_But_UC : UserControl
    {
        private int id;
        private double size, price;
        private string type, name, program_name, version, TPK_version, reference, TPK_reference, description, shortcut_description, hot_key;
        private BitmapImage image, image_1, image_2, image_3, image_4;

        public Update_App_But_UC(int id, string type, double size, string name, string program_name, BitmapImage image, BitmapImage image_1, BitmapImage image_2, BitmapImage image_3, BitmapImage image_4, string description, string shortcut_description, string hot_key, double price, string version, string TPK_version, string reference, string TPK_reference)
        {
            InitializeComponent();
            this.id = id; this.type = type; this.name = name; this.program_name = program_name; this.image = image; this.image_1 = image_1; this.image_2 = image_2; this.image_3 = image_3; this.image_4 = image_4; this.description = description; this.shortcut_description = shortcut_description; this.hot_key = hot_key; this.price = price; this.version = version; this.TPK_version = TPK_version; this.reference = reference; this.TPK_reference = TPK_reference; this.size = size;
            App_But.Content = $"{name} | Ver.{version} | TPK Ver.{TPK_version}";
        }

        private void App_But_Click(object sender, RoutedEventArgs e)
        {
            StaticVars.Store_Admin_Frame.NavigationService.Navigate(new Update_App_Page(id, type, size, name, program_name, image, image_1, image_2, image_3, image_4, description, shortcut_description, hot_key, price, version, TPK_version, reference, TPK_reference));
        }
    }
}
