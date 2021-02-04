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
            var items = input.Split("\r\n")
                             .Select(x => x.Trim())
                             .ToArray();

            string mask = "";

            Dictionary<long, long> memory = new Dictionary<long, long>();

            foreach (var item in items)
            {
                if (item.IndexOf("mask") != -1)
                {
                    mask = GetMask(item);
                }
                else
                {
                    var (memKey, memValue) = GetKeyValue(item);

                    var bits = ApplyMask(memValue, mask, 'X');

                    var updatedValue = Convert.ToInt64(bits, 2);

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

        long PartTwo(string input)
        {
            var items = input.Split("\r\n")
                             .Select(x => x.Trim())
                             .ToArray();

            string mask = "";

            Dictionary<long, long> memory = new Dictionary<long, long>();

            foreach (var item in items)
            {
                if (item.IndexOf("mask") != -1)
                {
                    mask = GetMask(item);
                }
                else
                {
                    var (memKey, memValue) = GetKeyValue(item);

                    var bits = ApplyMask(memKey, mask, '0');

                    var keys = GetKeysToUpdate(bits);

                    foreach (var key in keys)
                    {
                        if (!memory.ContainsKey(key))
                        {
                            memory.Add(key, memValue);
                        }
                        else
                        {
                            memory[key] = memValue;
                        }
                    }
                }
            }

            return memory.Sum(z => z.Value);
        }

        string GetMask(string item)
        {
            return item.Split("=")
                    .Select(x => x.Trim())
                    .ToArray()[1];
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

        string ApplyMask(long value, string mask, char unChangedCharacter)
        {
            //convert to padded bits with leading zeros
            var paddedBits = Convert.ToString(value, 2).PadLeft(36, '0');

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= paddedBits.Length - 1; i++)
            {
                if (mask[i] == unChangedCharacter)
                {
                    sb.Append(paddedBits[i]);
                }
                else
                {
                    sb.Append(mask[i]);
                }
            }

            return sb.ToString();
        }

        List<long> GetKeysToUpdate(string item)
        {
            var charItems = item.ToCharArray();
            List<long> result = new List<long>();
            List<int> bitPositions = new List<int>();

            //find bit positions and initialize to 0
            for (int i = 0; i <= charItems.Length - 1; i++)
            {
                if (item[i] == 'X')
                {
                    bitPositions.Add(i);
                    charItems[i] = '0';
                }
            }

            //initial key
            var k1 = Convert.ToInt64(new string(charItems), 2);
            result.Add(k1);

            for (int i = 0; i < Math.Pow(2, bitPositions.Count) - 1; i++)
            {
                StringBuilder sb = new StringBuilder();

                //get bits to process
                for (int j = 0; j <= bitPositions.Count - 1; j++)
                {
                    var p = bitPositions[j];
                    sb.Append(charItems[p]);
                }

                //convert to long
                var temp = Convert.ToInt64(sb.ToString(), 2);

                //add 1
                temp += 1;

                //re convert to bits
                var bits = Convert.ToString(temp, 2).PadLeft(bitPositions.Count, '0');

                //plug back in
                for (int j = 0; j <= bits.Length - 1; j++)
                {
                    var itemToInsert = bits[j];
                    var positionToInsert = bitPositions[j];

                    charItems[positionToInsert] = itemToInsert;
                }

                //recalculate key
                var kNth = Convert.ToInt64(new string(charItems), 2);
                result.Add(kNth);
            }

            return result;
        }
    }
}