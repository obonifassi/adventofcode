using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace adventofcode.Y2020.Day13
{
    [DisplayNameAttribute("Shuttle Search")]
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
            var parsedInput = input.Split("\r\n").Select(x => x.Trim()).ToArray();
            var minTime = Convert.ToInt32(parsedInput[0]);
            var busIds = parsedInput[1]
                            .Split(new char[] { ',' })
                            .Where(x => x != "x")
                            .Select(x => Convert.ToInt32(x))
                            .ToHashSet();

            bool processing = true;
            bool foundRoute = false;
            int numOfMinutes = 0;
            int busId = 0;
            int counter = minTime;

            while(processing)
            {
                foreach(var bus in busIds)
                {
                    if(counter % bus == 0)
                    {
                        processing = false;
                        foundRoute = true;
                        numOfMinutes = counter;
                        busId = bus;
                        break;
                    }
                }

                if(foundRoute)
                {
                    break;
                }

                counter++;
            }

            int earliestTime = (numOfMinutes - minTime) * busId;

            return earliestTime;
        }

        long PartTwo(string input)
        {
            var parsedInput = input.Split("\r\n").Select(x => x.Trim()).ToArray();

            var buses = parsedInput[1]
                            .Split(new char[] { ',' })
                            .Select(x => x == "x" ? 1 : Convert.ToInt32(x))
                            .ToArray();

            long time = 0;
            long stepSize = buses[0];

            for (int i = 1; i < buses.Length; i++)
            {
                long bus = buses[i];

                while ((time + i) % bus != 0)
                {
                    time += stepSize;
                }

                stepSize *= bus;
            }

            return time;
        }
    }
}