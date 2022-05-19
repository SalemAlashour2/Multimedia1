using System;
using System.Collections.Generic;
using System.Text;

namespace ImageProcessing
{
    public class PointsComparer: IComparer<ControlPoint>
    {
        #region IComparer<ControlPoint> Members

        public int Compare(ControlPoint x, ControlPoint y)
        {
            if (x.Level > y.Level)
            {
                return 1;
            }
            if (x.Level < y.Level)
            {
                return -1;
            }
            return 0;
        }

        #endregion
    }
}
