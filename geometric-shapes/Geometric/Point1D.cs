using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometric_shapes
{
    public class Point1D
    {
        protected int dim;
        protected double[] x;

        public Point1D(int dim)
        {
            this.dim = dim;
            x = new double[dim];
        }

        public Point1D(int dim, double[] x)
        {
            if (dim != x.Length)
            {
                throw new ArgumentException("Размерность массива не совпадает с заданной размерностью пространства.");
            }
            this.dim = dim;
            this.x = x;
        }

        public int GetDim()
        {
            return dim;
        }

        public double[] GetX()
        {
            return x;
        }

        public double GetX(int i)
        {
            if (i < 0 || i >= dim)
            {
                throw new ArgumentException("Неверный индекс координаты.");
            }
            return x[i];
        }

        public void SetX(double[] x)
        {
            if (dim != x.Length)
            {
                throw new ArgumentException("Размерность массива не совпадает с заданной размерностью пространства.");
            }
            this.x = x;
        }

        public void SetX(double x, int i)
        {
            if (i < 0 || i >= dim)
            {
                throw new ArgumentException("Неверный индекс координаты.");
            }
            this.x[i] = x;
        }

        public double Abs()
        {
            double sum = 0;
            for (int i = 0; i < dim; i++)
            {
                sum += x[i] * x[i];
            }
            return Math.Sqrt(sum);
        }

        public static Point1D Add(Point1D a, Point1D b)
        {
            if (a.dim != b.dim)
            {
                throw new ArgumentException("Размерности точек не совпадают.");
            }
            double[] res = new double[a.dim];
            for (int i = 0; i < a.dim; i++)
            {
                res[i] = a.x[i] + b.x[i];
            }
            return new Point1D(a.dim, res);
        }

        public Point1D Add(Point1D b)
        {
            if (dim != b.dim)
            {
                throw new ArgumentException("Размерности точек не совпадают.");
            }
            double[] res = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                res[i] = x[i] + b.x[i];
            }
            return new Point1D(dim, res);
        }

        public static Point1D Sub(Point1D a, Point1D b)
        {
            if (a.dim != b.dim)
            {
                throw new ArgumentException("Размерности точек не совпадают.");
            }
            double[] res = new double[a.dim];
            for (int i = 0; i < a.dim; i++)
            {
                res[i] = a.x[i] - b.x[i];
            }
            return new Point1D(a.dim, res);
        }

        public Point1D Sub(Point1D b)
        {
            if (dim != b.dim)
            {
                throw new ArgumentException("Размерности точек не совпадают.");
            }
            double[] res = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                res[i] = x[i] - b.x[i];
            }
            return new Point1D(dim, res);
        }

        public static Point1D Mult(Point1D a, double r)
        {
            double[] res = new double[a.dim];
            for (int i = 0; i < a.dim; i++)
            {
                res[i] = a.x[i] * r;
            }
            return new Point1D(a.dim, res);
        }

        public Point1D Mult(double r)
        {
            double[] res = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                res[i] = x[i] * r;
            }
            return new Point1D(dim, res);
        }

        public static double Mult(Point1D a, Point1D b)
        {
            if (a.dim != b.dim)
            {
                throw new ArgumentException("Размерности точек не совпадают.");
            }
            double res = 0;
            for (int i = 0; i < a.dim; i++)
            {
                res += a.x[i] * b.x[i];
            }
            return res;
        }

        public double Mult(Point1D b)
        {
            if (dim != b.dim)
            {
                throw new ArgumentException("Размерности точек не совпадают.");
            }
            double res = 0;
            for (int i = 0; i < dim; i++)
            {
                res += x[i] * b.x[i];
            }
            return res;
        }

        public static Point1D SymAxis(Point1D a, int i)
        {
            double[] res = new double[a.dim];
            for (int j = 0; j < a.dim; j++)
            {
                res[j] = a.x[j];
            }

            if (i < 0 || i >= a.dim)
            {
                throw new ArgumentException("Неверный номер оси.");
            }

            res[i] = -res[i];

            return new Point1D(a.dim, res);
        }

        public virtual Point1D SymAxis(int i)
        {
            double[] res = new double[dim];
            for (int j = 0; j < dim; j++)
            {
                res[j] = x[j];
            }

            if (i < 0 || i >= dim)
            {
                throw new ArgumentException("Неверный номер оси.");
            }

            for (int k = 0; k < dim; k++)
            {
                if (k != i)
                {
                    res[k] = -res[k];
                }
            }

            return new Point1D(dim, res);
        }

        public override string ToString()
        {
            string res = "(" + x[0];
            for (int i = 1; i < dim; i++)
            {
                res += ", " + x[i];
            }
            res += ")";
            return res;
        }
    }
}
