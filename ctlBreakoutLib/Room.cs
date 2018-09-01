using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctlBreakoutLib
{
    // Room class. It has 4 walls and 3 of them rebounding the ball.(except "gound")
    class Room : Figure
    {
        public Room(Object o, Vector p, Size s, Color c) : base(o, p, s, c)
        {
        }
        public override void Draw(Graphics g)
        {
        }
        public override void Update()
        {
        }

    }
}
