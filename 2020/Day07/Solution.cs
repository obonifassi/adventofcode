using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace adventofcode.Y2020.Day07
{
    [DisplayNameAttribute("Handy Haversacks")]

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
            var map = ConstructGraph(input);

            long runningCount = 0;

            foreach (var kvp in map)
            {
                var colors = kvp.Value;

                var foundGold = Traverse(map, colors);

                if (foundGold) //if we found gold, lets tally the count and keep looking
                {
                    runningCount++;
                }
            }

            return runningCount;
        }

        long PartTwo(string input)
        {
            var map = ConstructGraph(input);

            var goldColors = map["shiny_gold"];

            var result = TraverseGoldBag(map, goldColors);

            return result - 1;
        }

        bool Traverse(Dictionary<string, LinkedList<string>> map, LinkedList<string> colors)
        {
            foreach (var color in colors)
            {
                var runningColorKey = color;

                if (Int32.TryParse(color.Substring(0, 1), out var colorBagCount))
                {
                    runningColorKey = color[2..];
                }

                if (runningColorKey == "shiny_gold")
                {
                    return true;
                }

                if (runningColorKey == "no_other")
                {
                    return false;
                }

                var runningColors = map[runningColorKey];

                bool foundGold = Traverse(map, runningColors);

                if (foundGold) //if we found gold, just return to the caller. no need to keep looking
                {
                    return true;
                }
            }

            return false;
        }

        long TraverseGoldBag(Dictionary<string, LinkedList<string>> map, LinkedList<string> colors)
        {
            long linearSum = 0;

            foreach (var color in colors)
            {
                string colorKey = color;

                if (Int32.TryParse(color.Substring(0, 1), out var colorKeyCount))
                {
                    colorKey = color[2..];
                }

                if (colorKey == "no_other")
                {
                    return 1;
                }

                var runningColors = map[colorKey];

                var result = TraverseGoldBag(map, runningColors);

                linearSum += (colorKeyCount * result);
            }

            return 1 + linearSum;
        }

        Dictionary<string, LinkedList<string>> ConstructGraph(string input)
        {
            Dictionary<string, LinkedList<string>> map = new Dictionary<string, LinkedList<string>>();
            var groups = input.Split("\r\n");

            foreach (var g in groups)
            {
                var item = g.Split("contain");

                //format the key
                var mapColorKey = item[0].Replace("bags", "").Replace("bag", "").Trim().Replace(" ", "_");

                //there can be multiple color bags that are mapped
                LinkedList<string> colors = new LinkedList<string>();
                var values = item[1].Split(",");
                foreach (var v in values)
                {
                    //format each color
                    var color = v.Replace("bags", "").Replace("bag", "").Replace(".", "").Trim().Replace(" ", "_");
                    colors.AddLast(color);
                }

                //TODO: handle duplicate color keys if needed
                //TODO: upsert the color to the end of the linked list if needed.
                map.Add(mapColorKey, colors);
            }

            return map;
        }
    }
}