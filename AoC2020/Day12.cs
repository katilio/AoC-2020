using System;

namespace AoC2020
{
    enum DirectionEnum
    {
        N,
        E,
        S,
        W
    }
    
    public class Day12
    {
        private string[] input = InputManager.StringArrayFromFile("day12.txt");


        public void Part1And2()
        {
            DirectionEnum direction = DirectionEnum.E;
            (int, int) orientation = (0, 1);
            (int, int) position = (0, 0);
            //Part 1
            foreach (var instruction in input)
            {
                ProcessInstructionPart1(instruction, ref orientation, ref position, ref direction);
            }
            Console.WriteLine("Day 12, part 1: Manhattan distance = " + (Math.Abs(position.Item1) + Math.Abs(position.Item2)));

            //Part 2
            (int, int) waypoint = (-1, 10);
            position = (0, 0);
            foreach (var instruction in input)
            {
                ProcessInstructionPart2(instruction, ref orientation, ref position, ref direction, ref waypoint);
            }
            Console.WriteLine("Day 12, part 2: Manhattan distance = " + (Math.Abs(position.Item1) + Math.Abs(position.Item2)));

        }

        private void ProcessInstructionPart1(string instruction, ref (int, int) orientation, ref (int, int) position, ref DirectionEnum direction)
        {
            var value = int.Parse(instruction.Substring(1));
            switch (instruction[0])
            {
                case ('N'):
                    position.Item1 -= value;
                    break;
                case ('S'):
                    position.Item1 += value;
                    break;
                case ('E'):
                    position.Item2 += value;
                    break;
                case ('W'):
                    position.Item2 -= value;
                    break;
                case ('L'):
                    orientation = HeadingToOrientation(ref direction, value, instruction[0]);
                    break;
                case ('R'):
                    orientation = HeadingToOrientation(ref direction, value, instruction[0]);
                    break;
                case ('F'):
                    MoveForward(orientation, value, ref position);
                    break;

            }
        }

        private void ProcessInstructionPart2(string instruction, ref (int, int) orientation, ref (int, int) position, ref DirectionEnum direction, ref (int, int) waypoint)
        {
            var value = int.Parse(instruction.Substring(1));
            switch (instruction[0])
            {
                case ('N'):
                    MoveWaypoint(DirectionEnum.N, value, ref waypoint);
                    break;
                case ('S'):
                    MoveWaypoint(DirectionEnum.S, value, ref waypoint);
                    break;
                case ('E'):
                    MoveWaypoint(DirectionEnum.E, value, ref waypoint);
                    break;
                case ('W'):
                    MoveWaypoint(DirectionEnum.W, value, ref waypoint);
                    break;
                case ('L'):
                    waypoint = RotateWaypoint('L', value, waypoint);
                    break;
                case ('R'):
                    waypoint = RotateWaypoint('R', value, waypoint);
                    break;
                case ('F'):
                    MoveShip(ref position, waypoint, value);
                    break;

            }
        }

        private void MoveShip(ref (int, int) position, (int, int) waypoint, int value)
        {
            for (int i = 0; i < value; i++)
            {
                position.Item1 = position.Item1 + waypoint.Item1;
                position.Item2 = position.Item2 + waypoint.Item2;
            }
        }

        private void MoveWaypoint(DirectionEnum direction, int value, ref (int, int) waypoint)
        {
            switch (direction)
            {
                case (DirectionEnum.N):
                    waypoint.Item1 += (value*-1);
                    break;
                case (DirectionEnum.E):
                    waypoint.Item2 += (value*1);
                    break;
                case (DirectionEnum.S):
                    waypoint.Item1 += (value*1);
                    break;
                case (DirectionEnum.W):
                    waypoint.Item2 += (value*-1);
                    break;
            }
        }


        private void MoveForward((int, int) orientation, int value, ref (int, int) position)
        {
            position.Item1 += orientation.Item1 * value;
            position.Item2 += orientation.Item2 * value;
        }

        private (int, int) RotateWaypoint(char turningSide, int amount, (int, int) startingPos)
        {
            (int, int) result = (0, 0);
            var turns = amount / 90;
            if (turningSide == 'R')
            {
                switch (turns)
                {
                    case (1):
                        result.Item1 = startingPos.Item2;
                        result.Item2 = -(startingPos.Item1);
                        break;
                    case (2):
                        result.Item1 = -(startingPos.Item1);
                        result.Item2 = -(startingPos.Item2);
                        break;
                    case (3):
                        result.Item1 = -(startingPos.Item2);
                        result.Item2 = startingPos.Item1;
                        break;
                }
            }
            else
            {
                switch (turns)
                {
                    case (3):
                        result.Item1 = startingPos.Item2;
                        result.Item2 = -(startingPos.Item1);
                        break;
                    case (2):
                        result.Item1 = -(startingPos.Item1);
                        result.Item2 = -(startingPos.Item2);
                        break;
                    case (1):
                        result.Item1 = -(startingPos.Item2);
                        result.Item2 = startingPos.Item1;
                        break;
                }
            }

            return result;
        }

        private (int, int) HeadingToOrientation(ref DirectionEnum direction, int value, char turningSide)
        {
            (int, int) newOrientation = (0, 0);

            int absoluteDirectionChange = 0;

            if (turningSide == 'R')
            {
                absoluteDirectionChange = ((int)(direction + (value / 90)) % 4);
            }
            else
            {
                absoluteDirectionChange = ((int)(direction - (value / 90)) % 4);
            }
            
            if (absoluteDirectionChange < 0)
            {
                absoluteDirectionChange = (absoluteDirectionChange == -1) ? 3 :
                    (absoluteDirectionChange == -2) ? 2 :
                    (absoluteDirectionChange == -3) ? 1 : 0;
            }

            direction = (DirectionEnum) absoluteDirectionChange;

            switch (direction)
            {
                case (DirectionEnum.N):
                    newOrientation = (-1, 0);
                    break;
                case (DirectionEnum.E):
                    newOrientation = (0, 1);
                    break;
                case (DirectionEnum.S):
                    newOrientation = (1, 0);
                    break;
                case (DirectionEnum.W):
                    newOrientation = (0, -1);
                    break;
            }
            return newOrientation;
        }
    }
}