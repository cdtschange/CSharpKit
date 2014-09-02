using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Algorithm.Facet
{
    public class Vector : IComparable
    {
        public int Id { get; set; }
        public double[] Values { get; set; }
        public int Dimension { get { return Values.Length; } }

        public Vector(int length)
        {
            this.Values = new double[length];
        }
        public Vector(double[] value)
        {
            this.Values = value;
        }

        public double this[int index]
        {
            get
            {
                if (index < this.Dimension)
                {
                    return Values[index];
                }
                return 0;
            }
            set
            {
                if (index < this.Dimension)
                {
                    Values[index] = value;
                }
            }
        }

        public double SqrMagnitude
        {
            get
            {
                return this.Values.Sum(v => v * v);
            }
        }
        public double Magnitude
        {
            get
            {
                return Math.Sqrt(this.SqrMagnitude);
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Magnitude need to equal greater than 0");
                }
                this.Values = (this * value / this.Magnitude).Values;
            }
        }
        public static Vector Normalize(Vector v1)
        {
            if (v1.Magnitude == 0)
            {
                throw new InvalidOperationException("Magnitude of vector is 0!");
            }
            return v1 / v1.Magnitude;
        }
        public void Normalize()
        {
            Vector v = Normalize(this);
            this.Values = v.Values;
        }

        public static Vector Power(Vector v1, double power)
        {
            Vector v = new Vector(v1.Dimension);
            for (int i = 0; i < v1.Dimension; i++)
            {
                v[i] = Math.Pow(v1[i], power);
            }
            return v;
        }
        public void Power(double power)
        {
            Vector v = Power(this, power);
            this.Values = v.Values;
        }

        public static double DotProduct(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new InvalidOperationException("Dimension of two vectors are not equal!");
            }
            double result = 0;
            for (int i = 0; i < v1.Dimension; i++)
            {
                result += v1[i] * v2[i];
            }
            return result;
        }
        public double DotProduct(Vector other)
        {
            return DotProduct(this, other);
        }
        public static double Distance(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new InvalidOperationException("Dimension of two vectors are not equal!");
            }
            double dis = 0;
            for (int i = 0; i < v1.Dimension; i++)
            {
                dis += (v1[i] - v2[i]) * (v1[i] - v2[i]);
            }
            dis = Math.Sqrt(dis);
            return dis;
        }
        public double Distance(Vector other)
        {
            return Distance(this, other);
        }
        public static double Angle(Vector v1, Vector v2)
        {
            return Math.Acos(Normalize(v1).DotProduct(Normalize(v2)));
        }
        public double Angle(Vector other)
        {
            return Angle(this, other);
        }

        public static Vector Max(Vector v1, Vector v2)
        {
            return v1 >= v2 ? v1 : v2;
        }
        public static Vector Min(Vector v1, Vector v2)
        {
            return v1 <= v2 ? v1 : v2;
        }

        /// <summary>
        /// 取两点线段中间的点
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static Vector Interpolate(Vector v1, Vector v2, double delta)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new InvalidOperationException("Dimension of two vectors are not equal!");
            }
            Vector v = new Vector(v1.Dimension);
            for (int i = 0; i < v1.Dimension; i++)
            {
                v[i] = v1[i] * (1 - delta) + v2[i] * delta;
            }
            return v;
        }
        public Vector Interpolate(Vector other, double delta)
        {
            return Interpolate(this, other, delta);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            if (this.Dimension == 0)
            {
                return "()";
            }
            string str = "";
            str += "(";
            for (int i = 0; i < this.Dimension; i++)
            {
                str += this[i] + ",";
            }
            str = str.Substring(0, str.Length - 1);
            str += ")";

            return str;
        }

        public override bool Equals(object other)
        {
            // Check object other is a Vector3 object
            if (other is Vector)
            {
                // Convert object to Vector3
                Vector otherVector = (Vector)other;

                // Check for equality
                return otherVector == this;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is Vector)
            {
                if (this < (Vector)obj)
                {
                    return -1;
                }
                else if (this > (Vector)obj)
                {
                    return 1;
                }
                return 0;
            }
            else
            {
                // Error condition: other is not a Vector3 object
                throw new ArgumentException("Compare type is not vector");
            }
        }

        public static bool IsUnitVector(Vector v1)
        {
            return Math.Abs(v1.Magnitude - 1) <= EqualityTolerence;
        }
        public bool IsUnitVector()
        {
            return IsUnitVector(this);
        }
        /// <summary>
        /// 是否呈钝角
        /// </summary>
        public static bool IsBackFace(Vector normal, Vector lineOfSight)
        {
            return Vector.DotProduct(normal, lineOfSight) < 0;
        }
        /// <summary>
        /// 是否呈钝角
        /// </summary>
        public bool IsBackFace(Vector lineOfSight)
        {
            return IsBackFace(this, lineOfSight);
        }
        /// <summary>
        /// 是否垂直
        /// </summary>
        public static bool IsPerpendicular(Vector v1, Vector v2)
        {
            return Vector.DotProduct(v1, v2) == 0;
        }
        /// <summary>
        /// 是否垂直
        /// </summary>
        public bool IsPerpendicular(Vector other)
        {
            return IsPerpendicular(this, other);
        }

        public static Vector GetAxis(int dim, int index)
        {
            Vector v = new Vector(dim);
            for (int i = 0; i < dim; i++)
            {
                v[i] = 0;
            }
            v[index] = 1;
            return v;
        }
        public Vector GetAxis(int index)
        {
            return GetAxis(this.Dimension, index);
        }


        #region Opertors
        public static Vector operator +(Vector v1, Vector v2)
        {

            if (v1.Dimension != v2.Dimension)
            {
                throw new InvalidOperationException("Dimension of two vectors are not equal!");
            }
            Vector v = new Vector(v1.Dimension);
            for (int i = 0; i < v1.Dimension; i++)
            {
                v[i] = v1[i] + v2[i];
            }

            return v;
        }
        public static Vector operator -(Vector v1, Vector v2)
        {

            if (v1.Dimension != v2.Dimension)
            {
                throw new InvalidOperationException("Dimension of two vectors are not equal!");
            }
            Vector v = new Vector(v1.Dimension);
            for (int i = 0; i < v1.Dimension; i++)
            {
                v[i] = v1[i] - v2[i];
            }

            return v;
        }
        public static Vector operator *(Vector v1, double s2)
        {
            Vector v = new Vector(v1.Dimension);
            for (int i = 0; i < v1.Dimension; i++)
            {
                v[i] = v1[i] * s2;
            }

            return v;
        }
        public static Vector operator *(double s1, Vector v2)
        {
            return v2 * s1;
        }

        public static Vector operator /(Vector v1, double s2)
        {
            Vector v = new Vector(v1.Dimension);
            for (int i = 0; i < v1.Dimension; i++)
            {
                v[i] = v1[i] / s2;
            }

            return v;
        }
        public static Vector operator -(Vector v1)
        {
            Vector v = new Vector(v1.Dimension);
            for (int i = 0; i < v1.Dimension; i++)
            {
                v[i] = -v1[i];
            }

            return v;
        }
        public static Vector operator +(Vector v1)
        {
            Vector v = new Vector(v1.Dimension);
            for (int i = 0; i < v1.Dimension; i++)
            {
                v[i] = +v1[i];
            }

            return v;
        }
        public static bool operator <(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new InvalidOperationException("Dimension of two vectors are not equal!");
            }

            return v1.SqrMagnitude < v2.SqrMagnitude;
        }
        public static bool operator <=(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new InvalidOperationException("Dimension of two vectors are not equal!");
            }

            return v1.SqrMagnitude <= v2.SqrMagnitude;
        }
        public static bool operator >(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new InvalidOperationException("Dimension of two vectors are not equal!");
            }

            return v1.SqrMagnitude > v2.SqrMagnitude;
        }
        public static bool operator >=(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new InvalidOperationException("Dimension of two vectors are not equal!");
            }

            return v1.SqrMagnitude >= v2.SqrMagnitude;
        }
        public static bool operator ==(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new InvalidOperationException("Dimension of two vectors are not equal!");
            }
            bool result = true;
            for (int i = 0; i < v1.Dimension; i++)
            {
                result &= (Math.Abs(v1[i] - v2[i]) <= EqualityTolerence);
            }
            return result;
        }
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }


        #endregion

        #region Constants

        /// <summary>
        /// The tolerence used when determining the equality of two vectors 
        /// </summary>
        public const double EqualityTolerence = Double.Epsilon;

        #endregion


    }
}
