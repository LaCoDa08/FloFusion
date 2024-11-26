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
    public partial class ViewScheduleForm : Form
    {
        Connection conn = new Connection();
        public ViewScheduleForm()
        {
            InitializeComponent();
        }

        private void ViewScheduleForm_Load(object sender, EventArgs e)
        {
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            try
            {
                conn.OpenConnection();
                string query = $"SELECT workDate, startTime, endTime FROM employee_schedule WHERE employeeID = {LoginInfo.EmployeeID}";
                DataSet ds = conn.DataSet(query);

                calendarSchedule.RemoveAllBoldedDates();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DateTime workDate = Convert.ToDateTime(row["workDate"]);
                    calendarSchedule.AddBoldedDate(workDate);
                }

                calendarSchedule.UpdateBoldedDates();
                conn.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading schedule: " + ex.Message);
            }
        }

        private void calendarSchedule_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = e.Start;

        }

        private void ShowDailySchedule(DateTime date)
        {
            try
            {
                conn.OpenConnection();
                string query = $"SELECT startTime, endTime FROM employee_schedule WHERE employeeID = " +
                    $"{LoginInfo.EmployeeID} AND workDate = '{date.ToString("yyyy-MM-dd")}'";
                MySqlDataReader reader = conn.DataReader(query);

                string scheduleInfo = $"Schedule for {date.ToShortDateString()}:\n";
                bool hasSchedule = false;

                while (reader.Read())
                {
                    hasSchedule = true;
                    scheduleInfo += $"- {reader["startTime"]} to {reader["endTime"]}\n";
                }

                if (!hasSchedule)
                {
                    scheduleInfo += "No work scheduled.";
                }

                MessageBox.Show(scheduleInfo, "Daily Schedule");
                conn.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading daily schedule: " + ex.Message);
            }
        }
    }
}
