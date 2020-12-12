using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2020
{
    public class Day11
    {
        private string[] input = InputManager.StringArrayFromFile("day11.txt");

        private int TraverseSeatsPart2(int[] input, (int, int) direction, (int, int) startingIndex, int gridWidth)
        {
            var traversing = true;
            int row = 1;
            int column = 1;
            (int, int) cursorIndex = (0, 0);
            int currentValue = 0;
            
            while (traversing)
            {
                cursorIndex = (startingIndex.Item1 + (row * direction.Item1),
                    startingIndex.Item2 + (column * direction.Item2));
                //If within bounds
                if ((cursorIndex.Item1 > -1 && cursorIndex.Item1 < (input.Length / gridWidth))
                    && (cursorIndex.Item2 > -1 && cursorIndex.Item2 < gridWidth))
                {
                    currentValue = input[(cursorIndex.Item1 * gridWidth) + cursorIndex.Item2];
                    if (input[(cursorIndex.Item1 * gridWidth) + cursorIndex.Item2] == 0)
                    {
                        row++;
                        column++;
                    }
                    else
                    {
                        traversing = false;
                    }
                }
                else
                {
                    traversing = false;
                    
                }
            }
            //Console.WriteLine("Traversed to seat: " + cursorIndex + " - value found: " + currentValue);
            return currentValue;
        }

        private int[] UpdateSeatsPart2(int[] input, int gridWidth)
        {
            int[] result = new int[input.Length];

            //Every grid line
            for (int i = 0; i < (input.Length / gridWidth); i++)
            {
                //Every seat in that line
                for (int j = 0; j < gridWidth; j++)
                {
                    if (input[(i * gridWidth) + j] == 0)
                    {
                        //result[(i * gridWidth) + j] = 0;
                    }
                    else
                    {

                        //Console.WriteLine("Traversing from seat: " + ((i * gridWidth) + j));
                        //Traverse in every direction
                        int usedNeighbors = 0;
                        List<Thread> startedThreads = new List<Thread>();
                        for (int k = -1; k < 2; k++)
                        {
                            for (int l = -1; l < 2; l++)
                            {
                                if (!(l == 0 && k == 0))
                                {
                                    int myInt = TraverseSeatsPart2(input, (k, l), (i, j), gridWidth);
                                    if (myInt == 2)
                                    {
                                        usedNeighbors++;
                                    }
                                }
                            }
                            
                            switch (input[(i * gridWidth) + j])
                            {
                                case (1):
                                    result[(i * gridWidth) + j] = (usedNeighbors == 0) ? 2 : 1;
                                    //Console.WriteLine("Seat was: " + input[(i * gridWidth) + j] + ", is now: " + result[(i * gridWidth) + j] + ". It had used neighbours: " + usedNeighbors);
                                    break;
                                case (2):
                                {
                                    result[(i * gridWidth) + j] = (usedNeighbors > 4) ? 1 : 2;
                                    //Console.WriteLine("Seat was: " + input[(i * gridWidth) + j] + ", is now: " + result[(i * gridWidth) + j] + ". It had used neighbours: " + usedNeighbors);
                                    break;
                                }
                                case (0):
                                    break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private int[] UpdateSeatsPart1(int[] seats, int gridWidth)
        {
            int[] result = new int[seats.Length];
            for (int i = 0; i < seats.Length; i++)
            {
                int usedNeighbors = 0;
                for (int j = -1; j < 2; j++)
                {
                    for (int k = -1; k < 2; k++)
                    {
                        //Left edge
                        if (i % gridWidth == 0 && k == -1)
                        {
                            k++;
                        }

                        //Right edge
                        if (i % gridWidth == (gridWidth-1) && k == 1)
                        {
                            break;
                        }
                        if (((i + (j * gridWidth) + k)) > -1 && ((i + (j * gridWidth) + k)) < seats.Length && !(j == 0 && j == k))
                        {
                            //Console.WriteLine("Seat: " + i + ", visiting " + ((i + (j * gridWidth) + k)) + ", " + (j+k) + " neighbor");
                            switch (seats[((i + (j * gridWidth) + k))])
                            {
                                case (2):
                                    usedNeighbors++;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                switch (seats[i])
                {
                    case (1):
                        result[i] = (usedNeighbors == 0) ? 2 : 1;
                        break;
                    case (2):
                    {
                        result[i] = (usedNeighbors > 3) ? 1 : 2;
                        break;
                    }
                    case (0):
                        break;
                }
            }

            return result;
        }
        
        public void Part1()
        {
            int gridWidth = input[0].Length;
            //char[] allChars = new char[input.Length * rowLen];
            int[] intSeats = new int[input.Length * gridWidth];

            
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    //allChars[(i * rowLen) + j] = input[i][j];
                    intSeats[(i * gridWidth) + j] = (input[i][j] == '.') ? 0 : (input[i][j] == 'L') ? 1 : 2;
                }
            }
            
            //int freeSeats = intSeats.Count(x => x == 1);
            int occupiedSeats = 1;
            //int emptySpots = ((input.Length * gridWidth) - (freeSeats + occupiedSeats));
            int prevOccupiedSeats = 0;
            int iteration = 1;

            while (occupiedSeats != prevOccupiedSeats)
            {
                prevOccupiedSeats = occupiedSeats;
                intSeats = UpdateSeatsPart1(intSeats, gridWidth);
                occupiedSeats = intSeats.Count(x => x == 2);
                Console.WriteLine("Iteration number " + iteration + ", previously occupied: " + prevOccupiedSeats + ", occupied now " + occupiedSeats);
                if (prevOccupiedSeats == occupiedSeats) {Console.WriteLine("Day 11, Part 1: seats = " + occupiedSeats);
                    break;
                }

                iteration++;
            }
        }
        
        public void Part2()
        {
            int gridWidth = input[0].Length;
            int[] intSeats = new int[input.Length * gridWidth];

            
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    intSeats[(i * gridWidth) + j] = (input[i][j] == '.') ? 0 : (input[i][j] == 'L') ? 1 : 2;
                }
            }
            int occupiedSeats = 1;
            int prevOccupiedSeats = 0;
            int iteration = 1;

            while (occupiedSeats != prevOccupiedSeats)
            {
                prevOccupiedSeats = occupiedSeats;
                intSeats = UpdateSeatsPart2(intSeats, gridWidth);
                occupiedSeats = intSeats.Count(x => x == 2);
                Console.WriteLine("Iteration number " + iteration + ", previously occupied: " + prevOccupiedSeats + ", occupied now " + occupiedSeats);
                if (prevOccupiedSeats == occupiedSeats) {Console.WriteLine("Day 11, Part 1: seats = " + occupiedSeats);
                    break;
                }
                iteration++;
            }
        }
    }
}