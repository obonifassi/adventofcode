using System;
using System.Diagnostics;

namespace adventofcode
{
    public abstract class BaseSolution
    {
        public virtual Tuple<long, T> Decorator<T>(string input, Func<string, T> methodToRun)
        {
            Stopwatch sw = new();
            
            sw.Start();

            var solution = methodToRun(input);

            sw.Stop();

            Tuple<long, T> result = new(sw.ElapsedMilliseconds, solution);
            return result;
        }
    }
}
