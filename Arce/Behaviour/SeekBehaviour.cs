using Arce.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(DynamicGameEntity dynamicEntity) : base(dynamicEntity)
        {
        }

        public override Vector2 Calculate() => SteeringBehaviours.Seek(DynamicEntity, DynamicEntity.Targets.First());
    }
}
