using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace AsyncBox
{
    public partial class Form1 : Form
    {
        private int requestLimit = 10;
        private int currentRequestCount = 0;
         
        public Form1()
        {
            InitializeComponent();
            SetLabel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CanRequest();
        }

        private void SetLabel()
        {
            label1.Text = "Current: " + currentRequestCount;
        }

        private void AddLine(string line)
        {
            textBox1.AppendText(DateTime.UtcNow.ToString("ss.fff", CultureInfo.InvariantCulture) + " " + line + "\r\n");
        }
        
        private void CanRequest()
        {
            if (currentRequestCount < requestLimit)
            {
                Process();
            }
            else
            {
                button1.Enabled = false;
            }
        }


        private async void Process()
        {

            //Task pause = Task.Delay(10000);
            Task pause = Task.Factory.StartNew(() => {
                //AddLine("BrownBag");
                Thread.Sleep(2355);
                //Task.Delay(3000);
                throw new Exception("Oops");
            }); 
            currentRequestCount++;
            SetLabel();

            AddLine("Start processing: " + pause.Id);
            await pause;
            AddLine("Finished: " + pause.Id);

            currentRequestCount--;
            SetLabel();

            // We will never reach this part unless a request finishes. Therefore we can safely re-enable our button unconditionally.
            button1.Enabled = true;
        }

    }
}
