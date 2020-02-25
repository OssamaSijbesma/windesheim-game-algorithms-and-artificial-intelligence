using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.entity
{
    class MapTile : StaticEntity
    {
        private Image _image;
        public MapTile(Vector2D pos, Image image) : base(pos)
        {
            _image = image;
        }

        public override void Render(Graphics g)
        {            
            // Draw your image here.
            g.DrawImage(_image, (int)Pos.X, (int)Pos.Y);
        }
    }
}
