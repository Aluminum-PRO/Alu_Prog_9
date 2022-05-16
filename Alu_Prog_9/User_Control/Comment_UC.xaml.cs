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
    /// Логика взаимодействия для Comment_UC.xaml
    /// </summary>
    public partial class Comment_UC : UserControl
    {
        public Comment_UC()
        {
            InitializeComponent();
        }

        public Comment_UC(string name, string surname, string comment_text, string time_send)
        {
            InitializeComponent();

            User_Name_SurName.Text = name + " " + surname;
            Time_Send_Text_Block.Text = time_send;
            CommentText.Text = comment_text;
        } 
    }
}

