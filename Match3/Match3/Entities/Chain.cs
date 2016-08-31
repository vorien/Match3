using System.Collections.Generic;

using CocosSharp;

namespace Match3.Entities
{
    class Chain : CCNode
    {
        public List<candy> candies;
        public ChainType chainType;

        public enum ChainType
        {
            Horizontal, Vertical
        };

        public Chain()
        {
            candies = new List<candy>();
        }

        public void addCandy(candy candy)
        {
            candies.Add(candy);
        }
    }
}