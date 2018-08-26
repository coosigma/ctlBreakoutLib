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
        public Ball(Object o, Vector p, Size s, Color c) : base(o, p, s, c)
        {
        }
        public override void Draw(Graphics g)
        {
            g.FillEllipse(this.Brush, new Rectangle(this.Position.ToPoint(), this.Size));
        }
        public override void Update()
        {
            Position = Position + Offset;
        }
        public bool Collision(Figure f)
        {

            return false;
        }
    }
}
