﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctlBreakoutLib
{
    class Brick : Figure
    {
        enum Status { Exists, Vacancy }
        Status status { get; set; }
        public Brick (Object o, Point p, Size s, Color c) : base(o, p, s, c)
        {
            status = Status.Exists;
        }
        public override void Draw(Graphics g)
        {
            g.FillRectangle(this.Brush, new Rectangle(this.Position, this.Size));
        }
        public override void Move(int xOffset, int yOffset)
        {
            // Nothing to do in this version
        }
    }
}
