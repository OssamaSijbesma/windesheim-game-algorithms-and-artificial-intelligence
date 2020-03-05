using Arce.Entity;
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
            throw new NotImplementedException();
        }

        public override GoalStatus Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
