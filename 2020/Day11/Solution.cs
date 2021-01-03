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
            var inputItems = input.Split("\r\n").Select(x => x.ToCharArray()).ToArray();
            var items = inputItems.Select(a => a.ToArray()).ToArray();

            while (true)
            {
                char[][] itemsToUpdate = new char[items.Length][];

                for (int i = 0; i <= items.Length - 1; i++)
                {
                    var row = items[i];

                    char[] rowToUpdate = new char[row.Length];

                    for (int j = 0; j <= row.Length - 1; j++)
                    {
                        bool IsValid = true;

                        if (row[j] == 'L')
                        {
                            rowToUpdate[j] = 'L';

                            IsValid = CheckAdjacentSeats(items, i, j, '#');

                            if (IsValid)
                            {
                                rowToUpdate[j] = '#'; //seat becomes occupied
                            }
                        }
                        else if (row[j] == '#')
                        {
                            rowToUpdate[j] = '#';

                            IsValid = CheckAdjacentSeats(items, i, j, '#');

                            if (IsValid)
                            {
                                rowToUpdate[j] = 'L'; //seat becomes empty
                            }
                        }
                        else
                        {
                            rowToUpdate[j] = '.';
                        }
                    }

                    itemsToUpdate[i] = rowToUpdate;
                }

                if(items.SequenceEqual(itemsToUpdate))
                {
                    break;
                }

                items = itemsToUpdate.Select(a => a.ToArray()).ToArray();
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
            if (j - 1 >= 0 && row[j - 1] == characterToCheck)
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
            if ((i - 1 >= 0) && items[i - 1][j] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check right diagonal
            if ((i - 1 >= 0) && (j + 1 < row.Length - 1) && items[i - 1][j + 1] == characterToCheck)
            {
                IsValid = false;
                runningCounter++;
            }

            //check left diagonal
            if ((i + 1 < items.Length - 1) && (j - 1 >= 0) && items[i + 1][j - 1] == characterToCheck)
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
            if ((i - 1 >= 0) && (j - 1 >= 0) && items[i - 1][j - 1] == '#')
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