﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Alu_Prog_9.Classes
{
    public static class StaticVars
    {
        public static MainWindow MainWindow;

        public static Frame Main_Frame, Store_Frame, Store_Home_Frame, Store_Library_Frame, Store_Soft_Frame;

        public static int Count_Update, Count_Update_Al, Count_Soft, Gender = 0, Soft_License, Bot_License;

        public static string Login, Rec_Email;

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