using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity
{
    interface IDynamicGameEntityFactory
    {
        DynamicGameEntity CreateEntity(Vector2 position);

        List<DynamicGameEntity> CreateEntities(Vector2 position, int radius, int amount);
    }
}
