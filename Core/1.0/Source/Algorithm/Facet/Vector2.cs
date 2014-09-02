using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Algorithm.Facet
{
    public class Vector2 : Vector
    {
        public double X { get { return this.Values[0]; } }
        public double Y { get { return this.Values[1]; } }

        public Vector2(double x, double y)
            : base(new double[] { x, y })
        {
        }

        /// <summary>
        /// 叉积
        public static double CrossProduct(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }
        /// <summary>
        /// 叉积
        /// </summary>
        public double CrossProduct(Vector2 other)
        {
            return CrossProduct(this, other);
        }

        //逆時針 p: (x,y) --------> p': ( x*cos(d)-y*sin(d) , x*sin(d)+y*cos(d) )
        public static Vector2 Rotate(Vector2 v1, double degree)
        {
            double x = (v1.X * Math.Cos(degree)) - (v1.Y * Math.Sin(degree));
            double y = (v1.X * Math.Sin(degree)) + (v1.Y * Math.Cos(degree));
            return new Vector2(x, y);
        }
        public Vector2 Rotate(double degree)
        {
            return Rotate(this, degree);
        }

        public static double RotateAngle(Vector2 v1, Vector2 v2)
        {
            double dot = Normalize(v1).DotProduct(Normalize(v2));
            if (Math.Abs(Math.Round(dot - 1, 6)) < EqualityTolerence)
            {
                return 0;
            }
            else if (Math.Abs(Math.Round(dot + 1, 6)) < EqualityTolerence)
            {
                return Math.PI;
            }
            double ang = Angle(v1, v2);
            double cross = CrossProduct(v1, v2);
            if (cross < 0)
            {
                ang = 2 * Math.PI - ang;
            }
            return ang;
        }
        public double RotateAngle(Vector2 other)
        {
            return RotateAngle(this, other);
        }


    }

    public static partial class VectorExtends
    {
        public static Vector2 ToVector2(this Vector v)
        {
            if (v.Values.Length != 2)
            {
                return null;
            }
            return new Vector2(v.Values[0], v.Values[1]);
        }
    }
}
