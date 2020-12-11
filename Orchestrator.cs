using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;

namespace adventofcode
{
    class Orchestrator
    {
        public static SolverResult RunSolver(ISolver solver)
        {
            var workingDirectory = solver.WorkingDirectory();

            Write(ConsoleColor.White, $"{solver.DayName()}: {solver.GetName()}");
            WriteLine();
            
            var file = Path.Combine(workingDirectory, "input.txt");
            var input = GetNormalizedInput(file);

            List<string> answers = new List<string>();
            
            foreach (var line in solver.Solve(input))
            {
                answers.Add(line.ToString());

                Write(ConsoleColor.Green, $"✓");
                Console.Write($" {line} ");
                WriteLine();
            }

            return new SolverResult(answers.ToArray());
        }

        private static string GetNormalizedInput(string file)
        {
            var input = File.ReadAllText(file);

            if(input.EndsWith("\n"))
            {
                input = input.Substring(0, input.Length - 1);
            }

            return input;
        }

        private static void WriteLine(ConsoleColor color = ConsoleColor.Gray, string text = "")
        {
            Write(color, text + "\n");
        }

        private static void Write(ConsoleColor color = ConsoleColor.Gray, string text = "")
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = c;
        }
    }
}
