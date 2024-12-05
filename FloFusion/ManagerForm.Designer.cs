namespace FloFusion
{
    partial class ManagerForm
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
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnViewSchedule = new System.Windows.Forms.Button();
            this.btnApproveTimeOff = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnInputSchedule = new System.Windows.Forms.Button();
            this.btnOpenRegister = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(20, 240);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(150, 40);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnViewSchedule
            // 
            this.btnViewSchedule.Location = new System.Drawing.Point(20, 120);
            this.btnViewSchedule.Name = "btnViewSchedule";
            this.btnViewSchedule.Size = new System.Drawing.Size(150, 40);
            this.btnViewSchedule.TabIndex = 6;
            this.btnViewSchedule.Text = "View Schedule";
            this.btnViewSchedule.UseVisualStyleBackColor = true;
            this.btnViewSchedule.Click += new System.EventHandler(this.btnViewSchedule_Click);
            // 
            // btnApproveTimeOff
            // 
            this.btnApproveTimeOff.Location = new System.Drawing.Point(20, 60);
            this.btnApproveTimeOff.Name = "btnApproveTimeOff";
            this.btnApproveTimeOff.Size = new System.Drawing.Size(150, 40);
            this.btnApproveTimeOff.TabIndex = 5;
            this.btnApproveTimeOff.Text = "Approve Time Off";
            this.btnApproveTimeOff.UseVisualStyleBackColor = true;
            this.btnApproveTimeOff.Click += new System.EventHandler(this.btnRequestTimeOff_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(186, 22);
            this.lblWelcome.TabIndex = 4;
            this.lblWelcome.Text = "Welcome, Manager";
            // 
            // btnInputSchedule
            // 
            this.btnInputSchedule.Location = new System.Drawing.Point(20, 180);
            this.btnInputSchedule.Name = "btnInputSchedule";
            this.btnInputSchedule.Size = new System.Drawing.Size(150, 40);
            this.btnInputSchedule.TabIndex = 8;
            this.btnInputSchedule.Text = "Create Schedule";
            this.btnInputSchedule.UseVisualStyleBackColor = true;
            this.btnInputSchedule.Click += new System.EventHandler(this.btnInputSchedule_Click);
            // 
            // btnOpenRegister
            // 
            this.btnOpenRegister.Location = new System.Drawing.Point(176, 60);
            this.btnOpenRegister.Name = "btnOpenRegister";
            this.btnOpenRegister.Size = new System.Drawing.Size(150, 40);
            this.btnOpenRegister.TabIndex = 9;
            this.btnOpenRegister.Text = "Register";
            this.btnOpenRegister.UseVisualStyleBackColor = true;
            this.btnOpenRegister.Click += new System.EventHandler(this.btnOpenRegister_Click);
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOpenRegister);
            this.Controls.Add(this.btnInputSchedule);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnViewSchedule);
            this.Controls.Add(this.btnApproveTimeOff);
            this.Controls.Add(this.lblWelcome);
            this.Name = "ManagerForm";
            this.Text = "Manager Portal";
            this.Load += new System.EventHandler(this.ManagerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnViewSchedule;
        private System.Windows.Forms.Button btnApproveTimeOff;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnInputSchedule;
        private System.Windows.Forms.Button btnOpenRegister;
    }
}