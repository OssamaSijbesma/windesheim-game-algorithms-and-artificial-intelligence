using Arce.Entity;
using Arce.NavigationGraph;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System;
using System.Collections.Generic;

namespace Arce
{
    class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private static GameWorld instance = new GameWorld();

        private TiledMap map;
        private Graph navigationGraph;
        private TiledMapRenderer mapRenderer;
        private List<StaticGameEntity> staticEntities = new List<StaticGameEntity>();
        private List<DynamicGameEntity> dynamicEntities = new List<DynamicGameEntity>();

        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);

            // Adjust Size
            graphics.PreferredBackBufferWidth = 1760;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        public static GameWorld Instance => instance;

        internal List<DynamicGameEntity> GetMovingEntities() => dynamicEntities;


        protected override void Initialize()
        {
            // Load the compiled map
            map = Content.Load<TiledMap>("TiledMap/structure");

            // Generate the graph with the map
            navigationGraph = new Graph(map);

            // Make sure the graphicsdevice 
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            // Create the map renderer
            mapRenderer = new TiledMapRenderer(GraphicsDevice);

            // Display the mouse
            this.IsMouseVisible = true;

            dynamicEntities.Add(new DynamicGameEntity(new Vector2(230, 200)));
            dynamicEntities.Add(new DynamicGameEntity(new Vector2(201, 180)));
            dynamicEntities.Add(new DynamicGameEntity(new Vector2(222, 160)));
            dynamicEntities.Add(new DynamicGameEntity(new Vector2(213, 210)));
            dynamicEntities.Add(new DynamicGameEntity(new Vector2(204, 200)));
            dynamicEntities.Add(new DynamicGameEntity(new Vector2(205, 201)));
            dynamicEntities.Add(new DynamicGameEntity(new Vector2(205, 202)));
            dynamicEntities.Add(new DynamicGameEntity(new Vector2(205, 240)));
            dynamicEntities.Add(new DynamicGameEntity(new Vector2(205, 207)));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update the map
            mapRenderer.Update(map, gameTime);

            // Update the dynamic entity
            dynamicEntities.ForEach(d => d.Update((float)gameTime.ElapsedGameTime.TotalSeconds * 0.8F));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Clear the screen
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the map
            mapRenderer.Draw(map);

            // Draw the entities
            spriteBatch.Begin();
            staticEntities.ForEach(s => s.Draw(spriteBatch));
            dynamicEntities.ForEach(d => d.Draw(spriteBatch));
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void TagNeighbours(DynamicGameEntity centralEntity, double radius)
        {
            foreach (DynamicGameEntity entity in dynamicEntities)
            {
                // Clear current tag.
                entity.Tag = false;

                // Calculate the difference in space
                Vector2 difference = Vector2.Subtract(entity.Pos, centralEntity.Pos);

                // When the entity is in range it gets tageed.
                if (entity != centralEntity && difference.LengthSquared() < radius * radius)
                    entity.Tag = true;
            }
        }

    }
}
