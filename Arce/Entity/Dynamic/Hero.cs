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
        private string curAnimation = "front";
        private string goalInfo = "no info";

        public Hero(Vector2 pos) : base(pos)
        {
            Mass = 1.0F;
            MaxSpeed = 80.0F;
            SteeringBehaviour = new SeekBehaviour(this, pos);
            Brain = new FollowTargetGoal(this);

            animations.Add("front", new Rectangle(24, 24, 24, 24));
            animations.Add("front-left", new Rectangle(48, 0, 24, 24));
            animations.Add("front-right", new Rectangle(48, 24, 24, 24));
            animations.Add("back", new Rectangle(0, 0, 24, 24));
            animations.Add("back-left", new Rectangle(24, 0, 24, 24));
            animations.Add("back-right", new Rectangle(0, 24, 24, 24));
            animations.Add("left", new Rectangle(0, 48, 24, 24));
            animations.Add("right", new Rectangle(24, 48, 24, 24));
        }

        public override void Update(float timeElapsed)
        {
            Brain.Process();
            base.Update(timeElapsed);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Heading.X > 0.5 && Heading.Y > 0.5) curAnimation = "front-right";
            else if (Heading.X < -0.5 && Heading.Y > 0.5) curAnimation = "front-left";
            else if (Heading.X > 0.5 && Heading.Y < -0.5) curAnimation = "back-right";
            else if (Heading.X < -0.5 && Heading.Y < -0.5) curAnimation = "back-left";
            else if (Heading.X > 0.5) curAnimation = "right";
            else if (Heading.X < -0.5) curAnimation = "left";
            else if (Heading.Y > 0.5) curAnimation = "front";
            else if (Heading.Y < -0.5) curAnimation = "back";


            spriteBatch.Draw(GameWorld.Instance.mageTexture, Pos, animations[curAnimation], Color.White);
            spriteBatch.DrawString(GameWorld.Instance.font, goalInfo, new Vector2(Pos.X + 28, Pos.Y), Color.Black);
        }
    }
}
