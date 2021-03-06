﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace adventofcode.Y2020.Day03
{
    [DisplayNameAttribute("Toboggan Trajectory")]

    public class Solution : BaseSolution, ISolver
    {
        public IEnumerable<Tuple<long, long>> Solve(string input)
        {
            var partOne = Decorator(input, PartOne);
            yield return partOne;

            var partTwo = Decorator(input, PartTwo);
            yield return partTwo;
        }

        long PartOne(string input) => TreeCount(input, (1, 3));

        long PartTwo(string input) => TreeCount(input, (1, 1), (1, 3), (1, 5), (1, 7), (2, 1));

        long TreeCount(string input, params (int a, int b)[] slopes)
        {
            var lines = input.Split("\n");

            var (totalRows, totalColumns) = (lines.Length, (lines[0].Length - 1));

            var multiplyResult = 1L;

            foreach (var (slopeRow, slopeColumn) in slopes)
            {
                var (currentRow, currentColumn) = (slopeRow, slopeColumn);

                var treeCount = 0;

                while (currentRow < totalRows)
                {
                    if (lines[currentRow][currentColumn % totalColumns] == '#')
                    {
                        treeCount++;
                    }

                    (currentRow, currentColumn) = (currentRow + slopeRow, currentColumn + slopeColumn);
                }

                multiplyResult = multiplyResult * treeCount;
            }

            return multiplyResult;
        }
    }
}