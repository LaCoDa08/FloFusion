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
        static string password = "******"; // Add your password to your database
        public static string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" 
            + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        /// <summary>
        /// Opens a connection to the database
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        public void CloseConnection()
        {
            conn.Close();
            conn.Dispose();
        }

        /// <summary>
        /// The DataSet method executes a SQL query and returns the results in a DataSet object. 
        /// If an error occurs during the execution, it catches the exception and prints the error 
        /// message to the console. This method is useful for retrieving and working with data in a 
        /// disconnected manner, as DataSet can hold multiple tables and relationships between them.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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

        /// <summary>
        /// The DataReader method executes a SQL query and returns the results in a MySqlDataReader object.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public MySqlDataReader DataReader(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            return dataReader;
        }

        /// <summary>
        /// The ExecuteNonQuery method in the Connection class is designed to execute a SQL query that does not return any data.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, conn);
            return cmd.ExecuteNonQuery();
        }
    }
}
