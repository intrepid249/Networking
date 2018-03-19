using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmServer : Form
    {
        public frmServer()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;
        private void frmServer_Load(object sender, EventArgs e)
        {
            btnStop.Enabled = false;

            server = new SimpleTcpServer();
            server.Delimiter = 0x00D; // Enter
            server.StringEncoder = System.Text.ASCIIEncoding.ASCII;
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                txtStatus.Text += e.MessageString + Environment.NewLine;
                e.ReplyLine(string.Format("You said: {0}", e.MessageString));
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            txtStatus.Text += "Server starting..." + Environment.NewLine;
            try
            {
                System.Net.IPAddress ip = IPAddress.Parse(txtHost.Text);
                server.Start(ip, Convert.ToInt32(txtPort.Text));
                txtStatus.Text += "Server started successfully!" + Environment.NewLine;
                btnStop.Enabled = true;
            } catch (Exception ex)
            {
                txtStatus.Text += ex.Message;
                btnStart.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
            {
                txtStatus.Text += "Stopping server..." + Environment.NewLine;
                server.Stop();

                PauseAndExit(2000);
            }
        }

        private async void PauseAndExit(int milliseconds)
        {
            await Task.Delay(milliseconds);
            Application.Exit();
        }
    }
}
