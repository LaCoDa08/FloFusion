using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloFusion
{
    public partial class LoginForm : Form
    {
        Connection conn = new Connection();
        public string username, password;

        public LoginForm()
        {
            InitializeComponent();
            HidePassword();
        }

        /// <summary>
        /// When the button is clicked, the program will check if the username and password are correct 
        /// by checking the database. If the username and password are correct, the program will show
        /// the next form. If the username and password are incorrect, the program will show an error message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "" && txtPassword.Text != "")
                {
                    conn.OpenConnection();
                    username = txtUsername.Text;
                    password = txtPassword.Text;
                    string query = "SELECT * FROM employee_account WHERE userName = '" + username + "' AND e_password = '" + password + "'";
                    MySqlDataReader reader = conn.DataReader(query);

                    if (reader.Read())
                    {
                        MessageBox.Show("Login Successful");
                        LoginInfo.Username = username;
                        LoginInfo.EmployeeID = reader.GetInt32("employeeID");
                        reader.Close();

                        string roleQuery = $"SELECT title FROM employee WHERE employeeID = {LoginInfo.EmployeeID}";
                        MySqlDataReader roleReader = conn.DataReader(roleQuery);

                        if (roleReader.Read())
                        {
                            string role = roleReader["title"].ToString();
                            roleReader.Close();

                            this.Hide();

                            if (role.Equals("Manager", StringComparison.OrdinalIgnoreCase))
                            {
                                ManagerForm managerForm = new ManagerForm();
                                managerForm.Show();
                            }
                            else
                            {
                                EmployeeForm employeeForm = new EmployeeForm();
                                employeeForm.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to determine user role. Please contact support.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password");
                        reader.Close();
                    }

                    conn.CloseConnection();
                }
                else
                {
                    MessageBox.Show("Please enter both username and password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.CloseConnection();
            }
        }

        /// <summary>
        /// Hides the password as the user types it in.
        /// </summary>
        private void HidePassword()
        {
            txtPassword.Text = "";
            txtPassword.PasswordChar = '*';
            txtPassword.MaxLength = 14;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
