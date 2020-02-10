using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.entity
{
    class Knight : MovingEntity
    {
        private Color color;
        private Image _sprite;

        public Knight(Vector2D pos, World w) : base(pos, w)
        {
            Scale = 5;
            Mass = 40;
            MaxSpeed = 80;
            color = Color.PaleVioletRed;
            _sprite = Image.FromFile("../../assets/sprites/chicken.png");
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            Rectangle region = new Rectangle(32, 0, 16, 16);
            g.DrawImage(_sprite, Convert.ToSingle(Pos.X), Convert.ToSingle(Pos.Y), region, GraphicsUnit.Pixel);
        }
    }
}
