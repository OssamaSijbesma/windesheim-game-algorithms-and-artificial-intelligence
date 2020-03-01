using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity.Dynamic
{
    class Chicken : DynamicGameEntity
    {
        public Chicken(Vector2 pos) : base(pos)
        {
            Mass = 1.0F;
            MaxSpeed = 20.0F;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
