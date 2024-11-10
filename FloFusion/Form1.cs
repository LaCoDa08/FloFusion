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
        string username, password;
        public LoginForm()
        {
            InitializeComponent();
        }

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
                        this.Hide();
                        MainForm mainForm = new MainForm();
                        mainForm.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password");
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
