using System;
using System.Collections.Generic;
using System.Text;

namespace adventofcode
{
    interface ISolver
    {
        IEnumerable<Tuple<long, long>> Solve(string input);
    }
}
