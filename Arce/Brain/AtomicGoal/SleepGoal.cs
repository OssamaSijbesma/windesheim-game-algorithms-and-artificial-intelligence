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

        private ConsciousGameEntity entity;
        private float oldMaxSpeed;
        private int ticks;

        public SleepGoal(ConsciousGameEntity entity) 
        {
            this.entity = entity;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;
            oldMaxSpeed = entity.MaxSpeed;
            entity.MaxSpeed = 0;
        }

        public GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive) Activate();
            if (entity.Sleep >= 10) Terminate();
            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed) return GoalStatus;

            ticks++;
            if (ticks >= 50) entity.Sleep++;

            return GoalStatus;
        }

        public void Terminate()
        {
            entity.MaxSpeed = oldMaxSpeed;
            GoalStatus = GoalStatus.Completed;
        }

        public override string ToString()
        {
            return "Sleep";
        }
    }
}
