using Arce.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class FleeBehaviour : SteeringBehaviour
    {
        public FleeBehaviour(MovingEntity movingEntity) : base(movingEntity)
        {
        }

        public override Vector2D Calculate()
        {
            Vector2D desiredVelocity = MovingEntity.Pos.Clone().Sub(MovingEntity.MyWorld.Target.Pos).Normalize().Multiply(MovingEntity.MaxSpeed);
            return desiredVelocity.Sub(MovingEntity.Velocity);
        }
    }
}
