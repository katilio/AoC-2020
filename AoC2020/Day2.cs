using System;
using System.Linq;

namespace AoC2020
{
    public class Day2
    {
        
        public void BothParts(string[] input)
        {
            string[] passwords = new string[input.Length];
            int[] minimums = new int[input.Length];
            int[] maximums = new int[input.Length];
            char[] character = new char[input.Length];
            int validPasswords1 = 0;
            int validPasswords2 = 0;

            for (int i = 0; i < input.Length; i++)
            {
                string line = input[i];
                int dashIndex = line.IndexOf("-");
                int spaceIndex = line.IndexOf(" ");
                int colonIndex = line.IndexOf(":");
                minimums[i] = (int.Parse(line.Substring(0, dashIndex)));
                maximums[i] = (int.Parse(line.Substring(dashIndex+1, spaceIndex-dashIndex)));
                character[i] = (line.Substring(spaceIndex+1, colonIndex-spaceIndex))[0];
                passwords[i] = line.Substring(colonIndex, (input[i].Length - colonIndex));
            }

            for (int i = 0; i < passwords.Length; i++)
            {
                int count = (from c in passwords[i].ToCharArray()
                    where c.Equals(character[i])
                    select c).Count();
                if (count >= minimums[i] && count <= maximums[i])
                {
                    validPasswords1++;
                }

                if ( (Convert.ToInt16(passwords[i][(minimums[i]+1)] == character[i]) + Convert.ToInt16(passwords[i][(maximums[i]+1)] == character[i])) == 1)
                {
                    validPasswords2++;
                }
            }

            Console.WriteLine("Day 2, Part 1: " + validPasswords1);
            Console.WriteLine("Day 2, Part 2: " + validPasswords2);
        }
    }
}