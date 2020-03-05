using Arce.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        private Vector2 target;

        public SeekBehaviour(DynamicGameEntity dynamicEntity, Vector2 target) : base(dynamicEntity)
        {
            this.target = target;
        }

        public override Vector2 Calculate() => SteeringBehaviours.Seek(DynamicEntity, target);
    }
}
