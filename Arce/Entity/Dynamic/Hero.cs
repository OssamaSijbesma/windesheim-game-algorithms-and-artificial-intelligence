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
        private Rectangle[] animationFrames;
        private int animation;
        private string goalInfo;

        public Hero(Vector2 pos) : base(pos)
        {
            Mass = 1.0F;
            MaxSpeed = 80.0F;
            SteeringBehaviour = new SeekBehaviour(this, pos);
            Brain = new FollowTargetGoal(this);
            goalInfo = "no info";

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
            /*
            animations.Add("front", new Rectangle(24, 24, 24, 24));
            animations.Add("front-left", new Rectangle(48, 0, 24, 24));
            animations.Add("front-right", new Rectangle(48, 24, 24, 24));
            animations.Add("back", new Rectangle(0, 0, 24, 24));
            animations.Add("back-left", new Rectangle(24, 0, 24, 24));
            animations.Add("back-right", new Rectangle(0, 24, 24, 24));
            animations.Add("left", new Rectangle(0, 48, 24, 24));
            animations.Add("right", new Rectangle(24, 48, 24, 24));
            */
        }

        public override void Update(float timeElapsed)
        {
            Brain.Process();
            goalInfo = Brain.ToString();

            base.Update(timeElapsed);

            if (Heading.X > 0.5 && Heading.Y > 0.5) animation = 1;
            else if (Heading.X > 0.5 && Heading.Y < -0.5) animation = 3;
            else if (Heading.X < -0.5 && Heading.Y < -0.5) animation = 5;
            else if (Heading.X < -0.5 && Heading.Y > 0.5) animation = 7;
            else if (Heading.Y > 0.5) animation = 0;
            else if (Heading.X > 0.5) animation = 2;
            else if (Heading.Y < -0.5) animation = 4;
            else if (Heading.X < -0.5) animation = 6;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameWorld.Instance.mageTexture, Pos, animationFrames[animation], Color.White);
            if(GameWorld.Instance.showInfo) spriteBatch.DrawString(GameWorld.Instance.font, goalInfo, new Vector2(Pos.X + 28, Pos.Y), Color.Black);
        }
    }
}
