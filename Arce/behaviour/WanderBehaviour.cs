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

            Vector2D wanderTarget = new Vector2D(RandomDirection(random) * wanderJitter, RandomDirection(random) * wanderJitter);

            // Reproject this new vector back onto a unit circle.
            wanderTarget.Normalize();

            // Increase the length of the vector to the same as the radius of the wander circle.
            wanderTarget.Multiply(wanderRadius);

            // Add the wanderdistance 
            wanderTarget.Add(new Vector2D(wanderDistance, 0));

            // Officialy wandertarget is localspace and it has to become world space
            ME.MyWorld.Target.Pos = wanderTarget.Clone().Add(ME.Pos);

            // Wander to the position
            return wanderTarget.Clone().Sub(ME.Pos);
        }

        private static double RandomDirection(Random random) 
        {
            double next = random.NextDouble();
            return -1 + (next * 2);
        }
    }
}
