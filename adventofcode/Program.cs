using adventofcode._2020;
using System;

namespace adventofcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Day2 day2 = new Day2();
            var validPasswordCount = day2.PasswordPhilosophy();
            Console.WriteLine($"valid passwords: {validPasswordCount}");
        }
    }
}
