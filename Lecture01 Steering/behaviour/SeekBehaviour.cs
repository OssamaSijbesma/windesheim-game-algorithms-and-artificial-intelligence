using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            Vector2D desiredVelocity = ME.MyWorld.Target.Pos.Clone().Sub(ME.Pos).Normalize().Multiply(ME.MaxSpeed);
            return desiredVelocity.Sub(ME.Velocity);
        }
    }
}
