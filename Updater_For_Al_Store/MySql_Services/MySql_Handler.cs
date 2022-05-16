using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater_For_Al_Store.MySql_Services
{
    public class MySql_Handler
    {
        MySql_Connector My_Con;
        MySqlCommand command;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;


        public void Getting_Information_of_Update_Al(out double size, out string reference)
        {
            My_Con = new MySql_Connector();
            adapter = new MySqlDataAdapter();

            command = new MySqlCommand("SELECT * FROM `Tab_Al_Store_Db` WHERE `id` = 1", My_Con.getConnection());
            size = 0;
            reference = "";

            My_Con.openConnection();

            reader = null;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                size = Convert.ToDouble(reader["size_store"]);
                if (Properties.Settings.Default.Edition == "'Standart'")
                {
                    reference = reader["reference_store"].ToString();
                }
                else if (Properties.Settings.Default.Edition == "'TPK'")
                {
                    reference = reader["reference_store_TPK_Ed"].ToString();
                }
            }
            My_Con.closeConnection();
        }
    }
}
