using Arce.Entity;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.behaviour
{
    class FlockingBehaviour : SteeringBehaviour
    {
        private float maxSteeringForce;
        private float separationAmount;
        private float cohesionAmount;
        private float alignmentAmount;

        public FlockingBehaviour(DynamicGameEntity dynamicEntity) : base(dynamicEntity)
        {
            maxSteeringForce = 5.0F;
            separationAmount = 3.0F;
            cohesionAmount = 2.0F;
            alignmentAmount = 1.0F;
        }

        public override Vector2 Calculate()
        {
            Vector2 steeringForce = new Vector2(0, 0);

            GameWorld.Instance.TagNeighbours(DynamicEntity, 100);
            List<DynamicGameEntity> entities = GameWorld.Instance.GetMovingEntities();

            DynamicEntity.EnforceNonPenetrationConstraint(GameWorld.Instance.GetMovingEntities());

            steeringForce += Vector2.Multiply(SteeringBehaviours.Cohesion(DynamicEntity, entities), cohesionAmount);
            steeringForce += Vector2.Multiply(SteeringBehaviours.Alignment(DynamicEntity, entities), alignmentAmount);
            steeringForce += Vector2.Multiply(SteeringBehaviours.Separation(DynamicEntity, entities), separationAmount);
            Console.WriteLine(steeringForce);
            return steeringForce.Truncate(maxSteeringForce);
        }
    }
}
