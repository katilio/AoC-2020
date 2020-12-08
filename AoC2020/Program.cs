using System;
using System.IO;

namespace AoC2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = InputManager.StringArrayFromFile("day6.txt");
            Day7 day7 = new Day7();
            day7.Part1();
        }
    }
}