﻿using Arce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    class ThinkGoal : CompositeGoal
    {
        public ThinkGoal(DynamicGameEntity dynamicGameEntity) : base(dynamicGameEntity)
        { }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive) Activate();

            if (GameWorld.Instance.Target != default && !Subgoals.OfType<FollowTargetGoal>().Any())
                AddSubgoal(new FollowTargetGoal(DynamicEntity));

            if (!Subgoals.OfType<VitalityGoal>().Any())
                AddSubgoal(new VitalityGoal(DynamicEntity));

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
