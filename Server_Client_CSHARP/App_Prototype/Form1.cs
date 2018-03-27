using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextAdventureNodes;

namespace App_Prototype
{
    public partial class frmApplication : Form
    {
        public frmApplication()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ngGraph.AddNode(new Node("Node 1"));

            Node node2 = new Node("Node 2", "This is a bit of a longer description than the standard default one. I can\'t even remember what the " +
                "default one is");
            node2.NodeRectangle = new Rectangle(100, 100, 200, 100);
            ngGraph.AddNode(node2);
        }
    }
}
