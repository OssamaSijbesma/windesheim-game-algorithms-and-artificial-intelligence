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
        private Dictionary<string, Rectangle> animations = new Dictionary<string, Rectangle>();
        private string curAnimation = "front";

        public Sheep(Vector2 pos) : base(pos)
        {
            Mass = 1.0F;
            MaxSpeed = 20.0F;
            SteeringBehaviour = new FlockingBehaviour(this);

            animations.Add("front", new Rectangle(0, 0, 32, 32));
            animations.Add("back", new Rectangle(32, 0, 32, 32));
            animations.Add("left", new Rectangle(0, 32, 32, 32));
            animations.Add("right", new Rectangle(32, 32, 32, 32));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Heading.X > 0.5) curAnimation = "back";
            else if (Heading.X < -0.5) curAnimation = "right";
            else if (Heading.Y > 0.5) curAnimation = "left";
            else if (Heading.Y < -0.5) curAnimation = "front";

            spriteBatch.Draw(GameWorld.Instance.sheepTexture, Pos, animations[curAnimation], Color.White);
        }
    }
}
