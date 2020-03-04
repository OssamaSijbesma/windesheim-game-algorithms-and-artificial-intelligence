using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    interface Goal
    {
        void Activate();
        int Process();
        void Terminate();
    }
}
