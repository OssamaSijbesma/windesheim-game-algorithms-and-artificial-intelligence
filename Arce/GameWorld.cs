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
        // Singleton
        private static GameWorld instance = new GameWorld();

        // Input states
        private MouseState mouseState = Mouse.GetState();
        private KeyboardState previousState = Keyboard.GetState();

        // Private variables
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private TiledMap map;
        private TiledMapRenderer mapRenderer;

        // Public variables
        public Graph navigationGraph;
        public EntityManager entityManager;
        public bool showGraph = false;
        public bool showInfo = false;

        public Vector2 Target = new Vector2(200, 200);

        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);

            // Adjust Size
            graphics.PreferredBackBufferWidth = 1760;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();

            // Set root directory
            Content.RootDirectory = "Content";
        }

        public static GameWorld Instance => instance;

        protected override void Initialize()
        {
            // Display the mouse
            this.IsMouseVisible = true;

            // Load the compiled map
            map = Content.Load<TiledMap>("TiledMap/structure");

            // Make sure the graphicsdevice 
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            // Create the map renderer
            mapRenderer = new TiledMapRenderer(GraphicsDevice);

            // Generate the graph with the map
            navigationGraph = new Graph(map);

            // Initialize the entity manager
            entityManager = new EntityManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load entity content
            entityManager.LoadContent(Content);
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

            // Show graph when G pressed
            if (Keyboard.GetState().IsKeyDown(Keys.I) && !previousState.IsKeyDown(Keys.I))
                showInfo = !showInfo;

            // Set target
            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
                Target = new Vector2(mouseState.X, mouseState.Y);

            // Update the map
            mapRenderer.Update(map, gameTime);

            // Update the entity managers
            entityManager.Update(gameTime);

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

            // Draw the naviagtion graph
            if (showGraph) navigationGraph.Draw(spriteBatch);

            // Draw the entities
            entityManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
