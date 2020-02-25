using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.entity
{
    class ChickenFactory : MovingEntityFactory
    {
        Random random = new Random();

        public override List<MovingEntity> GetMovingEntities(int amount)
        {
            List <MovingEntity> chickens = new List<MovingEntity>();

            for (int i = 0; i < amount; i++)
            {
                chickens.Add(GetMovingEntity());
            }

            return chickens;
        }
        public override MovingEntity GetMovingEntity()
        {
            Vector2D pos = new Vector2D(random.Next(25, 750), random.Next(25, 750));
            return new Chicken(pos);
        }
    }
}
