using System;
using System.Windows.Forms;

namespace nvan.PoKeysConnector
{
    public partial class LogForm : Form
    {
        private bool isPaused = true;

        public LogForm()
        {
            InitializeComponent();
        }

        public void closeButton_Click(object sender, EventArgs e)
        {
            isPaused = true;
            startButton.Text = "Start";
            Hide();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Start")
            {
                isPaused = false;
                startButton.Text = "Stop";
            }
            else
            {
                isPaused = true;
                startButton.Text = "Start";
            }
        }

        public void log(string text)
        {
            if (isPaused)
                return;

            logBox.AppendText("\n" + text);
            logBox.ScrollToCaret();
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e != null)
                e.Cancel = true;

            closeButton_Click(null, null);
        }
    }
}
