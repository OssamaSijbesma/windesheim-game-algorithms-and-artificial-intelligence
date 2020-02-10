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
        public ArriveBehaviour(MovingEntity me) : base(me)
        { 
        }

        public override Vector2D Calculate()
        {
            Vector2D toTarget = MovingEntity.MyWorld.Target.Pos.Clone().Sub(MovingEntity.Pos.Clone());
            double distance = toTarget.Length();

            if (distance > 0)
            {
                // fine tweaking of deceleration
                const double decelerationTweaker = 4;

                // calculate the speed required to reach the target
                double speed = distance / (1 * decelerationTweaker);

                // make sure velocity doesn't exceed the MaxSpeed
                speed = Math.Min(speed, MovingEntity.MaxSpeed);

                Vector2D desiredVelocity = toTarget.Multiply(speed / distance);
                return desiredVelocity.Sub(MovingEntity.Velocity.Clone());
            }

            return new Vector2D(0, 0);
        }
    }
}
