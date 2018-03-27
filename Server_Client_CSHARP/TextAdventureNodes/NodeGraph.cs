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
                n.PaintNode(pe);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        private bool LMB_DOWN = false;
        private bool RMB_DOWN = false;
        private bool MMB_DOWN = false;
        private Point mousePos;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
                LMB_DOWN = true;
            if (e.Button.Equals(MouseButtons.Right))
                RMB_DOWN = true;
            if (e.Button.Equals(MouseButtons.Middle))
                MMB_DOWN = true;

            mousePos = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
                LMB_DOWN = false;
            if (e.Button.Equals(MouseButtons.Right))
                RMB_DOWN = false;
            if (e.Button.Equals(MouseButtons.Middle))
                MMB_DOWN = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point deltaPos = new Point(e.Location.X - mousePos.X, e.Location.Y - mousePos.Y);
            
            foreach (Node node in nodes)
            {
                if (LMB_DOWN && node.NodeRectangle.Contains(e.Location))
                {
                    node.SetPosition(node.NodeRectangle.X + deltaPos.X, node.NodeRectangle.Y + deltaPos.Y);

                    foreach (Node otherNode in nodes)
                    {
                        if (otherNode != node)
                        {
                            // Check if the nodes are colliding
                            if (!(node.NodeRectangle.Left > otherNode.NodeRectangle.Right || node.NodeRectangle.Right < otherNode.NodeRectangle.Left ||
                                node.NodeRectangle.Top > otherNode.NodeRectangle.Bottom || node.NodeRectangle.Bottom < otherNode.NodeRectangle.Top))
                            {
                                // If they are colliding then separate according to the smallest overlap
                                int leftOverlap = Math.Abs(node.NodeRectangle.Left - otherNode.NodeRectangle.Right);
                                int rightOverlap = Math.Abs(node.NodeRectangle.Right - otherNode.NodeRectangle.Left);
                                int topOverlap = Math.Abs(node.NodeRectangle.Top - otherNode.NodeRectangle.Bottom);
                                int bottomOverlap = Math.Abs(node.NodeRectangle.Bottom - otherNode.NodeRectangle.Top);

                                // Left hand of the node being moved is 
                                if (leftOverlap < rightOverlap && leftOverlap < topOverlap && leftOverlap < bottomOverlap)
                                {
                                    node.SetPosition(otherNode.NodeRectangle.Right, node.NodeRectangle.Y);
                                }
                                else if (rightOverlap < leftOverlap && rightOverlap < topOverlap && rightOverlap < bottomOverlap)
                                {
                                    node.SetPosition(otherNode.NodeRectangle.Left - node.NodeRectangle.Width, node.NodeRectangle.Y);
                                }
                                else if (topOverlap < leftOverlap && topOverlap < rightOverlap && topOverlap < bottomOverlap)
                                {
                                    node.SetPosition(node.NodeRectangle.X, otherNode.NodeRectangle.Bottom);
                                }
                                else if (bottomOverlap < leftOverlap && bottomOverlap < rightOverlap && bottomOverlap < topOverlap)
                                {
                                    node.SetPosition(node.NodeRectangle.X, otherNode.NodeRectangle.Top - node.NodeRectangle.Height);
                                }
                            }

                        }
                    }
                }

                if (MMB_DOWN)
                {
                    node.SetPosition(node.NodeRectangle.X + deltaPos.X, node.NodeRectangle.Y + deltaPos.Y);
                }
            }

            mousePos = e.Location;
            Invalidate();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            AddNode(new Node("Node " + (nodes.Count + 1), e.Location.X, e.Location.Y));

            foreach (Node node in nodes)
            {
                if (node.NodeRectangle.Contains(e.Location))
                {
                    return;
                }
                else
                {
                    // Create a new node with no connections if double clicking on empty space
                    
                }
            }
        }
    }
}
