using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TextAdventureNodes
{
    public class Node
    {
        private const int DEFAULT_WIDTH = 200;
        private const int DEFAULT_HEIGHT = 100;
        public System.Drawing.Font TitleFont = new System.Drawing.Font("Arial", 12);

        public Node(string _title)
        {
            Title = _title;
            Description = "Edit this node to change the description";
            NodeRectangle = new Rectangle(0, 0, DEFAULT_WIDTH, DEFAULT_HEIGHT);
        }
        public Node(string _title, string _description)
        {
            Title = _title;
            Description = _description;
            NodeRectangle = new Rectangle(0, 0, DEFAULT_WIDTH, DEFAULT_HEIGHT);
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public Rectangle NodeRectangle { get; set; }
    }
}
