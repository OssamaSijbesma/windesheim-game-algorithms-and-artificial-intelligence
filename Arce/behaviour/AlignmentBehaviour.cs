using Arce.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class AlignmentBehaviour : SteeringBehaviour
    {
        private Vector2D averageHeading;
        private int neighbourCount = 0;

        public AlignmentBehaviour(MovingEntity movingEntity) : base(movingEntity)
        {
        }

        public override Vector2D Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
