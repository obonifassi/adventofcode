﻿using System;
using System.Collections.Generic;
using System.IO;

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

            List<string> answers = new();

            foreach (var line in solver.Solve(input))
            {
                answers.Add(line.ToString());
                Write(ConsoleColor.Green, $"✓");
                Console.Write($" {line.Item2} ({line.Item1}ms)");
                WriteLine();
            }

            return new SolverResult(answers.ToArray());
        }

        private static string GetNormalizedInput(string file)
        {
            var input = File.ReadAllText(file);

            if (input.EndsWith("\n"))
            {
                input = input[0..^1];
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
