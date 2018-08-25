using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ctlBreakoutLib
{
    abstract class Figure
    {
        public Point Position { get; set; }
        public Point Offset { get; set; }
        public Size Size { get; set; }
        public Brush Brush { get; set; }
        public Object Caller { get; set; }
        public Figure (Object o, Point p, Size s, Color c)
        {
            Caller = o;
            Position = p;
            Size = s;
            Brush = new SolidBrush(c);
            Offset = new Point(0, 0);
        }
        public abstract void Move(int xOffset, int yOffset);
        public abstract void Draw(Graphics g);
    }
}
