using Arce.Entity.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity
{
    class EntityManager
    {
        // All entities
        private List<StaticGameEntity> staticEntities = new List<StaticGameEntity>();
        private List<DynamicGameEntity> dynamicEntities = new List<DynamicGameEntity>();

        // Entity content
        public Texture2D chickenTexture;
        public Texture2D sheepTexture;
        public Texture2D mageTexture;
        public Texture2D tentTexture;
        public SpriteFont font;

        public EntityManager() 
        {
            dynamicEntities.Add(new Hero(this, new Vector2(300, 300)));

            IDynamicGameEntityFactory sheepfactory = new SheepFactory(this);
            dynamicEntities.AddRange(sheepfactory.CreateEntities(new Vector2(230, 200), 20, 13));

            staticEntities.Add(new Tent(this, new Vector2(900, 300)));
        }

        public void LoadContent(ContentManager Content) 
        {
            // Load content
            chickenTexture = Content.Load<Texture2D>("chicken");
            sheepTexture = Content.Load<Texture2D>("sheep");
            mageTexture = Content.Load<Texture2D>("mage");
            tentTexture = Content.Load<Texture2D>("tent");
            font = Content.Load<SpriteFont>("info");
        }

        public void Update(GameTime gameTime) 
        {
            // Update the dynamic entity
            dynamicEntities.ForEach(d => d.Update((float)gameTime.ElapsedGameTime.TotalSeconds * 0.8F));
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            // Draw the entities
            spriteBatch.Begin();
            staticEntities.ForEach(s => s.Draw(spriteBatch));
            dynamicEntities.ForEach(d => d.Draw(spriteBatch));
            spriteBatch.End();
        }

        internal List<DynamicGameEntity> GetMovingEntities() => dynamicEntities;

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

        public void EnforceNonPenetrationConstraint(DynamicGameEntity centralEntity)
        {
            foreach (DynamicGameEntity entity in dynamicEntities)
            {
                //make sure we don't check against the individual
                if (entity == centralEntity) continue;

                // calculate the distance between the positions of the entities
                Vector2 ToEntity = Vector2.Subtract(centralEntity.Pos, entity.Pos);

                float distFromEachOther = ToEntity.Length();

                //if this distance is smaller than the sum of their radii then this entity must be moved away in the direction parallel to the ToEntity vector
                float amountOfOverlap = 10 + 10 - distFromEachOther;

                //move the entity a distance away equivalent to the amount of overlap
                if (amountOfOverlap >= 0)
                    centralEntity.Pos += Vector2.Multiply(Vector2.Divide(ToEntity, distFromEachOther), amountOfOverlap);
            }
        }

    }
}
