using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace geometric_shapes
{
    public class Segment : OpenFigure
    {
        private Point2D start;
        private Point2D finish;

        public Segment(Point2D s, Point2D f)
        {
            start = s;
            finish = f;
        }

        public Point2D GetStart()
        {
            return start;
        }

        public void SetStart(Point2D a)
        {
            start = a;
        }

        public Point2D GetFinish()
        {
            return finish;
        }

        public void SetFinish(Point2D a)
        {
            finish = a;
        }

        public override double Length()
        {
            double dx = finish.GetX(0) - start.GetX(0);
            double dy = finish.GetX(1) - start.GetX(1);
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public override double Square()
        {
            return 0;
        }

        public override IShape Shift(Point2D a)
        {
            Point2D newStart = new Point2D(new double[] { start.GetX(0) + a.GetX(0), start.GetX(1) + a.GetX(1) });
            Point2D newFinish = new Point2D(new double[] { finish.GetX(0) + a.GetX(0), finish.GetX(1) + a.GetX(1) });
            return new Segment(newStart, newFinish);
        }

        public override IShape Rot(double phi)
        {
            Point2D newStart = start.Rot(phi);
            Point2D newFinish = finish.Rot(phi);
            return new Segment(newStart, newFinish);
        }

        public override IShape SymAxis(int i)
        {
            Point2D newStart = (Point2D)start.SymAxis(i);
            Point2D newFinish = (Point2D)finish.SymAxis(i);
            return new Segment(newStart, newFinish);
        }

        public override bool Cross(IShape i)
        {
            if (!(i is Segment))
            {
                throw new ArgumentException("Аргумент должен быть экземпляром класса Segment");
            }

            Segment other = i as Segment;

            var a = start;
            var b = finish;
            var c = other.start;
            var d = other.finish;

            if (a.GetX(0) == c.GetX(0) && a.GetX(1) == c.GetX(1) && b.GetX(0) == d.GetX(0) && b.GetX(1) == d.GetX(1)) return true;

            var ua = (d.GetX(0) - c.GetX(0)) * (a.GetX(1) - c.GetX(1)) - (d.GetX(1) - c.GetX(1)) * (a.GetX(0) - c.GetX(0));
            var ub = (b.GetX(0) - a.GetX(0)) * (a.GetX(1) - c.GetX(1)) - (b.GetX(1) - a.GetX(1)) * (a.GetX(0) - c.GetX(0));
            var denom = (d.GetX(1) - c.GetX(1)) * (b.GetX(0) - a.GetX(0)) - (d.GetX(0) - c.GetX(0)) * (b.GetX(1) - a.GetX(1));

            if (denom == 0)
            {
                return false;
            }

            ua /= denom;
            ub /= denom;

            if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
            {
                return true;
            }

            return false;

        }


        public override void Draw(Chart chart, int k)
        {
            chart.Series.Add(new Series());
            chart.Series[k].ChartType = SeriesChartType.Line;
            chart.Series[k].IsVisibleInLegend = false;
            chart.Series[k].Color = Color.Black;

            chart.Series[k].Points.AddXY(start.GetX(0), start.GetX(1));
            chart.Series[k].Points.AddXY(finish.GetX(0), finish.GetX(1));
        }

        public override string ToString()
        {
            string res = "[ " + start + "; " + finish + " ]";
            return res;
        }
    }
}
