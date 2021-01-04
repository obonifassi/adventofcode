using System;
using System.Collections.Generic;
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
            var inputItems = input.Split("\r\n").Select(x => x.ToCharArray()).ToArray();
            var currentIteration = inputItems.Select(a => a.ToArray()).ToArray();

            bool isStable = false;
            var debugRunningCounter = 0;
            while (!isStable)
            {
                var (nextIteration, stable) = GetNextIteration(currentIteration);
                isStable = stable;
                currentIteration = nextIteration;
                debugRunningCounter++;
            }

            //determine final count
            var seatOccupiedCount = 0;
            for (int i = 0; i < currentIteration.Length; i++)
            {
                for (int j = 0; j < currentIteration[i].Length; j++)
                {
                    if (currentIteration[i][j] == '#')
                    {
                        seatOccupiedCount++;
                    }
                }
            }

            return seatOccupiedCount;
        }

        (char[][], bool) GetNextIteration(char[][] items)
        {
            bool IsStable = true;

            char[][] itemsToUpdate = new char[items.Length][];

            for (int i = 0; i <= items.Length - 1; i++)
            {
                var row = items[i];

                char[] rowToUpdate = new char[row.Length];

                for (int j = 0; j <= row.Length - 1; j++)
                {
                    bool IsValid;

                    if (row[j] == 'L')
                    {
                        rowToUpdate[j] = 'L';

                        IsValid = CheckAdjacentSeats(items, i, j, '#');

                        if (IsValid)
                        {
                            rowToUpdate[j] = '#'; //seat becomes occupied
                            IsStable = false; //we know we are not stable, when a change was required
                        }
                    }
                    else if (row[j] == '#')
                    {
                        rowToUpdate[j] = '#';

                        IsValid = CheckAdjacentSeats(items, i, j, '#', true);

                        if (IsValid)
                        {
                            rowToUpdate[j] = 'L'; //seat becomes empty
                            IsStable = false; //we know we are not stable, when a change was required
                        }
                    }
                    else
                    {
                        rowToUpdate[j] = '.';
                    }
                }

                itemsToUpdate[i] = rowToUpdate;
            }

            return (itemsToUpdate, IsStable);
        }

        bool CheckAdjacentSeats(char[][] items, int i, int j, char characterToCheck, bool checkRunningCounter = false)
        {
            bool IsValid = true;
            int runningCounter = 0;
            var row = items[i];

            //check right
            if ((j + 1 <= row.Length - 1) && row[j + 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check left
            if (j - 1 >= 0 && row[j - 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check down
            if ((i + 1 <= items.Length - 1) && items[i + 1][j] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check up
            if ((i - 1 >= 0) && items[i - 1][j] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check right diagonal
            if ((i - 1 >= 0) && (j + 1 <= row.Length - 1) && items[i - 1][j + 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check left diagonal
            if ((i + 1 <= items.Length - 1) && (j - 1 >= 0) && items[i + 1][j - 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check right reverse diagonal
            if ((i + 1 <= items.Length - 1) && (j + 1 <= row.Length - 1) && items[i + 1][j + 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check left reverse diagonal
            if ((i - 1 >= 0) && (j - 1 >= 0) && items[i - 1][j - 1] == '#')
            {
                IsValid = false;
                runningCounter++;
            }

            if (checkRunningCounter)
            {
                return (runningCounter >= 4);
            }
            else
            {
                return IsValid;
            }
        }
        long PartTwo(string input)
        {
            return -1;
        }
    }
}