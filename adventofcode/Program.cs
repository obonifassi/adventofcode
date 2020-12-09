using adventofcode._2020;
using System;

namespace adventofcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Day2 day2 = new Day2();

            var validPasswordCount = day2.PasswordPhilosophyPart1();

            Console.WriteLine($"valid passwords part 1: {validPasswordCount}");

            var result = day2.PasswordPhilosophyPart2();

            Console.WriteLine($"valid passwords part 2: {result}");
        }
    }
}
