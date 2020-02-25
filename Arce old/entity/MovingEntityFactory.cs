using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.entity
{
    abstract class MovingEntityFactory
    {
        public abstract List<MovingEntity> GetMovingEntities(int amount);
        public abstract MovingEntity GetMovingEntity();
    }
}
