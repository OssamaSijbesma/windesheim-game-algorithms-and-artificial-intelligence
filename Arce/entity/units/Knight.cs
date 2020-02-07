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
        public Knight(Vector2D pos, World w) : base(pos, w)
        {
        }
        public override void Render(Graphics g)
        {
        }
    }
}
