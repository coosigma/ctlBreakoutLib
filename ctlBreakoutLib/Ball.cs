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
            Vector u = Offset.GetUnit();
            if (Offset.GetLen() > 3) // Limit max speed
                Offset = u * 3;
            Position = Position + Offset;
        }
        public bool Collision(Figure f)
        {
            Vector[] node = new Vector[8];
            node[0] = new Vector(f.Position.x, f.Position.y);
            node[1] = new Vector(f.Position.x+f.Size.Width, f.Position.y);
            node[2] = new Vector(f.Position.x, f.Position.y+f.Size.Height);
            node[3] = new Vector(f.Position.x + f.Size.Width, f.Position.y + f.Size.Height);
            node[4] = node[0];
            node[5] = node[2];
            node[6] = node[1];
            node[7] = node[3];
            for (int i = 0; i < node.Length; i += 2)
            {
                Vector l = node[i];
                Vector u = node[i + 1];
                Vector iv = GetIntersectVector(l, u);
                double p_length = this.Size.Width*0.75 - iv.GetLen();
                if (p_length > 0)
                {       
                    Position += p_length * iv.GetUnit();
                    Offset = GetBounceVector(l, u, f.Offset);
                    return true;
                }
            }
            return false;
        }
        private Vector GetBounceVector(Vector l, Vector u, Vector friction)
        {
            Vector w = u - l;
            Vector pw = w.GetProject(Offset);
            Vector pn;
            if ((Position-l).IsLeft(l, u))
            {
                pn = w.GetNormal(true).GetProject(Offset);
            } else
            {
                pn = w.GetNormal(false).GetProject(Offset);
            }
            Vector fn = friction.GetUnit();
            return pw - pn + friction.GetUnit();
        }

        public Vector GetIntersectVector(Vector l, Vector u)
        {
            Vector w = u - l;
            Vector p1 = Position - l;
            if (p1 * w < 0)
                return p1;
            Vector p2 = Position - u;
            if (p2 * w > 0)
                return p2;
            Vector p3;
            if ((Position-l).IsLeft(l,u))
            {
                p3 = w.GetNormal(true).GetProject(p1);
            } else
            {
                p3 = w.GetNormal(false).GetProject(p1);
            }
            return p3;
        }

        internal bool CheckOutside(Size s)
        {
            if (Position.y >= s.Height - Size.Height)
                return true;
            return false;
        }
    }
}
