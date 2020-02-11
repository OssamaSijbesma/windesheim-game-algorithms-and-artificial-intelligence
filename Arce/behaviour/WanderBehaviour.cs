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
        double wanderRadius = 5;
        double wanderDistance = 4;
        double wanderJitter = 50;

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
            MovingEntity.MyWorld.Target.Pos = wanderTarget.Clone().Add(MovingEntity.Pos);

            // Wander to the position
            return wanderTarget.Clone().Sub(MovingEntity.Pos);
        }

        private static double RandomDirection(Random random) 
        {
            double next = random.NextDouble();
            return -1 + (next * 2);
        }
        /*
        private static Vector2D PointToWorldSpace(Vector2D point, Vector2D heading, Vector2D side, Vector2D pos) 
        {
            //make a copy of the point
            Vector2D TransPoint = point;

            //create a transformation matrix
            C2DMatrix matTransform;

            //rotate
            matTransform.Rotate(heading, side);

            //and translate
            matTransform.Translate(pos.X, pos.Y);

            //now transform the vertices
            matTransform.TransformVector2Ds(TransPoint);

            return TransPoint;
        }
        */
    }
}
