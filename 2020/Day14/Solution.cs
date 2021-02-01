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
            var items = input.Split("\r\n");
            var mask = items[0].Split("=")
                                .Select(x => x.Trim())
                                .ToArray()[1];

            Dictionary<int, long> memory = new Dictionary<int, long>();

            for (int i = 1; i <= items.Length - 1; i++)
            {
                var instruction = items[i].Split("=");
                var startPointer = instruction[0].IndexOf("[");
                var endPointer = instruction[0].IndexOf("]");

                startPointer += 1;
                endPointer -= startPointer;

                var key = instruction[0].Substring(startPointer, endPointer);
                var value = instruction[1];

                var memKey = Convert.ToInt32(key.Trim());
                var memValue = Convert.ToInt32(value.Trim());

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

            //var mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X";
            //mem[8] = 11
            //mem[7] = 101
            //mem[8] = 0
            //var andMask = Convert.ToInt64(mask.Replace("X", "1"), 2);
            //var orMask = Convert.ToInt64(mask.Replace("X", "0"), 2);
            //var a = 11;
            //var r = a & andMask;
            //var r2 = r | orMask;

            return -1;
        }

        long ApplyMask(int value, string mask)
        {
            var paddedBits = Convert.ToString(value, 2).PadLeft(36, 0);
            
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i <= paddedBits.Length - 1; i++)
            {
                if(mask[i] == 'X')
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