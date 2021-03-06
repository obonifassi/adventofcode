﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode.Y2020.Day04
{
    [DisplayNameAttribute("Passport Processing")]

    public class Solution : BaseSolution, ISolver
    {
        Dictionary<string, string> requiredFields = new Dictionary<string, string>()
        {
            //the required fields to check
            { "byr", "19[2-9][0-9]|200[0-2]"},
            { "iyr", "201[0-9]|2020"},
            { "eyr", "202[0-9]|2030"},
            { "hgt", "1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in"},
            { "hcl", "#[0-9a-f]{6}"},
            { "ecl", "amb|blu|brn|gry|grn|hzl|oth"},
            { "pid", "[0-9]{9}"},
        };

        public IEnumerable<Tuple<long, long>> Solve(string input)
        {
            var partOne = Decorator(input, PartOne);
            yield return partOne;

            var partTwo = Decorator(input, PartTwo);
            yield return partTwo;
        }

        long PartOne(string input) => ValidCount(input, (map) =>
        {
            bool IsPassportValid = requiredFields.All(kvp => map.ContainsKey(kvp.Key));

            return IsPassportValid;
        });

        long PartTwo(string input) => ValidCount(input, (map) =>
        {
            bool IsPassportValid = true;

            foreach (var kvp in requiredFields)
            {
                var c1 = map.ContainsKey(kvp.Key);

                if (!c1)
                {
                    return false;
                }

                var value = map[kvp.Key];

                var c2 = Regex.IsMatch(value.Trim(), "^(" + kvp.Value + ")$");

                if (!c2)
                {
                    return false;
                }
            }

            return IsPassportValid;
        });

        long ValidCount(string input, Func<Dictionary<string, string>, bool> isValid)
        {
            var items = input
                            .Split("\r\n\r\n")
                            .Select(x => x.Split(new char[] { ' ', '\n' })
                                            .Select(y => y.Split(":"))
                                            .ToDictionary(y => y[0], y => y[1]));

            var count = items.Count(isValid);

            return count;

        }
    }
}