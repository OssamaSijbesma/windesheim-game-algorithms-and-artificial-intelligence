using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Entity
{
    class SheepFactory: IDynamicGameEntityFactory
    {
        private EntityManager entityManager;
        private Random random;

        public SheepFactory(EntityManager entityManager) 
        {
            this.entityManager = entityManager;
            random = new Random();
        }
        public List<DynamicGameEntity> CreateEntities(Vector2 position, int radius, int amount)
        {
            List<DynamicGameEntity> dynamicGameEntities = new List<DynamicGameEntity>();
            int xMin = (int)position.X - radius;
            int xMax = (int)position.X + radius;
            int yMin = (int)position.Y - radius;
            int yMax = (int)position.Y + radius;

            for (int i = 0; i < amount; i++)
                dynamicGameEntities.Add(CreateEntity(new Vector2(random.Next(xMin, xMax), random.Next(yMin, yMax))));

            return dynamicGameEntities;
        }

        public DynamicGameEntity CreateEntity(Vector2 position)
        {
            return new Sheep(entityManager, position);
        }
    }
}
