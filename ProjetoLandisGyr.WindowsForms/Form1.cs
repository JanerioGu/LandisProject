using System;
using System.Windows.Forms;
using ProjetoLandisGyr.Models;
using ProjetoLandisGyr.Repositories;

namespace ProjetoLandisGyr.WindowsForms
{
    public partial class Form1 : Form
    {
        private readonly IEndpointRepository _repository;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents(); 
            _repository = new InMemoryEndpointRepository();
            ConfigureListView();
        }

        private void InitializeCustomComponents()
        {
            txtSerial.PlaceholderText = "Enter Serial Number";
            txtModelId.PlaceholderText = "Enter Model ID (16-19)";
            txtMeterNumber.PlaceholderText = "Enter Meter Number";
            txtFirmware.PlaceholderText = "Enter Firmware Version";
            txtSwitchState.PlaceholderText = "Enter Switch State (0-2)";

            listViewEndpoints.View = View.Details;
            listViewEndpoints.Columns.Add("Serial Number", 120);
            listViewEndpoints.Columns.Add("Model Name", 100);
            listViewEndpoints.Columns.Add("Meter Number", 100);
            listViewEndpoints.Columns.Add("Firmware", 100);
            listViewEndpoints.Columns.Add("Switch State", 100);

            txtSerial.TextChanged += txtSerial_TextChanged;
            txtFirmware.TextChanged += txtFirmware_TextChanged;
            txtSwitchState.TextChanged += txtSwitchState_TextChanged;
            listViewEndpoints.SelectedIndexChanged += ListViewEndpoints_SelectedIndexChanged;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                var serial = txtSerial.Text;
                var modelId = int.Parse(txtModelId.Text);
                var meterNumber = int.Parse(txtMeterNumber.Text);
                var firmware = txtFirmware.Text;
                var switchState = int.Parse(txtSwitchState.Text);

                var endpoint = new Endpoint(serial, modelId, meterNumber, firmware, switchState);
                _repository.AddEndpoint(endpoint);

                MessageBox.Show("Endpoint added successfully!");

                RefreshEndpointList();

                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var serial = txtSerial.Text;
                var newSwitchState = int.Parse(txtSwitchState.Text);

                if (_repository.EditSwitchState(serial, newSwitchState))
                {
                    MessageBox.Show("Switch State updated successfully!");
                    ClearInputFields();
                    RefreshEndpointList();
                    ConfigureListView();
                }
                else
                {
                    MessageBox.Show("Endpoint not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ClearInputFields()
        {
            txtSerial.Clear();
            txtModelId.Clear();
            txtMeterNumber.Clear();
            txtFirmware.Clear();
            txtSwitchState.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var serial = txtSerial.Text;

            if (_repository.DeleteEndpoint(serial))
            {
                MessageBox.Show("Endpoint deleted successfully!");
                RefreshEndpointList();
            }
            else
            {
                MessageBox.Show("Endpoint not found.");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void RefreshEndpointList()
        {
            listViewEndpoints.Items.Clear();
            foreach (var endpoint in _repository.GetAllEndpoints())
            {
                var item = new ListViewItem(endpoint.EndpointSerialNumber);
                item.SubItems.Add(endpoint.GetMeterModelName());
                item.SubItems.Add(endpoint.MeterNumber.ToString());
                item.SubItems.Add(endpoint.MeterFirmwareVersion);
                item.SubItems.Add(endpoint.SwitchState.ToString());

                listViewEndpoints.Items.Add(item);
            }
        }


        private void listViewEndpoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewEndpoints.SelectedItems.Count > 0)
            {
                var selectedItem = listViewEndpoints.SelectedItems[0];
                var endpoint = _repository.FindEndpoint(selectedItem.Text);

                if (endpoint != null)
                {
                    txtSerial.Text = endpoint.EndpointSerialNumber;
                    txtModelId.Text = endpoint.MeterModelId.ToString();
                    txtMeterNumber.Text = endpoint.MeterNumber.ToString();
                    txtFirmware.Text = endpoint.MeterFirmwareVersion;
                    txtSwitchState.Text = endpoint.SwitchState.ToString();
                }
            }
        }

        private void ConfigureListView()
        {
            listViewEndpoints.View = View.Details;
            listViewEndpoints.Columns.Clear();
            listViewEndpoints.Columns.Add("Serial Number", 120);
            listViewEndpoints.Columns.Add("Model Name", 100);
            listViewEndpoints.Columns.Add("Meter Number", 100);
            listViewEndpoints.Columns.Add("Firmware", 100);
            listViewEndpoints.Columns.Add("Switch State", 100);
        }


        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtSerial.Text) ||
                string.IsNullOrWhiteSpace(txtModelId.Text) ||
                string.IsNullOrWhiteSpace(txtMeterNumber.Text) ||
                string.IsNullOrWhiteSpace(txtFirmware.Text) ||
                string.IsNullOrWhiteSpace(txtSwitchState.Text))
            {
                MessageBox.Show("All fields must be filled!");
                return false;
            }

            if (!int.TryParse(txtModelId.Text, out _) ||
                !int.TryParse(txtMeterNumber.Text, out _) ||
                !int.TryParse(txtSwitchState.Text, out _))
            {
                MessageBox.Show("Model Id, Meter Number, and Switch State must be valid numbers.");
                return false;
            }

            return true;
        }

        private void txtSerial_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSerial.Text))
            {
                btnAdd.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
            }
        }

        private void txtFirmware_TextChanged(object sender, EventArgs e)
        {
            string firmware = txtFirmware.Text;

            var regex = new System.Text.RegularExpressions.Regex(@"^\d+\.\d+$");

            if (string.IsNullOrEmpty(firmware))
            {
                lblStatus.Text = "Firmware field is required.";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                btnAdd.Enabled = false;
            }
            else if (!regex.IsMatch(firmware))
            {
                lblStatus.Text = "Invalid firmware format. Use e.g., 1.1";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                btnAdd.Enabled = false;
            }
            else
            {
                lblStatus.Text = "Firmware format is valid.";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                btnAdd.Enabled = true;
            }
        }


        private void ListViewEndpoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewEndpoints.SelectedItems.Count > 0)
            {
                var selectedItem = listViewEndpoints.SelectedItems[0];
                var endpoint = _repository.FindEndpoint(selectedItem.Text);

                if (endpoint != null)
                {
                    txtSerial.Text = endpoint.EndpointSerialNumber;
                    txtModelId.Text = endpoint.MeterModelId.ToString();
                    txtMeterNumber.Text = endpoint.MeterNumber.ToString();
                    txtFirmware.Text = endpoint.MeterFirmwareVersion;
                    txtSwitchState.Text = endpoint.SwitchState.ToString();
                    lblStatus.Text = "Endpoint loaded.";
                }
            }
        }

        private void TxtModelId_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtModelId.Text, out int modelId))
            {
                string modelName = modelId switch
                {
                    16 => "NSX1P2W",
                    17 => "NSX1P3W",
                    18 => "NSX2P3W",
                    19 => "NSX3P4W",
                    _ => "Unknown Model"
                };

                if (modelName == "Unknown Model")
                {
                    status2.Text = $"Invalid Model ID: {modelId}. Enter a value between 16 and 19.";
                    status2.ForeColor = System.Drawing.Color.Red;
                    btnAdd.Enabled = false;
                }
                else
                {
                    status2.Text = $"Model ID changed to {modelId}: {modelName}";
                    status2.ForeColor = System.Drawing.Color.Green;
                    btnAdd.Enabled = true;
                }
            }
            else
            {
                status2.Text = "Invalid value to 'Model ID'. Please enter a number between 16 and 19.";
                status2.ForeColor = System.Drawing.Color.Red;
                btnAdd.Enabled = false;
            }
        }

        private void txtSwitchState_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtSwitchState.Text, out int switchId))
            {
                string switchName = switchId switch
                {
                    0 => "Disconnected",
                    1 => "Connected",
                    2 => "Armed",
                    _ => "Unknown Model"
                };

                if (switchName == "Unknown Model")
                {
                    status3.Text = $"Invalid Switch State: {switchId}. Enter a value between 0 and 2.";
                    status3.ForeColor = System.Drawing.Color.Red;
                    btnAdd.Enabled = false;
                }
                else
                {
                    status3.Text = $"Switch State is valid: {switchName}";
                    status3.ForeColor = System.Drawing.Color.Green;
                    btnAdd.Enabled = true;
                }
            }
            else
            {
                status3.Text = "Invalid input. Enter a number (0, 1, or 2).";
                status3.ForeColor = System.Drawing.Color.Red;
                btnAdd.Enabled = false;
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void status2_Click(object sender, EventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void status3_Click(object sender, EventArgs e)
        {

        }
    }
}
