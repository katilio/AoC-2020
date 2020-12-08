using System.Collections.Generic;

namespace AoC2020
{
    public class LuggageBag
    {
        public string myAdjective;
        public string myColor;
        public List<LuggageBag> myContents;
        public List<int> myAmounts;

        public LuggageBag(string adjective, string color, List<LuggageBag> contents)
        {
            myAdjective = adjective;
            myColor = color;
            myContents = contents;
            myAmounts = new List<int>();
        }
        public bool Contains(LuggageBag bag)
        {
            foreach (var content in myContents)
            {
                if (bag.myAdjective == content.myAdjective && bag.myColor == content.myColor)
                {
                    return true;
                }

                if (content.Contains(bag))
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}