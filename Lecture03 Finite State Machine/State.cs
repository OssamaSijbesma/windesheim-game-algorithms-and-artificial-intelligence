using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture03_Finite_State_Machine
{
    abstract class State
    {
        public Character Character;

        public State(Character character)
        {
            this.Character = character;
        }

        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();
    }
}
