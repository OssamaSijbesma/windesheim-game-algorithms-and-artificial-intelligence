using Arce.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Behaviour
{
    abstract class SteeringBehaviour
    {
        public DynamicGameEntity DynamicEntity { get; set; }

        public abstract Vector2 Calculate();

        public SteeringBehaviour(DynamicGameEntity dynamicEntity)
        {
            DynamicEntity = dynamicEntity;
        }
    }
}
