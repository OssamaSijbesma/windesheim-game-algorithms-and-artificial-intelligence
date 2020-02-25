using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce
{
    static class GameUtil
    {
        public static Bitmap cropAtRect(this Bitmap bitmap, int x, int y)
        {
            Bitmap croppedBitmap = new Bitmap(16, 16);
            Graphics graphics = Graphics.FromImage(croppedBitmap);
            graphics.DrawImage(bitmap, -x, -y);
            return croppedBitmap;
        }
    }
}
