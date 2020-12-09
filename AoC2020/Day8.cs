using System;

namespace AoC2020
{
    public class Day8
    {
        private string[] input = InputManager.StringArrayFromFile("day8.txt");

        private enum CommandType
        {
            nop,
            acc,
            jmp
        }

        //Returns next index, writes to accumulator
        private int ProcessCommand(CommandType cmd, int modifier, ref int accumulator)
        {
            int nextIndex = 1;
            switch (cmd)
            {
                case (CommandType.acc):
                    accumulator += modifier;
                    break;
                
                case (CommandType.nop):
                    break;
                
                case (CommandType.jmp):
                    nextIndex = modifier;
                    break;
            }

            return nextIndex;
        }

        public int Part1()
        {
            int accumulator = 0;
            CommandType[] commands = new CommandType[input.Length];
            int[] modifiers = new int[input.Length];
            bool[] visited = new bool[input.Length];

            //Populate from input
            for (int i = 0; i < input.Length; i++)
            {
                var line = input[i];
                
                switch (line.Substring(0, 3))
                    {
                        case ("jmp"): commands[i] = CommandType.jmp;
                            break;
                        case("acc"): commands[i] = CommandType.acc;
                            break;
                        case ("nop"): commands[i] = CommandType.nop;
                            break;
                    }
                modifiers[i] = Int32.Parse(line.Substring(4, line.Length-4));
            }

            for (int i = 0; i < commands.Length;)
            {
                if (visited[i] == true)
                {
                    Console.WriteLine("Accumulator: " + accumulator);
                    break;
                }
                visited[i] = true;
                i += ProcessCommand(commands[i], modifiers[i], ref accumulator);
            }

            return accumulator;
        }
        
        public int Part2()
        {
            int accumulator = 0;
            CommandType[] originalCommands = new CommandType[input.Length];
            var commands = originalCommands;
            int[] modifiers = new int[input.Length];
            bool[] visited = new bool[input.Length];
            

            for (int i = 0; i < input.Length; i++)
            {
                accumulator = 0;
                visited = new bool[input.Length];
                for (int k = 0; k < input.Length; k++)
                {
                    var line = input[k];
                
                    switch (line.Substring(0, 3))
                    {
                        case ("jmp"): commands[k] = CommandType.jmp;
                            break;
                        case("acc"): commands[k] = CommandType.acc;
                            break;
                        case ("nop"): commands[k] = CommandType.nop;
                            break;
                    }
                }
                if (commands[i] == CommandType.jmp || commands[i] == CommandType.nop)
                {
                    commands[i] = (commands[i] == CommandType.jmp) ? CommandType.nop : CommandType.jmp;
                    for (int j = 0; j < commands.Length;)
                    {
                        if (j == (commands.Length-1))
                        {
                            j += ProcessCommand(commands[j], modifiers[j], ref accumulator);
                            Console.WriteLine("Part 2: accumulator =" + accumulator);
                            break;
                        }
                        else if (visited[j] == true)
                        {
                            break;
                        }
                        visited[j] = true;
                        j += ProcessCommand(commands[j], modifiers[j], ref accumulator);
                    }
                }
            }


            return accumulator;
        }
        
    }
}