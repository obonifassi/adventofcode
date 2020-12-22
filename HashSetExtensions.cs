using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace adventofcode
{
    public static class HashSetExtensions
    {
        public static HashSet<T> Union<T>(this HashSet<T> self, HashSet<T> other)
        {
            var set = new HashSet<T>(self); // don't change the original set
            set.UnionWith(other);
            return set;
        }
    }
}
