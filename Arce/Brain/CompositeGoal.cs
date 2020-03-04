using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    abstract class CompositeGoal : Goal
    {

        public abstract void Activate();

        public abstract int Process();

        public abstract void Terminate();

        public virtual void AddSubgoal(Goal goal) 
        {
        
        }
    }
}
