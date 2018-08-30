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
        public Plate(Object o, Vector p, Size s, Color c) : base(o, p, s, c)
        {
        }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(this.Brush, new Rectangle(this.Position.ToPoint(), this.Size));
        }
        public void Move(int xOffset, int yOffset)
        {
            ctlBreakout c = Caller as ctlBreakout;
            // X axis
            double newX = Position.x + xOffset;
            Offset.x = xOffset;
            if (newX < 0)
                newX = 0;
            if (newX + this.Size.Width > c.PlayGround.Width)
                newX = c.PlayGround.Width - this.Size.Width;
            // Y axis
            double newY = Position.y + yOffset;
            Offset.y = yOffset;
            if (newY < 400)
                newY = 400;
            if (newY + this.Size.Height > c.PlayGround.Height)
                newY = c.PlayGround.Height - this.Size.Height;

            this.Position.x = newX;
            this.Position.y = newY;
        }
        public override void Update()
        {
            Offset.x = 0;
            Offset.y = 0;
        }
    }
}
