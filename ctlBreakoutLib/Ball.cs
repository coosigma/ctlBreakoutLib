using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctlBreakoutLib
{
    class Ball : Figure
    {
        public Ball(Object o, Point p, Size s, Color c) : base(o, p, s, c)
        {
        }
        public override void Draw(Graphics g)
        {
            g.FillEllipse(this.Brush, new Rectangle(this.Position, this.Size));
        }
        public override void Move(int xOffset=0, int yOffset=0)
        {
            Offset = new Point(0, 2);
            int newX = Position.X + Offset.X + xOffset;
            int newY = Position.Y + Offset.Y + yOffset;
            this.Position = new Point(newX, newY);
        }
        public void Collision(Figure f)
        {

        }
    }
}
