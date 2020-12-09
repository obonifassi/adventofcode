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

        public int PasswordPhilosophyPart1()
        {
            int validPassword = 0;

            foreach (var item in _input)
            {
                var currentItem = ProcessInput(item, IsMap: true);

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

        public int PasswordPhilosophyPart2()
        {
            int validPassword = 0;

            foreach (var item in _input)
            {
                var currentItem = ProcessInput(item);

                var min = (currentItem.Min - 1);
                var max = (currentItem.Max - 1);

                bool p1 = false;
                if(min <= currentItem.Values.Length)
                {
                    p1 = currentItem.Values[(currentItem.Min - 1)] == currentItem.Key;
                }

                bool p2 = false;
                if (max <= currentItem.Values.Length)
                {
                    p2 = currentItem.Values[(currentItem.Max - 1)] == currentItem.Key;
                }

                if(p1 != p2)
                {
                    validPassword++;
                }
            }

            return validPassword;
        }

        private PasswordInfo ProcessInput(string item, bool IsMap = false)
        {
            PasswordInfo result = new PasswordInfo();

            var entries = item.Split(" ");

            var ranges = entries[0].Split("-");

            var min = Convert.ToInt32(ranges[0]);
            var max = Convert.ToInt32(ranges[1]);

            var key = entries[1].Split(":").FirstOrDefault();

            PasswordInfo pwd = new PasswordInfo()
            {
                Min = min,
                Max = max,
                Key = key
            };

            if (IsMap)
            {
                pwd.Map = entries[2].GroupBy(x => x).ToDictionary(x => x.Key.ToString(), x => x.Count());
            }
            else
            {
                pwd.Values = entries[2].Select(x => x.ToString()).ToArray();
            }

            return pwd;
        }

        public class PasswordInfo
        {
            public string Key { get; set; }
            public int Max { get; set; }
            public int Min { get; set; }
            public Dictionary<string,int> Map { get; set; }
            public string[] Values { get; set; }
        }
    }
}
