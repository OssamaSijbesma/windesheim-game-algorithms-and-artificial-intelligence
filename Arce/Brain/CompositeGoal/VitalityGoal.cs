using Arce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    class VitalityGoal : CompositeGoal
    {
        public VitalityGoal(DynamicGameEntity dynamicEntity) :base(dynamicEntity)
        {
        
        }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            if (!Subgoals.OfType<EatGoal>().Any())
                AddSubgoal(new EatGoal(DynamicEntity));

            if (!Subgoals.OfType<SleepGoal>().Any())
                AddSubgoal(new SleepGoal(DynamicEntity));


            Subgoals.ForEach(g => g.Process());


            return GoalStatus;
        }

        public override void Terminate()
        {
        }

        public override string ToString()
        {
            return "Vitality" + Environment.NewLine + base.ToString();
        }
    }
}
