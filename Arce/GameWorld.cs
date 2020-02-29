using Arce.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System.Collections.Generic;

namespace Arce
{
    public class GameWorld : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private TiledMap map;
        private TiledMapRenderer mapRenderer;
        private List<StaticGameEntity> staticEntities = new List<StaticGameEntity>();
        private List<DynamicGameEntity> dynamicEntities = new List<DynamicGameEntity>();

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);

            // Adjust Size
            graphics.PreferredBackBufferWidth = 1760;
            graphics.PreferredBackBufferHeight = 960;
            graphics.ApplyChanges();


            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // Load the compiled map
            map = Content.Load<TiledMap>("TiledMap/structure");

            // Make sure the graphicsdevice 
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            // Create the map renderer
            mapRenderer = new TiledMapRenderer(GraphicsDevice);

            // Display the mouse
            this.IsMouseVisible = true;

            dynamicEntities.Add(new DynamicGameEntity(new Vector2(20, 20)));

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

            spriteBatch.Begin();
            // Draw the entities
            staticEntities.ForEach(s => s.Draw(spriteBatch));
            dynamicEntities.ForEach(d => d.Draw(spriteBatch));
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /*
        public void TagNeighbours(DynamicGameEntity centralEntity, double radius)
        {
            foreach (DynamicGameEntity entity in movingEntities)
            {
                // Clear current tag.
                entity.Tag = false;

                // Calculate the difference in space
                Vector2 difference = entity.Pos.Clone().Sub(centralEntity.Pos);

                // When the entity is in range it gets tageed.
                if (entity != centralEntity && difference.LengthSquared() < radius * radius)
                    entity.Tag = true;
            }
        }
        */

    }
}
