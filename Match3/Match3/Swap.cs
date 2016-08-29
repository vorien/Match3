using CocosSharp;
using Match3.Entities;

namespace Match3
{
    class Swap : CCNode
    {
        // candies that will be swapped
        public candy candyA, candyB;

        // This class is supposed to be a set of candies that can/(are to) be swapped
        public Swap()
        {
            // initializes the two candy pointers to null
            candyA = null;
            candyB = null;
        }
    }
}