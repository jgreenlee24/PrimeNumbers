using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PrimeNumbers_GUI
{
    public partial class MainForm : Form
    {
        private bool cancelJob = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            // Find all prime numbers starting between the first and last numbers
            int first = Convert.ToInt32(startNumTextBox.Text);
            int last = Convert.ToInt32(endNumTextBox.Text);

            numbersTextBox.Clear();

            // Prevent user from messing with certain controls while job is running
            progressBar1.Minimum = first;
            progressBar1.Maximum = last;
            progressBar1.Visible = true;
            cancelButton.Enabled = true;
            pauseButton.Enabled = true;            
            startNumTextBox.Enabled = false;
            endNumTextBox.Enabled = false;

            // See which numbers are factors and append them to the numbers text box
            for (int i = first; i < last; i++)
            {
                // Make the progress bar show the percent we've completed
                progressBar1.Value = i;

                if (IsPrime(i))
                    numbersTextBox.AppendText(i + "\n");
            
                // Leave the loop when the Cancel button has been pressed
                // (This won't actually work because the UI thread is tied-up.)
                if (cancelJob) 
                    break;
            }

            // Reset the form
            startNumTextBox.Enabled = true;
            endNumTextBox.Enabled = true;
            progressBar1.Visible = false;
            cancelButton.Enabled = false;
            pauseButton.Enabled = false;
        }

		// Return true if the given number is prime, false otherwise
        private bool IsPrime(int num)
        {
			if (num < 2)
				return false;
				
            // Look for a number that evenly divides the num
            for (int i = 2; i < num / 2; i++)
                if (num % i == 0)
                    return false;

            // No divisors means the number is prime
            return true;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            // Pause or resume the current job 
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // We'll try to cancel the job, but it's not going to work because the
            // UI thread is busy running the for loop!
            cancelJob = true;
        }
    }
}
