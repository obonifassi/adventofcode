using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace adventofcode.Y2020.Day12
{
    [DisplayNameAttribute("Rain Risk")]
    public class Solution : BaseSolution, ISolver
    {
        public IEnumerable<Tuple<long, long>> Solve(string input)
        {
            var partOne = Decorator(input, PartOne);
            yield return partOne;

            var partTwo = Decorator(input, PartTwo);
            yield return partTwo;
        }

        long PartOne(string input)
        {
            var instructions = input.Split("\r\n").Select(x => x.Trim()).ToArray();

            //initialize boat
            var boat = new Boat()
            {
                X = 0,
                Y = 0,
                Angle = 90
            };

            foreach (var instruction in instructions)
            {
                var action = instruction.Substring(0, 1);
                var value = Convert.ToInt32(instruction[1..]);
                switch (action)
                {
                    case "N":
                        boat.Y += value;
                        break;
                    case "S":
                        boat.Y -= value;
                        break;
                    case "E":
                        boat.X += value;
                        break;
                    case "W":
                        boat.X -= value;
                        break;
                    case "L":
                        boat.Angle = (360 + boat.Angle - value) % 360;
                        break;
                    case "R":
                        boat.Angle = (boat.Angle + value) % 360;
                        break;
                    case "F":
                        switch (boat.Angle.ToString())
                        {
                            case "0":
                                boat.Y += value;
                                break;
                            case "180":
                                boat.Y -= value;
                                break;
                            case "90":
                                boat.X += value;
                                break;
                            case "270":
                                boat.X -= value;
                                break;
                        }
                        break;
                }
            }

            var distance = Math.Abs(boat.X) + Math.Abs(boat.Y);
            return distance;
        }

        long PartTwo(string input)
        {
            var instructions = input.Split("\r\n").Select(x => x.Trim()).ToArray();

            //initialize the boat
            var boat = new Boat { X = 0, Y = 0 };

            //initialize the waypoint
            var wayPoint = new WayPoint() { X = 10, Y = 1 };

            foreach (var instruction in instructions)
            {
                var action = instruction.Substring(0, 1);
                var value = Convert.ToInt32(instruction[1..]);
                switch (action)
                {
                    case "N":
                        wayPoint.Y += value;
                        break;
                    case "S":
                        wayPoint.Y -= value;
                        break;
                    case "E":
                        wayPoint.X += value;
                        break;
                    case "W":
                        wayPoint.X -= value;
                        break;
                    case "L":
                        var leftAngle = DegreeToRadian(value);
                        (wayPoint.X, wayPoint.Y) = RotatePoints(wayPoint.X, wayPoint.Y, leftAngle);
                        break;
                    case "R":
                        var rightAngle = DegreeToRadian(value);
                        (wayPoint.X, wayPoint.Y) = RotatePoints(wayPoint.X, wayPoint.Y, -rightAngle);
                        break;
                    case "F":
                        boat.X += wayPoint.X * value;
                        boat.Y += wayPoint.Y * value;
                        break;
                }
            }

            var distance = Math.Abs(boat.X) + Math.Abs(boat.Y);
            return distance;
        }

        (int, int) RotatePoints(int x, int y, double angle)
        {
            var newX = Math.Round((x * Math.Cos(angle)) - (y * Math.Sin(angle)));
            var newY = Math.Round((y * Math.Cos(angle)) + (x * Math.Sin(angle)));

            return ((int)newX, (int)newY);
        }

        double DegreeToRadian(int degree)
        {
            var radian = degree * (Math.PI / 180);
            return radian;
        }
    }

    abstract class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class WayPoint : Vector
    {
    }

    class Boat : Vector
    {
        public int Angle { get; set; }
    }
}