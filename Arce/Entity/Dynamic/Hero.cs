using Arce.Behaviour;
using Arce.Brain;
using Arce.NavigationGraph;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity
{
    class Hero : ConsciousGameEntity
    {
        private Rectangle[] animationFrames;
        private int direction;

        public Hero(EntityManager entityManager, Vector2 position) : base(entityManager, position)
        {
            Mass = 1.0F;
            MaxSpeed = 80.0F;
            SteeringBehaviour = new SeekBehaviour(this, position);

            // Set animation frames
            animationFrames = new Rectangle[]
            {
                new Rectangle(24, 24, 24, 24),  // North
                new Rectangle(48, 24, 24, 24),  // North East
                new Rectangle(24, 48, 24, 24),  // East
                new Rectangle(0, 24, 24, 24),   // South East
                new Rectangle(0, 0, 24, 24),    // South
                new Rectangle(24, 0, 24, 24),   // South West
                new Rectangle(0, 48, 24, 24),   // West
                new Rectangle(48, 0, 24, 24)    // North West
            };
        }

        public override void Update(float timeElapsed)
        {
            base.Update(timeElapsed);

            // Set the animation direction
            if (Heading.X > 0.5 && Heading.Y > 0.5) direction = 1;
            else if (Heading.X > 0.5 && Heading.Y < -0.5) direction = 3;
            else if (Heading.X < -0.5 && Heading.Y < -0.5) direction = 5;
            else if (Heading.X < -0.5 && Heading.Y > 0.5) direction = 7;
            else if (Heading.Y > 0.5) direction = 0;
            else if (Heading.X > 0.5) direction = 2;
            else if (Heading.Y < -0.5) direction = 4;
            else if (Heading.X < -0.5) direction = 6;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(entityManager.mageTexture, Pos, animationFrames[direction], Color.White);
        }
    }
}
