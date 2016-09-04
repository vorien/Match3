using System.Collections.Generic;

using CocosSharp;

namespace Match3.Entities
{
    class Chain : CCNode
    {
        public List<Candy> candies;
        public ChainType chainType;

        public enum ChainType
        {
            Horizontal, Vertical
        };

        public Chain()
        {
            candies = new List<Candy>();
        }

        public void addCandy(Candy candy)
        {
            candies.Add(candy);
        }
    }
}