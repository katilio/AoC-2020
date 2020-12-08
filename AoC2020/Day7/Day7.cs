using System;
using System.Collections.Generic;

namespace AoC2020
{
    public class Day7
    {

        private int RecursiveContain(LuggageBag bag, List<LuggageBag> rules, ref List<LuggageBag> containers)
        {
            foreach (var bag2 in rules)
            {
                if (bag2.Contains(bag))
                {
                    if (!containers.Contains(bag2))
                    {
                        containers.Add(bag2);
                        Console.WriteLine(bag.myAdjective + " " + bag.myColor + " contains one!");
                        //amount++;
                        RecursiveContain(bag2, rules, ref containers);
                    }
                }
            }
            return containers.Count;
        }

        private double BagContents(LuggageBag bag, List<LuggageBag> rules)
        {
            double amount = 0;
            for (int i = 0; i < bag.myContents.Count; i++)
            {
                amount += bag.myAmounts[i];
                amount += bag.myAmounts[i] * BagContents(rules.Find(x => x.myAdjective == bag.myContents[i].myAdjective && x.myColor == bag.myContents[i].myColor), rules);
            }
            return amount;
        }
        public void Part1()
        {
            var input = InputManager.StringArrayFromFile("day7.txt");
            List<LuggageBag> rules = new List<LuggageBag>();
            for (int i = 0; i < input.Length; i++)
            {
                var words = input[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                rules.Add((new LuggageBag(words[0], words[1], new List<LuggageBag>())));
                int amount = 0;

                for (int j = 2; j < words.Length; j++)
                {
                    if (words[j] == "bags" || words[j] == "contain" || words[j] == ",")
                    {
                        j++;
                    }

                    if (int.TryParse(words[j], out amount))
                    {
                        rules[i].myContents.Add(new LuggageBag(words[j+1], words[j+2], new List<LuggageBag>()));
                        rules[i].myAmounts.Add(amount);
                    }
                }
            }

            int totalAmount = 0;
            List<LuggageBag> containShinyGold = new List<LuggageBag>();

            Console.WriteLine("Day 7, Part 1:" + RecursiveContain(new LuggageBag("shiny", "gold", new List<LuggageBag>()), rules, ref containShinyGold));
            
            Console.Write("Day 7, Part 2: " + BagContents(rules.Find(x => x.myAdjective == "shiny" && x.myColor == "gold"), rules));
        }
    }
}