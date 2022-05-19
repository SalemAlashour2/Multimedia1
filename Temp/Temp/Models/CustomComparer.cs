using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp.Models
{
    class CustomComparer : IEqualityComparer<ColorMap>
    {
        public bool Equals(ColorMap x, ColorMap y)
        {
            return (
                x.OldColor.R == y.OldColor.R &&
                x.OldColor.G == y.OldColor.G &&
                x.OldColor.B == y.OldColor.B
                );
        }

        public int GetHashCode(ColorMap obj)
        {
            return obj.OldColor.GetHashCode();
        }
    }
}
