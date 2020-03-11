using Arce.Behaviour;
using Arce.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    class TraverseVertexGoal : IGoal
    {
        public GoalStatus GoalStatus { get; set; }

        private ConsciousGameEntity entity;
        private Vector2 target;
        private int vertexCount;

        public TraverseVertexGoal(ConsciousGameEntity entity, Vector2 target, int vertexCount)
        {
            this.entity = entity;
            this.target = target;
            this.vertexCount = vertexCount;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public GoalStatus Process()
        {
            // Activate goal when inactive
            if (GoalStatus == GoalStatus.Inactive) Activate();

            // When completed or failed return
            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed) return GoalStatus;

            // Remove waypoints if the hero gets close
            if (Vector2.Subtract(target, entity.Pos).Length() < 16)
                Terminate();

            // Decide what behaviour is fitting
            switch (vertexCount)
            {
                case 10: case 9: case 8: case 7:
                    entity.SteeringBehaviour = new ArriveBehaviour(entity, target, Decelaration.Fast);
                    break;
                case 6: case 5: case 4:
                    entity.SteeringBehaviour = new ArriveBehaviour(entity, target, Decelaration.Normal);
                    break;
                case 3: case 2: case 1:
                    entity.SteeringBehaviour = new ArriveBehaviour(entity, target, Decelaration.Slow);
                    break;
                default:
                    entity.SteeringBehaviour = new SeekBehaviour(entity, target);
                    break;
            }

            return GoalStatus;
        }

        public void Terminate()
        {
            GoalStatus = GoalStatus.Completed;
            entity.Hunger -= 0.01f;
        }

        public override string ToString()
        {
            return $"Traverse Vertex {target}";
        }
    }
}
