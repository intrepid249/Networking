using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TextAdventureNodes
{
    // http://etodd.io/2014/05/16/the-poor-mans-dialogue-tree/
    // https://www.youtube.com/watch?v=e-VZFC2TLCU
    public partial class NodeGraph : Control
    {
        List<Node> nodes = new List<Node>();

        public NodeGraph()
        {
            InitializeComponent();
        }

        public void AddNode()
        {

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(new HatchBrush(HatchStyle.LargeGrid, Color.Gray, Color.DarkGray), ClientRectangle);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }
    }
}
