using System;
using System.IO;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = InputManager.StringArrayFromFile("day3.txt");
            Day5 day5 = new Day5();
            day5.BothParts();
        }
    }
}