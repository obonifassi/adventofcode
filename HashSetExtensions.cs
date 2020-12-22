using System.Collections.Generic;

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
