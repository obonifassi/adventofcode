using adventofcode;
using System;
using System.Linq;
using System.Reflection;

var allValidTypes = Assembly.GetEntryAssembly()!.GetTypes()
                .Where(t => t.GetTypeInfo().IsClass && typeof(ISolver).IsAssignableFrom(t))
                .OrderBy(t => t.FullName)
                .ToArray();

var solutions = allValidTypes
                .Select(t => Activator.CreateInstance(t) as ISolver)
                .ToArray();

foreach (var s in solutions)
{
    Orchestrator.RunSolver(s);
}
