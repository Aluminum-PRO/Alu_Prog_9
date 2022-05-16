using Alu_Prog_9.Classes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Alu_Prog_9.User_Control
{
    /// <summary>
    /// Логика взаимодействия для Application_Soft_Box_UC.xaml
    /// </summary>
    public partial class Application_Soft_Box_UC : UserControl
    {
        private readonly int soft_id = 0;
        private string soft_download = "";
        string soft_file_type;
        double soft_file_size;
        private string soft_name = "";
        private string soft_program_name = "";
        private string soft_pass = "";
        private readonly BitmapImage soft_image;
        private string soft_reference = "";

        public Application_Soft_Box_UC()
        {
            InitializeComponent();
        }
        public Application_Soft_Box_UC(int id, string download, string type, double size, string name, string program_name, string pass, BitmapImage image, string reference)
        {
            InitializeComponent();
            soft_id = id;
            soft_download = download;
            soft_file_type = type;
            soft_file_size = size;
            soft_name = name;
            Tag = name;
            soft_program_name = program_name;
            soft_pass = pass;
            soft_image = image;
            soft_reference = reference;

            Name_Application.Text = name.ToString();
            Application_Image.Source = image;
            Size_Application.Text = size.ToString() + " МБ";
            Update_Effect.BlurRadius = 0;
            
            _ = Visible_();
        }

        public async Task Visible_()
        {
            int Qre = 0;
            
            while (Qre != 1)
            {
                string Wre = Tag.ToString().ToLower();
                string pattern = StaticVars.Search_Text_Box.Text.ToLower();
                if (Regex.IsMatch(Wre, pattern, RegexOptions.IgnoreCase))
                    Visibility = Visibility.Visible;
                else
                    Visibility = Visibility.Collapsed;
                await Task.Delay(30);
            }
        }

            private void Open_Application_Box_But_Click(object sender, RoutedEventArgs e)
        {
            Application_Soft_Box_Window application_Soft_Box_Window = new Application_Soft_Box_Window(soft_id, soft_download, soft_file_size, soft_name, soft_pass, soft_image, soft_reference);
            application_Soft_Box_Window.ShowDialog();
        }
    }
}
