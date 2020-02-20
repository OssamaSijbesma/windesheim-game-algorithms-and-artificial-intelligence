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
        private static readonly string mapPath = "../../assets/map/";

        private List<MovingEntity> movingEntities = new List<MovingEntity>();
        private List<StaticEntity> staticEntities = new List<StaticEntity>();
        public Vehicle Target { get; set; }

        private World()
        {
            // Preload the tile images
            Bitmap image = new Bitmap(mapPath + "OutdoorsTileset.png", false);
            Bitmap[] bitmaps = new Bitmap[54];
            for (int y = 0, i = 0; y < image.Height; y += 16)
                for (int x = 0; x < image.Width; x += 16, i++)
                    bitmaps[i] = WorldBuilder.cropAtRect(image, x, y);

            // Reads the file and creates the tiles
            using (System.IO.FileStream stream = File.OpenRead(mapPath + "structure.tmx"))
            {
                Map map = Map.FromStream(stream, ts => File.OpenRead(Path.Combine(Path.GetDirectoryName(mapPath + "structure.tmx"), ts.Source)));

                foreach (TileLayer layer in map.Layers.OfType<TileLayer>())
                {
                    for (int y = 0, i = 0; y < layer.Height; y++)
                        for (int x = 0; x < layer.Width; x++, i++)
                        {
                            int gid = layer.Data[i];

                            if (gid == 0)
                                continue;

                            //ITileset tileset = map.Tilesets.Single(ts => gid >= ts.FirstGid && ts.FirstGid + ts.TileCount > gid);

                            //Tile tile = tileset[gid];

                            MapTile mapTile = new MapTile(new Vector2D(x * 16, y * 16), bitmaps[gid - 1]);
                            staticEntities.Add(mapTile);
                        }
                }
            }

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
