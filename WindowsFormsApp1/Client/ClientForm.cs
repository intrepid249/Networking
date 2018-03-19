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

namespace Client
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
        }

        SimpleTcpClient client;
        private void ClientForm_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.Delimiter = 0x0D;
            client.StringEncoder = Encoding.ASCII;
            client.DataReceived += Client_DataReceived;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            try
            {
                client.Connect(txtHost.Text, Convert.ToInt32(txtPort.Text));
                txtStatus.Text += "Connected to " + txtHost.Text + ":" + txtPort.Text + " successfully!" + Environment.NewLine;
            } catch (System.Net.Sockets.SocketException ex)
            {
                txtStatus.Text += ex.Message + Environment.NewLine;
                btnConnect.Enabled = true;
            }
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                txtStatus.Text += e.MessageString + Environment.NewLine;
            });
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                client.WriteLineAndGetReply(txtMessage.Text, TimeSpan.FromSeconds(1));
            } catch (Exception ex)
            {
                txtStatus.Text += ex.Message + Environment.NewLine;
            }
            txtMessage.Text = "";
        }
    }
}
