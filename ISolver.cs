using System;
using System.Collections.Generic;

namespace adventofcode
{
    interface ISolver
    {
        IEnumerable<Tuple<long, long>> Solve(string input);
    }
}
