﻿using System;
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

        public Chicken(Vector2D pos) : base(pos)
        {
            Mass = 20;
            MaxSpeed = 5;
            _sprite = Image.FromFile("../../assets/sprites/chicken.png");
        }

        public override void Render(Graphics g)
        {
            Rectangle region = new Rectangle(96, 0, 16, 16);

            if (Heading.X > 0.5)
                region.X = 16;
            else if (Heading.X < -0.5)
                region.X = 48;
            else if (Heading.Y > 0.5)
                region.X = 32;
            else if (Heading.Y < -0.5)
                region.X = 0;
            else
                region.X = 96;

            g.DrawImage(_sprite, (int) Pos.X-8, (int) Pos.Y-8, region, GraphicsUnit.Pixel);
        }
    }
}
