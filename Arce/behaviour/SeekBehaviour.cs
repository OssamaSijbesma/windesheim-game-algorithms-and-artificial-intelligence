using Arce.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            Vector2D desiredVelocity = MovingEntity.MyWorld.Target.Pos.Clone().Sub(MovingEntity.Pos).Normalize().Multiply(MovingEntity.MaxSpeed);
            return desiredVelocity.Sub(MovingEntity.Velocity);
        }
    }
}
