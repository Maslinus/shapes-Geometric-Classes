using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometric_shapes
{
    public class TGon : NGon
    {
        public TGon(Point2D[] p) : base(p) { }

        public override double Square()
        {
            double a = Math.Sqrt(Math.Pow(p[1].GetX(0) - p[0].GetX(0), 2) + Math.Pow(p[1].GetX(1) - p[0].GetX(1), 2));
            double b = Math.Sqrt(Math.Pow(p[2].GetX(0) - p[1].GetX(0), 2) + Math.Pow(p[2].GetX(1) - p[1].GetX(1), 2));
            double c = Math.Sqrt(Math.Pow(p[0].GetX(0) - p[2].GetX(0), 2) + Math.Pow(p[0].GetX(1) - p[2].GetX(1), 2));
            double pr = (a + b + c) / 2;
            return Math.Sqrt(pr * (pr - a) * (pr - b) * (pr - c));
        }

        public override IShape Shift(Point2D a)
        {
            Point2D[] res = new Point2D[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = new Point2D(new double[] { p[i].GetX(0) + a.GetX(0), p[i].GetX(1) + a.GetX(1) });
            }
            return new TGon(res);
        }

        public override IShape Rot(double phi)
        {
            Point2D[] res = new Point2D[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = p[i].Rot(phi);
            }
            return new TGon(res);
        }

        public override IShape SymAxis(int i)
        {
            Point2D[] res = new Point2D[n];
            for (int j = 0; j < n; j++)
            {
                res[j] = (Point2D)p[j].SymAxis(i);
            }
            return new TGon(res);
        }
    }
}
