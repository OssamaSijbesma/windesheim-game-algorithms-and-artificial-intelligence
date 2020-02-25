using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture03_Finite_State_Machine.States
{
    class Hide : State
    {
        public Hide(Character character) : base(character)
        { }

        public override void Enter()
        {
            Console.WriteLine("Enemy in sight!");
        }

        public override void Execute()
        {
            Character.strength++;
            Console.WriteLine("Hiding...");

            if (!Character.enemyClose)
                Character.ChangeState(new Patrol(Character)); ;
        }

        public override void Exit()
        {
            Console.WriteLine("It's save again");
        }
    }
}
