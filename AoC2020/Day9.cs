using System;
using System.Linq;

namespace AoC2020
{
    public class Day9
    {
        private string[] input = InputManager.StringArrayFromFile("day9.txt");

        private bool IsValidNumber(long[] numbers, int index, int preambleLen)
        {
            int startIndex = index - preambleLen;
            for (int i = startIndex; i < index; i++)
            {
                for (int j = startIndex; j < index; j++)
                {
                    if (numbers[j] + numbers[i] == numbers[index] && i != j)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private long[] FindContiguousSet(long[] numbers, long sum)
        {
            var filtered = numbers.TakeWhile(x => x < sum).ToArray();
            
            for (int j = 1; j < filtered.Count()-1; j++)
            {
                for (int i = filtered.Count()-1; i > j; i--)
                {
                    var summed = new long[i-j];
                    Array.Copy(filtered, j, summed, 0, i-j);
                    if (summed.Sum(x => x) == sum)
                    {
                        return summed;
                    }
                }
            }

            Console.WriteLine("Couldn't find a set.");
            return new long[0];
        }
        public void Part1()
        {
            long invalidNumber = 0;
            long[] numbers = new long[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                numbers[i] = long.Parse(input[i]);
            }

            for (int i = 25; i < numbers.Length; i++)
            {
                if (!IsValidNumber(numbers, i, 25))
                {
                    Console.WriteLine("Day 9, part 1: " + numbers[i]);
                    invalidNumber = numbers[i];
                }
            }
            var set = FindContiguousSet(numbers, invalidNumber);
            Console.WriteLine("Day 9, part 2 : " + (set.Min() + set.Max()));

        }
    }
}