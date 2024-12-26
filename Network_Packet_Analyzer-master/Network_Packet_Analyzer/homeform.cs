using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Network_Packet_Analyzer
{
    public partial class homeform : Form
    {
        public homeform()
        {
            InitializeComponent();
        }


        private void homeform_Load(object sender, EventArgs e)
        {
            // Initialize progress bar
            progressbarstart.Value = 0;
            progressbarstart.ForeColor = Color.Teal; // Set the initial color to teal
            progressbarstart.Maximum = 100; // Set the maximum value for the progress bar
            progressbarstart.Step = 1; // Set step increment for each update
            StartProgressBar();
        }

        private void StartProgressBar()
        {
            // Simulate a task that updates the progress bar
            Timer progressTimer = new Timer();
            progressTimer.Interval = 100; // Set interval for the timer to update the progress bar
            progressTimer.Tick += (s, e) =>
            {
                // Increment the progress bar value
                if (progressbarstart.Value < progressbarstart.Maximum)
                {
                    progressbarstart.PerformStep(); // Increase progress by the step value
                }
                else
                {
                    // When the progress reaches 100, stop the timer, hide this form, and show Form1
                    progressTimer.Stop();
                    this.Hide(); // Hide the current form
                    Form1 form1 = new Form1();
                    form1.Show(); // Show Form1
                }
            };
            progressTimer.Start(); // Start the timer to update the progress bar
        }

        private void progressbarstart_Click(object sender, EventArgs e)
        {

        }
    }
}
