using Alu_Prog_9.Classes;
using Alu_Prog_9.Pages;
using Alu_Prog_9.Pages.Store_Pages.Applications;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alu_Prog_9.User_Control
{
    /// <summary>
    /// Логика взаимодействия для Application_Box_UC.xaml
    /// </summary>
    public partial class Application_Box_UC : UserControl   
    {
        private readonly int _id = 0;
        private string _type = "";
        private double _size = 0;
        private string _name = "";
        private string _program_name = "";
        private readonly BitmapImage _image;
        private readonly BitmapImage _image_1;
        private readonly BitmapImage _image_2;
        private readonly BitmapImage _image_3;
        private readonly BitmapImage _image_4;
        private string _description = "";
        private string _shortcut_description = "";
        private string _hot_key = "";
        private int _have_application = 0;
        private double _price = 0;
        private string _version = "";
        private string _reference = "";

        public Application_Box_UC(int id, string type, double size, string name, string program_name, BitmapImage image, BitmapImage image_1, BitmapImage image_2, BitmapImage image_3, BitmapImage image_4, string description, string shortcut_description, string hot_key, int have_application, double price, string version, string reference)
        {
            InitializeComponent();
            _id = id;
            _type = type;
            _size = size;
            _name = name;
            _program_name = program_name;
            _image = image; _image_1 = image_1; _image_2 = image_2; _image_3 = image_3; _image_4 = image_4;
            _description = description;
            _shortcut_description = shortcut_description;
            _hot_key = hot_key;
            _have_application = have_application;
            _price = price;
            _version = version;
            _reference = reference;

            Name_Application.Text = name.ToString()/*.Replace(@"_", " ")*/;
            // Метод для поля "Have_Application"
            {
                if (have_application == 1)
                {
                    Have_Application.Text = "Имеется";
                }
                else if (have_application == 0)
                {
                    if (price == 0)
                    {
                        Have_Application.Text = "Бесплатно";
                    }
                    else if (price > 0)
                    {
                        Have_Application.Text = "Цена: " + price.ToString() + " ₽";
                    }
                }
            }
            Application_Image.Source = image;

        }

        private void Open_Application_Box_But_Click(object sender, RoutedEventArgs e)
        {
            //Application_Box_Page application_Box_Page = new Application_Box_Page(id, name, program_name, image, have_application, price, version, version_TPK_Ed, reference, reference_TPK_Ed);
            //PostPage postUC = new PostPage(sourceImg, authorId, author, sourceAuthorImg, dt, text, lks, myLks, id);
            //StaticVars.MainWnd.Content = postUC;

            Application_Box_Page application_Box_Page = new Application_Box_Page(_id, _type, _size, _name, _program_name, _image, _image_1, _image_2, _image_3, _image_4, _description, _shortcut_description, _hot_key, _have_application, _price, _version, _reference);
            StaticVars.Store_Home_Frame.Content = application_Box_Page;
        }
    }
}
