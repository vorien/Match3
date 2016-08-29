using System.Collections.Generic;

using CocosSharp;
using Match3.Entities;

namespace Match3
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