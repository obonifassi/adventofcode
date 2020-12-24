using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace adventofcode.Y2020.Day06
{
    [DisplayNameAttribute("Custom Customs")]

    public class Solution : BaseSolution, ISolver
    {
        public IEnumerable<Tuple<long, long>> Solve(string input)
        {
            var partOne = Decorator(input, PartOne);
            yield return partOne;

            var partTwo = Decorator(input, PartTwo);
            yield return partTwo;
        }

        long PartOne(string input) => Solve(input, (a, b) => a.Union(b));

        long PartTwo(string input) => Solve(input, (a, b) => a.Intersect(b));

        long Solve(string input, Func<HashSet<char>, HashSet<char>, HashSet<char>> aggregateMethodToRun)
        {
            var groups = input.Split("\r\n\r\n")
                    .Select(x =>
                    {
                        return x.Split("\r\n")
                                .Select(y => y.ToHashSet());

                    });

            int runningSum = 0;

            foreach (var group in groups)
            {
                var g = group.Aggregate(aggregateMethodToRun);
                var count = g.Count;
                runningSum += count;
            }

            return runningSum;
        }
    }
}