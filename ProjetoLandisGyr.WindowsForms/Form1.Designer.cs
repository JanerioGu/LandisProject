namespace ProjetoLandisGyr.WindowsForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.TextBox txtModelId;
        private System.Windows.Forms.TextBox txtMeterNumber;
        private System.Windows.Forms.TextBox txtFirmware;
        private System.Windows.Forms.TextBox txtSwitchState;
        private System.Windows.Forms.ListView listViewEndpoints;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label status2;
        private System.Windows.Forms.Label status3;
        private System.Windows.Forms.Label title;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.BackColor = System.Drawing.Color.White;
            this.Text = "Landis+Gyr - Manager";

            title = new System.Windows.Forms.Label();
            title.Text = "Landis+Gyr - Manager";
            title.Font = new System.Drawing.Font("Arial", 22, System.Drawing.FontStyle.Bold);
            title.ForeColor = System.Drawing.Color.FromArgb(0, 153, 0);
            title.AutoSize = true;
            title.Location = new System.Drawing.Point((ClientSize.Width - title.Width) / 2, 10);
            title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            txtSerial = new System.Windows.Forms.TextBox();
            txtSerial.Location = new System.Drawing.Point(20, 60);
            txtSerial.PlaceholderText = "Enter Serial Number";
            txtSerial.Size = new System.Drawing.Size(320, 27);
            txtSerial.TextChanged += txtSerial_TextChanged;

            txtModelId = new System.Windows.Forms.TextBox();
            txtModelId.Location = new System.Drawing.Point(20, 100);
            txtModelId.PlaceholderText = "Enter Model ID (16-19)";
            txtModelId.Size = new System.Drawing.Size(320, 27);
            txtModelId.TextChanged += TxtModelId_TextChanged;

            txtMeterNumber = new System.Windows.Forms.TextBox();
            txtMeterNumber.Location = new System.Drawing.Point(20, 140);
            txtMeterNumber.PlaceholderText = "Enter Meter Number";
            txtMeterNumber.Size = new System.Drawing.Size(320, 27);

            txtFirmware = new System.Windows.Forms.TextBox();
            txtFirmware.Location = new System.Drawing.Point(20, 180);
            txtFirmware.PlaceholderText = "Enter Firmware Version";
            txtFirmware.Size = new System.Drawing.Size(320, 27);
            txtFirmware.TextChanged += txtFirmware_TextChanged;

            txtSwitchState = new System.Windows.Forms.TextBox();
            txtSwitchState.Location = new System.Drawing.Point(20, 220);
            txtSwitchState.PlaceholderText = "Enter Switch State (0-2)";
            txtSwitchState.Size = new System.Drawing.Size(320, 27);
            txtSwitchState.TextChanged += txtSwitchState_TextChanged;

            listViewEndpoints = new System.Windows.Forms.ListView();
            listViewEndpoints.Location = new System.Drawing.Point(422, 60);
            listViewEndpoints.Size = new System.Drawing.Size(604, 230);
            listViewEndpoints.View = System.Windows.Forms.View.Details;

            btnAdd = new System.Windows.Forms.Button();
            btnAdd.Location = new System.Drawing.Point(20, 260);
            btnAdd.Size = new System.Drawing.Size(75, 30);
            btnAdd.Text = "Add";
            btnAdd.BackColor = System.Drawing.Color.FromArgb(0, 153, 0);  // Verde Landis+Gyr
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.Click += btnAdd_Click;

            btnUpdate = new System.Windows.Forms.Button();
            btnUpdate.Location = new System.Drawing.Point(100, 260);
            btnUpdate.Size = new System.Drawing.Size(75, 30);
            btnUpdate.Text = "Update";
            btnUpdate.BackColor = System.Drawing.Color.FromArgb(102, 204, 0);  // Verde claro
            btnUpdate.ForeColor = System.Drawing.Color.White;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete = new System.Windows.Forms.Button();
            btnDelete.Location = new System.Drawing.Point(180, 260);
            btnDelete.Size = new System.Drawing.Size(75, 30);
            btnDelete.Text = "Delete";
            btnDelete.BackColor = System.Drawing.Color.Red;
            btnDelete.ForeColor = System.Drawing.Color.White;
            btnDelete.Click += btnDelete_Click;

            btnExit = new System.Windows.Forms.Button();
            btnExit.Location = new System.Drawing.Point(260, 260);
            btnExit.Size = new System.Drawing.Size(75, 30);
            btnExit.Text = "Exit";
            btnExit.BackColor = System.Drawing.Color.Gray;
            btnExit.ForeColor = System.Drawing.Color.White;
            btnExit.Click += btnExit_Click;

            lblStatus = new System.Windows.Forms.Label();
            lblStatus.Location = new System.Drawing.Point(20, 320);
            lblStatus.Size = new System.Drawing.Size(566, 25);
            lblStatus.Text = "Waiting...";

            status2 = new System.Windows.Forms.Label();
            status2.Location = new System.Drawing.Point(20, 350);
            status2.Size = new System.Drawing.Size(501, 25);
            status2.Text = "Waiting...";

            status3 = new System.Windows.Forms.Label();
            status3.Location = new System.Drawing.Point(20, 380);
            status3.Size = new System.Drawing.Size(300, 25);
            status3.Text = "Waiting...";

            Controls.Add(title);
            Controls.Add(txtSerial);
            Controls.Add(txtModelId);
            Controls.Add(txtMeterNumber);
            Controls.Add(txtFirmware);
            Controls.Add(txtSwitchState);
            Controls.Add(listViewEndpoints);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnExit);
            Controls.Add(lblStatus);
            Controls.Add(status2);
            Controls.Add(status3);

            Name = "Form1";
            ClientSize = new System.Drawing.Size(1081, 450);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
