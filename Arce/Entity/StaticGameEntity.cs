using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity
{
    class StaticGameEntity : BaseGameEntity
    {
        public int TextureWidth { get; set; }
        public int TextureHeight { get; set; }
        public StaticGameEntity(EntityManager entityManager, Vector2 pos, int width, int height) : base(entityManager, pos)
        {
            TextureWidth = width;
            TextureHeight = height;
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }
    }
}