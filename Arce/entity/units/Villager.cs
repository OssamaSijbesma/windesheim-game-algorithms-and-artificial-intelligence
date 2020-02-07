using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.entity
{
    class Villager : MovingEntity
    {
        private Color color;

        public Villager(Vector2D pos, World w) : base(pos, w)
        {
            Scale = 3;
            Mass = 40;
            MaxSpeed = 80;
            color = Color.Aqua;
        }
        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            Pen p = new Pen(color, 2);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        }
    }
}
