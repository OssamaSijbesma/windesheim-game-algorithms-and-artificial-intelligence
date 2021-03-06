﻿using Arce.Entity;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Behaviour
{
    class FlockingBehaviour : SteeringBehaviour
    {
        private float maxSteeringForce;
        private float separationAmount;
        private float cohesionAmount;
        private float alignmentAmount;
        private float wanderAmount;


        public FlockingBehaviour(DynamicGameEntity dynamicEntity) : base(dynamicEntity)
        {
            maxSteeringForce = 5.0F;
            separationAmount = 140.0F;
            cohesionAmount = 1.0F;
            alignmentAmount = 1.0F;
            wanderAmount = 1.0F;
        }

        public override Vector2 Calculate()
        {
            Vector2 steeringForce = new Vector2(0, 0);

            DynamicEntity.entityManager.TagNeighbours(DynamicEntity, 100);
            DynamicEntity.entityManager.EnforceNonPenetrationConstraint(DynamicEntity);
            List<DynamicGameEntity> entities = DynamicEntity.entityManager.GetMovingEntities();

            steeringForce += Vector2.Multiply(SteeringBehaviours.Cohesion(DynamicEntity, entities), cohesionAmount);
            steeringForce += Vector2.Multiply(SteeringBehaviours.Alignment(DynamicEntity, entities), alignmentAmount);
            steeringForce += Vector2.Multiply(SteeringBehaviours.Separation(DynamicEntity, entities), separationAmount);
            steeringForce += Vector2.Multiply(SteeringBehaviours.Wander(DynamicEntity, 20, 5, 5), wanderAmount);

            return steeringForce.Truncate(maxSteeringForce);
        }
    }
}
