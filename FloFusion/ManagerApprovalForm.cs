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
    public partial class ManagerApprovalForm : Form
    {
        Connection conn = new Connection();

        public ManagerApprovalForm()
        {
            InitializeComponent();
        }

        private void ManagerApprovalForm_Load(object sender, EventArgs e)
        {
            LoadTimeOffRequests();
        }

        private void LoadTimeOffRequests()
        {
            try
            {
                conn.OpenConnection();
                string query = "SELECT r.requestID, e.employeeID, e.employeeName, " +
                               "r.startDate, r.endDate, r.reason, r.status " +
                               "FROM time_off_requests r " +
                               "JOIN employee e ON r.employeeID = e.employeeID " +
                               "WHERE r.status = 'Pending'";
                DataSet ds = conn.DataSet(query);

                dgvRequests.DataSource = ds.Tables[0];
                conn.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading time off requests: " + ex.Message);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            UpdateRequestStatus("Approved");
        }

        private void btnDeny_Click_1(object sender, EventArgs e)
        {
            UpdateRequestStatus("Denied");
        }

        private void UpdateRequestStatus(string status)
        {
            if (dgvRequests.SelectedRows.Count > 0)
            {
                int requestId = (int)dgvRequests.SelectedRows[0].Cells["requestID"].Value;

                try
                {
                    conn.OpenConnection();
                    string query = $"UPDATE time_off_requests SET status = '{status}' WHERE requestID = {requestId}";
                    conn.ExecuteNonQuery(query);
                    conn.CloseConnection();

                    MessageBox.Show($"Request {status.ToLower()} successfully.");
                    LoadTimeOffRequests(); // Refresh the data grid
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating request: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a request to process.");
            }
        }
    }
}
