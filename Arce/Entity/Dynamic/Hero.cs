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
    class Hero : DynamicGameEntity
    {
        private Vector2 oldTarget = new Vector2(0, 0);
        private Dictionary<string, Rectangle> animations = new Dictionary<string, Rectangle>();
        private string curAnimation = "up";

        public Hero(Vector2 pos) : base(pos)
        {
            Mass = 1.0F;
            MaxSpeed = 80.0F;
            SteeringBehaviour = new SeekBehaviour(this, pos);
            Brain = new FollowTargetGoal(this);

            animations.Add("front", new Rectangle(0, 0, 16, 16));
            animations.Add("back", new Rectangle(16, 0, 16, 16));
            animations.Add("left", new Rectangle(32, 0, 16, 16));
            animations.Add("right", new Rectangle(48, 0, 16, 16));
        }

        public override void Update(float timeElapsed)
        {
            Brain.Process();
            base.Update(timeElapsed);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Heading.X > 0.5) curAnimation = "back";
            else if (Heading.X < -0.5) curAnimation = "right";
            else if (Heading.Y > 0.5) curAnimation = "left";
            else if (Heading.Y < -0.5) curAnimation = "front";

            spriteBatch.Draw(GameWorld.Instance.chickenTexture, Pos, animations[curAnimation], Color.White);
        }
    }
}
