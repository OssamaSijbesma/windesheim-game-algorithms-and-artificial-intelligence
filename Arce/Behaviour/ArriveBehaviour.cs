using Arce.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class ArriveBehaviour : SteeringBehaviour
    {
        public ArriveBehaviour(DynamicGameEntity dynamicEntity) : base(dynamicEntity)
        {
        }

        public override Vector2 Calculate() => SteeringBehaviours.Arrive(DynamicEntity, DynamicEntity.Targets.First(), Decelaration.Fast);
    }
}
