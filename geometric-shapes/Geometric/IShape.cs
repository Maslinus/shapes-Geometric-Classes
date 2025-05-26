using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace geometric_shapes
{
    public interface IShape
    {

        double Square();

        double Length();

        IShape Shift(Point2D a);

        IShape Rot(double phi);

        IShape SymAxis(int i);

        bool Cross(IShape other);

        void Draw(Chart chart, int k);
    }
}
