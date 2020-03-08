using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Arce.Entity
{
    abstract class BaseGameEntity
    {
        public Vector2 Pos { get; set; }
        public float Scale { get; set; }

        public readonly EntityManager entityManager;

        public BaseGameEntity(EntityManager entityManager, Vector2 pos)
        {
            this.entityManager = entityManager;
            Pos = pos;
        }

        public abstract void Update(float delta);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(Pos, 5f, 64, Color.Black);
        }
    }
}
