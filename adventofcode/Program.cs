using adventofcode._2020;
using System;

namespace adventofcode
{
    class Program
    {
        static void Main(string[] args)
        {
            var day1Result = Day1.find2Sum();
            Console.WriteLine("day 1a: " + day1Result);

            day1Result = Day1.find3Sum();
            Console.WriteLine("day 1b: " + day1Result);
        }
    }
}
