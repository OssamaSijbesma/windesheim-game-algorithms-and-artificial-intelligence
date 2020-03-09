using Arce.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    class ThinkGoal : CompositeGoal
    {
        private Vector2 oldTarget;

        public ThinkGoal(DynamicGameEntity dynamicGameEntity) : base(dynamicGameEntity)
        { }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive) Activate();

            // Remove all completed subgoals
            Subgoals.RemoveAll(g => g.GoalStatus == GoalStatus.Completed || g.GoalStatus == GoalStatus.Failed);

            if (!Subgoals.OfType<VitalityGoal>().Any())
                AddSubgoal(new VitalityGoal(DynamicEntity));

            // If the target changes start a follow target goal
            if (oldTarget != GameWorld.Instance.Target)
            {
                AddSubgoal(new FollowTargetGoal(DynamicEntity));
                oldTarget = GameWorld.Instance.Target;
            }

            // Process all subgoals
            Subgoals.ForEach(g => g.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
        }

        public override string ToString()
        {
            return "Think" + Environment.NewLine + base.ToString();
        }
    }
}
