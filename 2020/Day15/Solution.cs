using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace adventofcode.Y2020.Day15
{
    [DisplayNameAttribute("Rambunctious Recitation")]
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
            var items = input.Split(",")
                        .Select(x => Convert.ToInt32(x.Trim()))
                        .ToArray();

            var answer = Process(items, 2020);

            return answer;
        }

        long PartTwo(string input)
        {
            var items = input.Split(",")
                         .Select(x => Convert.ToInt32(x.Trim()))
                         .ToArray();

            var answer = Process(items, 30000000);

            return answer;
        }

        long Process(int[] items, long target)
        {
            Dictionary<int, LinkedList<int>> cache = new Dictionary<int, LinkedList<int>>();

            //initialize
            for (int i = 0; i <= items.Length - 1; i++)
            {
                var l = new LinkedList<int>();
                l.AddLast(i + 1);
                cache.Add(items[i], l);
            }

            var previous = items.Last();
            var nextNumber = -1;
            var currentCounter = items.Length + 1;

            while (currentCounter <= target)
            {
                if (cache.ContainsKey(previous))
                {
                    var previousItems = cache[previous];

                    //check for first the instance
                    if (previousItems.Count == 1)
                    {
                        nextNumber = 0;
                    }
                    else
                    {
                        //calculcate the next number
                        var difference = 0;
                        foreach (var n in previousItems)
                        {
                            difference = Math.Abs(difference - n);
                        }

                        nextNumber = difference;
                    }

                    LinkedList<int> newList;
                    //insert the next number into the cache
                    if (!cache.ContainsKey(nextNumber))
                    {
                        newList = new LinkedList<int>();
                        newList.AddLast(currentCounter);
                        cache.Add(nextNumber, newList);
                    }
                    else
                    {
                        newList = cache[nextNumber];

                        //before we add, we only want to keep a running list of the two most recent entries
                        if (newList.Count == 2)
                        {
                            newList.RemoveFirst();
                        }

                        newList.AddLast(currentCounter);
                    }
                }

                currentCounter += 1;

                //set the previous number
                previous = nextNumber;
            }

            return previous;
        }
    }
}