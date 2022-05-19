﻿using System;
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
        int id, have_application;
        double size;
        string type, name, program_name, version, TPK_version, reference, TPK_reference;

        public Update_App_But_UC(int id, string type, double size, string name, string program_name, BitmapImage image, BitmapImage image_1, BitmapImage image_2, BitmapImage image_3, BitmapImage image_4, string description, string shortcut_description, string hot_key, int have_application, double price, string version, string TPK_version, string reference, string TPK_reference)
        {
            InitializeComponent();
            this.name = name; this.version = version; this.TPK_version = TPK_version; this.reference = reference; this.TPK_reference = TPK_reference; this.size = size;
            App_But.Content = $"{name} | Ver.{version} | TPK Ver.{TPK_version}";
        }

        private void App_But_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
