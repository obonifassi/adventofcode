using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace adventofcode.Y2020.Day16
{
    [DisplayNameAttribute("Ticket Translation")]
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
            var items = input.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
            HashSet<int> map = new HashSet<int>();

            //process the first group: departure location: 37-479 or 485-954
            var g1 = items[0].Split("\r\n");
            foreach (var item in g1)
            {
                if (item.IndexOf("or") != -1)
                {
                    var p = item.IndexOf(":");
                    var s = item[(p + 1)..].Trim();
                    var ranges = s.Split("or").Select(x => x.Trim());

                    foreach(var r in ranges)
                    {
                        var start = r.Split("-").Select(x => Convert.ToInt32(x.Trim())).ToArray()[0];
                        var end = r.Split("-").Select(x => Convert.ToInt32(x.Trim())).ToArray()[1];
                        var count = ((end - start) + 1);
                        var range = Enumerable.Range(start, count).ToHashSet();
                        map.UnionWith(range);
                    }
                }
            }

            //process the second group: your ticket:\r\n97,101,149,103,137,61,59,223,263,179,131,113,241,127,53,109,89,173,107,211
            var g2 = items[1].Split(new string[] { "\r\n", "," }, StringSplitOptions.None)
                             .Where(x => Int32.TryParse(x.Trim(), out int value) == true)
                             .Select(x => Convert.ToInt32(x));
            
            var sum = 0;
            foreach (var number in g2)
            {
                if(!map.Contains(number))
                {
                    sum += number;
                }
            }

            //process the third group: nearby tickets:\r\n446,499,748,453,135,109,525,721,179,796,622,944,175,303,882,287,177,185,828,423
            var g3 = items[2].Split(new string[] { "\r\n", "," }, StringSplitOptions.None)
                             .Where(x => Int32.TryParse(x.Trim(), out int value) == true)
                             .Select(x => Convert.ToInt32(x));

            foreach (var number in g3)
            {
                if (!map.Contains(number))
                {
                    sum += number;
                }
            }

            return sum;
        }

        long PartTwo(string input)
        {
            return -1;
        }
    }
}