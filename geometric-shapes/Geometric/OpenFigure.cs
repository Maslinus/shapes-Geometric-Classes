using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace geometric_shapes
{
    public abstract class OpenFigure : IShape
    {
        public abstract double Square();

        public abstract double Length();

        public abstract IShape Shift(Point2D vector);

        public abstract IShape Rot(double phi);

        public abstract IShape SymAxis(int axis);

        public abstract bool Cross(IShape other);

        public abstract void Draw(Chart chart, int k);
    }
}
