﻿using Arce.behaviour;
using Arce.Behaviour;
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
        private Dictionary<string, Rectangle> animations = new Dictionary<string, Rectangle>();
        private string curAnimation = "up";
        private Vector2 oldTarget = new Vector2(0, 0);

        public Hero(Vector2 pos) : base(pos)
        {
            Mass = 1.0F;
            MaxSpeed = 80.0F;
            SteeringBehaviour = new SeekBehaviour(this);

            animations.Add("up", new Rectangle(0, 0, 16, 16));
            animations.Add("down", new Rectangle(16, 0, 16, 16));
            animations.Add("left", new Rectangle(32, 0, 16, 16));
            animations.Add("right", new Rectangle(48, 0, 16, 16));
        }

        public override void Update(float timeElapsed)
        {
            if (Targets.Count > 1 && Vector2.Subtract(Targets.First(), Pos).Length() < 16)
                Targets.RemoveFirst();

            switch (Targets.Count)
            {
                case 9:
                case 8:
                case 7:
                    SteeringBehaviour = new ArriveBehaviour(this, Decelaration.Fast);
                    break;
                case 6:
                case 5:
                case 4:
                    SteeringBehaviour = new ArriveBehaviour(this, Decelaration.Normal);
                    break;
                case 3:
                case 2:
                case 1:
                    SteeringBehaviour = new ArriveBehaviour(this, Decelaration.Slow);
                    break;
                default:
                    SteeringBehaviour = new SeekBehaviour(this);
                    break;
            }


            if (oldTarget != GameWorld.Instance.Target)
            {
                oldTarget = GameWorld.Instance.Target;
                Targets.Clear();
                foreach (Vertex vertex in GameWorld.Instance.navigationGraph.Dijkstra(Pos, GameWorld.Instance.Target))
                {
                    Targets.AddFirst(vertex.coordinate);
                }
            }

            base.Update(timeElapsed);
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
