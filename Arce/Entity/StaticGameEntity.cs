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
        public StaticGameEntity(EntityManager entityManager, Vector2 pos) : base(entityManager, pos)
        {
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }
    }
}