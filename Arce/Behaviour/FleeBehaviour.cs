using Arce.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class FleeBehaviour : SteeringBehaviour
    {
        public FleeBehaviour(DynamicGameEntity dynamicEntity) : base(dynamicEntity)
        {
        }

        public override Vector2 Calculate() => throw new NotImplementedException();
    }
}
