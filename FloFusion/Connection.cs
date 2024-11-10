using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data;


namespace FloFusion
{
    internal class Connection
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string myConnectionString;
        static string server = "localhost";
        static string database = "flofusion";
        static string uid = "root";
        static string password = "PnckIs2Coo!";
        public static string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        public bool OpenConnection()
        {
            conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server. Contact administrator");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }

        }

        public void CloseConnection()
        {
            conn.Close();
            conn.Dispose();
        }

        public DataSet DataSet(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }

        public MySqlDataReader DataReader(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;
        }

        public int ExecuteNonQuery(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, conn);
            return cmd.ExecuteNonQuery();
        }
    }
}
