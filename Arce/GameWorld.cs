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
        public Graph navigationGraph;
        private TiledMapRenderer mapRenderer;
        private List<StaticGameEntity> staticEntities = new List<StaticGameEntity>();
        private List<DynamicGameEntity> dynamicEntities = new List<DynamicGameEntity>();

        private MouseState mouseState = Mouse.GetState();
        private KeyboardState previousState =  Keyboard.GetState();

        private bool showGraph = false;

        public Vector2 Target = new Vector2(200, 200);
        public Texture2D chickenTexture;
        public Texture2D sheepTexture;
        public Texture2D mageTexture;

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

            dynamicEntities.Add(new Hero(new Vector2(20, 20)));
            dynamicEntities.Add(new Sheep(new Vector2(230, 200)));
            dynamicEntities.Add(new Sheep(new Vector2(201, 180)));
            dynamicEntities.Add(new Sheep(new Vector2(222, 160)));
            dynamicEntities.Add(new Sheep(new Vector2(213, 210)));
            dynamicEntities.Add(new Sheep(new Vector2(204, 200)));
            dynamicEntities.Add(new Sheep(new Vector2(205, 201)));
            dynamicEntities.Add(new Sheep(new Vector2(205, 202)));
            dynamicEntities.Add(new Sheep(new Vector2(205, 240)));
            dynamicEntities.Add(new Sheep(new Vector2(205, 207)));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load textures
            chickenTexture = Content.Load<Texture2D>("chicken");
            sheepTexture = Content.Load<Texture2D>("sheep");
            mageTexture = Content.Load<Texture2D>("mage");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Exit game when Escape pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Show graph when G pressed
            if (Keyboard.GetState().IsKeyDown(Keys.G) && !previousState.IsKeyDown(Keys.G))
                showGraph = !showGraph;

            // Set target
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
                Target = new Vector2(mouseState.X, mouseState.Y);

            // Update the map
            mapRenderer.Update(map, gameTime);

            // Update the dynamic entity
            dynamicEntities.ForEach(d => d.Update((float)gameTime.ElapsedGameTime.TotalSeconds * 0.8F));

            // Record previous state
            previousState = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Clear the screen
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the map
            mapRenderer.Draw(map);

            // Draw NavGraph when 
            if (showGraph) navigationGraph.Draw(spriteBatch);

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
