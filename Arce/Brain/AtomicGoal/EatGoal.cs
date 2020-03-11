using Arce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Arce.Brain
{
    class EatGoal : IGoal
    {
        public GoalStatus GoalStatus { get; set; }

        private ConsciousGameEntity entity;
        private float oldMaxSpeed;
        private Timer timer;

        public EatGoal(ConsciousGameEntity entity)
        {
            this.entity = entity;
        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;
            oldMaxSpeed = entity.MaxSpeed;
            entity.MaxSpeed = 0;

            timer = new Timer();
            timer.Interval = 1000;
            timer.AutoReset = true;
            timer.Elapsed += Eat;
            timer.Enabled = true;
        }

        public GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive) Activate();
            if (entity.Hunger >= 1) Terminate();
            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed) return GoalStatus;

            return GoalStatus;
        }

        public void Terminate()
        {
            timer.Stop();
            entity.MaxSpeed = oldMaxSpeed;
            GoalStatus = GoalStatus.Completed;
        }

        private void Eat(Object source, ElapsedEventArgs e) => entity.Hunger += 0.1f;

        public override string ToString()
        {
            return "Eat";
        }
    }
}
