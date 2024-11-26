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
    public partial class InputScheduleForm : Form
    {
        Connection conn = new Connection();
        public InputScheduleForm()
        {
            InitializeComponent();
        }

        private void InputScheduleForm_Load(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }

        private void LoadEmployeeList()
        {
            try
            {
                conn.OpenConnection();
                string query = "SELECT employeeID, employeeName FROM employee";
                DataSet ds = conn.DataSet(query);

                cmbEmployee.DataSource = ds.Tables[0];
                cmbEmployee.DisplayMember = "employeeName";
                cmbEmployee.ValueMember = "employeeID";

                conn.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading employee list: " + ex.Message);
            }
        }

        private void btnSaveSchedule_Click(object sender, EventArgs e)
        {
            int employeeId = (int)cmbEmployee.SelectedValue;
            DateTime workDate = dtpWorkDate.Value;
            TimeSpan startTime = dtpStartTime.Value.TimeOfDay;
            TimeSpan endTime = dtpEndTime.Value.TimeOfDay;

            if (startTime >= endTime)
            {
                MessageBox.Show("Start time must be before end time");
                return;
            }

            try
            {
                conn.OpenConnection();
                string query = $"INSERT INTO employee_schedule (employeeID, workDate, startTime, endTime) " +
                               $"VALUES ({employeeId}, '{workDate:yyyy-MM-dd}', '{startTime}', '{endTime}')";
                conn.ExecuteNonQuery(query);
                conn.CloseConnection();

                MessageBox.Show("Schedule saved successfully");
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving schedule: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            cmbEmployee.SelectedIndex = -1;
            dtpWorkDate.Value = DateTime.Today;
            dtpStartTime.Value = DateTime.Now.Date;
            dtpEndTime.Value = DateTime.Now.Date;
        }

        private void cmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
