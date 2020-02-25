using Arce.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class ArriveBehaviour : SteeringBehaviour
    {
        public ArriveBehaviour(MovingEntity movingEntity) : base(movingEntity)
        { 
        }

        public override Vector2D Calculate()
        {
            return SteeringBehaviours.Arrive(MovingEntity, World.Instance.Target.Pos);
        }
    }
}
