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


        public TraverseVertexGoal(DynamicGameEntity dynamicGameEntity, Vector2 target)
        {
            this.dynamicGameEntity = dynamicGameEntity;
            this.target = target;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public GoalStatus Process()
        {
            Activate();

            dynamicGameEntity.SteeringBehaviour = new SeekBehaviour(dynamicGameEntity, target);
            /*
            // Decide what behaviour is fitting
            switch (Targets.Count)
            {
                case 9:
                case 8:
                case 7:
                    dynamicGameEntity.SteeringBehaviour = new ArriveBehaviour(dynamicGameEntity, Decelaration.Fast);
                    break;
                case 6:
                case 5:
                case 4:
                    dynamicGameEntity.SteeringBehaviour = new ArriveBehaviour(dynamicGameEntity, Decelaration.Normal);
                    break;
                case 3:
                case 2:
                case 1:
                    dynamicGameEntity.SteeringBehaviour = new ArriveBehaviour(dynamicGameEntity, Decelaration.Slow);
                    break;
                default:
                    break;
            }
            */

            return GoalStatus;
        }

        public void Terminate()
        {
        }
    }
}
