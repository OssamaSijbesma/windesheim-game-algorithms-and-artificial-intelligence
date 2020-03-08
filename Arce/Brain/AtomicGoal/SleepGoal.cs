using Arce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    class SleepGoal : IGoal
    {
        public GoalStatus GoalStatus { get; set; }

        private DynamicGameEntity dynamicGameEntity;

        public SleepGoal(DynamicGameEntity dynamicGameEntity) 
        {
            this.dynamicGameEntity = dynamicGameEntity;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed) return GoalStatus;
            if (GoalStatus == GoalStatus.Inactive) Activate();

            return GoalStatus;
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Sleep";
        }
    }
}
