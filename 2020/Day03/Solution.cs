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
        }

        long PartOne(string input) => TreeCount(input, (1,3));

        long TreeCount(string input, params(int a, int b)[] slopes)
        {
            var lines = input.Split("\n");

            var (totalRows, totalColumns) = (lines.Length, (lines[0].Length - 1));

            var treeCount = 0;

            foreach(var (slopeRow, slopeColumn) in slopes)
            {
                var (currentRow, currentColumn) = (slopeRow, slopeColumn);

                while(currentRow < totalRows)
                {
                    if(lines[currentRow][currentColumn % totalColumns] == '#')
                    {
                        treeCount++;
                    }

                    (currentRow, currentColumn) = (currentRow + slopeRow, currentColumn + slopeColumn);
                }
            }

            return treeCount;
        }
    }
}