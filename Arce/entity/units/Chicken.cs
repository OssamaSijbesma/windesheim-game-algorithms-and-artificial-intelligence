using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.entity
{
    class Chicken : MovingEntity
    {
        private Image _sprite;

        public Chicken(Vector2D pos, World w) : base(pos, w)
        {
            Scale = 5;
            Mass = 10;
            MaxSpeed = 5;
            _sprite = Image.FromFile("../../assets/sprites/chicken.png");
        }

        public override void Render(Graphics g)
        {
            Rectangle region;

            if (Heading.X > 0.5)
                region = new Rectangle(16, 0, 16, 16);
            else if (Heading.X < -0.5)
                region = new Rectangle(48, 0, 16, 16);
            else if (Heading.Y > 0.5)
                region = new Rectangle(32, 0, 16, 16);
            else if (Heading.Y < -0.5)
                region = new Rectangle(0, 0, 16, 16);
            else
                region = new Rectangle(0, 16, 16, 16);

            g.DrawImage(_sprite, (int) Pos.X, (int) Pos.Y, region, GraphicsUnit.Pixel);
        }
    }
}
