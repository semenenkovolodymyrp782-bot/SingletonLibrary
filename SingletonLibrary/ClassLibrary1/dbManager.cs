using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class dbManager
    {
        private static dbManager instance;
        private MySqlConnection connection;

        private dbManager()
        {
            string connectionString =
                "Server=localhost;Database=ProductStore;Uid=root;Pwd=dc23rf34;";

            connection = new MySqlConnection(connectionString);
        }

        public static dbManager getInstance()
        {
            if (instance == null)
            {
                instance = new dbManager();
            }

            return instance;
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void ExecuteQuery(string query)
        {
            OpenConnection();

            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();

            CloseConnection();
        }

        public DataTable GetData(string query)
        {
            OpenConnection();

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
            DataTable table = new DataTable();

            adapter.Fill(table);

            CloseConnection();

            return table;
        }
    }
}
