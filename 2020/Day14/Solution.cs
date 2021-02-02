using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace adventofcode.Y2020.Day14
{
    [DisplayNameAttribute("Docking Data")]
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
            Dictionary<long, long> memory = new Dictionary<long, long>();

            string[] stringSeparators = new string[] { "\r\n" };
            var items = input.Split(stringSeparators, StringSplitOptions.None);
            string mask = "";

            foreach (var item in items)
            {
                if (item.IndexOf("mask") != -1)
                {
                    mask = GetMask(item);
                }
                else
                {
                    var (memKey, memValue) = GetKeyValue(item);

                    var updatedValue = ApplyMask(memValue, mask);

                    if (!memory.ContainsKey(memKey))
                    {
                        memory.Add(memKey, updatedValue);
                    }
                    else
                    {
                        memory[memKey] = updatedValue;
                    }
                }
            }

            return memory.Sum(y => y.Value);
        }

        (long, long) GetKeyValue(string item)
        {
            var instruction = item.Split("=");
            var startPointer = instruction[0].IndexOf("[");
            var endPointer = instruction[0].IndexOf("]");

            startPointer += 1;
            endPointer -= startPointer;

            var key = instruction[0].Substring(startPointer, endPointer);
            var value = instruction[1];

            var memKey = Convert.ToInt64(key.Trim());
            var memValue = Convert.ToInt64(value.Trim());

            return (memKey, memValue);
        }

        string GetMask(string item)
        {
            return item.Split("=")
                    .Select(x => x.Trim())
                    .ToArray()[1];
        }

        long ApplyMask(long value, string mask)
        {
            var paddedBits = Convert.ToString(value, 2).PadLeft(36, '0');

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= paddedBits.Length - 1; i++)
            {
                if (mask[i] == 'X')
                {
                    sb.Append(paddedBits[i]);
                }
                else
                {
                    sb.Append(mask[i]);
                }
            }

            return Convert.ToInt64(sb.ToString(), 2);
        }

        long PartTwo(string input)
        {
            return -1;
        }
    }
}