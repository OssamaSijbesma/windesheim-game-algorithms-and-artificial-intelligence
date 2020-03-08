using Arce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    abstract class CompositeGoal : IGoal
    {
        public DynamicGameEntity DynamicEntity { get; set; }

        public GoalStatus GoalStatus { get; set; }

        public List<IGoal> Subgoals { get; set; }

        public CompositeGoal(DynamicGameEntity dynamicEntity)
        {
            DynamicEntity = dynamicEntity;
            Subgoals = new List<IGoal>();
        }

        public abstract void Activate();

        public abstract GoalStatus Process();

        public abstract void Terminate();

        public virtual void AddSubgoal(IGoal goal) => Subgoals.Add(goal);

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            Subgoals.ForEach(g => stringBuilder.AppendLine($"  {g.ToString()}"));
            return stringBuilder.ToString();
        }
    }
}
