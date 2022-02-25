using System;
using System.Drawing;
using System.Windows.Forms;
using nvan.PoKeysConnector.Config;
using PoKeysDevice_DLL;
using nvan.PoKeysConnector.Events;
using System.Collections.Generic;

namespace nvan.PoKeysConnector
{
    public partial class mainForm : Form
    {
        private ConfigManager configManager;
        private SimEventsManager simEventsManager;
        private LogForm logForm;

        public mainForm(ref ConfigManager configManager, ref SimEventsManager simEventsManager)
        {
            logForm = new LogForm();

            this.configManager = configManager;
            this.simEventsManager = simEventsManager;

            InitializeComponent();

            poKeysIpTextBox.Text = configManager.GetConfig().poKeysIp;
            xPlaneIpTextBox.Text = configManager.GetConfig().xPlaneIp;
            autoStartCheckBox.Checked = configManager.GetConfig().autoStart;

            updateList();

            if (autoStartCheckBox.Checked)
                connectButton_Click(null, null);
        }

        XPlaneConnector.XPlaneConnector xPlaneConnector;

        PoKeysDevice device = new PoKeysDevice();

        private Point GetPositionByPinNumber(byte pin)
        {
            int y = 9 - (int)Math.Floor((pin - 1) / 8d);
            int x = pin - (9 - y) * 8 - 1;

            return new Point(x, y);
        }

        private float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (connectButton.Text == "Disconnect")
            {
                connectButton.Text = "Disconnecting...";
                connectButton.Enabled = false;
                device.DisconnectDevice();
                return;
            }

            connectButton.Text = "Connecting...";
            connectButton.Enabled = false;

            configManager.UpdateConfig(new Config.Config { autoStart = autoStartCheckBox.Checked, poKeysIp = poKeysIpTextBox.Text, xPlaneIp = xPlaneIpTextBox.Text});
            xPlaneConnector = new XPlaneConnector.XPlaneConnector(
                    xPlaneIpTextBox.Text,
                    49000
                );

            xPlaneConnector.Start();

            int serialNumber = 0;
            int versionMajor = 0;
            int versionMinor = 0;

            if (device.ConnectToNetworkDevice(poKeysIpTextBox.Text))
            {
                device.GetDeviceIDEx(ref serialNumber, ref versionMajor, ref versionMinor);
                statusLabel.Text = "Status: Connected to PoKeys - " + serialNumber;
                connectButton.Text = "Disconnect";
                connectButton.Enabled = true;
                sync = true;

                device.SetAutoSetOutputs(1);
                timer.Start();
            }
            else
            {
                statusLabel.Text = "Status: Error while connecting, try again";
                connectButton.Text = "Connect";
                connectButton.Enabled = true;
                return;
            }

            xPlaneConnector.OnDataRefReceived += onDataRefUpdate;

            updateList();
        }

        bool[] oldInputs = new bool[55];

        int[] oldAnalogInputs = new int[7];

        int[] oldEncoders = new int[25];

        bool sync = false;


        Dictionary<int, bool> poExtBusOutputs = new Dictionary<int, bool>();
        Dictionary<int, bool> poExtBusOutputsTemp = new Dictionary<int, bool>();

        Dictionary<int, bool> normalOutputs = new Dictionary<int, bool>();
        Dictionary<int, bool> normalOutputsTemp = new Dictionary<int, bool>();

        Dictionary<string, float> finalDataRefs = new Dictionary<string, float>();

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            if (!device.Connected())
            {
                xPlaneConnector.Stop();
                connectButton.Enabled = true;
                connectButton.Text = "Connect";
                statusLabel.Text = "Status: Disconnected";
                reconnectTimer.Start();
                return;
            }

            byte poKeysCpuUsage = 0;
            if (device.GetLoadStatus(ref poKeysCpuUsage))
            {
                cpuProgressBar.Value = poKeysCpuUsage;
            }
            if (poKeysCpuUsage == 0)
            {
                timer.Start();
                return;
            }

            bool[] inputs = new bool[55];

            if (device.BlockGetInputAll55(ref inputs))
            {
                // DIGITAL AND DUAL
                if (sync)
                    inputs.CopyTo(oldInputs, 0);

                foreach (SimEvent simEvent in simEventsManager.GetEvents())
                {
                    if ((simEvent.pinType != PinTypes.PIN && simEvent.pinType != PinTypes.DUAL)
                        || simEvent.pinNumber < 1 || simEvent.pinNumber > 55
                        || (simEvent.pinType == PinTypes.DUAL && (simEvent.pin2Number < 1 || simEvent.pin2Number > 55)))
                        continue;

                    if (simEvent.io == InputOutput.Output)
                        continue;

                    if ((inputs[simEvent.pinNumber - 1] == oldInputs[simEvent.pinNumber - 1]
                        && (simEvent.pinType != PinTypes.DUAL || inputs[simEvent.pin2Number - 1] == oldInputs[simEvent.pin2Number - 1]))
                        && !(sync && simEvent.syncOnStartup))
                        continue;

                    if (simEvent.readValue > 0 == inputs[simEvent.pinNumber - 1]
                        && (simEvent.pinType != PinTypes.DUAL || simEvent.readMaxValue > 0 == inputs[simEvent.pin2Number - 1]))
                    {
                        if (simEvent.dataRef.StartsWith("%"))
                        {
                            xPlaneConnector.SendCommand(new XPlaneConnector.XPlaneCommand(simEvent.dataRef.Substring(1), ""));
                            continue;
                        }

                        finalDataRefs.Remove(simEvent.dataRef);
                        finalDataRefs.Add(simEvent.dataRef, simEvent.writeValue);
                    }
                }

                foreach (KeyValuePair<string, float> finalDataRef in finalDataRefs)
                {
                    xPlaneConnector.SetDataRefValue(new XPlaneConnector.DataRefElement { DataRef = finalDataRef.Key }, finalDataRef.Value);
                }

                inputs.CopyTo(oldInputs, 0);

                // ANALOG MAPPING
                int[] analogInputs = new int[7];
                device.GetAllAnalogInputs(ref analogInputs);

                if (sync)
                    analogInputs.CopyTo(oldAnalogInputs, 0);

                bool[] filteredInputs = new bool[7];

                foreach (SimEvent simEvent in simEventsManager.GetEvents())
                {
                    if (simEvent.pinType != PinTypes.ANALOG || simEvent.pinNumber < 41 || simEvent.pinNumber > 47)
                    {
                        continue;
                    }

                    if (analogInputs[simEvent.pinNumber - 41] != oldAnalogInputs[simEvent.pinNumber - 41])
                    {
                        // FILTER
                        if (!sync && Math.Abs(analogInputs[simEvent.pinNumber - 41] - oldAnalogInputs[simEvent.pinNumber - 41]) > 2000)
                        {
                            filteredInputs[simEvent.pinNumber - 41] = true;
                            continue;
                        }

                        float value = Map(analogInputs[simEvent.pinNumber - 41], simEvent.readValue, simEvent.readMaxValue, simEvent.writeValue, simEvent.writeMaxValue);

                        if (simEvent.round)
                            value = (float)Math.Round((decimal)value);

                        xPlaneConnector.SetDataRefValue(new XPlaneConnector.DataRefElement { DataRef = simEvent.dataRef }, value);
                    }
                }

                for(int i = 0; i < 7; i++)
                    if(!filteredInputs[i])
                        oldAnalogInputs[i] = analogInputs[i];

                // ENCODER
                if (sync)
                    for(int i = 0; i < 25; i++)
                        device.ResetEncoderValue((byte)i);

                int[] encoders = new int[25];
                for (int i = 0; i < 25; i++)
                    device.GetEncoderRAWValue((byte)i, ref encoders[i]);

                if (sync)
                    encoders.CopyTo(oldEncoders, 0);

                foreach (SimEvent simEvent in simEventsManager.GetEvents())
                {
                    if (simEvent.pinType != PinTypes.ENCODER || simEvent.pinNumber < 1 || simEvent.pinNumber > 55)
                        continue;

                    if (encoders[simEvent.pinNumber - 1] != oldEncoders[simEvent.pinNumber - 1])
                        xPlaneConnector.SetDataRefValue(new XPlaneConnector.DataRefElement { DataRef = simEvent.dataRef }, encoders[simEvent.pinNumber - 1] + simEvent.readValue);
                }

                encoders.CopyTo(oldEncoders, 0);

                sync = false;

                // NORMAL OUTPUTS
                try
                {
                    normalOutputsTemp.Clear();

                    // FAST CLONE TO AVOID PROBLEMS IF COLLECTION IS MODIFIED WHILE SENDING DATA TO POKEYS
                    foreach (KeyValuePair<int, bool> normalOutput in normalOutputs)
                    {
                        normalOutputsTemp.Add(normalOutput.Key, normalOutput.Value);
                    }

                    normalOutputs.Clear();
                    // END CLONATION

                    foreach (KeyValuePair<int, bool> normalOutput in normalOutputsTemp)
                    {
                        device.SetPinData((byte)normalOutput.Key, (byte)4);
                        device.SetOutput((byte)normalOutput.Key, normalOutput.Value);
                        logForm.log("Sent to Normal Output (" + normalOutput.Key + "): " + (normalOutput.Value ? "ON" : "OFF"));
                    }
                }
                catch (Exception ex)
                {
                    logForm.log("Error while sending normal outputs: " + ex.Message);
                }


                // POEXTBUS OUTPUTS
                try
                {
                    poExtBusOutputsTemp.Clear();

                    // FAST CLONE TO AVOID PROBLEMS IF COLLECTION IS MODIFIED WHILE SENDING DATA TO POKEYS
                    foreach (KeyValuePair<int, bool> poExtBusOutput in poExtBusOutputs)
                    {
                        poExtBusOutputsTemp.Add(poExtBusOutput.Key, poExtBusOutput.Value);
                    }

                    poExtBusOutputs.Clear();
                    // END CLONATION

                    foreach (KeyValuePair<int, bool> poExtBusOutput in poExtBusOutputsTemp)
                    {
                        var pos = GetPositionByPinNumber((byte)poExtBusOutput.Key);

                        device.AuxilaryBusSetData(1, (byte)pos.Y, (byte)pos.X, poExtBusOutput.Value);
                        logForm.log("Sent to PoExtBus (" + poExtBusOutput.Key + "): " + (poExtBusOutput.Value ? "ON" : "OFF"));
                    }
                }
                catch (Exception ex)
                {
                    logForm.log("Error while sending POEXT outputs: " + ex.Message);
                }
            }

            timer.Start();
        }

        public void updateList()
        {
            simEventsManager.GetEvents();
            simEventsManager.SaveEvents();

            eventsList.Items.Clear();
            
            foreach(SimEvent simEvent in simEventsManager.GetEvents())
            {
                ListViewItem item = new ListViewItem(simEvent.name);
                item.Tag = simEvent;
                item.SubItems.Add(simEvent.pinNumber.ToString() + (simEvent.pinType == PinTypes.DUAL ? "/" + simEvent.pin2Number.ToString() : "") + (simEvent.pinType == PinTypes.POEXTBUS ? " (PE)" : (simEvent.pinType == PinTypes.ENCODER ? "(ENC)" : "")));
                item.SubItems.Add(simEvent.readValue.ToString() + (simEvent.pinType == PinTypes.DUAL ? "/" + simEvent.readMaxValue.ToString() : ""));
                item.SubItems.Add(simEvent.dataRef);
                item.SubItems.Add(simEvent.writeValue.ToString());
                item.SubItems.Add(simEvent.io.ToString());

                eventsList.Items.Add(item);
            }

            suscribeToDataRefs();
        }

        private void suscribeToDataRefs()
        {
            if (xPlaneConnector == null)
                return;

            foreach(SimEvent simEvent in simEventsManager.GetEvents())
            {
                if (simEvent.io == InputOutput.Input)
                    continue;

                xPlaneConnector.Unsubscribe(simEvent.dataRef); // To avoid duplicates
                xPlaneConnector.Subscribe(new XPlaneConnector.DataRefElement { DataRef = simEvent.dataRef }, 5);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var simEvent = new SimEvent();
            simEventsManager.GetEvents().Add(simEvent); 
            new eventsForm(ref simEvent, updateList).ShowDialog();           
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in eventsList.SelectedItems)
            {
                simEventsManager.GetEvents().Remove((SimEvent)item.Tag);

                eventsList.Items.Remove(item);
            }

            updateList();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (eventsList.SelectedItems.Count == 0)
                return;

            var simEvent = (SimEvent)eventsList.SelectedItems[0].Tag;
            new eventsForm(ref simEvent, updateList).ShowDialog();
        }

        private void onDataRefUpdate(XPlaneConnector.DataRefElement dataRef)
        {
            logForm.log("Received DataRef: " + dataRef.DataRef + " - Value: " + dataRef.Value);

            if (!device.Connected())
            {
                logForm.log("DataRef not saved due to device not connected");
                return;
            }

            foreach(SimEvent simEvent in simEventsManager.GetEvents())
            {
                if (simEvent.io == InputOutput.Input)
                    continue;

                if (simEvent.dataRef != dataRef.DataRef)
                    continue;

                if ((int)simEvent.readValue != (int)Math.Round(dataRef.Value))
                    continue;

                switch(simEvent.pinType.ToString())
                {
                    case "PIN":
                        normalOutputs.Remove(simEvent.pinNumber - 1);
                        normalOutputs.Add(simEvent.pinNumber - 1, simEvent.writeValue > 0);
                        logForm.log("Queued DataRef for Normal Output");
                        break;

                    case "POEXTBUS":
                        poExtBusOutputs.Remove(simEvent.pinNumber);
                        poExtBusOutputs.Add(simEvent.pinNumber, simEvent.writeValue > 0);
                        logForm.log("Queued DataRef for PoExt Output");
                        break;
                }
            }
        }

        int reconnectTime = 0;
        private void reconnectTimer_Tick(object sender, EventArgs e)
        {
            reconnectTimer.Stop();
            if(reconnectTime == 1)
            {
                reconnectTime = 0;
                connectButton_Click(null, null);
                return;
            }

            if (reconnectTime == 0)
                reconnectTime = 5;

            statusLabel.Text = "Status: Reconnecting in " + --reconnectTime + "...";

            reconnectTimer.Start();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            if (!logForm.Visible)
            {
                logForm.Show();
                return;
            }

            logForm.closeButton_Click(null, null);
        }
    }
}
