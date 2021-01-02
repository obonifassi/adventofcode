using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;

namespace adventofcode.Y2020.Day10
{
    [DisplayNameAttribute("Adapter Array")]
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
            var sumDifferenceOf1 = 0;
            var sumDifferenceOf3 = 0;

            var items = GetJoltItems(input);
            
            for(int i = 0; i < items.Count - 1; i++)
            {
                long difference = items[i + 1] - items[i];

                if(difference == 1)
                {
                    sumDifferenceOf1++;
                }
                if(difference == 3)
                {
                    sumDifferenceOf3++;
                }
            }

            return sumDifferenceOf1 * sumDifferenceOf3;
        }

        long PartTwo(string input)
        {
            var items = GetJoltItems(input);

            var result = NumOfWays(items, 0, new Dictionary<int, long>());

            return result;
        }
        
        ImmutableList<int> GetJoltItems(string input)
        {
            var items = input.Split("\r\n").Select(x => Int32.Parse(x.Trim())).OrderBy(x => x).ToList();
            
            var result = ImmutableList.Create(0).AddRange(items).Add(items.Last() + 3);

            return result;
        }

        long NumOfWays(ImmutableList<int> items, int i, Dictionary<int, long> cache)
        {
            if(cache.ContainsKey(i))
            {
                var cachedValue = cache[i];
                return cachedValue;
            }

            //base case
            if (i == items.Count - 1)
            {
                return 1;
            }

            long sum = 0;

            if ((i + 1 <= items.Count - 1) && (items[i + 1] - items[i] <= 3))
            {
                sum += NumOfWays(items, i + 1, cache);
            }

            if ((i + 2 <= items.Count - 1) && (items[i + 2] - items[i] <= 3))
            {
                sum += NumOfWays(items, i + 2, cache);
            }

            if ((i + 3 <= items.Count - 1) && (items[i + 3] - items[i]) <= 3)
            {
                sum += NumOfWays(items, i + 3, cache);
            }

            if(cache.ContainsKey(i))
            {
                cache[i] = i;
            }
            else
            {
                cache.Add(i, sum);
            }

            return sum;
        }
    }
}