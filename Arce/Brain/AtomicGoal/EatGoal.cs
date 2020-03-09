using Arce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    class EatGoal : IGoal
    {
        public GoalStatus GoalStatus { get; set; }

        private DynamicGameEntity dynamicGameEntity;
        private float oldMaxSpeed;

        public EatGoal(DynamicGameEntity dynamicGameEntity)
        {
            this.dynamicGameEntity = dynamicGameEntity;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;
            oldMaxSpeed = dynamicGameEntity.MaxSpeed;
            //dynamicGameEntity.MaxSpeed = 0;
        }

        public GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive) Activate();
            if (dynamicGameEntity.Hunger >= 10) Terminate();
            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed) return GoalStatus;

            dynamicGameEntity.Hunger += 1;

            return GoalStatus;
        }

        public void Terminate()
        {
            dynamicGameEntity.MaxSpeed = oldMaxSpeed;
            GoalStatus = GoalStatus.Completed;
        }

        public override string ToString()
        {
            return "Eat";
        }
    }
}
