using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.entity
{
    abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public Vector2D Heading { get; set; }
        public Vector2D Side { get; set; }
        public float MaxSpeed { get; set; }
        public SteeringBehaviour SteeringBehaviour { get; set; }
        public LinkedList<Vector2D> Targets { get; set; }
        public bool Tag { get; set; }

        public MovingEntity(Vector2D pos) : base(pos)
        {
            Mass = 60;
            MaxSpeed = 40;
            Velocity = new Vector2D();
            Heading = new Vector2D();
        }

        public override void Update(float timeElapsed)
        {
            // Calculate the combined force from each steering behaviour.
            Vector2D steeringForce = SteeringBehaviour.Calculate();

            // Acceleration = Force / Mass (Newton's laws of physics).
            Vector2D acceleration = steeringForce.Clone().Divide(Mass);

            // Update velocity.
            Velocity.Add(acceleration.Clone().Multiply(timeElapsed));

            // Make sure the moving entity does not exceed maximum speed.
            Velocity.Truncate(MaxSpeed);

            // Update position.
            Pos.Add(Velocity.Clone().Multiply(timeElapsed));

            // Update heading if the velocity is bigger than 0
            if (Velocity.LengthSquared() > 0) 
            {
                Heading = Velocity.Clone().Normalize();
                Side = Heading.Perpendicular();
            }
        }

        public void TagNeighbours(MovingEntity centralEntity, List<MovingEntity> movingEntities, double radius) 
        {
            foreach (MovingEntity entity in movingEntities)
            {
                // Clear current tag.
                entity.Tag = false;

                // Calculate the difference in space
                Vector2D difference = entity.Pos.Clone().Sub(centralEntity.Pos);

                // When the entity is in range it gets tageed.
                if (entity != centralEntity &&  difference.LengthSquared() < radius * radius)
                    entity.Tag = true;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
