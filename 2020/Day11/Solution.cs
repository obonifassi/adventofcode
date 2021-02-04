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
                var (nextIteration, stable) = GetNextIteration(currentIteration, CheckAdjacentSeats, 4);
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
            var inputItems = input.Split("\r\n").Select(x => x.ToCharArray()).ToArray();
            var currentIteration = inputItems.Select(a => a.ToArray()).ToArray();

            bool isStable = false;
            var debugRunningCounter = 0;
            while (!isStable)
            {
                var (nextIteration, stable) = GetNextIteration(currentIteration, NearestNeighbors, 5);
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

        (char[][], bool) GetNextIteration(char[][] items, Func<char[][], int, int, List<char>> methodToRun, int patientTolerance)
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
                    int numOccupied = 0;
                    var neighbors = methodToRun(items, i, j);

                    foreach (var n in neighbors)
                    {
                        if (n == '#')
                        {
                            numOccupied += 1;
                        }
                    }

                    if (row[j] == 'L')
                    {
                        rowToUpdate[j] = 'L';

                        IsValid = numOccupied == 0;

                        if (IsValid)
                        {
                            rowToUpdate[j] = '#'; //seat becomes occupied
                            IsStable = false; //we know we are not stable, when a change was required
                        }
                    }
                    else if (row[j] == '#')
                    {
                        rowToUpdate[j] = '#';

                        IsValid = numOccupied >= patientTolerance;

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

        (List<char>, List<char>) GetX(char[][] items, int x, int y)
        {
            var left = new List<char>();

            for (int i = 0; i < y; i++)
            {
                left.Add(items[x][i]);
            }

            left.Reverse();

            var right = new List<char>();
            for (int i = y + 1; i <= (items[x].Length - 1); i++)
            {
                right.Add(items[x][i]);
            }

            return (left, right);
        }

        (List<char>, List<char>) GetY(char[][] items, int x, int y)
        {
            var up = new List<char>();
            for (int i = 0; i < x; i++)
            {
                up.Add(items[i][y]);
            }

            up.Reverse();

            var down = new List<char>();
            for (int i = x + 1; i <= items.Length - 1; i++)
            {
                down.Add(items[i][y]);
            }

            return (up, down);
        }

        (List<char>, List<char>) GetZ(char[][] items, int x, int y)
        {
            var left = new List<char>();
            for (int i = 1; (x - i) >= 0 && (y - i) >= 0; i++)
            {
                left.Add(items[x - i][y - i]);
            }

            var right = new List<char>();
            for (int i = 1; (x + i) <= (items.Length - 1) && (y + i) <= (items[x].Length - 1); i++)
            {
                right.Add(items[x + i][y + i]);
            }

            return (left, right);
        }

        (List<char>, List<char>) GetW(char[][] items, int x, int y)
        {
            var left = new List<char>();
            for (int i = 1; (x + i) <= items.Length - 1 && (y - i) >= 0; i++)
            {
                left.Add(items[x + i][y - i]);
            }

            var right = new List<char>();
            for (int i = 1; (x - i) >= 0 && (y + i) <= (items[x].Length - 1); i++)
            {
                right.Add(items[x - i][y + i]);
            }

            return (left, right);
        }

        List<char> NearestNeighbors(char[][] items, int i, int j)
        {
            List<char> neighbors = new List<char>();

            List<Func<char[][], int, int, (List<char>, List<char>)>> functionsToExecute = new List<Func<char[][], int, int, (List<char>, List<char>)>>
            {
                GetX,
                GetY,
                GetZ,
                GetW
            };

            foreach (var f in functionsToExecute)
            {
                var results = f.Invoke(items, i, j);

                foreach (var spot in results.Item1)
                {
                    if (spot != '.')
                    {
                        neighbors.Add(spot);
                        //we know the list being returned has the neighbors closest to the point, at the beginning of the list. 
                        //if we find a seat, we can break. it's considered the first in the list
                        break;
                    }
                }

                foreach (var spot in results.Item2)
                {
                    if (spot != '.')
                    {
                        neighbors.Add(spot);
                        //we know the list being returned has the neighbors closest to the point, at the beginning of the list. 
                        //if we find a seat, we can break. it's considered the first in the list
                        break;
                    }
                }
            }

            return neighbors;
        }

        List<char> CheckAdjacentSeats(char[][] items, int i, int j)
        {
            var row = items[i];
            char characterToCheck = '#';
            List<char> neighbors = new List<char>();

            //check right
            if ((j + 1 <= row.Length - 1) && row[j + 1] == characterToCheck)
            {
                neighbors.Add(row[j + 1]);
            }

            //check left
            if (j - 1 >= 0 && row[j - 1] == characterToCheck)
            {
                neighbors.Add(row[j - 1]);
            }

            //check down
            if ((i + 1 <= items.Length - 1) && items[i + 1][j] == characterToCheck)
            {
                neighbors.Add(items[i + 1][j]);
            }

            //check up
            if ((i - 1 >= 0) && items[i - 1][j] == characterToCheck)
            {
                neighbors.Add(items[i - 1][j]);
            }

            //check right diagonal
            if ((i - 1 >= 0) && (j + 1 <= row.Length - 1) && items[i - 1][j + 1] == characterToCheck)
            {
                neighbors.Add(items[i - 1][j + 1]);
            }

            //check left diagonal
            if ((i + 1 <= items.Length - 1) && (j - 1 >= 0) && items[i + 1][j - 1] == characterToCheck)
            {
                neighbors.Add(items[i + 1][j - 1]);
            }

            //check right reverse diagonal
            if ((i + 1 <= items.Length - 1) && (j + 1 <= row.Length - 1) && items[i + 1][j + 1] == characterToCheck)
            {
                neighbors.Add(items[i + 1][j + 1]);
            }

            //check left reverse diagonal
            if ((i - 1 >= 0) && (j - 1 >= 0) && items[i - 1][j - 1] == '#')
            {
                //IsValid = false;
                //runningCounter++;
                neighbors.Add(items[i - 1][j - 1]);
            }

            return neighbors;
        }
    }
}