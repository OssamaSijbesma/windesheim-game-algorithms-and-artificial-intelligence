using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public Vector2D Heading { get; set; }
        public float MaxSpeed { get; set; }

        public SteeringBehaviour SB { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 150;
            Velocity = new Vector2D();
        }

        public override void Update(float timeElapsed)
        {
            // Calculate the combined force from each steering behaviour.
            Vector2D steeringForce = SB.Calculate();

            // Acceleration = Force / Mass (Newton's laws of physics).
            Vector2D acceleration = steeringForce.Clone().divide(Mass);

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
            }
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
