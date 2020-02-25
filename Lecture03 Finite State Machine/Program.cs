using System;
using System.Diagnostics;
using System.Timers;

namespace Lecture03_Finite_State_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            Character character = new Character();

            Timer timer = new Timer(1000);
            timer.AutoReset = true; 
            timer.Elapsed += character.Update;
            timer.Start();
            Console.ReadLine();
        }
    }
}
