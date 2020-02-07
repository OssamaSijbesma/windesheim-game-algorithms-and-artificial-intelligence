using Arce.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class WanderBehaviour : SteeringBehaviour
    {
        public WanderBehaviour(MovingEntity me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
