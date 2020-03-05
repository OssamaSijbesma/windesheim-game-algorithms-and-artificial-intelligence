using Arce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    class SeekGoal : IGoal
    {
        public GoalStatus GoalStatus { get; set; }

        public SeekGoal(DynamicGameEntity dynamicGameEntity)
        {

        }

        public void Activate()
        {
            GoalStatus = GoalStatus.Active;

            throw new NotImplementedException();
        }

        public GoalStatus Process()
        {
            throw new NotImplementedException();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
