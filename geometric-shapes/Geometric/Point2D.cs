using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometric_shapes
{
    public class Point2D : Point1D
    {

        public Point2D() : base(2) { }
        public Point2D(double[] x) : base(2, x) { }

        public static Point2D Rot(Point2D a, double phi)
        {
            double[] res = new double[2];
            res[0] = a.x[0] * Math.Cos(phi) + a.x[1] * Math.Sin(phi);
            res[1] = -a.x[0] * Math.Sin(phi) + a.x[1] * Math.Cos(phi);
            return new Point2D(res);
        }

        public Point2D Rot(double phi)
        {
            double[] res = new double[2];
            res[0] = x[0] * Math.Cos(phi) - x[1] * Math.Sin(phi);
            res[1] = x[0] * Math.Sin(phi) + x[1] * Math.Cos(phi);
            return new Point2D(res);
        }

        public override Point1D SymAxis(int i)
        {
            double[] res = new double[2];
            for (int j = 0; j < dim; j++)
            {
                res[j] = x[j];
            }
            if (i < 0 || i >= 2)
            {
                throw new ArgumentException("Неверный номер оси.");
            }

            if (i == 0)
                res[1] = -res[1];
            else res[0] = -res[0];

            return new Point2D(res);
        }
    }
}
