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
    /// Логика взаимодействия для Update_Al_Store_But_UC.xaml
    /// </summary>
    public partial class Update_Al_Store_But_UC : UserControl
    {
        int id;
        double size;
        string name, version, TPK_version, reference, TPK_reference;

        public Update_Al_Store_But_UC(int id, string name, string version, string TPK_version, string reference, string TPK_reference, double size)
        {
            InitializeComponent();
            this.id = id; this.name = name; this.version = version; this.TPK_version = TPK_version; this.reference = reference; this.TPK_reference = TPK_reference; this.size = size;
            Al_Store_But.Content = $"{name} | Ver.{version} | TPK Ver.{TPK_version}";
        }

        private void Al_Store_But_Click(object sender, RoutedEventArgs e)
        {
            StaticVars.Store_Admin_Frame.NavigationService.Navigate(new Update_Al_Store_Page(id, name, version, TPK_version, reference, TPK_reference, size));
        }
    }
}
