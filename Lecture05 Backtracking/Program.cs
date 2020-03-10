using System;

namespace Lecture05_Backtracking
{
    class Program
    {
        static void Main(string[] args)
        {
            NQueens nQueens = new NQueens(4);
            nQueens.SolveBacktracking();
        }
    }
}
