using Arce.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TiledLib;
using TiledLib.Layer;

namespace Arce.world
{
    class WorldMap
    {
        private static readonly WorldMap instance = new WorldMap();
        private readonly string path;
        private readonly Bitmap spriteSheet;

        private Bitmap[] sprites;
        private List<MapTile> tiles;

        private WorldMap() 
        {
            path = "../../assets/map/";
            spriteSheet = new Bitmap(path + "spritesheet.png");

            // Reads the file and creates the tiles
            using (FileStream stream = File.OpenRead(path + "structure.tmx"))
            {
                Map map = Map.FromStream(stream, ts => File.OpenRead(Path.Combine(Path.GetDirectoryName(path + "structure.tmx"), ts.Source)));

                // Calculate the array sizes
                sprites = new Bitmap[(spriteSheet.Width / 16) * (spriteSheet.Height / 16)];
                tiles = new List<MapTile>();

                // Preload the tile images
                for (int y = 0, i = 0; y < spriteSheet.Height; y += 16)
                    for (int x = 0; x < spriteSheet.Width; x += 16, i++)
                        sprites[i] = GameUtil.cropAtRect(spriteSheet, x, y);

                foreach (TileLayer layer in map.Layers.OfType<TileLayer>())
                {
                    for (int y = 0, i = 0; y < layer.Height; y++)
                        for (int x = 0; x < layer.Width; x++, i++)
                        {
                            int gid = layer.Data[i];

                            if (gid == 0)
                                continue;

                            tiles.Add(new MapTile(new Vector2D(x * 16, y * 16), sprites[gid - 1]));
                        }
                }
            }
        }

        public static WorldMap Instance
        {
            get => instance;
        }

        internal void Render(Graphics graphics) => tiles.ForEach(s => s.Render(graphics));

        public List<MapTile> GetTiles() => tiles;
    }
}
