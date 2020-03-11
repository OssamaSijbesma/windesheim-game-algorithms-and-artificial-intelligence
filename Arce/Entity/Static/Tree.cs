using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity.Static
{
    class Tree : StaticGameEntity
    {
        public Tree(EntityManager entityManager, Vector2 pos, int width, int height) : base(entityManager, pos, width, height)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(entityManager.treeTexture, Pos);
        }
    }
}
