using System;

namespace AoC2020
{
    public class Day10
    {
        private string[] input = InputManager.StringArrayFromFile("day10.txt");


        public void Part1()
        {
            int[] joltageList = new int[input.Length];
            //Always at least 1 gap for both
            int gapsBy1 = 1;
            int gapsBy3 = 1;
            
            for (int i = 0; i < input.Length; i++)
            {
                joltageList[i] = Int32.Parse(input[i]);
            }

            Array.Sort(joltageList);

            for (int i = 0; i < joltageList.Length-1; i++)
            {
                int diff = joltageList[i + 1] - joltageList[i];
                if (diff == 1)
                {
                    gapsBy1++;
                }
                else if (diff == 3)
                {
                    gapsBy3++;
                }
            }
            
            Console.WriteLine("Day 10, part 1, gaps by 1 = " + gapsBy1 + ", gaps by 3 =" + gapsBy3 + ", solution = " + (gapsBy1*gapsBy3));
        }

        public void Part2()
        {
            int[] joltageList = new int[input.Length];
            //Always at least 1 gap for both
            int streaksOf2 = 0;
            int streaksOf3 = 0;
            int streaksOf4 = 0;
            int streaksOf5 = 0;
            int streaksOf6 = 0;

            long totalCombinations = 1;

            for (int i = 0; i < input.Length; i++)
            {
                joltageList[i] = Int32.Parse(input[i]);
            }
            Array.Sort(joltageList);

            for (int i = 0; i < joltageList.Length-1;)
            {
                int cursorIndex = 1;
                while ((i+cursorIndex < joltageList.Length) && (joltageList[i+cursorIndex] == joltageList[i]+cursorIndex))
                {
                    cursorIndex++;
                }

                switch (cursorIndex)
                {
                    case (3):
                        streaksOf3++;
                        totalCombinations *= 2;
                        break;
                    case (4):
                        streaksOf4++;
                        totalCombinations *= 4;
                        break;
                    case (5):
                        streaksOf5++;
                        totalCombinations = (totalCombinations == 1) ? 8 : totalCombinations * 7;
                        break;
                    case (6):
                        streaksOf6++;
                        totalCombinations *= 13;
                        break;
                }
                i += cursorIndex;
            }
            
            Console.WriteLine("Day 10, Part 2: " + (Math.Pow(2, streaksOf3) * Math.Pow(4, streaksOf4) * Math.Pow(7, streaksOf5)));

        }
    }
}