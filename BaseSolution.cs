using System;
using System.Diagnostics;

namespace adventofcode
{
    public abstract class BaseSolution
    {
        public virtual Tuple<long, T> Decorator<T>(string input, Func<string, T> methodToRun)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var solution = methodToRun(input);

            sw.Stop();

            Tuple<long, T> result = new Tuple<long, T>(sw.ElapsedMilliseconds, solution);
            return result;
        }
    }
}
