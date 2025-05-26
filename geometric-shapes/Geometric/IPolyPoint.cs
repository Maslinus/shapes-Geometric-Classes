using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometric_shapes
{
    public interface IPolyPoint
    {
        Point2D GetP(int i);
        void SetP(Point2D p, int i);
    }
}
