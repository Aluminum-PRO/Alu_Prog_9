using Alu_Prog_9.MySql_Services;
using Alu_Prog_9.User_Control;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Alu_Prog_9.Pages.Store_Pages.Home_Library
{
    /// <summary>
    /// Логика взаимодействия для Store_Home_Application_Page.xaml
    /// </summary>
    public partial class Store_Home_Application_Page : Page
    {
        private int id;
        private MySql_Connector My_Con;
        private MySqlCommand command;
        private MySqlDataAdapter adapter;
        private MySqlDataReader reader;

        public static Thread thread;

        public Store_Home_Application_Page(string Type)
        {
            InitializeComponent();
            Spawn_Applications(Type);
        }

        private void Spawn_Applications(string Type)
        {

            thread = new Thread(() =>
            {
                My_Con = new MySql_Connector();

                adapter = new MySqlDataAdapter();
                command = new MySqlCommand("SELECT * FROM `Tab_Applications_Db`, `Tab_Programs_Db` WHERE Tab_Applications_Db.type = @type AND Tab_Programs_Db.login = @login", My_Con.getConnection());
                command.Parameters.Add("@type", MySqlDbType.VarChar).Value = Type; command.Parameters.Add("@login", MySqlDbType.VarChar).Value = Properties.Settings.Default.User_Login;

                // Другие варианты запроса Command
                {
                    //command = new MySqlCommand("SELECT `name`, `program_name`, `image`, `description`, `price`, `version`, `version_TPK_Ed`, `reference`, `reference_TPK_Ed`, `Off_PC`, `The_15_Puzzle` FROM `Tab_Applications_Db`, `Tab_Programs_Db` WHERE Tab_Applications_Db.type = @type AND Tab_Programs_Db.login = @login", My_Con.getConnection());

                    //command = new MySqlCommand("SELECT Tab_Applications_Db.name, Tab_Applications_Db.program_name, Tab_Applications_Db.image, " +
                    //    "Tab_Applications_Db.description, Tab_Applications_Db.price, Tab_Applications_Db.version, Tab_Applications_Db.version_TPK_Ed, " +
                    //    "Tab_Applications_Db.reference, Tab_Applications_Db.reference_TPK_Ed, Tab_Programs_Db.Off_PC FROM Tab_Applications_Db, Tab_Programs_Db WHERE Tab_Applications_Db.type = @type AND Tab_Programs_Db.login = @login", My_Con.getConnection());
                }

                My_Con.openConnection();

                reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["id"]);
                    Application_Box_UC application_Box_UC = new Application_Box_UC(id/*, type, size, name, program_name, image, image_1, image_2, image_3, image_4, description, shortcut_description, hot_key, have_application, price, version, reference*/);
                    Area_for_Applicatoin_Box.Children.Add(application_Box_UC);

                    //if (!Dispatcher.CheckAccess())
                    //{
                    //    Dispatcher.Invoke(() =>
                    //    {
                    //        Application_Box_UC application_Box_UC = new Application_Box_UC(id/*, type, size, name, program_name, image, image_1, image_2, image_3, image_4, description, shortcut_description, hot_key, have_application, price, version, reference*/);
                    //        Area_for_Applicatoin_Box.Children.Add(application_Box_UC);
                    //    }, DispatcherPriority.Normal);
                    //}
                    //else
                    //{
                    //    Application_Box_UC application_Box_UC = new Application_Box_UC(id/*, type, size, name, program_name, image, image_1, image_2, image_3, image_4, description, shortcut_description, hot_key, have_application, price, version, reference*/);
                    //    Area_for_Applicatoin_Box.Children.Add(application_Box_UC);
                    //}
                }
                My_Con.closeConnection();
            });
            thread.Start();
        }
    }
}
