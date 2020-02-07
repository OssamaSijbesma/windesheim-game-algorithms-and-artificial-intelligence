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
        double wanderRadius = 25;
        double wanderDistance = 50;
        double wanderJitter = 5;

        public WanderBehaviour(MovingEntity me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            Random random = new Random();
            Vector2D wanderTarget = new Vector2D(random.Next(-1, 1) * wanderJitter, random.Next(-1, 1) * wanderJitter);

            wanderTarget.Normalize();
            wanderTarget = wanderTarget.Multiply(wanderRadius);

            Vector2D targetLocal = wanderTarget.Add(new Vector2D(wanderDistance, 0));

            // to do

            return targetLocal.Sub(ME.Pos);
            
        }
    }
}
