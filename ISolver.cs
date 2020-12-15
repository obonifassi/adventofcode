using System;
using System.Collections.Generic;
using System.Text;

namespace adventofcode
{
    interface ISolver
    {
        IEnumerable<object> Solve(string input);
    }
}
