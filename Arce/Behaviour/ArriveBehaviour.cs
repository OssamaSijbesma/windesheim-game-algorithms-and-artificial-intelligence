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
        private Vector2 target;
        private Decelaration decelaration;

        public ArriveBehaviour(DynamicGameEntity dynamicEntity, Vector2 target, Decelaration decelaration) : base(dynamicEntity)
        {
            this.target = target;
            this.decelaration = decelaration;
        }

        public override Vector2 Calculate() => SteeringBehaviours.Arrive(DynamicEntity, target, decelaration);
    }
}
