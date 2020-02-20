using System.Drawing;

namespace Arce
{
    abstract class BaseGameEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }

        public BaseGameEntity(Vector2D pos)
        {
            Pos = pos;
        }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int) Pos.X,(int) Pos.Y, 16, 16));
        }
    }
}
