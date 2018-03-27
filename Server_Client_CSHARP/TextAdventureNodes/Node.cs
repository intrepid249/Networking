using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TextAdventureNodes
{
    public class Node
    {
        private const int DEFAULT_NODE_WIDTH = 200;
        private const int DEFAULT_NODE_HEIGHT = 100;

        // Variables for rendering the node
        private const int TextPadding = 5;
        private System.Drawing.Font TitleFont = new System.Drawing.Font("Consolas", 14, FontStyle.Bold);
        private System.Drawing.Font DescriptionFont = new System.Drawing.Font("Arial", 9);


        public Node(string _title)
        {
            Title = _title;
            Description = "Edit this node to change the description";
            NodeRectangle = new Rectangle(0, 0, DEFAULT_NODE_WIDTH, DEFAULT_NODE_HEIGHT);
        }
        public Node(string _title, int _x, int _y)
        {
            Title = _title;
            Description = "Edit this node to change the description";
            NodeRectangle = new Rectangle(_x, _y, DEFAULT_NODE_WIDTH, DEFAULT_NODE_HEIGHT);
        }
        public Node(string _title, string _description)
        {
            Title = _title;
            Description = _description;
            NodeRectangle = new Rectangle(0, 0, DEFAULT_NODE_WIDTH, DEFAULT_NODE_HEIGHT);
        }

        public void SetPosition(int _x, int _y)
        {
            NodeRectangle = new Rectangle(_x, _y, NodeRectangle.Width, NodeRectangle.Height);
        }

        public void PaintNode(PaintEventArgs pe)
        {
            // Background rectangle
            pe.Graphics.FillRectangle(new SolidBrush(Color.BlueViolet), new Rectangle(NodeRectangle.X, NodeRectangle.Y,
                NodeRectangle.Width, NodeRectangle.Height));
            // Header rectangle
            pe.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), new Rectangle(NodeRectangle.X, NodeRectangle.Y,
                NodeRectangle.Width, TitleFont.Height + TextPadding * 2));

            // Display title text
            SolidBrush TitleBrush = new SolidBrush(Color.White);
            pe.Graphics.DrawString(Title, TitleFont, TitleBrush, NodeRectangle.X + TextPadding, NodeRectangle.Y + TextPadding);

            // Display description text;
            SolidBrush DescriptionBrush = new SolidBrush(Color.DarkGray);
            pe.Graphics.DrawString(Description, DescriptionFont, DescriptionBrush, 
                new RectangleF(NodeRectangle.X + TextPadding, NodeRectangle.Y + TitleFont.Height + TextPadding * 3, NodeRectangle.Width, NodeRectangle.Height));
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public Rectangle NodeRectangle { get; set; }
    }
}
