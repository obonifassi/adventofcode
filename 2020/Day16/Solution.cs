using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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

        long PartTwo(string input)
        {
            var items = input.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
            Dictionary<string, HashSet<int>> rangeInstructions = new Dictionary<string, HashSet<int>>();
            HashSet<int> globalMap = new HashSet<int>(); //used to keep track of the valid numbers in our grid

            //process the first group: departure location: 37-479 or 485-954
            var g1 = items[0].Split("\r\n");
            foreach (var item in g1)
            {
                if (item.IndexOf("or") != -1)
                {
                    var p = item.IndexOf(":");
                    var s = item[(p + 1)..].Trim();
                    var ranges = s.Split("or").Select(x => x.Trim());
                    var key = item.Substring(0, p);

                    HashSet<int> map = new HashSet<int>();

                    foreach (var r in ranges)
                    {
                        var start = r.Split("-").Select(x => Convert.ToInt32(x.Trim())).ToArray()[0];
                        var end = r.Split("-").Select(x => Convert.ToInt32(x.Trim())).ToArray()[1];
                        var count = ((end - start) + 1);
                        var range = Enumerable.Range(start, count).ToHashSet();
                        map.UnionWith(range);
                        globalMap.UnionWith(range);
                    }

                    rangeInstructions.Add(key.Trim().Replace(":", "").Replace(" ", "_"), map);
                }
            }

            //process the second group: your ticket:\r\n97,101,149,103,137,61,59,223,263,179,131,113,241,127,53,109,89,173,107,211
            var myTicket = items[1].Split(new string[] { "\r\n", "," }, StringSplitOptions.None)
                             .Where(x => Int32.TryParse(x.Trim(), out int value) == true)
                             .Select(x => Convert.ToInt32(x))
                             .ToArray();

            //process the third group: nearby tickets:\r\n446,499,748,453,135,109,525,721,179,796,622,944,175,303,882,287,177,185,828,423
            var g3 = items[2].Split(new string[] { "\r\n" }, StringSplitOptions.None)
                             .Where(x => x.Contains(","))
                             .Select(x => x.Split(',')
                                .Select(x => Int32.Parse(x.Trim())).ToArray())
                             .ToArray();

            Dictionary<int, Dictionary<string, int>> lookupMap = new Dictionary<int, Dictionary<string, int>>();

            foreach (var kvp in rangeInstructions)
            {
                for (int i = 0; i < rangeInstructions.Count; i++)
                {
                    bool isValid = true;

                    for (int j = 0; j < g3.Length; j++)
                    {
                        var num = g3[j][i];

                        if (!globalMap.Contains(num)) // ignore, an invalid ticket from part one
                        {
                            continue;
                        }

                        var ranges = kvp.Value;
                        if (!ranges.Contains(num))
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (isValid)
                    {
                        //update
                        if (lookupMap.ContainsKey(i))
                        {
                            var existingMap = lookupMap[i];

                            //update
                            if (existingMap.ContainsKey(kvp.Key))
                            {
                                var existingValue = existingMap[kvp.Key];
                                existingValue += 1;
                                existingMap[kvp.Key] = existingValue;
                            }
                            else //insert
                            {
                                existingMap.Add(kvp.Key, 1);
                            }
                        }
                        else //insert
                        {
                            Dictionary<string, int> temp = new Dictionary<string, int>();
                            int amountOfTimes = 1;
                            temp.Add(kvp.Key, amountOfTimes);
                            lookupMap.Add(i, temp);
                        }
                    }
                }
            }

            Dictionary<int, string> result = new Dictionary<int, string>();

            //go through our constructed lookup map and work our way backwards. 
            //keep finding the key that only contains 1 item, and continue removing from others, until we have no more items to process.
            while (result.Count < rangeInstructions.Count)
            {
                foreach (var kvp in lookupMap)
                {
                    var key = kvp.Value.FirstOrDefault().Key;
                    var value = kvp.Value.FirstOrDefault().Value;

                    if (kvp.Value.Count == 1 && value == 1)
                    {
                        result.Add(kvp.Key, key);

                        //go and find this key and remove from all others
                        foreach (var otherKvp in lookupMap)
                        {
                            var others = otherKvp.Value;
                            if (others.Count > 0 && others.ContainsKey(key))
                            {
                                //if we only have 1, remove it
                                var possible = others[key];
                                if (possible == 1)
                                {
                                    others.Remove(key);
                                }
                                else
                                {
                                    //reduce by 1
                                    possible -= possible;
                                    others[key] = possible;
                                }
                            }
                        }
                    }
                }
            }

            //go through our ticket, and find the first 6 fields that start with the word "departure"
            long resultMultiplied = 1;
            for (int i = 0; i < result.Count; i++)
            {
                var resultItem = result[i];
                if (resultItem.IndexOf("departure") != -1)
                {
                    resultMultiplied *= myTicket[i];
                }
            }

            return resultMultiplied;
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

                    foreach (var r in ranges)
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
                if (!map.Contains(number))
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
    }
}