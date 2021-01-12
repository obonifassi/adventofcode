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

        long PartTwo(string input)
        {
            // b d a g l
            // u n h x b
            // f m q p v

            char[][] table = new char[5][];

            char[] c1 = new char[] { 'j', 'e', 'f', 'y', 'l' };
            char[] c2 = new char[] { 'b', 'd', 'a', 'g', 'l'};
            char[] c3 = new char[] { 'u', 'n', 'h', 'x', 'b' };
            char[] c4 = new char[] { 'f', 'm', 'q', 'p', 'v' };
            char[] c5 = new char[] { 'z', 'l', 'c', 'o', 'r' };

            table[0] = c1;
            table[1] = c2;
            table[2] = c3;
            table[3] = c4;
            table[4] = c5;

            var t1 = GetX(table, 2, 2);
            var t2 = GetY(table, 2, 2);
            var t3 = GetZ(table, 2, 2);


            return -1;
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

                        IsValid = CheckAdjacentSeats(items, i, j, '#', 4);

                        if (IsValid)
                        {
                            rowToUpdate[j] = '#'; //seat becomes occupied
                            IsStable = false; //we know we are not stable, when a change was required
                        }
                    }
                    else if (row[j] == '#')
                    {
                        rowToUpdate[j] = '#';

                        IsValid = CheckAdjacentSeats(items, i, j, '#', 4, true);

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

        List<List<char>> GetX(char[][]items, int x, int y)
        {
            List<List<char>> result = new List<List<char>>();

            var left = new List<char>();

            for(int i = 0; i < x; i++)
            {
                left.Add(items[x][i]);
            }

            result.Add(left);

            var right = new List<char>();
            for (int i = y + 1; i <= (items[x].Length - 1); i++)
            {
                right.Add(items[x][i]);
            }

            result.Add(right);

            return result;
        }

        List<List<char>> GetY(char[][] items, int x, int y)
        {
            List<List<char>> result = new List<List<char>>();

            var up = new List<char>();
            for (int i = 0; i < y; i++)
            {
                up.Add(items[i][x]);
            }

            result.Add(up);

            var down = new List<char>();
            for (int i = x + 1; i <= items.Length - 1; i++)
            {
                down.Add(items[i][x]);
            }

            result.Add(down);

            return result;
        }

        List<List<char>> GetZ(char[][] items, int x, int y)
        {
            List<List<char>> result = new List<List<char>>();

            var left = new List<char>();
            for (int i = 1; (x - i) >= 0 && (y - i) >= 0; i++)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
            {
                left.Add(items[x - i][y - i]);
            }

            result.Add(left);

            var right = new List<char>();
            for (int i = 1; (x + i) <= (items.Length - 1) && (y + i) <= (items[x].Length - 1); i++)
            {
                right.Add(items[x + i][y + i]);
            }

            result.Add(right);

            return result;
        }

        bool CheckAdjacentSeats(char[][] items, int i, int j, char characterToCheck, int amountToCount, bool checkRunningCounter = false)
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
                return (runningCounter >= amountToCount);
            }
            else
            {
                return IsValid;
            }
        }
        
    }
}