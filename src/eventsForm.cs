using nvan.PoKeysConnector.Events;
using System;
using System.Windows.Forms;

namespace nvan.PoKeysConnector
{
    public partial class eventsForm : Form
    {
        private SimEvent simEvent;
        private Action updateList;

        public eventsForm(ref SimEvent simEvent, Action updateList)
        {
            InitializeComponent();

            this.simEvent = simEvent;
            this.updateList = updateList;

            nameTextBox.Text = simEvent.name;
            dataRefTextBox.Text = simEvent.dataRef;

            readValueTextBox.Text = simEvent.readValue.ToString();
            writeValueTextBox.Text = simEvent.writeValue.ToString();

            readMaxTextBox.Text = simEvent.readMaxValue.ToString();
            writeMaxTextBox.Text = simEvent.writeMaxValue.ToString();

            roundValue.Checked = simEvent.round;

            if (simEvent.io == InputOutput.Input)
                inputRadio.Checked = true;
            else
                outputRadio.Checked = true;

            pinTypeSelector.Text = simEvent.pinType.ToString();
            pinSelector.Text = simEvent.pinNumber.ToString();
            pin2Selector.Text = simEvent.pin2Number.ToString();

            syncCheckBox.Checked = simEvent.syncOnStartup;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            simEvent.name = nameTextBox.Text;
            simEvent.dataRef = dataRefTextBox.Text;

            simEvent.readValue = (float)Convert.ToDecimal(readValueTextBox.Text);
            simEvent.writeValue = (float)Convert.ToDecimal(writeValueTextBox.Text);

            simEvent.readMaxValue = (float)Convert.ToDecimal(readMaxTextBox.Text);
            simEvent.writeMaxValue = (float)Convert.ToDecimal(writeMaxTextBox.Text);

            simEvent.io = inputRadio.Checked ? InputOutput.Input : InputOutput.Output;

            simEvent.pinType = (PinTypes)Enum.Parse(typeof(PinTypes), pinTypeSelector.Text);

            simEvent.pinNumber = Convert.ToInt32(pinSelector.Text);
            simEvent.pin2Number = Convert.ToInt32(pin2Selector.Text);

            simEvent.syncOnStartup = syncCheckBox.Checked;

            simEvent.round = roundValue.Checked;

            updateList();

            Close();
        }

        private void pinTypeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            pinSelector.Items.Clear();
            pin2Selector.Items.Clear();

            int minNumber = 0;
            int maxNumber = 0;

            switch (pinTypeSelector.Text)
            {
                case "PIN":
                case "DUAL":
                    minNumber = 1;
                    maxNumber = 55;
                    break;

                case "POEXTBUS":
                    minNumber = 1;
                    maxNumber = 80;
                    break;

                case "ANALOG":
                    minNumber = 41;
                    maxNumber = 47;
                    break;

                case "ENCODER":
                    minNumber = 1;
                    maxNumber = 25;
                    break;
            }

            if (minNumber == 0 && maxNumber == 0)
                return;

            for (int i = minNumber; i <= maxNumber; i++)
            {
                pinSelector.Items.Add(i.ToString());
                pin2Selector.Items.Add(i.ToString());
            }

            if (pinTypeSelector.Text == "ANALOG")
            {
                readMaxLabel.Show();
                readMaxTextBox.Show();
                writeMaxLabel.Show();
                writeMaxTextBox.Show();
                roundValue.Show();
                readValueLabel.Text = "Read Min Value";
                writeValueLabel.Text = "Write Min Value";
            }
            else
            {
                readMaxLabel.Hide();
                readMaxTextBox.Hide();
                writeMaxLabel.Hide();
                writeMaxTextBox.Hide();
                roundValue.Hide();
                readValueLabel.Text = "Read Value";
                writeValueLabel.Text = "Write Value";
            }

            if(pinTypeSelector.Text == "ENCODER")
            {
                readValueLabel.Text = "Offset";
                writeValueLabel.Hide();
                writeValueTextBox.Hide();
                syncCheckBox.Hide();
                pinLabel.Text = "Encoder";
            }
            else
            {
                writeValueLabel.Show();
                writeValueTextBox.Show();
                syncCheckBox.Show();
                pinLabel.Text = "Pin";
            }

            if(pinTypeSelector.Text == "DUAL")
            {
                readMaxLabel.Text = "Read Value";
                readMaxLabel.Show();
                readMaxTextBox.Show();
                pin2Label.Show();
                pin2Selector.Show();
            }
            else
            {
                readMaxLabel.Text = "Read Max Value";
                pin2Label.Hide();
                pin2Selector.Hide();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
