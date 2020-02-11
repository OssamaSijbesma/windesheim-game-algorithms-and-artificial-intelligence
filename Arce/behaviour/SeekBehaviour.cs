using Arce.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            return SteeringBehaviours.Seek(MovingEntity, MovingEntity.MyWorld.Target.Pos);
        }
    }
}
