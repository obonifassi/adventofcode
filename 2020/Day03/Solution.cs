using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode.Y2020.Day03
{
    [DisplayNameAttribute("Toboggan Trajectory")]

    public class Solution : ISolver
    {
        public IEnumerable<object> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        long PartOne(string input) => TreeCount(input, (1, 3));

        long PartTwo(string input) => TreeCount(input, (1, 1), (1, 3), (1, 5), (1, 7), (2, 1));

        long TreeCount(string input, params(int a, int b)[] slopes)
        {
            var lines = input.Split("\n");

            var (totalRows, totalColumns) = (lines.Length, (lines[0].Length - 1));

            var multiplyResult = 1L;

            foreach(var (slopeRow, slopeColumn) in slopes)
            {
                var (currentRow, currentColumn) = (slopeRow, slopeColumn);

                var treeCount = 0;

                while (currentRow < totalRows)
                {
                    if(lines[currentRow][currentColumn % totalColumns] == '#')
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