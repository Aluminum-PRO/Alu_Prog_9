using System.Windows;
using System.Windows.Controls;

namespace Alu_Prog_9.User_Control
{
    /// <summary>
    /// Логика взаимодействия для Telegram_Messages_UC.xaml
    /// </summary>
    public partial class Telegram_Messages_UC : UserControl
    {
        public Telegram_Messages_UC(string msg, bool Admin)
        {
            InitializeComponent();

            Messages_Text.Text = msg;
            if (Admin)
            { Border.HorizontalAlignment = HorizontalAlignment.Left; Name_TextBlock.HorizontalAlignment = HorizontalAlignment.Left; Name_TextBlock.Text = "Никита (Admin)"; }
            else if (!Admin)
            { Border.HorizontalAlignment = HorizontalAlignment.Right; Name_TextBlock.HorizontalAlignment = HorizontalAlignment.Right; Name_TextBlock.Text = $"{Properties.Settings.Default.User_Name} ({Properties.Settings.Default.User_Role})"; }
        }
    }
}
