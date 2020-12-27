using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace adventofcode.Y2020.Day08
{
    [DisplayNameAttribute("Handheld Halting")]
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
            var instructions = ConstructInstructions(input);

            var (_, accumulator) = ProcessInstructions(instructions);

            return accumulator;
        }

        long PartTwo(string input)
        {
            var instructions = ConstructInstructions(input);
            var ranges = Enumerable.Range(0, instructions.Count);
            var listOfItems = ranges.Where(x => instructions[x].Name != "acc").ToList();

            List<List<Instruction>> itemsToProcess = new List<List<Instruction>>();

            foreach (var item in listOfItems)
            {
                List<Instruction> n = new List<Instruction>(instructions);

                var value = n.ElementAt(item);

                Instruction instr = new Instruction();

                if (value.Name == "nop")
                {
                    instr.Name = "jmp";
                    instr.Status = value.Status;
                    instr.Value = value.Value;
                }
                else if (value.Name == "jmp")
                {
                    instr.Name = "nop";
                    instr.Status = value.Status;
                    instr.Value = value.Value;
                }

                n[item] = instr;

                itemsToProcess.Add(n);
            }

            var (isComplete, acc) = (false, 0L);

            foreach (var item in itemsToProcess)
            {
                (isComplete, acc) = ProcessInstructions(item);

                if (isComplete)
                {
                    break;
                }
            }

            return acc;
        }

        (bool, long) ProcessInstructions(List<Instruction> instructions)
        {
            long accumulator = 0;
            var nextInstructionPointer = 0;
            var nextInstruction = instructions[nextInstructionPointer];

            while (nextInstruction != null && nextInstructionPointer < instructions.Count - 1)
            {
                var previousPointer = nextInstructionPointer;

                Instruction newIns = new Instruction();
                newIns.Name = nextInstruction.Name;
                newIns.Status = nextInstruction.Status;
                newIns.Value = nextInstruction.Value;

                newIns.Status = Status.Visiting;

                switch (newIns.Name)
                {
                    case "acc":
                        var newvalue = accumulator + newIns.Value;
                        accumulator = newvalue;
                        nextInstructionPointer++;
                        break;
                    case "nop":
                        nextInstructionPointer++;
                        break;
                    case "jmp":
                        var currentPointer = nextInstructionPointer;
                        var newPointer = newIns.Value + currentPointer;
                        nextInstructionPointer = newPointer;
                        break;
                    default:
                        nextInstructionPointer++;
                        break;
                }

                newIns.Status = Status.Visited;
                instructions[previousPointer] = newIns;

                nextInstruction = instructions[nextInstructionPointer];

                if (nextInstruction.Status == Status.Visited)
                {
                    return (false, accumulator);
                }
            }

            return (true, accumulator);
        }

        List<Instruction> ConstructInstructions(string input)
        {
            List<Instruction> instructions = new List<Instruction>();
            var parsedInstructions = input.Split("\r\n");

            //construct instructions
            foreach (var pi in parsedInstructions)
            {
                var item = pi.Split(" ");
                var name = item[0].Trim();

                _ = Int32.TryParse(item[1].ToString().Trim(), out var value);

                Instruction instruction = new Instruction()
                {
                    Name = name,
                    Value = value,
                    Status = Status.Unvisited
                };

                instructions.Add(instruction);
            }

            return instructions;
        }
    }

    public class Instruction
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Unvisited,
        Visiting,
        Visited
    }
}