using System;
using System.Collections.Generic;
using System.Text;

namespace adventofcode
{
    interface ISolver
    {
        IEnumerable<Tuple<object, long>> Solve(string input);
    }
}
