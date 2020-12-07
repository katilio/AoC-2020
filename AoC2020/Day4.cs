using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2020
{
    public class Day4
    {
        private int validPassportsPt1 = 0;
        private int validPassportsPt2 = 0;
        int totalPassports = 0;

        List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();
        List<HashSet<String>> passportFields = new List<HashSet<string>>();
        
        private (int, int) ValidPassports(string[] input, HashSet<String> requiredFields, HashSet<String> optionalFields)
        {
            passportFields.Add(new HashSet<string>());
            passports.Add(new Dictionary<string, string>());
            int listIndex = 0;
            int stringIndex = 0;
            int substringEndIndex = 0;
            for (int i = 0; i < input.Length; i++)
            {
                stringIndex = 0;
                substringEndIndex = 0;
                if (input[i] == "")
                {
                    passportFields.Add(new HashSet<string>());
                    passports.Add(new Dictionary<string, string>());
                    listIndex++;
                }
                else 
                {
                    while (stringIndex < input[i].Length)
                    {
                        substringEndIndex = input[i].IndexOf(':', stringIndex);
                        if (substringEndIndex != -1)
                        {
                            passportFields[listIndex].Add(input[i].Substring((substringEndIndex - 3), 3));
                            stringIndex = substringEndIndex+1;
                            if (input[i].IndexOf(' ', stringIndex) != -1)
                            {
                                passports[listIndex].Add(input[i].Substring((substringEndIndex - 3), 3),
                                    input[i].Substring(substringEndIndex+1, 
                                        input[i].IndexOf(' ', stringIndex) - (substringEndIndex+1)));
                            }
                            else
                            {
                                passports[listIndex].Add(input[i].Substring((substringEndIndex - 3), 3),
                                    input[i].Substring(substringEndIndex+1, input[i].Length - (substringEndIndex+1)));
                            }
                        }
                        else
                        {
                            stringIndex = input[i].Length;
                        }
                    }
                }
            }

            for (int i = 0; i < passportFields.Count; i++)
            {
                var passFields = passportFields[i];
                if (passFields.IsSupersetOf(requiredFields))
                {
                    validPassportsPt1++;
                    if (DataValidation(i))
                    {
                        validPassportsPt2++;
                    }
                }
            }

            return (validPassportsPt1, validPassportsPt2);
        }

        private bool DataValidation(int index)
        {
            foreach (var entry in passports[index])
            {
                switch (entry.Key)
                {
                    case ("byr"):
                        if (!(Convert.ToInt32(entry.Value) > 1919 && Convert.ToInt32(entry.Value) < 2003))
                        {
                            return false;
                        }
                        break;
                    
                    case ("iyr"):
                        if (!(Convert.ToInt32(entry.Value) > 2009 && Convert.ToInt32(entry.Value) < 2021))
                        {
                            return false;
                        }
                        break;
                    
                    case ("eyr"):
                        if (!(Convert.ToInt32(entry.Value) > 2019 && Convert.ToInt32(entry.Value) < 2031))
                        {
                            return false;
                        }
                        break;
                    
                    case ("hgt"):
                        if (!(entry.Value.Contains("cm") || entry.Value.Contains("in")))
                        {
                            return false;
                        }
                        int parsedNumber = 0;
                        if (((entry.Value.Contains("cm") && !(Int32.TryParse(entry.Value.Substring(0,3), out parsedNumber)))))
                        {
                            return false;
                        }
                        else if ((entry.Value.Contains("cm") && !(parsedNumber > 149 && parsedNumber < 194)))
                        {
                            return false;
                        }
                        if (((entry.Value.Contains("in") && !(Int32.TryParse(entry.Value.Substring(0,2), out parsedNumber)))))
                        {
                            return false;
                        }
                        else if ((entry.Value.Contains("in") && !(parsedNumber > 58 && parsedNumber < 77)))
                        {
                            return false;
                        }
                        break;
                    
                    case ("hcl"):
                        if (entry.Value[0] != '#')
                        {
                            return false;
                        }
                        else if (entry.Value.Length != 7)
                        {
                            return false;
                        }
                        else
                            foreach (char c in entry.Value)
                            {
                                if (!((((int) c > 96 && (int) c < 103) || ((int) c > 46 && (int) c < 58))) && c != '#')
                                {
                                    return false;
                                }
                            }
                        break;
                    
                    case ("ecl"):
                        if (!(entry.Value == "amb" || entry.Value == "blu" ||
                              entry.Value == "brn" || entry.Value == "gry" ||
                              entry.Value == "grn" || entry.Value == "hzl" || entry.Value == "oth"))
                        {
                            return false;
                        }
                        break;
                    
                    case ("pid"):
                        if (entry.Value.Length != 9)
                        {
                            return false;
                        }
                        else if (!(entry.Value.All(c => c >= '0' && c <= '9')))
                        {
                            return false;
                        }
                        break;
                    case ("cid"):
                    {
                        break;
                    }
                    default:
                        break;
                }
            }
            return true;
        }

        public void Part1()
        {
            var input = InputManager.StringArrayFromFile("day4.txt");
            HashSet<String> required = new HashSet<string>{"byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
            };
            
            HashSet<String> optional = new HashSet<string>{"cid"};

            Console.WriteLine("Day 4 Part 1, valid passports: " + ValidPassports(input, required, optional));
        }
        
        public void Part2()
        {
            var input = InputManager.StringArrayFromFile("day4.txt");
            HashSet<String> required = new HashSet<string>{"byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
            };
            
            HashSet<String> optional = new HashSet<string>{"cid"};

            Console.WriteLine(ValidPassports(input, required, optional));
        }
    }
}