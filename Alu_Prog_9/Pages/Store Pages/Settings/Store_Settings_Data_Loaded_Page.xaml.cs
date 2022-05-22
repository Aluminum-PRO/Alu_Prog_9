using Alu_Prog_9.Classes;
using Alu_Prog_9.MySql_Services;
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

namespace Alu_Prog_9.Pages.Store_Pages.Settings
{
    /// <summary>
    /// Логика взаимодействия для Store_Settings_Data_Loaded_Page.xaml
    /// </summary>
    public partial class Store_Settings_Data_Loaded_Page : Page
    {
        MySql_Handler My_Hand;
        public Store_Settings_Data_Loaded_Page()
        {
            InitializeComponent();
            if (StaticVars.Load_Soft_Info == 0)
            { Load_Soft_Info_But.IsChecked = false; }
            else if (StaticVars.Load_Soft_Info == 1)
            { Load_Soft_Info_But.IsChecked = true; }
        }

        private void Load_Soft_Info_But_Click(object sender, RoutedEventArgs e)
        {
            My_Hand = new MySql_Handler();
            if (Load_Soft_Info_But.IsChecked == false)
                StaticVars.Load_Soft_Info = 0;
            else if (Load_Soft_Info_But.IsChecked == true)
                StaticVars.Load_Soft_Info = 1;
            My_Hand.Set_Properties("Load_Soft_Info", StaticVars.Load_Soft_Info, out bool Result);
            if (Result == true)
            { }
            else if (Result == false)
            {
                if (StaticVars.Load_Soft_Info == 0)
                { StaticVars.Load_Soft_Info = 1; Load_Soft_Info_But.IsChecked = true; }
                else if (StaticVars.Load_Soft_Info == 1)
                { StaticVars.Load_Soft_Info = 0; Load_Soft_Info_But.IsChecked = false; }
                MessageBox.Show(" Не удалось обновить данные. Проверьте подключение к интернету или обратитесь к разработчику за помощью. \n Aluminum.Company163@gmail.com или Aluminum.Company163.reserve@gmail.com", "Ошибка!");
            }
        }
    }
}
