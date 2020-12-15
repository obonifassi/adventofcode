using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode.Y2020.Day04
{
    [DisplayNameAttribute("Passport Processing")]

    public class Solution : ISolver
    {
        Dictionary<string, string> requiredFields = new Dictionary<string, string>()
        {
            //the required fields to check
            { "byr", "byr"},
            { "iyr", "iyr"},
            { "eyr", "eyr"},
            { "hgt", "hgt"},
            { "hcl", "hgt"},
            { "ecl", "ecl"},
            { "pid", "pid"},
        };

        public IEnumerable<object> Solve(string input)
        {
            yield return PartOne(input);
            //
        }

        int PartOne(string input) => ValidCount(input, (map) =>
        {
            bool IsPassportValid = requiredFields.All(kvp => map.ContainsKey(kvp.Key));

            return IsPassportValid;
        });

        int ValidCount(string input, Func<Dictionary<string, string>, bool> isValid)
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