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
    public partial class EmployeeForm : Form
    {
        Connection conn = new Connection();

        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {GetEmployeeName()}!";
        }

        private object GetEmployeeName()
        {
            string name = "Employee";
            try
            {
                int ID = LoginInfo.EmployeeID;
                string query = $"SELECT employeeName FROM employee WHERE employeeID = {ID}";
                conn.OpenConnection();
                MySqlDataReader reader = conn.DataReader(query);
                if (reader.Read())
                {
                    name = reader["employeeName"].ToString();
                }
                conn.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting employee name: " + ex.Message);
            }
            return name;
        }

        private void btnRequestTimeOff_Click(object sender, EventArgs e)
        {
            TimeOffRequestForm timeOffRequestForm = new TimeOffRequestForm();
            timeOffRequestForm.ShowDialog();
        }

        private void btnViewSchedule_Click_1(object sender, EventArgs e)
        {
            ViewScheduleForm scheduleForm = new ViewScheduleForm();
            scheduleForm.ShowDialog();
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void btnOpenRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }
    }
}
