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
using System.Drawing.Text;

namespace TextAdventureNodes
{
    // http://etodd.io/2014/05/16/the-poor-mans-dialogue-tree/
    // https://www.youtube.com/watch?v=e-VZFC2TLCU
    public partial class NodeGraph : Control
    {
        private List<Node> nodes = new List<Node>();

        public NodeGraph()
        {
            InitializeComponent();
        }

        public void AddNode(Node n)
        {
            nodes.Add(n);

            Invalidate(); // Force a paint update of the NodeGraph control
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(new HatchBrush(HatchStyle.LargeGrid, Color.Gray, Color.DarkGray), ClientRectangle);

            foreach (Node n in nodes)
            {
                // Background rectangle
                pe.Graphics.FillRectangle(new SolidBrush(Color.BlueViolet), new Rectangle(n.NodeRectangle.X, n.NodeRectangle.Y,
                    n.NodeRectangle.Width, n.NodeRectangle.Height));
                // Header rectangle
                pe.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), new Rectangle(n.NodeRectangle.X, n.NodeRectangle.Y,
                    n.NodeRectangle.Width, n.TitleFont.Height + 10));

                
                SolidBrush TitleBrush = new SolidBrush(Color.White);
                pe.Graphics.DrawString(n.Title, n.TitleFont, TitleBrush, n.NodeRectangle.X + 5, n.NodeRectangle.Y + 5);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        private bool mouseDown = false;
        private Point mousePos;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mouseDown = true;
            mousePos = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            mouseDown = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            foreach (Node node in nodes)
            {
                if (mouseDown && node.NodeRectangle.Contains(e.Location))
                {
                    Point deltaPos = new Point(e.Location.X - mousePos.X, e.Location.Y - mousePos.Y);

                    node.NodeRectangle = new Rectangle(node.NodeRectangle.X + deltaPos.X, node.NodeRectangle.Y + deltaPos.Y, 
                        node.NodeRectangle.Width, node.NodeRectangle.Height);

                    Invalidate();
                }
            }
            mousePos = e.Location;
        }
    }
}
