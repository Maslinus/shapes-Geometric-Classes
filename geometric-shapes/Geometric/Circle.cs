using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace geometric_shapes
{
    public class Circle : IShape    {
        private double r;
        private Point2D p;

        public Circle(Point2D p, double r)
        {
            if (r <= 0)
            {
                throw new ArgumentException("Неверный радиус.");
            }
            this.p = p;
            this.r = r;
        }

        public Point2D GetP()
        {
            return p;
        }

        public void SetP(Point2D p)
        {
            this.p = p;
        }

        public double GetR()
        {
            return r;
        }

        public void SetR(double r)
        {
            this.r = r;
        }

        public double Square()
        {
            return Math.PI * r * r;
        }

        public double Length()
        {
            return 2 * Math.PI * r;
        }

        public IShape Shift(Point2D a)
        {
            return new Circle(new Point2D(new double[] { p.GetX(0) + a.GetX(0), p.GetX(1) + a.GetX(1) }), r);
        }

        public IShape Rot(double phi)
        {
            return new Circle(p.Rot(phi), r);
        }

        public IShape SymAxis(int i)
        {
            return new Circle((Point2D)p.SymAxis(i), r);
        }

        public bool Cross(IShape i)
        {
            if (i is Circle)
            {
                Circle c = (Circle)i;
                double res = Math.Sqrt(Math.Pow(p.GetX(0) - c.p.GetX(0), 2) + Math.Pow(p.GetX(1) - c.p.GetX(1), 2));
                return res < (r + c.r);
            }
            else
            {
                throw new ArgumentException("Аргумент должен быть экземпляром класса Circle");
            }
        }


        public void Draw(Chart chart, int k)
        {
            chart.Series.Add(new Series());
            chart.Series[k].ChartType = SeriesChartType.Line;
            chart.Series[k].IsVisibleInLegend = false;
            chart.Series[k].Color = Color.Black;

            for (int j = 0; j <= 100; j++)
            {
                double x = p.GetX(0) + r * Math.Cos(j * 2 * Math.PI / 100);
                double y = p.GetX(1) + r * Math.Sin(j * 2 * Math.PI / 100);
                chart.Series[k].Points.AddXY(x, y);
            }
        }

        public override string ToString()
        {
            string res = "[center:" + p + ", rad: " + r + "]";
            return res;
        }

    }
}
