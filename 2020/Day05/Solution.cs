﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace adventofcode.Y2020.Day05
{
    [DisplayNameAttribute("Binary Boarding")]

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
            var seats = GetSeats(input);
            var maxSeatId = seats.Max();

            return maxSeatId;
        }

        long PartTwo(string input)
        {
            var mySeat = 0;
            var seats = GetSeats(input);
            var (min, max) = (seats.Min(), seats.Max());

            var seatSequence = Enumerable.Range((int)min, (int)(max - min + 1));

            foreach (var s in seatSequence)
            {
                if (!seats.Contains(s))
                {
                    mySeat = s;
                }
            }

            return mySeat;
        }

        HashSet<long> GetSeats(string input)
        {
            HashSet<long> seats = new HashSet<long>();

            var groups = input.Split("\r\n");

            foreach (var group in groups)
            {
                //process row
                var row = ProcessRow(group, 0, 7, 127, "F", "B");

                //process column
                var column = ProcessRow(group, 7, 3, 7, "L", "R");

                //calculate seat id
                var currentSeatId = row * 8 + column;

                seats.Add(currentSeatId);
            }

            return seats;
        }

        long ProcessRow(string group, int startingSubstring, int endLength, int endingSearch, string frontKey, string backKey)
        {
            var instructions = group
                                .Substring(startingSubstring, endLength)
                                .Select(x => x.ToString().Trim())
                                .ToArray();

            var start = 0;
            var end = endingSearch;
            var middle = 0;

            foreach (var i in instructions)
            {
                middle = start + (end - start) / 2; //This prevents overflow in large numbers

                if (i == frontKey)
                {
                    end = middle - 1;
                }
                else if (i == backKey)
                {
                    start = middle + 1;
                }
            }

            return Math.Max(start, Math.Max(end, middle));
        }
    }
}