using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Arce
{
    abstract class BaseGameEntity
    {
        public Vector2 Pos { get; set; }
        public float Scale { get; set; }

        public BaseGameEntity(Vector2 pos)
        {
            Pos = pos;
        }

        public abstract void Update(float delta);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(Pos, 10f, 0, Color.Blue);
        }
    }
}
