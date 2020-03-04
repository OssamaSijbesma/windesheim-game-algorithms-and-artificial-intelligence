using Arce.Behaviour;
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
        public Vector2 Heading { get; set; }
        public Vector2 Side { get; set; }
        public Vector2 Velocity { get; set; }

        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public SteeringBehaviour SteeringBehaviour { get; set; }

        public LinkedList<Vector2> Targets { get; set; }

        public bool Tag { get; set; }
        public bool Selected { get; set; }

        public DynamicGameEntity(Vector2 pos) : base(pos)
        {
            Velocity = new Vector2();
            Targets = new LinkedList<Vector2>();

            Mass = 1.0F;
            MaxSpeed = 150.0F;
            Targets.AddLast(new Vector2(900, 300));
            SteeringBehaviour = new FlockingBehaviour(this);
        }

        public void EnforceNonPenetrationConstraint(List<DynamicGameEntity> dynamicEntities)
        {
            foreach (DynamicGameEntity entity in dynamicEntities)
            {
                //make sure we don't check against the individual
                if (entity == this) continue;

                // calculate the distance between the positions of the entities
                Vector2 ToEntity = Vector2.Subtract(Pos, entity.Pos);

                float distFromEachOther = ToEntity.Length();

                //if this distance is smaller than the sum of their radii then this entity must be moved away in the direction parallel to the ToEntity vector
                float amountOfOverlap = 10 + 10 - distFromEachOther;

                //move the entity a distance away equivalent to the amount of overlap
                if (amountOfOverlap >= 0)
                    Pos += Vector2.Multiply(Vector2.Divide(ToEntity, distFromEachOther), amountOfOverlap);
            }

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
