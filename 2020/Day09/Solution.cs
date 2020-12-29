using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace adventofcode.Y2020.Day09
{
    [DisplayNameAttribute("Encoding Error")]
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
            var items = input.Split("\r\n").Select(x => Int64.Parse(x.Trim())).ToArray();

            var startingPointer = 0;
            var endingPointer = items.Length - 1;
            var runningPointer = 25;
            var invalidNumber = 0L;

            while (runningPointer < endingPointer)
            {
                if (runningPointer < items.Length - 1)
                {
                    bool isValid = false;
                    var itemToProcess = items[runningPointer];

                    //process the item
                    for (int i = startingPointer; i <= (startingPointer + 25); i++)
                    {
                        var calculatedJ = i + 24 > items.Length - 1 ? items.Length - 1 : i + 24;
                        for (int j = i + 1; j <= calculatedJ; j++)
                        {
                            var sum = items[i] + items[j];
                            if (sum == itemToProcess)
                            {
                                //the item is valid, lets go to the next item
                                isValid = true;
                                break;
                            }
                        }
                    }

                    if (!isValid)
                    {
                        //the item to process had no valid pair.
                        invalidNumber = itemToProcess;
                        break;
                    }

                    runningPointer++;
                    startingPointer++;
                }
            }

            return invalidNumber;
        }

        long PartTwo(string input)
        {
            var invalidNumber = PartOne(input);

            var items = input.Split("\r\n").Select(x => Int64.Parse(x.Trim())).ToArray();

            var startingPointer = 0;
            var endingPointer = 1;
            var runningSum = items[0] + items[1];

            while (startingPointer < items.Length - 1)
            {
                if (runningSum < invalidNumber)
                {
                    ++endingPointer;
                    runningSum = runningSum + items[endingPointer];
                }
                else if (runningSum > invalidNumber)
                {
                    runningSum = Math.Abs(runningSum - items[startingPointer]);
                    startingPointer++;
                }
                else if (runningSum == invalidNumber)
                {
                    break;
                }
            }

            if (runningSum == invalidNumber)
            {
                //find the max and min between startingPointer and endingPointer
                var (min, max) = (Int64.MaxValue, Int64.MinValue);

                for (int i = startingPointer; i <= endingPointer; i++)
                {
                    var item = items[i];

                    if (item > max)
                    {
                        max = item;
                    }

                    if (item < min)
                    {
                        min = item;
                    }
                }

                var result = (max + min);
                return result;
            }
            else
            {
                throw new Exception("invalid target. no point in finding min and max");
            }
        }
    }
}