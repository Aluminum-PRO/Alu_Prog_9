using Alu_Prog_9.MySql_Services;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Alu_Prog_9.Pages.Store_Pages.Admins.Update
{
    /// <summary>
    /// Логика взаимодействия для Update_Al_Store_Page.xaml
    /// </summary>
    public partial class Update_Al_Store_Page : Page
    {
        private MySql_Connector My_Con;
        private MySqlCommand command;
        private MySqlDataAdapter adapter;
        private MySqlDataReader reader;

        int id;
        double size;
        string name, version, TPK_version, reference, TPK_reference;

        public Update_Al_Store_Page(int id, string name, string version, string TPK_version, string reference, string TPK_reference, double size)
        {
            InitializeComponent();
            this.id = id; this.name = name; this.version = version; this.TPK_version = TPK_version; this.reference = reference; this.TPK_reference = TPK_reference; this.size = size;
            Name_TextBox.Text = name;
            Version_TextBox.Text = version; TPK_Version_TextBox.Text = TPK_version;
            Reference_TextBox.Text = reference; TPK_Reference_TextBox.Text = TPK_reference;
            Size_TextBox.Text = size.ToString();
        }

        private void Edit_But_Click(object sender, RoutedEventArgs e)
        {
            My_Con = new MySql_Connector();
            command = new MySqlCommand("UPDATE `Tab_Al_Store_Db` SET `name` = @name, `size` = @size, `version` = @version, `TPK_version` = @TPK_version, `reference` = @reference, `TPK_reference` = @TPK_reference" +
                " WHERE `id` = @id",
                My_Con.getConnection());

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = Name_TextBox.Text;
            command.Parameters.Add("@size", MySqlDbType.Double).Value = Size_TextBox.Text;
            command.Parameters.Add("@version", MySqlDbType.VarChar).Value = Version_TextBox.Text;
            command.Parameters.Add("@TPK_version", MySqlDbType.VarChar).Value = TPK_Version_TextBox.Text;
            command.Parameters.Add("@reference", MySqlDbType.VarChar).Value = Reference_TextBox.Text;
            command.Parameters.Add("@TPK_reference", MySqlDbType.VarChar).Value = TPK_Reference_TextBox.Text;

            My_Con.openConnection();

            if (command.ExecuteNonQuery() == 1)
            { MessageBox.Show("ok", "Al-Store", MessageBoxButton.OK, MessageBoxImage.Information); }
            else
            { MessageBox.Show("error", "Al-Store", MessageBoxButton.OK, MessageBoxImage.Error); }

            My_Con.closeConnection();
        }

        private void Cancel_But_Click(object sender, RoutedEventArgs e)
        {
            Name_TextBox.Text = name;
            Version_TextBox.Text = version; TPK_Version_TextBox.Text = TPK_version;
            Reference_TextBox.Text = reference; TPK_Reference_TextBox.Text = TPK_reference;
            Size_TextBox.Text = size.ToString();
        }
    }
}
