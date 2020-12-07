using System;
using System.Collections.Generic;

namespace AoC2020
{
    public class Day6
    {
        public void Part1(string[] input)
        {
            List<Day6_FormResults> results = new List<Day6_FormResults>();
            //List<Dictionary<string, int>> answers = new List<Dictionary<string, int>>();
            int groupIndex = 0;
            int part1Total = 0;
            int part2Total = 0;
            results.Add(new Day6_FormResults());

            foreach (var line in input)
            {
                if (line == "")
                {
                    groupIndex++;
                    results.Add(new Day6_FormResults());
                }

                else
                {
                    results[groupIndex].respondants++;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (results[groupIndex].answers.ContainsKey(line[i].ToString()))
                        {
                            results[groupIndex].answers[line[i].ToString()]++;
                        }
                        else
                        {
                            results[groupIndex].answers.Add(line[i].ToString(), 1);
                        }
                    }
                }
            }

            foreach (var surveyResult in results)
            {
                foreach (var key in surveyResult.answers)
                {
                    part1Total++;
                    if (key.Value == surveyResult.respondants)
                    {
                        part2Total++;
                    }
                }
            }
            
            Console.WriteLine("Day 6, Part 1: " + part1Total + "-- Part 2: " + part2Total);
        }
    }
}