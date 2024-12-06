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
    public partial class AddEmployeeForm : Form
    {
        Connection conn = new Connection();

        public AddEmployeeForm()
        {
            InitializeComponent();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text.Trim();
            string employeeEmail = txtEmployeeEmail.Text.Trim();
            string employeeTitle = cbEmployeeRole.SelectedItem?.ToString();
            string employeeUsername = txtEmployeeUsername.Text.Trim();
            string employeePassword = txtEmployeePassword.Text.Trim();

            // Validate input fields
            if (string.IsNullOrEmpty(employeeName) || string.IsNullOrEmpty(employeeEmail) || 
                string.IsNullOrEmpty(employeeTitle) || string.IsNullOrEmpty(employeeUsername) || 
                string.IsNullOrEmpty(employeePassword))
            {
                MessageBox.Show("Please enter valid employee details.");
                return;
            }

            try
            {
                conn.OpenConnection();
                string query = "INSERT INTO employee (employeeName, email, title) VALUES (@employeeName, @email, @title)";
                long employeeId;

                using (MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@employeeName", employeeName);
                    cmd.Parameters.AddWithValue("@email", employeeEmail);
                    cmd.Parameters.AddWithValue("@title", employeeTitle);
                    cmd.ExecuteNonQuery();
                    employeeId = cmd.LastInsertedId;
                }

                query = "INSERT INTO employee_account (employeeID, userName, e_password) VALUES (@employeeID, @username, @password)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@employeeID", employeeId);
                    cmd.Parameters.AddWithValue("@username", employeeUsername);
                    cmd.Parameters.AddWithValue("@password", employeePassword);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Employee added successfully!");
                txtEmployeeName.Clear();
                txtEmployeeEmail.Clear();
                txtEmployeeUsername.Clear();
                txtEmployeePassword.Clear();
                cbEmployeeRole.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding employee: " + ex.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
        }
    }
}
