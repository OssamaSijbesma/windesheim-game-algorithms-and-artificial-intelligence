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
            // Remove all completed subgoals
            Subgoals.RemoveAll(g => g.GoalStatus == GoalStatus.Completed || g.GoalStatus == GoalStatus.Failed);

            // Activate goal when inactive
            if (GoalStatus == GoalStatus.Inactive) Activate();


            // Decide which atomic goal to choose or to stop
            if(Subgoals.Count == 0)
                if (DynamicEntity.Hunger >= 8 && DynamicEntity.Sleep >= 5) 
                    Terminate();
                else if (DynamicEntity.Hunger < DynamicEntity.Sleep)
                    AddSubgoal(new EatGoal(DynamicEntity));
                else
                    AddSubgoal(new SleepGoal(DynamicEntity));

            Subgoals.ForEach(g => g.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
            GoalStatus = GoalStatus.Completed;
        }

        public override string ToString()
        {
            return "Vitality" + Environment.NewLine + base.ToString();
        }
    }
}
