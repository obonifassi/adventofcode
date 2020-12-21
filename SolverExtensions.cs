using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace adventofcode
{
    static class SolverExtensions
    {
        public static string WorkingDirectory(this ISolver solver)
        {
            var year = solver.Year();
            var day = solver.Day();

            var path = Path.Combine(year.ToString(), $"Day{day.ToString("00")}");

            return path;
        }

        public static int Year(this ISolver solver)
        {
            var fullname = solver
                            .GetType()
                            .FullName;

            var year = fullname.Split(".")[1].Substring(1);

            return int.Parse(year);
        }

        public static int Day(this ISolver solver)
        {
            var fullName = solver
                            .GetType()
                            .FullName;

            var day = fullName.Split(".")[2].Substring(3);

            return int.Parse(day);
        }

        public static string DayName(this ISolver solver)
        {
            return $"Day {solver.Day()}";
        }

        public static string GetName(this ISolver solver)
        {
            return solver
                .GetType()
                .GetCustomAttribute<DisplayNameAttribute>().DisplayName;
        }
    }
}
