using System;
using System.IO;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = InputManager.StringArrayFromFile("day2.txt");
            Day2 day2 = new Day2();
            day2.Part1(input);
            //day1.Part2(input);
        }
    }
}