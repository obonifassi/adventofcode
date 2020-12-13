using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace adventofcode.Y2020.Day01
{
    [DisplayNameAttribute("Report Repair")]
    public class Solution : ISolver
    {
        protected const int TARGET = 2020;
        
        public IEnumerable<object> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }
            
        long PartOne(string input)
        {
            var items = input
                            .Split("\n")
                            .ToDictionary(x => Convert.ToInt32(x), y => Convert.ToInt32(y));

            long secretKey = 0;

            foreach (var kvp in items)
            {
                //2Sum algorithm
                var difference = Math.Abs(kvp.Key - TARGET);

                if (items.ContainsKey(difference))
                {
                    secretKey = (kvp.Key * difference);
                    break;
                }
            }

            return secretKey;
        }

        long PartTwo(string input)
        {
            var items = input
                            .Split("\n")
                            .Select(x => Convert.ToInt32(x.Trim()))
                            .ToArray();

            long secretKey = 0;

            for (int i = 0; i < (items.Length - 1); i++)
            {
                HashSet<int> map = new HashSet<int>();

                //find the difference for items[i]
                int currentSum = (TARGET - items[i]);

                for (int j = i + 1; j < (items.Length - 1); j++)
                {
                    //find two numbers that equal items[i], using the 2Sum algorithm
                    var difference = currentSum - items[j];

                    if (map.Contains(difference))
                    {
                        secretKey = items[i] * items[j] * difference;
                        break;
                    }

                    map.Add(items[j]);
                }
            }

            return secretKey;
        }
    }
}
