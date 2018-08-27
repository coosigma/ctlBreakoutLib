using System;
using System.Drawing;

namespace ctlBreakoutLib
{
    public class Vector
    {
        public double x { get; set; }
        public double y { get; set; }
        public Vector() { }
        public Vector (double X, double Y)
        {
            x = X;
            y = Y;
        }
        public static Vector operator +(Vector l1, Vector l2)
        {
            Vector result = new Vector();
            result.x = l1.x + l2.x;
            result.y = l1.y + l2.y;
            return result;
        }
        public static Vector operator -(Vector l1, Vector l2)
        {
            Vector result = new Vector();
            result.x = l1.x - l2.x;
            result.y = l1.y - l2.y;
            return result;
        }
        public static Vector operator -(Vector l1)
        {
            Vector result = new Vector();
            result.x = -l1.x;
            result.y = -l1.y;
            return result;
        }
        public static double operator *(Vector l1, Vector l2)
        {
            double result;
            result = l1.x * l2.x + l1.y * l2.y;
            return result;
        }
        public static Vector operator *(double l1, Vector l2)
        {
            Vector result = new Vector();
            result.x = l1 * l2.x;
            result.y = l1 * l2.y;
            return result;
        }
        public static Vector operator *(Vector l1, double l2)
        {
            return l2 * l1;
        }
        public static Vector operator /(Vector l1, double l2)
        {
            return (1/l2) * l1;
        }
        public bool IsLeft(Vector a, Vector b)
        {
            return (b.x - a.x) * (y - a.y) - (b.y - a.y) * (x - a.x) < 0;
        }
        public double GetLen()
        {
            return System.Math.Sqrt(x * x + y * y);
        }
        public Vector GetUnit()
        {
            Vector result = new Vector();
            double len = GetLen();
            if (len == 0) // divide 0 solution
                len = 0.0000001;
            result.x = x / len;
            result.y = y / len;
            return result;
        }
        public Vector GetNormal(bool left)
        {
            Vector result = new Vector();
            if (left)
            {
                result.x = y;
                result.y = -x;
            } else
            {
                result.x = -y;
                result.y = x;
            }
            return result;
        }
        public Vector GetProject(Vector v)
        {
            Vector u = GetUnit();
            return (v * u) * u;
        }
        public Point ToPoint()
        {
            return new Point(Convert.ToInt32(Math.Round(x,0)), Convert.ToInt32(Math.Round(y, 0)));
        }
    }
}