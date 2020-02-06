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
            Vector2D steeringForce = SB.Calculate();
            Vector2D acceleration = steeringForce.divide(Mass);
            Velocity.Add(acceleration.Multiply(timeElapsed));
            Velocity.truncate(MaxSpeed);
            Pos.Add(Velocity.Multiply(timeElapsed));

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
