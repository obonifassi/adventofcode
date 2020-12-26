using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace adventofcode.Y2020.Day08
{
    [DisplayNameAttribute("Handheld Halting")]

    public class Solution : BaseSolution, ISolver
    {
        public IEnumerable<Tuple<long, long>> Solve(string input)
        {
            var partOne = Decorator(input, PartOne);
            yield return partOne;
        }

        long PartOne(string input)
        {
            List<Instruction> instructions = new List<Instruction>();
            var parsedInstructions = input.Split("\r\n");
            long accumulator = 0;

            //construct instructions
            foreach(var pi in parsedInstructions)
            {
                var item = pi.Split(" ");
                var name = item[0].Trim();

                var operationType = item[1][0..1];
                _ = Int32.TryParse(item[1][1..].ToString().Trim(), out var value);

                Instruction instruction = new Instruction()
                {
                    Name = name,
                    Value = value,
                    Status = Status.Unvisited,
                    OperationType = operationType == "+" ? OperationType.Add : OperationType.Subtract
                };

                instructions.Add(instruction);
            }

            //process instructions
            var insPointer = 0;
            var ins = instructions[insPointer]; //start at the beginning.
            while(ins != null && insPointer < (instructions.Count - 1))
            {
                if (ins.Status == Status.Visited) { break; }

                var instructionName = ins.Name;
                ins.Status = Status.Visiting;
                switch (instructionName)
                {
                    case "acc":
                        var newvalue = ins.OperationType == OperationType.Add ? (accumulator + ins.Value) : (accumulator - ins.Value);
                        accumulator = newvalue;
                        insPointer++;
                        break;
                    case "nop":
                        insPointer++;
                        break;
                    case "jmp":
                        var currentPointer = insPointer;
                        var newPointer = ins.OperationType == OperationType.Add ? (ins.Value + currentPointer) : (currentPointer - ins.Value);
                        insPointer = newPointer;
                        break;
                    default:
                        insPointer++;
                        break;
                }

                ins.Status = Status.Visited;
                ins = instructions[insPointer];
            }

            return accumulator;
        }
    }

    public class Instruction
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Status Status { get; set; }
        public OperationType OperationType { get; set; }
    }

    public enum Status
    {
        Unvisited,
        Visiting,
        Visited
    }

    public enum OperationType
    {
        Add,
        Subtract
    }
}