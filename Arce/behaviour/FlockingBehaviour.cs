using Arce.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class FlockingBehaviour : SteeringBehaviour
    {
        private double maxSteeringForce;
        private double separationAmount;
        private double cohesionAmount;
        private double alignmentAmount;

        public FlockingBehaviour(MovingEntity movingEntity) : base(movingEntity)
        {
            maxSteeringForce = 5;
            separationAmount = 100;
            cohesionAmount = 2;
            alignmentAmount = 1;
        }

        public override Vector2D Calculate()
        {
            Vector2D steeringForce = new Vector2D(0, 0);
            World.Instance.TagNeighbours(MovingEntity, 100);
            List<MovingEntity> entities = World.Instance.GetMovingEntities();

            steeringForce.Add(SteeringBehaviours.Cohesion(MovingEntity, entities).Multiply(cohesionAmount));
            steeringForce.Add(SteeringBehaviours.Alignment(MovingEntity, entities).Multiply(alignmentAmount));
            steeringForce.Add(SteeringBehaviours.Separation(MovingEntity, entities).Multiply(separationAmount));

            return steeringForce.Truncate(maxSteeringForce);
        }
    }
}
