using System;
using System.IO;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = InputManager.StringArrayFromFile("day6.txt");
            Day6 day6 = new Day6();
            day6.Part1(input);
        }
    }
}