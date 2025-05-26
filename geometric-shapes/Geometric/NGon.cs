using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace geometric_shapes
{
    public class NGon : IShape, IPolyPoint
    {
        protected int n;
        protected Point2D[] p;

        public NGon(Point2D[] p)
        {
            if (p.Length < 3)
            {
                throw new ArgumentException("Неверное количество углов.");
            }
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

        public virtual double Square()
        {
            double res = 0;
            double ox = p[0].GetX(0);
            double oy = p[0].GetX(1);
            Point2D[] t = new Point2D[n + 1];
            for (int i = 0; i < n; i++)
            {
                t[i] = p[i];
            }
            t[n] = p[0];

            for (int i = 1; i < n + 1; i++)
            {
                double x = t[i].GetX(0);
                double y = t[i].GetX(1);
                res += (x * oy - y * ox);
                ox = x;
                oy = y;
            }

            return res / 2;
        }

        public double Length()
        {
            double res = 0;
            for (int i = 0; i < n - 1; i++)
            {
                double dx = p[i + 1].GetX(0) - p[i].GetX(0);
                double dy = p[i + 1].GetX(1) - p[i].GetX(1);
                res += Math.Sqrt(dx * dx + dy * dy);
            }
            double l1 = p[n - 1].GetX(0) - p[0].GetX(0);
            double l2 = p[n - 1].GetX(1) - p[0].GetX(1);
            res += Math.Sqrt(l1 * l1 + l2 * l2);
            return res;
        }

        public virtual IShape Shift(Point2D a)
        {
            Point2D[] res = new Point2D[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = new Point2D(new double[] { p[i].GetX(0) + a.GetX(0), p[i].GetX(1) + a.GetX(1) });
            }
            return new NGon(res);
        }

        public virtual IShape Rot(double phi)
        {
            Point2D[] res = new Point2D[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = p[i].Rot(phi);
            }
            return new NGon(res);
        }

        public virtual IShape SymAxis(int i)
        {
            Point2D[] res = new Point2D[n];
            for (int j = 0; j < n; j++)
            {
                res[j] = (Point2D)p[j].SymAxis(i);
            }
            return new NGon(res);
        }

        public bool Cross(IShape i)
        {
            if (!(i is NGon))
            {
                throw new ArgumentException("Аргумент должен быть экземпляром класса NGon или наследуемого от него класса");
            }

            NGon other = i as NGon;
            for (int j = 0; j < other.GetN(); j++)
            {
                for (int k = 0; k < n; k++)
                {
                    var a = GetP(k);
                    var b = GetP((k + 1) % n);
                    var c = other.GetP(j);
                    var d = other.GetP((j + 1) % other.GetN());

                    if (new Segment(a, b).Cross(new Segment(c, d)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Draw(Chart chart, int k)
        {
            chart.Series.Add(new Series());
            chart.Series[k].ChartType = SeriesChartType.Line;
            chart.Series[k].IsVisibleInLegend = false;
            chart.Series[k].Color = Color.Black;

            foreach (var v in p)
            {
                chart.Series[k].Points.AddXY(v.GetX(0), v.GetX(1));
            }

            chart.Series[k].Points.AddXY(p[0].GetX(0), p[0].GetX(1));
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
