using Arce.behaviour;
using Arce.entity;
using Arce.entity.building;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce
{
    class World
    {
        private List<MovingEntity> movingEntities = new List<MovingEntity>();

        private List<StaticEntity> staticEntities = new List<StaticEntity>();
        public Vehicle Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            generateMap();
            populate();
        }

        private void generateMap()
        {
            Tent tent = new Tent(new Vector2D(280, 180), this);
            staticEntities.Add(tent);
        }

        private void populate()
        {
            /*Vehicle v = new Vehicle(new Vector2D(10,10), this);
            v.VColor = Color.Blue;
            entities.Add(v);
            */

            Chicken chicken = new Chicken(new Vector2D(100, 100), this);
            movingEntities.Add(chicken);
            Chicken chicken2 = new Chicken(new Vector2D(100, 110), this);
            movingEntities.Add(chicken2);
            Chicken chicken3 = new Chicken(new Vector2D(110, 105), this);
            movingEntities.Add(chicken3);

            Target = new Vehicle(new Vector2D(100, 60), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(100, 40);
        }

        public void TagNeighbours(MovingEntity centralEntity, double radius)
        {
            foreach (MovingEntity entity in movingEntities)
            {
                // Clear current tag.
                entity.Tag = false;

                // Calculate the difference in space
                Vector2D difference = entity.Pos.Clone().Sub(centralEntity.Pos);

                // When the entity is in range it gets tageed.
                if (entity != centralEntity && difference.LengthSquared() < radius * radius)
                    entity.Tag = true;
            }
        }

        public List<MovingEntity> GetMovingEntities() => movingEntities;


        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in movingEntities)
            {
                me.SteeringBehaviour = new FlockingBehaviour(me);
                me.Update(timeElapsed);
            }  
        }

        public void Render(Graphics g)
        {
            movingEntities.ForEach(e => e.Render(g));
            staticEntities.ForEach(s => s.Render(g));
            Target.Render(g);
        }
    }
}
