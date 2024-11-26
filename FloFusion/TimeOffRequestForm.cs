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
    public partial class TimeOffRequestForm : Form
    {
        Connection conn = new Connection();
        LoginForm loginForm = new LoginForm();

        public TimeOffRequestForm()
        {
            InitializeComponent();
        }

        private void txtReason_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string startDate = dtpStartDate.Value.ToString("yyyy-MM-dd");
            string endDate = dtpEndDate.Value.ToString("yyyy-MM-dd");
            string reason = txtReason.Text;
            int employeeID = LoginInfo.EmployeeID; 

            if (string.IsNullOrEmpty(reason))
            {
                MessageBox.Show("Please provide a reason for the time off.");
                return;
            }

            try
            {
                conn.OpenConnection();
                string query = $"INSERT INTO time_off_requests (employeeID, startDate, endDate, reason, status) VALUES ({employeeID}, '{startDate}', '{endDate}', '{reason}', 'Pending')";
                conn.ExecuteNonQuery(query);
                conn.CloseConnection();

                MessageBox.Show("Time-off request submitted successfully.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting the request: " + ex.Message);
            }
        }
    }
}
