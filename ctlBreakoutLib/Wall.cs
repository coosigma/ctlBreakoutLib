﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctlBreakoutLib
{
    class Wall : Figure
    {
        public Wall(Object o, Point p, Size s, Color c) : base(o, p, s, c)
        {
        }
        public override void Draw(Graphics g)
        {
        }
        public override void Move(int xOffset, int yOffset)
        {
        }

    }
}
