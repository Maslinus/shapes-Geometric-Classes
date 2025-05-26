using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometric_shapes
{
    public class Trapeze : QGon
    {
        public Trapeze(Point2D[] p) : base(p) { }
        public override double Square()
        {
            double k1 = (p[2].GetX(1) - p[0].GetX(1)) / (p[2].GetX(0) - p[0].GetX(0));
            double k2 = (p[3].GetX(1) - p[1].GetX(1)) / (p[3].GetX(0) - p[1].GetX(0));

            if (k1 == double.PositiveInfinity || k2 == double.PositiveInfinity || k1 == double.NaN || k2 == double.NaN)
            {
                return new NGon(p).Square();
            }

            double phi = Math.Atan((k2 - k1) / (1 + k1 * k2));
            double a = Math.Sqrt(Math.Pow(p[2].GetX(0) - p[0].GetX(0), 2) + Math.Pow(p[2].GetX(1) - p[0].GetX(1), 2));
            double b = Math.Sqrt(Math.Pow(p[3].GetX(0) - p[1].GetX(0), 2) + Math.Pow(p[3].GetX(1) - p[1].GetX(1), 2));

            return 0.5 * a * b * Math.Abs(Math.Sin(phi));
        }

        public override IShape Shift(Point2D a)
        {
            Point2D[] res = new Point2D[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = new Point2D(new double[] { p[i].GetX(0) + a.GetX(0), p[i].GetX(1) + a.GetX(1) });
            }
            return new Trapeze(res);
        }

        public override IShape Rot(double phi)
        {
            Point2D[] res = new Point2D[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = p[i].Rot(phi);
            }
            return new Trapeze(res);
        }

        public override IShape SymAxis(int i)
        {
            Point2D[] res = new Point2D[n];
            for (int j = 0; j < n; j++)
            {
                res[j] = (Point2D)p[j].SymAxis(i);
            }
            return new Trapeze(res);
        }
    }
}
