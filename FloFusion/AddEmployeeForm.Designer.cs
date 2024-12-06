namespace FloFusion
{
    partial class AddEmployeeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.cbEmployeeRole = new System.Windows.Forms.ComboBox();
            this.btnAddEmployee = new System.Windows.Forms.Button();
            this.txtEmployeeEmail = new System.Windows.Forms.TextBox();
            this.txtEmployeeUsername = new System.Windows.Forms.TextBox();
            this.txtEmployeePassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(20, 20);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(200, 20);
            this.txtEmployeeName.TabIndex = 0;
            this.txtEmployeeName.Text = "Employee Name";
            // 
            // cbEmployeeRole
            // 
            this.cbEmployeeRole.FormattingEnabled = true;
            this.cbEmployeeRole.Items.AddRange(new object[] {
            "Manager",
            "Associate"});
            this.cbEmployeeRole.Location = new System.Drawing.Point(20, 60);
            this.cbEmployeeRole.Name = "cbEmployeeRole";
            this.cbEmployeeRole.Size = new System.Drawing.Size(200, 21);
            this.cbEmployeeRole.TabIndex = 1;
            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Location = new System.Drawing.Point(20, 220);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(100, 30);
            this.btnAddEmployee.TabIndex = 2;
            this.btnAddEmployee.Text = "Add Employee";
            this.btnAddEmployee.UseVisualStyleBackColor = true;
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddEmployee_Click);
            // 
            // txtEmployeeEmail
            // 
            this.txtEmployeeEmail.Location = new System.Drawing.Point(20, 100);
            this.txtEmployeeEmail.Name = "txtEmployeeEmail";
            this.txtEmployeeEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmployeeEmail.TabIndex = 3;
            this.txtEmployeeEmail.Text = "Employee Email";
            // 
            // txtEmployeeUsername
            // 
            this.txtEmployeeUsername.Location = new System.Drawing.Point(20, 140);
            this.txtEmployeeUsername.Name = "txtEmployeeUsername";
            this.txtEmployeeUsername.Size = new System.Drawing.Size(200, 20);
            this.txtEmployeeUsername.TabIndex = 4;
            this.txtEmployeeUsername.Text = "Username";
            // 
            // txtEmployeePassword
            // 
            this.txtEmployeePassword.Location = new System.Drawing.Point(20, 180);
            this.txtEmployeePassword.Name = "txtEmployeePassword";
            this.txtEmployeePassword.Size = new System.Drawing.Size(200, 20);
            this.txtEmployeePassword.TabIndex = 5;
            this.txtEmployeePassword.Text = "Password";
            // 
            // AddEmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtEmployeePassword);
            this.Controls.Add(this.txtEmployeeUsername);
            this.Controls.Add(this.txtEmployeeEmail);
            this.Controls.Add(this.btnAddEmployee);
            this.Controls.Add(this.cbEmployeeRole);
            this.Controls.Add(this.txtEmployeeName);
            this.Name = "AddEmployeeForm";
            this.Text = "Add New Employee";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.ComboBox cbEmployeeRole;
        private System.Windows.Forms.Button btnAddEmployee;
        private System.Windows.Forms.TextBox txtEmployeeEmail;
        private System.Windows.Forms.TextBox txtEmployeeUsername;
        private System.Windows.Forms.TextBox txtEmployeePassword;
    }
}