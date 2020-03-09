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
        private float oldMaxSpeed;
        private int ticks;

        public SleepGoal(DynamicGameEntity dynamicGameEntity) 
        {
            this.dynamicGameEntity = dynamicGameEntity;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;
            oldMaxSpeed = dynamicGameEntity.MaxSpeed;
            dynamicGameEntity.MaxSpeed = 0;
        }

        public GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive) Activate();
            if (dynamicGameEntity.Sleep >= 10) Terminate();
            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed) return GoalStatus;

            ticks++;
            if (ticks >= 50) dynamicGameEntity.Sleep++;

            return GoalStatus;
        }

        public void Terminate()
        {
            dynamicGameEntity.MaxSpeed = oldMaxSpeed;
            GoalStatus = GoalStatus.Completed;
        }

        public override string ToString()
        {
            return "Sleep";
        }
    }
}
