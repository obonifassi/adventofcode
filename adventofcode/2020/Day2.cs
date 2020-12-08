using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode._2020
{
    public class Day2
    {
        protected const string INPUT_FILE = @"2020\Day2Input.txt";
        protected static string[] _input;

        public Day2 ()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), INPUT_FILE);
            _input = File.ReadAllLines(path); 
        }

        public int PasswordPhilosophy()
        {
            int validPassword = 0;

            foreach (var item in _input)
            {
                var currentItem = ProcessInput(item);

                if(currentItem.Map.ContainsKey(currentItem.Key))
                {
                    var value = currentItem.Map[currentItem.Key];
                    
                    if(value >= currentItem.Min && value <= currentItem.Max)
                    {
                        validPassword++;
                    }
                }    
            }

            return validPassword;
        }

        private PasswordInfo ProcessInput(string item)
        {
            PasswordInfo result = new PasswordInfo();

            var entries = item.Split(" ");

            var ranges = entries[0].Split("-");

            var min = Convert.ToInt32(ranges[0]);
            var max = Convert.ToInt32(ranges[1]);

            var key = entries[1].Split(":").FirstOrDefault();

            var map = entries[2].GroupBy(x => x)
                                .ToDictionary(y => y.Key.ToString(), y => y.Count());

            return new PasswordInfo
            {
                Min = min,
                Max = max,
                Map = map,
                Key = key
            };
        }

        public class PasswordInfo
        {
            public string Key { get; set; }
            public int Max { get; set; }
            public int Min { get; set; }
            public Dictionary<string,int> Map { get; set; }
        }
    }
}
