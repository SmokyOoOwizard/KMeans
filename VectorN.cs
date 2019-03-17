using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class VectorN
    {
        float[] Axis = new float[2];
        public int Dimension
        {
            get
            {
                if (Axis == null)
                {
                    return 0;
                }
                return Axis.Length;
            }
        }

        public VectorN(int dimension)
        {
            dimension = dimension > 0 ? dimension : 2;
            Axis = new float[dimension];
        }
        public VectorN(params float[] axis)
        {
            if (axis != null && axis.Length > 0)
            {
                Axis = new float[axis.Length];
                Array.Copy(axis, Axis, Dimension);
            }
            else
            {
                Axis = new float[2];
            }
        }
        public VectorN(int dimension, float[] axis)
        {
            dimension = dimension > 0 ? dimension : 2;
            Axis = new float[dimension];
            if (axis != null)
            {
                Array.Copy(axis, Axis, Dimension);
            }
        }
        public VectorN(VectorN vector)
        {
            Axis = new float[vector.Dimension];
            if (vector.Axis != null)
            {
                Array.Copy(vector.Axis, Axis, Dimension);
            }

        }


        public static float Distance(VectorN a, VectorN b)
        {
            return (float)Math.Sqrt(DistanceSqrt(a, b));
        }
        public static float DistanceSqrt(VectorN a, VectorN b)
        {
            float r = 0;
            a -= b;
            foreach (var item in a.Axis)
            {
                r += (float)Math.Pow(item, 2f);
            }
            return r;
        }

        public float this[int key]
        {
            get
            {
                return Axis[key];
            }
            set
            {
                Axis[key] = value;
            }
        }
        public static VectorN operator +(VectorN a, VectorN b)
        {

            a.Axis = a.Axis == null ? new float[2] : a.Axis;
            b.Axis = b.Axis == null ? new float[2] : b.Axis;
            /*
            int m = a.N >= b.N ? a.N : b.N;

            float[] q = new float[m];

            for (int i = 0; i < m; i++)
            {
                q[i] = (i < a.N ? a[i] : 0) + (i < b.N ? b[i] : 0);
            }
            */
            return new VectorN(a.Axis[0] + b.Axis[0], a.Axis[1] + b.Axis[1]);
        }
        public static VectorN operator -(VectorN a, VectorN b)
        {

            a.Axis = a.Axis == null ? new float[2] : a.Axis;
            b.Axis = b.Axis == null ? new float[2] : b.Axis;
            /*
            int m = a.N >= b.N ? a.N : b.N;

            float[] q = new float[m];

            for (int i = 0; i < m; i++)
            {
                q[i] = (i < a.N ? a[i] : 0) - (i < b.N ? b[i] : 0);
            }
            */
            return new VectorN(a.Axis[0] - b.Axis[0], a.Axis[1] - b.Axis[1]);
        }
        public static VectorN operator /(VectorN a, int b)
        {
            /*
            a.Axis = a.Axis == null ? new float[1] : a.Axis;
            for (int i = 0; i < a.N; i++)
            {
                a[i] /= b;
            }
            
            return a;
            */
            return new VectorN(a.Axis[0] / b, a.Axis[1] / b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            VectorN n = (VectorN)obj;
            if (obj.GetType() != typeof(VectorN))
            {
                return false;
            }
            if (Dimension == n.Dimension)
            {
                for (int i = 0; i < Dimension; i++)
                {
                    if (Axis[i] != n[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int i = 0;
            i += this.Dimension;
            if (i > 0)
            {
                i *= (int)this[0];
            }
            return i;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            foreach (var item in Axis)
            {
                sb.Append(item + ", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(")");
            return sb.ToString();
        }
    }
}
