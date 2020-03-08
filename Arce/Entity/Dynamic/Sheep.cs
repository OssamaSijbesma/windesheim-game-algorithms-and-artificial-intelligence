using Arce.Behaviour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity
{
    class Sheep : DynamicGameEntity
    {
        private Rectangle[] animationFrames;
        private int direction;

        public Sheep(EntityManager entityManager, Vector2 pos) : base(entityManager, pos)
        {
            Mass = 1.0F;
            MaxSpeed = 20.0F;
            SteeringBehaviour = new FlockingBehaviour(this);

            // Set animation frames
            animationFrames = new Rectangle[]
            {
                new Rectangle(1, 1, 32, 32),    // North
                new Rectangle(34, 34, 32, 32),  // East
                new Rectangle(34, 1, 32, 32),   // South
                new Rectangle(1, 34, 32, 32),   // West
            };
        }

        public override void Update(float timeElapsed)
        {
            base.Update(timeElapsed);

            // Set the animation direction
            if (Heading.Y > 0.5) direction = 0;
            else if (Heading.X > 0.5) direction = 1;
            else if (Heading.Y < -0.5) direction = 2;
            else if (Heading.X < -0.5) direction = 3;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(entityManager.sheepTexture, Pos, animationFrames[direction], Color.White);
        }
    }
}
