using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode.Y2020.Day02
{
    [DisplayNameAttribute("Password Philosophy")]
    public class Solution : ISolver
    {
        record PasswordEntry(int min, int max, char key, string password);

        public IEnumerable<object> Solve(string input)
        {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        int PartOne(string input) => ValidateCount(input, (PasswordEntry passwordEntry) =>
        {
            var count = passwordEntry.password.Count(ch => ch == passwordEntry.key);

            if (count >= passwordEntry.min && count <= passwordEntry.max)
            {
                return true;
            }

            return false;
        });

        int PartTwo(string input) => ValidateCount(input, (PasswordEntry passwordEntry) =>
        {
            bool p1 = false;
            if (passwordEntry.min <= passwordEntry.password.Length)
            {
                p1 = passwordEntry.password[(passwordEntry.min - 1)] == passwordEntry.key;
            }

            bool p2 = false;
            if (passwordEntry.max <= passwordEntry.password.Length)
            {
                p2 = passwordEntry.password[(passwordEntry.max - 1)] == passwordEntry.key;
            }

            if (p1 != p2)
            {
                return true;
            }

            return false;
        });

        int ValidateCount(string input, Func<PasswordEntry, bool> isValid) => 
            input.Split("\n")
                .Select(line =>
                {
                    var entries = line.Split(" ");

                    var ranges = entries[0].Split("-").Select(int.Parse).ToArray();

                    var key = entries[1][0];

                    return new PasswordEntry(ranges[0], ranges[1], key, entries[2]);

                })
                .Count(isValid);
    }
}
