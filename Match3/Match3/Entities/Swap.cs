using CocosSharp;

namespace Match3.Entities
{
    class Swap : CCNode
    {
        // candies that will be swapped
        public Candy candyA, candyB;

        // This class is supposed to be a set of candies that can/(are to) be swapped
        public Swap()
        {
            // initializes the two candy pointers to null
            candyA = null;
            candyB = null;
        }
    }
}