using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace adventofcode._2020
{
    public class Day1
    {
        protected const string INPUT_FILE = @"2020\Day1Input.txt";
        protected const int TARGET = 2020;
        protected static string[] _input;

        public Day1 ()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), INPUT_FILE);
            _input = File.ReadAllLines(path);
        }

        public long find2Sum()
        {
            var items = _input
                            .ToDictionary(x => Convert.ToInt32(x), y => Convert.ToInt32(y));

            long secretKey = 0;

            foreach(var kvp in items)
            {
                var difference = Math.Abs(kvp.Key - TARGET);

                if (items.ContainsKey(difference))
                {
                    secretKey = (kvp.Key * difference);
                    break;
                }
            }

            return secretKey;
        }

        public long find3Sum()
        {
            var items = _input
                            .Select(x => Convert.ToInt32(x.Trim()))
                            .ToArray();

            long secretKey = 0;

            for (int i = 0; i < (items.Length - 1); i++)
            {
                HashSet<int> map = new HashSet<int>();

                int currentSum = (TARGET - items[i]);

                for (int j = i + 1; j < (items.Length - 1); j++)
                {
                    var difference = currentSum - items[j];

                    if (map.Contains(difference))
                    {
                        secretKey = items[i] * items[j] * difference;
                        break;
                    }

                    map.Add(items[j]);
                }
            }

            return secretKey;
        }
    }
}
