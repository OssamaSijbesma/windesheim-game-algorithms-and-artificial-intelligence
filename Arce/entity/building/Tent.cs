using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.entity.building
{
    class Tent : StaticEntity
    {
        private Image _image;
        public Tent(Vector2D pos) : base(pos)
        {
            _image = Image.FromFile("../../assets/images/Tent.png");
        }

        public override void Render(Graphics g)
        {
            Rectangle region = new Rectangle(0, 0, 42, 39);

            g.DrawImage(_image, (float)Pos.X, (float)Pos.Y, region, GraphicsUnit.Pixel);
        }
    }
}
