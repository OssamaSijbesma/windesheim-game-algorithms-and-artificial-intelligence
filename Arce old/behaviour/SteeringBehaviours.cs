using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arce.entity;

namespace Arce.behaviour
{
    static class SteeringBehaviours
    {
        public static Vector2D Seek(MovingEntity entity, Vector2D target)
        {
            Vector2D desiredVelocity = target.Clone().Sub(entity.Pos).Normalize().Multiply(entity.MaxSpeed);
            return desiredVelocity.Sub(entity.Velocity);
        }

        public static Vector2D Arrive(MovingEntity entity, Vector2D target) 
        {
            Vector2D toTarget = target.Clone().Sub(entity.Pos);
            double distance = toTarget.Length();

            if (distance <= 0)
                return new Vector2D(0, 0);

            // fine tweaking of deceleration
            const double decelerationTweaker = 4;

            // calculate the speed required to reach the target
            double speed = distance / (1 * decelerationTweaker);

            // make sure velocity doesn't exceed the MaxSpeed
            speed = Math.Min(speed, entity.MaxSpeed);

            Vector2D desiredVelocity = toTarget.Multiply(speed / distance);

            return desiredVelocity.Sub(entity.Velocity);
        }

        public static Vector2D Flee(MovingEntity entity, Vector2D rapist)
        {
            Vector2D desiredVelocity = entity.Pos.Clone().Sub(rapist).Normalize().Multiply(entity.MaxSpeed);
            return desiredVelocity.Sub(entity.Velocity);
        }

        public static Vector2D Separation(MovingEntity entity, List<MovingEntity> neighbours) 
        {
            Vector2D steeringForce = new Vector2D(0, 0);

            foreach (MovingEntity neighbour in neighbours)
            {
                if (neighbour.Tag == false || entity.Pos == neighbour.Pos)
                    continue;

                Vector2D ToAgent = entity.Pos.Clone().Sub(neighbour.Pos);
                steeringForce.Add(ToAgent.Clone().Normalize().Divide(ToAgent.Length()));
            }

            return steeringForce;
        }

        public static Vector2D Alignment(MovingEntity entity, List<MovingEntity> neighbours)
        {
            Vector2D averageHeading = new Vector2D(0, 0);
            int neighbourCount = 0;

            foreach (MovingEntity neighbour in neighbours)
            {
                if (neighbour.Tag == false)
                    continue;

                averageHeading.Add(neighbour.Heading);
                neighbourCount++;
            }

            if (neighbourCount > 0)
            {
                averageHeading.Divide(neighbourCount);
                averageHeading.Sub(entity.Heading);
            }

            return averageHeading;
        }

        public static Vector2D Cohesion(MovingEntity entity, List<MovingEntity> neighbours)
        {
            Vector2D centerOfMass = new Vector2D(0, 0);
            Vector2D steeringForce = new Vector2D(0, 0);
            int neighbourCount = 0;

            foreach (MovingEntity neighbour in neighbours)
            {
                if (neighbour.Tag == false)
                    continue;

                centerOfMass.Add(neighbour.Pos);
                neighbourCount++;
            }

            if (neighbourCount > 0)
            {
                centerOfMass.Divide(neighbourCount);
                steeringForce = Seek(entity, centerOfMass);
            }

            return steeringForce;
        }
    }
}
