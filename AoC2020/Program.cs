using System;
using System.IO;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = InputManager.StringArrayFromFile("day3.txt");
            Day3 day3 = new Day3();
            day3.BothParts(input);
        }
    }
}