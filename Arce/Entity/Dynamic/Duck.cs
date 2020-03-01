using Arce.behaviour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity
{
    class Duck : DynamicGameEntity
    {
        private Dictionary<string, Rectangle> animations = new Dictionary<string, Rectangle>();
        private string curAnimation = "up";

        public Duck(Vector2 pos) : base(pos)
        {
            Mass = 1.0F;
            MaxSpeed = 20.0F;
            SteeringBehaviour = new FlockingBehaviour(this);

            animations.Add("up", new Rectangle(0, 0, 16, 16));
            animations.Add("down", new Rectangle(16, 0, 16, 16));
            animations.Add("left", new Rectangle(32, 0, 16, 16));
            animations.Add("right", new Rectangle(48, 0, 16, 16));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Heading.X > 0.5)
                curAnimation = "down";
            else if (Heading.X < -0.5)
                curAnimation = "right";
            else if (Heading.Y > 0.5)
                curAnimation = "left";
            else if (Heading.Y < -0.5)
                curAnimation = "up";

            spriteBatch.Draw(GameWorld.Instance.chickenTexture, Pos, animations[curAnimation], Color.White);
        }
    }
}
