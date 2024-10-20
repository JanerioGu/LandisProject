using System;
using System.Net;

namespace ProjetoLandisGyr.Models
{
    public class Endpoint
    {
        public string EndpointSerialNumber { get; private set; }
        public int MeterModelId { get; private set; }
        public int MeterNumber { get; private set; }
        public string MeterFirmwareVersion { get; private set; }
        public int SwitchState { get; private set; }

        public Endpoint(string serialNumber, int modelId, int meterNumber, string firmwareVersion, int switchState)
        {
            EndpointSerialNumber = serialNumber;
            MeterModelId = ValidateMeterModelId(modelId);
            MeterNumber = meterNumber;
            MeterFirmwareVersion = firmwareVersion;
            SwitchState = ValidateSwitchState(switchState);
        }

        private int ValidateMeterModelId(int modelId)
        {
            if (modelId < 16 || modelId > 19)
                throw new ArgumentException("Invalid Meter Model Id. Allowed values are 16, 17, 18, and 19.");
            return modelId;
        }

        private int ValidateSwitchState(int state)
        {
            if (state < 0 || state > 2)
                throw new ArgumentException("Invalid Switch State. Allowed values are 0 (Disconnected), 1 (Connected), or 2 (Armed).");
            return state;
        }

        public string GetMeterModelName()
        {
            return MeterModelId switch
            {
                16 => "NSX1P2W",
                17 => "NSX1P3W",
                18 => "NSX2P3W",
                19 => "NSX3P4W",
                _ => "Unknown"
            };
        }

        public void SetSwitchState(int newState)
        {
            SwitchState = ValidateSwitchState(newState);
        }



        public override string ToString()
        {
            return $"Serial: {EndpointSerialNumber}, Model: {GetMeterModelName()} ({MeterModelId}), " +
                   $"Meter Number: {MeterNumber}, Firmware: {MeterFirmwareVersion}, " +
                   $"Switch State: {SwitchState}";
        }
    }
}
