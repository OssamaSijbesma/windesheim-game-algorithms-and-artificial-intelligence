using Arce.Brain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity
{
    class ConsciousGameEntity : DynamicGameEntity
    {
        public float Hunger { get; set; }
        public float Sleep { get; set; }

        private CompositeGoal brain;

        private float totalTime;

        public ConsciousGameEntity(EntityManager entityManager, Vector2 position) : base(entityManager, position)
        {
            Sleep = 1f;
            Hunger = 1f;

            brain = new ThinkGoal(this);
        }

        public override void Update(float timeElapsed)
        {
            brain.Process();

            base.Update(timeElapsed);

            totalTime += timeElapsed;
            if (totalTime > 1)
            {
                totalTime = 0;
                Sleep -= 0.001f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (GameWorld.Instance.showInfo) spriteBatch.DrawString(entityManager.font, brain.ToString(), new Vector2(Pos.X + 28, Pos.Y), Color.Black);
        }
    }
}
