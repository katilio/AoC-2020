using System;

namespace AoC2020
{
    public class Day3
    {
        private int CalculateTreesHit(string[] input, int lateralSlope, int downSlope)
        {
            int treesHit = 0;
            bool[][] map = new bool[input.Length][];
            
            for (int i = 0; i < input.Length; i++)
            {
                map[i] = new bool[input[i].Length];
                for (int j = 0; j < input[i].Length; j++)
                {
                    map[i][j] = (input[i][j] == '#' ? true : false);
                }
            }

            for (int i = 0; i < map.Length;)
            {
                treesHit += Convert.ToInt32((map[i][((i*lateralSlope)%map[i].Length)]));
                i += downSlope;
            }
            
            return treesHit;
        }

        public void BothParts(string[] input)
        {
            Console.WriteLine("Part 1: " + CalculateTreesHit(input, 3, 1));
            int[] treesHit = new int[5];
            treesHit[0] = CalculateTreesHit(input, 1, 1);
            treesHit[1] = CalculateTreesHit(input, 3, 1);
            treesHit[2] = CalculateTreesHit(input, 5, 1);
            treesHit[3] = CalculateTreesHit(input, 7, 1);
            treesHit[4] = CalculateTreesHit(input, 1, 2);

            long totalTrees = 1;

            foreach (var number in treesHit)
            {
                totalTrees *= number;
            }
            
            Console.WriteLine("Part 2: " + totalTrees);
        }
    }
}