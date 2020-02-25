using Arce.behaviour;
using Arce.entity;
using Arce.entity.building;
using Arce.world;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledLib;
using TiledLib.Layer;

namespace Arce
{
    class World
    {
        private static World instance;
        private static readonly object padlock = new object();

        private List<MovingEntity> movingEntities = new List<MovingEntity>();
        private List<StaticEntity> staticEntities = new List<StaticEntity>();

        public Vehicle Target { get; set; }

        private World()
        {
            // Set tent
            Tent tent = new Tent(new Vector2D(280, 180));
            staticEntities.Add(tent);

            populate();
        }

        public static World Instance
        {
            get 
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new World();
                    return instance;
                }
            }
        }

        private void populate()
        {
            MovingEntityFactory chickenFactory = new ChickenFactory();
            movingEntities.AddRange(chickenFactory.GetMovingEntities(200));

            Target = new Vehicle(new Vector2D(100, 60))
            {
                VColor = Color.DarkRed,
                Pos = new Vector2D(100, 40)
            };
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
            staticEntities.ForEach(s => s.Render(g));
            movingEntities.ForEach(e => e.Render(g));
            Target.Render(g);
        }
    }
}
