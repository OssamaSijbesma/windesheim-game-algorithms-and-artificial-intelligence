using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture03_Finite_State_Machine.States
{
    class Patrol : State
    {
        public Patrol(Character character) : base(character)
        { }

        public override void Enter()
        {
            Console.WriteLine("On patrol...");
        }

        public override void Execute()
        {
            Character.strength++;
            Console.WriteLine("Patroling");

            if (Character.enemyClose)
                if (Character.strength > 5)
                    Character.ChangeState(new Attack(Character));
                else
                    Character.ChangeState(new Hide(Character));
        }

        public override void Exit()
        {
        }
    }
}
