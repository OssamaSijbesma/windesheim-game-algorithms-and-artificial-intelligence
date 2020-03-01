using Arce.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Behaviour
{
    class ArriveBehaviour : SteeringBehaviour
    {
        private Decelaration decelaration;

        public ArriveBehaviour(DynamicGameEntity dynamicEntity, Decelaration decelaration) : base(dynamicEntity)
        {
            this.decelaration = decelaration;
        }

        public override Vector2 Calculate() => SteeringBehaviours.Arrive(DynamicEntity, DynamicEntity.Targets.First(), decelaration);
    }
}
