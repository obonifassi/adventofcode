using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;

namespace adventofcode.Y2020.Day11
{
    [DisplayNameAttribute("Seating System")]
    public class Solution : BaseSolution, ISolver
    {
        public IEnumerable<Tuple<long, long>> Solve(string input)
        {
            var partOne = Decorator(input, PartOne);
            yield return partOne;

            var partTwo = Decorator(input, PartTwo);
            yield return partTwo;
        }

        long PartOne(string input)
        {
            var items = input.Split("\r\n").Select(x => x.ToCharArray()).ToArray();

            //TODO: add a cache layer after seats are set
            //If we see the seat arrangement again, we must have settled the arrangement
            //Consider counting the seats
            for(int i = 0; i < items.Length - 1; i++)
            {
                var row = items[i];

                for(int j = 0; j < row.Length - 1; j++)
                {
                    bool IsValid = true;

                    //If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                    if (row[j] == 'L')
                    {
                        IsValid = CheckAdjacentSeats(items, i, j, '#');

                        if (IsValid)
                        {
                            items[i][j] = '#'; //seat becomes occupied
                        }
                    }
                    else if(row[j] == '#')
                    {
                        //If a seat is occupied(#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                        IsValid = CheckAdjacentSeats(items, i, j, '#');

                        if (IsValid)
                        {
                            items[i][j] = 'L'; //seat becomes empty
                        }
                    }
                }
            }


            return -1;
        }
        
        bool CheckAdjacentSeats(char[][] items, int i, int j, char characterToCheck)
        {
            bool IsValid = true;
            int runningCounter = 0;
            var row = items[i];

            //check right
            if ((j + 1 < row.Length - 1) && row[j + 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check left
            if (j - 1 > 0 && row[j - 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check down
            if ((i + 1 < items.Length - 1) && items[i + 1][j] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check up
            if ((i - 1 > 0) && items[i - 1][j] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check right diagonal
            if ((i - 1 > 0) && (j + 1 < row.Length - 1) && items[i - 1][j + 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check left diagonal
            if ((i + 1 < items.Length - 1) && (j - 1 > 0) && items[i + 1][j - 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check right reverse diagonal
            if ((i + 1 < items.Length - 1) && (j + 1 < row.Length - 1) && items[i + 1][j + 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check left reverse diagonal
            if ((i - 1 > 0) && (j - 1 > 0) && items[i - 1][j - 1] == '#')
            {
                IsValid = false;
                runningCounter++;
            }

            return IsValid || (runningCounter >= 4);
        }
        long PartTwo(string input)
        {
            return -1;
        }
    }
}