using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Alu_Prog_9.Classes
{
    public static class StaticVars
    {
        public class DataBase
        {
            public int id = 0, have_application;
            public double price, size, file_size;
            public string type, name, program_name, description, shortcut_description, hot_key, version, reference, download, pass, file_type;
            public BitmapImage image, image_1, image_2, image_3, image_4;
        }
        public static List<DataBase> Application = new List<DataBase>();
        public static List<DataBase> Soft = new List<DataBase>();

        public static int Load_Soft_Info = 0, Auto_Update = 1, Update_Msg = 1;
        //public static string ;

        public static int Count_Update, Count_Update_Al, Count_Soft, Gender = 0, Soft_License, Bot_License;
        public static string Login, Rec_Email, what_news;
        public static bool Loading_Data = true, Logo_Animation = true;

        public static MainWindow MainWindow;

        public static Frame Main_Frame, Store_Frame, Store_Home_Frame, Store_Library_Frame, Store_Soft_Frame, Store_Admin_Frame;
        
        public static Grid Ellepse_Grid;
        public static TextBlock Ellipse_text;

        public static Grid Ellepse_Grid_Al;
        public static TextBlock Ellipse_Al_text;

        public static TextBox Search_Text_Box;
        public static TextBlock Count_Soft_Text_box;

        public static RadioButton Store_Home_But;
        public static RadioButton Store_Library_But;

        public static RadioButton Store_Library_Installed_But;
        public static RadioButton Store_Library_Not_Installed_But;

        public static RadioButton Store_Soft_Downloaded_But;
        public static RadioButton Store_Soft_Not_Downloaded_But;
    }
}
