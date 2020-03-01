using System;
using System.Collections.Generic;
using MonoGame.Extended;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arce.Entity;
using Microsoft.Xna.Framework;

namespace Arce.Behaviour
{
    public enum Decelaration { Slow = 3, Normal = 2, Fast = 1 }

    static class SteeringBehaviours
    {
        public static Vector2 Seek(DynamicGameEntity entity, Vector2 target)
        {
            Vector2 toTarget = Vector2.Subtract(target, entity.Pos);
            Vector2 desiredVelocity = Vector2.Multiply(Vector2.Normalize(toTarget), entity.MaxSpeed);
            return Vector2.Subtract(desiredVelocity, entity.Velocity);
        }

        public static Vector2 Flee(DynamicGameEntity entity, Vector2 rapist)
        {
            Vector2 awayFromRapist = Vector2.Subtract(entity.Pos, rapist);
            Vector2 desiredVelocity = Vector2.Multiply(Vector2.Normalize(awayFromRapist), entity.MaxSpeed);
            return Vector2.Subtract(desiredVelocity, entity.Velocity);
        }

        public static Vector2 Arrive(DynamicGameEntity entity, Vector2 target, Decelaration decelaration) 
        {
            Vector2 toTarget = Vector2.Subtract(target, entity.Pos);

            // Calculate the distance to the target position
            float distance = toTarget.Length();

            if (distance <= 0)
                return new Vector2(0, 0);

            // Fine tweaking of deceleration
            const float decelerationTweaker = 0.3F;

            // Calculate the speed required to reach the target
            float speed = distance / ((float)decelaration * decelerationTweaker);

            // Make sure velocity doesn't exceed the MaxSpeed
            speed = Math.Min(speed, entity.MaxSpeed);

            Vector2 desiredVelocity = Vector2.Multiply(toTarget, speed / distance);

            return Vector2.Subtract(desiredVelocity, entity.Velocity);
        }

        public static Vector2 Separation(DynamicGameEntity entity, List<DynamicGameEntity> neighbours) 
        {
            Vector2 steeringForce = new Vector2(0, 0);

            foreach (DynamicGameEntity neighbour in neighbours)
            {
                if (neighbour.Tag == false || entity.Pos == neighbour.Pos)
                    continue;

                Vector2 ToAgent = Vector2.Subtract(entity.Pos, neighbour.Pos);

                //scale the force inversely proportional to the agent's distance from its neighbor
                steeringForce += Vector2.Divide(Vector2.Normalize(ToAgent), ToAgent.Length());
            }

            return steeringForce;
        }

        public static Vector2 Alignment(DynamicGameEntity entity, List<DynamicGameEntity> neighbours)
        {
            Vector2 averageHeading = new Vector2(0, 0);
            int neighbourCount = 0;

            foreach (DynamicGameEntity neighbour in neighbours)
            {
                if (neighbour.Tag == false)
                    continue;

                averageHeading += neighbour.Heading;
                neighbourCount++;
            }

            if (neighbourCount > 0)
            {
                // Average heading vectors
                averageHeading /= neighbourCount;
                averageHeading -= entity.Heading;
            }

            return averageHeading;
        }
        
        public static Vector2 Cohesion(DynamicGameEntity entity, List<DynamicGameEntity> neighbours)
        {
            Vector2 centerOfMass = new Vector2(0, 0);
            Vector2 steeringForce = new Vector2(0, 0);
            int neighbourCount = 0;

            foreach (DynamicGameEntity neighbour in neighbours)
            {
                if (neighbour.Tag == false)
                    continue;

                centerOfMass += neighbour.Pos;
                neighbourCount++;
            }

            if (neighbourCount > 0)
            {
                centerOfMass /= neighbourCount;
                steeringForce = Seek(entity, centerOfMass);
            }

            return steeringForce;
        }
        
    }
}
