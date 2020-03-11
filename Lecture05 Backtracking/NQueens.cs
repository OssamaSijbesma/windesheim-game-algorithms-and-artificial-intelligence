using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lecture05_Backtracking
{
    class NQueens
    {
        private bool [,] queens;
        private int n;

        public bool ShowSolutions { get; set; }

        // initializes the board
        public NQueens(int n)
        {
            queens = new bool[n,n];
            this.n = n;
        }

        // driver method
        public void SolveBacktracking()
        {
            SolveBacktracking(0);
        }

        private void SolveBacktracking(int row)
        {
            // to do
        }

        /********** Helper methods **********/

        // counts the total number of queens on the board
        private int CountQueens()
        {
            return (from bool val in queens
                 select val)
                 .Count(q => q);
        }

        // check if there is no conflicting situation on the board
        private bool CheckBoard()
        {
            // iterate through all rows
            for (int r = 0; r < n; r++)
            {
                int queenCol = -1;
                int nrOfQueens = 0;

                // check horizontally
                for (int c = 0; c < n; c++)
                {
                    if (queens[r,c])
                    {
                        nrOfQueens++;
                        queenCol = c;
                    }
                    if (nrOfQueens > 1)
                        return false;
                    if (nrOfQueens > 0)
                    {
                        // check column
                        for (int qr = r + 1; qr < n; qr++) //start from next row
                        {
                            if (queens[qr,queenCol])
                                return false; // there is another queen on this column
                        }

                        // check diagonal -> r
                        int dc = queenCol + 1;
                        for (int qr = r + 1; qr < n && dc < n; qr++) //start from next row
                        {
                            if (queens[qr,dc])
                                return false; // there is another queen on this column
                            dc++;
                        }
                        // check diagonal -> l
                        dc = queenCol - 1;
                        for (int qr = r + 1; qr < n && dc >= 0; qr++) //start from next row
                        {
                            if (queens[qr,dc])
                                return false; // there is another queen on this column
                            dc--;
                        }
                    }
                }
            }
            return true;
        }

        private string ToString(int row)
        {
            string s = "";
            for (int col = 0; col < n; col++)
            {
                string temp = queens[row,col] ? "Q " : "- ";
                s += temp;
            }
            return s;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int row = 0; row < n; row++)
            {
                sb.AppendLine(ToString(row));
            }
            return sb.ToString();
        }

    }
}
