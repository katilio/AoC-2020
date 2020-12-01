using System;
using System.IO;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = InputManager.StringArrayFromFile("day1.txt");
            Day1 day1 = new Day1();
            day1.Part1(input);
            day1.Part2(input);
        }
    }
}