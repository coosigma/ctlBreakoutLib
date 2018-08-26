using System;
using System.Drawing;

namespace ctlBreakoutLib
{
    abstract class Figure
    {
        public Vector Position { get; set; }
        public Vector Offset { get; set; }
        public Size Size { get; set; }
        public Brush Brush { get; set; }
        public Object Caller { get; set; }
        public Figure (Object o, Vector p, Size s, Color c)
        {
            Caller = o;
            Position = p;
            Size = s;
            Brush = new SolidBrush(c);
            Offset = new Vector(0d, 0d);
        }
        public abstract void Update();
        public abstract void Draw(Graphics g);
    }
}
