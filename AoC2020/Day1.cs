using System;
using System.Linq;

namespace AoC2020
{
    public class Day1
    {
        public void Part1(string[] input)
        {
            for (int i = 0; i < input.Length-1; i++)
            {
                var wantedPair = (2020 - int.Parse(input[i])).ToString();
                if (input.Contains(wantedPair))
                {
                    Console.WriteLine(int.Parse((input[i])) * int.Parse(wantedPair));
                    break;
                }
            }
            return;
        }

        public void Part2(string[] input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                for (int j = 0; j < input.Length - 1; j++)
                {
                    var wantedPair = (2020 - ((int.Parse(input[i])) + int.Parse((input[j])))).ToString();

                    if (input.Contains(wantedPair))
                    {
                        Console.WriteLine(int.Parse((input[i])) * int.Parse(wantedPair) * int.Parse(input[j]));
                        break;
                    }
                }
            }
            return;
        }
    }
}