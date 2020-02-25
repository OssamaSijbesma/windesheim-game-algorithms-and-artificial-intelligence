using Lecture03_Finite_State_Machine.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Lecture03_Finite_State_Machine
{
    class Character
    {
        public int strength = 10;
        public bool enemyClose = false;
        private State state;
        private Random random;

        public Character()
        {
            random = new Random();

            state = new Patrol(this);
        }

        public void ChangeState(State newState)
        {
            state.Exit();
            state = newState;
            state.Enter();
        }

        public void Update(object sender, ElapsedEventArgs e)
        {
            state.Execute();
            enemyClose = random.NextDouble() > 0.75;
        }
    }
}
