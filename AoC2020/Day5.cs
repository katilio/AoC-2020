using System;
using System.Linq;

namespace AoC2020
{
    public class Day5
    {
        private int[] rows;
        private (int, bool)[] usedSeats;
        private int[] columns;
        private int[] IDs;
        private int highestID = 0;
        
        public void BothParts()
        {
            var input = InputManager.StringArrayFromFile("day5.txt");
            rows = new int[input.Length];
            columns = new int[input.Length];
            IDs = new int[input.Length];
            usedSeats = new (int, bool)[889];
            
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length-3; j++)
                {
                    rows[i] += (int)Math.Pow(2, 6-j) * ((input[i][j] == 'B') ? 1 : 0);
                }
                for (int j = input[i].Length-3; j < input[i].Length; j++)
                {
                    columns[i] += (int)Math.Pow(2, 9-j) * ((input[i][j] == 'R') ? 1 : 0);
                }

                IDs[i] = (rows[i] * 8) + columns[i];
                if (IDs[i] > highestID)
                {
                    highestID = IDs[i];
                }
            }

            Array.Sort(IDs);
            foreach (var id in IDs)
            {
                usedSeats[(((id / 8)+1) * (id % 8))] = (id, true);
            }

            for (int i = 0; i < usedSeats.Length; i++)
            {
                if (usedSeats[i].Item2 == false && usedSeats[i + 1].Item2 == true && usedSeats[i - 1].Item2 == true)
                {
                    Console.WriteLine("Day 5, part 2, seat ID: " + usedSeats[i].Item1);
                    break;
                }
            }

            Console.WriteLine("Day 5, Part 1: " + highestID);
        }

    }
}