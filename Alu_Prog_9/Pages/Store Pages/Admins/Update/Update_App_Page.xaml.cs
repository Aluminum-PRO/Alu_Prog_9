using Alu_Prog_9.MySql_Services;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using forms = System.Windows.Forms;

namespace Alu_Prog_9.Pages.Store_Pages.Admins.Update
{
    /// <summary>
    /// Логика взаимодействия для Update_App_Page.xaml
    /// </summary>
    public partial class Update_App_Page : Page
    {
        private MySql_Connector My_Con;
        private MySqlCommand command;

        int id;
        private double size, price;
        private string type, name, program_name, version, TPK_version, reference, TPK_reference, description, shortcut_description, hot_key;
        private BitmapImage image, image_1, image_2, image_3, image_4;

        public Update_App_Page(int id, string type, double size, string name, string program_name, BitmapImage image, BitmapImage image_1, BitmapImage image_2, BitmapImage image_3, BitmapImage image_4, string description, string shortcut_description, string hot_key, double price, string version, string TPK_version, string reference, string TPK_reference)
        {
            InitializeComponent();

            this.id = id; this.type = type; this.name = name; this.program_name = program_name; this.image = image; this.image_1 = image_1; this.image_2 = image_2; this.image_3 = image_3; this.image_4 = image_4; this.description = description; this.shortcut_description = shortcut_description; this.hot_key = hot_key; this.price = price; this.version = version; this.TPK_version = TPK_version; this.reference = reference; this.TPK_reference = TPK_reference; this.size = size;
            Name_TextBox.Text = name; Program_Name_TextBox.Text = program_name;
            Image_Main.Source = image; Image_1.Source = image_1; Image_2.Source = image_2; Image_3.Source = image_3; Image_4.Source = image_4;
            Version_TextBox.Text = version; TPK_Version_TextBox.Text = TPK_version;
            Reference_TextBox.Text = reference; TPK_Reference_TextBox.Text = TPK_reference;
            Size_TextBox.Text = size.ToString();
            if (type == "Programs")
                Prog_BoxItem_Sec.IsSelected = true;
            else if (type == "Games")
                Game_BoxItem_Sec.IsSelected = true;
            else if (type == "Tests")
                Test_BoxItem_Sec.IsSelected = true;
            else if (type == "Admins")
                Admin_BoxItem_Sec.IsSelected = true;
            Price_TextBox.Text = price.ToString(); Hot_Key_TextBox.Text = hot_key;
            Shortcut_Description_TextBox.Text = shortcut_description;
            Description_TextBox.Text = description;
        }

        private void Edit_But_Click(object sender, RoutedEventArgs e)
        {
            string type = "", _image_main = "", _image_1 = "", _image_2 = "", _image_3 = "", _image_4 = "";
            if (Prog_BoxItem_Sec.IsSelected)
                type = "Programs";
            else if (Game_BoxItem_Sec.IsSelected)
                type = "Games";
            else if (Test_BoxItem_Sec.IsSelected)
                type = "Tests";
            else if (Admin_BoxItem_Sec.IsSelected)
                type = "Admins";
            if (Image_Main_CheckBut.IsChecked == true)
            {
                _image_main = ", `image_main` = @image_main";
            }
            if (Image_1_CheckBut.IsChecked == true)
            {
                _image_1 = ", `image_1` = @image_1";
            }
            if (Image_2_CheckBut.IsChecked == true)
            {
                _image_2 = ", `image_2` = @image_2";
            }
            if (Image_3_CheckBut.IsChecked == true)
            {
                _image_3 = ", `image_3` = @image_3";
            }
            if (Image_4_CheckBut.IsChecked == true)
            {
                _image_4 = ", `image_4` = @image_4";
            }

            My_Con = new MySql_Connector();
            command = new MySqlCommand($"UPDATE `Tab_Applications_Db` SET `name` = @name, `size` = @size, `version` = @version, `version_TPK_Ed` = @version_TPK_Ed, `reference` = @reference, `reference_TPK_Ed` = @reference_TPK_Ed{_image_main}{_image_1}{_image_2}{_image_3}{_image_4}" +
                " WHERE `id` = @id",
                My_Con.getConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@type", MySqlDbType.VarChar).Value = type;
            command.Parameters.Add("@size", MySqlDbType.Double).Value = Size_TextBox.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = Name_TextBox.Text;
            command.Parameters.Add("@program_name", MySqlDbType.VarChar).Value = Program_Name_TextBox.Text;
            command.Parameters.Add("@description", MySqlDbType.VarChar).Value = Description_TextBox.Text;
            command.Parameters.Add("@shortcut_description", MySqlDbType.VarChar).Value = Shortcut_Description_TextBox.Text;
            command.Parameters.Add("@hot_key", MySqlDbType.VarChar).Value = Hot_Key_TextBox.Text;
            command.Parameters.Add("@price", MySqlDbType.Double).Value = Price_TextBox.Text;
            command.Parameters.Add("@version", MySqlDbType.VarChar).Value = Version_TextBox.Text;
            command.Parameters.Add("@version_TPK_Ed", MySqlDbType.VarChar).Value = TPK_Version_TextBox.Text;
            command.Parameters.Add("@reference", MySqlDbType.VarChar).Value = Reference_TextBox.Text;
            command.Parameters.Add("@reference_TPK_Ed", MySqlDbType.VarChar).Value = TPK_Reference_TextBox.Text;

            byte[] imgB;
            if (Image_Main_CheckBut.IsChecked == true)
            {
                Image_Encoder((BitmapImage)Image_Main.Source, out imgB);
                command.Parameters.Add("@image_main", MySqlDbType.Blob).Value = imgB;
            }
            if (Image_1_CheckBut.IsChecked == true)
            {
                Image_Encoder((BitmapImage)Image_1.Source, out imgB);
                command.Parameters.Add("@image_1", MySqlDbType.Blob).Value = imgB;
            }
            if (Image_2_CheckBut.IsChecked == true)
            {
                Image_Encoder((BitmapImage)Image_2.Source, out imgB);
                command.Parameters.Add("@image_2", MySqlDbType.Blob).Value = imgB;
            }
            if (Image_3_CheckBut.IsChecked == true)
            {
                Image_Encoder((BitmapImage)Image_3.Source, out imgB);
                command.Parameters.Add("@image_3", MySqlDbType.Blob).Value = imgB;
            }
            if (Image_4_CheckBut.IsChecked == true)
            {
                Image_Encoder((BitmapImage)Image_4.Source, out imgB);
                command.Parameters.Add("@image_4", MySqlDbType.Blob).Value = imgB;
            }

            My_Con.openConnection();

            if (command.ExecuteNonQuery() == 1)
            { MessageBox.Show("ok", "Al-Store", MessageBoxButton.OK, MessageBoxImage.Information); }
            else
            { MessageBox.Show("error", "Al-Store", MessageBoxButton.OK, MessageBoxImage.Error); }

            My_Con.closeConnection();
        }

        private void Change_Image(out BitmapImage image)
        {
            image = null;
            forms.OpenFileDialog ofd = new forms.OpenFileDialog();
            ofd.Filter = "*.png; *.jpg; *.ico; | *.png; *.jpg; *.ico;";
            if (ofd.ShowDialog() == forms.DialogResult.OK)
            {
                FileInfo file = new FileInfo(ofd.FileName);
                double size = file.Length;
                size /= 1024;
                if (size >= 64)
                { MessageBox.Show($" Файл весит {size} кб, это больше 64 кб!", "Al-Store", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(ofd.FileName, UriKind.Relative);
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                image = bi;
            }
        }

        private void Image_Encoder(BitmapImage bi, out byte[] _imgB)
        {
            PngBitmapEncoder pbe = new PngBitmapEncoder();
            MemoryStream ms = new MemoryStream();
            pbe.Frames.Add(BitmapFrame.Create(bi));
            pbe.Save(ms);
            byte[] imgB = ms.ToArray();
            _imgB = imgB;
        }

        private void Edit_Image_Main_But_Click(object sender, RoutedEventArgs e)
        {
            Change_Image(out BitmapImage image);
            if (image != null)
            {
                Image_Main.Source = image;
                Image_Main_CheckBut.IsChecked = true;
            }

        }

        private void Edit_Image_1_But_Click(object sender, RoutedEventArgs e)
        {
            Change_Image(out BitmapImage image);
            if (image != null)
            {
                Image_1.Source = image;
                Image_1_CheckBut.IsChecked = true;
            }
        }

        private void Edit_Image_2_But_Click(object sender, RoutedEventArgs e)
        {
            Change_Image(out BitmapImage image);
            if (image != null)
            {
                Image_2.Source = image;
                Image_2_CheckBut.IsChecked = true;
            }
        }

        private void Edit_Image_3_But_Click(object sender, RoutedEventArgs e)
        {
            Change_Image(out BitmapImage image);
            if (image != null)
            {
                Image_3.Source = image;
                Image_3_CheckBut.IsChecked = true;
            }
        }

        private void Edit_Image_4_But_Click(object sender, RoutedEventArgs e)
        {
            Change_Image(out BitmapImage image);
            if (image != null)
            {
                Image_4.Source = image;
                Image_4_CheckBut.IsChecked = true;
            }
        }

        private void Cancel_But_Click(object sender, RoutedEventArgs e)
        {
            Name_TextBox.Text = name; Program_Name_TextBox.Text = program_name;
            Image_Main.Source = image; Image_1.Source = image_1; Image_2.Source = image_2; Image_3.Source = image_3; Image_4.Source = image_4;
            Version_TextBox.Text = version; TPK_Version_TextBox.Text = TPK_version;
            Reference_TextBox.Text = reference; TPK_Reference_TextBox.Text = TPK_reference;
            Size_TextBox.Text = size.ToString();
            if (type == "Programs")
                Prog_BoxItem_Sec.IsSelected = true;
            else if (type == "Games")
                Game_BoxItem_Sec.IsSelected = true;
            else if (type == "Tests")
                Test_BoxItem_Sec.IsSelected = true;
            else if (type == "Admins")
                Admin_BoxItem_Sec.IsSelected = true;
            Price_TextBox.Text = price.ToString(); Hot_Key_TextBox.Text = hot_key;
            Shortcut_Description_TextBox.Text = shortcut_description;
            Description_TextBox.Text = description;
            Image_Main_CheckBut.IsChecked = false;
            Image_1_CheckBut.IsChecked = false;
            Image_2_CheckBut.IsChecked = false;
            Image_3_CheckBut.IsChecked = false;
            Image_4_CheckBut.IsChecked = false;
        }
    }
}
