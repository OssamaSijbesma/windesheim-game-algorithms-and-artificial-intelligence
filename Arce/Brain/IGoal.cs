using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    enum GoalStatus { Inactive, Active, Completed, Failed }

    interface IGoal
    {
        GoalStatus GoalStatus { get; set; }

        void Activate();

        GoalStatus Process();

        void Terminate();
    }
}
