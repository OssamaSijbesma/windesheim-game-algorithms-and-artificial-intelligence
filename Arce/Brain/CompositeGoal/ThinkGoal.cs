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

            // If hunger or sleep is below a certain point attent to vitality
            if (!Subgoals.OfType<VitalityGoal>().Any() && (DynamicEntity.Hunger < 5 || DynamicEntity.Sleep < 3))
                AddSubgoal(new VitalityGoal(DynamicEntity));

            // If the target changes follow the new target
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
