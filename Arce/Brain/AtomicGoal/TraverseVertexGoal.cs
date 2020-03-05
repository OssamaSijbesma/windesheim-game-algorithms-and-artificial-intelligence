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

        private DynamicGameEntity dynamicGameEntity;
        private Vector2 target;
        private int vertexCount;


        public TraverseVertexGoal(DynamicGameEntity dynamicGameEntity, Vector2 target, int vertexCount)
        {
            this.dynamicGameEntity = dynamicGameEntity;
            this.target = target;
            this.vertexCount = vertexCount;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed) return GoalStatus;
            if (GoalStatus == GoalStatus.Inactive) Activate();


            // Remove waypoints if the hero gets close
            if (Vector2.Subtract(target, dynamicGameEntity.Pos).Length() < 16)
                GoalStatus = GoalStatus.Completed;



            // Decide what behaviour is fitting
            switch (vertexCount)
            {
                case 10: case 9: case 8: case 7:
                    dynamicGameEntity.SteeringBehaviour = new ArriveBehaviour(dynamicGameEntity, target, Decelaration.Fast);
                    break;
                case 6: case 5: case 4:
                    dynamicGameEntity.SteeringBehaviour = new ArriveBehaviour(dynamicGameEntity, target, Decelaration.Normal);
                    break;
                case 3: case 2: case 1:
                    dynamicGameEntity.SteeringBehaviour = new ArriveBehaviour(dynamicGameEntity, target, Decelaration.Slow);
                    break;
                default:
                    dynamicGameEntity.SteeringBehaviour = new SeekBehaviour(dynamicGameEntity, target);
                    break;
            }

            return GoalStatus;
        }

        public void Terminate()
        {
        }
    }
}
