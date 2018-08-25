using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ctlBreakoutLib
{
    class Plate : Figure
    {
        public Plate(Object o, Point p, Size s, Color c) : base(o, p, s, c)
        {
        }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(this.Brush, new Rectangle(this.Position, this.Size));
        }
        public override void Move(int xOffset, int yOffset)
        {
            ctlBreakout c = Caller as ctlBreakout;
            // X axis
            int newX = Position.X + xOffset;
            if (newX < 0)
                newX = 0;
            if (newX + this.Size.Width > c.Size.Width)
                newX = c.Size.Width - this.Size.Width;
            // Y axis
            int newY = Position.Y + yOffset;
            if (newY < 400)
                newY = 400;
            if (newY + this.Size.Height > c.Size.Height)
                newY = c.Size.Height - this.Size.Height;

            this.Position = new Point(newX, newY);
        }
    }
}
