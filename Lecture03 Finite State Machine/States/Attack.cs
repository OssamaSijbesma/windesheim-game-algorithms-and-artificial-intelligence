using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture03_Finite_State_Machine.States
{
    class Attack : State
    {
        public Attack(Character character) : base(character) 
        { }

        public override void Enter()
        {
            Console.WriteLine("Start the fight!");
        }

        public override void Execute()
        {
            Character.strength--;
            Console.WriteLine("Fighting");

            if (Character.strength < 5)
                Character.ChangeState(new Hide(Character));
        }

        public override void Exit()
        {
            Console.WriteLine("I give up...");
        }
    }
}
