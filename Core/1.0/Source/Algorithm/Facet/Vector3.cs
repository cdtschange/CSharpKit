using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Algorithm.Facet
{
    public class Vector3 : Vector
    {
        public double X { get { return this.Values[0]; } }
        public double Y { get { return this.Values[1]; } }
        public double Z { get { return this.Values[2]; } }

        public Vector3(double x, double y, double z)
            : base(new double[] { x, y, z })
        {
        }

        public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
        {
            return
            (
                new Vector3(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X));
        }
        public Vector3 CrossProduct(Vector3 other)
        {
            return CrossProduct(this, other);
        }

        public static double MixedProduct(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            return DotProduct(CrossProduct(v1, v2), v3);
        }

        /// <summary>
        /// Rotates a Vector3 around the X axis
        public static Vector3 RotateWithX(Vector3 v1, double degree)
        {
            double x = v1.X;
            double y = (v1.Y * Math.Cos(degree)) - (v1.Z * Math.Sin(degree));
            double z = (v1.Y * Math.Sin(degree)) + (v1.Z * Math.Cos(degree));
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Rotates a Vector3 around the X axis
        public void RotateWithX(double degree)
        {
            this.Values = RotateWithX(this, degree).Values;
        }

        /// <summary>
        /// Rotates a Vector3 around the Y axis
        public static Vector3 RotateWithY(Vector3 v1, double degree)
        {
            double x = (v1.Z * Math.Sin(degree)) + (v1.X * Math.Cos(degree));
            double y = v1.Y;
            double z = (v1.Z * Math.Cos(degree)) - (v1.X * Math.Sin(degree));
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Rotates the Vector3 around the Y axis
        public void RotateWithY(double degree)
        {
            this.Values = RotateWithY(this, degree).Values;
        }

        /// <summary>
        /// Rotates a Vector3 around the Z axis
        public static Vector3 RotateWithZ(Vector3 v1, double degree)
        {
            double x = (v1.X * Math.Cos(degree)) - (v1.Y * Math.Sin(degree));
            double y = (v1.X * Math.Sin(degree)) + (v1.Y * Math.Cos(degree));
            double z = v1.Z;
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Rotates a Vector3 around the Z axis
        public void RotateWithZ(double degree)
        {
            this.Values = RotateWithZ(this, degree).Values;
        }
    }

    public static partial class VectorExtends
    {
        public static Vector3 ToVector3(this Vector v)
        {
            if (v.Values.Length != 3)
            {
                return null;
            }
            return new Vector3(v.Values[0], v.Values[1], v.Values[2]);
        }
    }

}
