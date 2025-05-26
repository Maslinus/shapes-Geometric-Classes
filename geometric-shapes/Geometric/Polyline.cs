using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace geometric_shapes
{
    public class Polyline : OpenFigure, IPolyPoint
    {
        private int n;
        private Point2D[] p;

        public Polyline(Point2D[] p)
        {
            n = p.Length;
            this.p = p;
        }

        public int GetN()
        {
            return n;
        }

        public Point2D[] GetP()
        {
            return p;
        }

        public Point2D GetP(int i)
        {
            if (i < 0 || i >= n)
            {
                throw new ArgumentException("Неверный индекс координаты.");
            }
            return p[i];
        }

        public void SetP(Point2D[] p)
        {
            this.p = p;
        }

        public void SetP(Point2D p, int i)
        {
            if (i < 0 || i >= n)
            {
                throw new ArgumentException("Неверный индекс координаты.");
            }
            this.p[i] = p;
        }

        public override double Length()
        {
            double res = 0;
            for (int i = 0; i < n - 1; i++)
            {
                double dx = p[i].GetX(0) - p[i + 1].GetX(0);
                double dy = p[i].GetX(1) - p[i + 1].GetX(1);
                res += Math.Sqrt(dx * dx + dy * dy);
            }
            return res;
        }

        public override double Square()
        {
            return 0;
        }

        public override IShape Shift(Point2D a)
        {
            Point2D[] res = new Point2D[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = new Point2D(new double[] { p[i].GetX(0) + a.GetX(0), p[i].GetX(1) + a.GetX(1) });
            }
            return new Polyline(res);
        }

        public override IShape Rot(double phi)
        {
            Point2D[] res = new Point2D[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = p[i].Rot(phi);
            }
            return new Polyline(res);
        }

        public override IShape SymAxis(int i)
        {
            Point2D[] res = new Point2D[n];
            for (int j = 0; j < n; j++)
            {
                res[j] = (Point2D)p[j].SymAxis(i);
            }
            return new Polyline(res);
        }

        public override bool Cross(IShape i)
        {
            if (!(i is Polyline))
            {
                throw new ArgumentException("Аргумент должен быть экземпляром класса Polyline");
            }

            Polyline other = i as Polyline;
            for (int j = 1; j < other.GetN() - 1; j++)
            {
                for (int k = 1; k < n - 1; k++)
                {
                    var a = GetP(k - 1);
                    var b = GetP(k);
                    var c = other.GetP(j - 1);
                    var d = other.GetP(j);

                    if (new Segment(a, b).Cross(new Segment(c, d)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Draw(Chart chart, int k)
        {
            chart.Series.Add(new Series());
            chart.Series[k].ChartType = SeriesChartType.Line;
            chart.Series[k].IsVisibleInLegend = false;
            chart.Series[k].Color = Color.Black;

            foreach (var v in p)
            {
                chart.Series[k].Points.AddXY(v.GetX(0), v.GetX(1));
            }
        }

        public override string ToString()
        {
            string res = "[ " + p[0].ToString();
            for (int i = 1; i < n; i++)
            {
                res += ", " + p[i].ToString();
            }
            res += " ]";
            return res;
        }
    }
}
