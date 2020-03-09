using Arce.Behaviour;
using Arce.Brain;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity
{
    class DynamicGameEntity : BaseGameEntity
    {
        public SteeringBehaviour SteeringBehaviour { get; set; }
        public Vector2 Heading { get; set; }
        public Vector2 Side { get; set; }
        public Vector2 Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public bool Tag { get; set; }

        public int Hunger { get; set; }
        public int Sleep { get; set; }

        public DynamicGameEntity(EntityManager entityManager, Vector2 pos) : base(entityManager, pos)
        {
            Velocity = new Vector2();

            Mass = 1.0F;
            MaxSpeed = 150.0F;
            SteeringBehaviour = new FlockingBehaviour(this);

            Hunger = 10;
            Sleep = 10;
        }

        public override void Update(float timeElapsed)
        {
            // Calculate the combined force from each steering behaviour.
            Vector2 steeringForce = SteeringBehaviour.Calculate();

            // Acceleration = Force / Mass (Newton's laws of physics).
            Vector2 acceleration = Vector2.Divide(steeringForce, Mass);

            // Update velocity.
            Velocity += Vector2.Multiply(acceleration, timeElapsed);

            // Make sure the moving entity does not exceed maximum speed.
            Velocity = Velocity.Truncate(MaxSpeed);

            // Update position.
            Pos += Vector2.Multiply(Velocity, timeElapsed);

            // Update heading if the velocity is bigger than 0
            if (Velocity.LengthSquared() > 0)
            {
                Heading = Velocity.NormalizedCopy();
                Side = Heading.PerpendicularClockwise();
            }
        }
    }
}
