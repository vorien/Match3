using CocosSharp;

namespace Match3.Entities
{
    class Swap : CCNode
    {
        // candies that will be swapped
        public Candy fromCandy, toCandy;
        private CCPointI initialFromLocation, initialToLocation;
        private CCPoint initialFromPosition, initialToPosition;

        // This class is supposed to be a set of candies that can/(are to) be swapped
        public Swap(Candy from, Candy to)
        {
            // initializes the two candy pointers to null
            fromCandy = from;
            toCandy = to;
            initialFromLocation = fromCandy.gridLocation;
            initialToLocation = toCandy.gridLocation;
            initialFromPosition = fromCandy.Position;
            initialToPosition = toCandy.Position;

            //toCandy.debugLabel.Text = "TO";

        }

        //  Visually animates the swap using the CCMoveTo function provided by CocosSharp,
        //  also updates the grid location of the candies

        public void AnimateSwap()
        {
            fromCandy.RunAction(new CCMoveTo(0.3f, initialToPosition));
            toCandy.RunAction(new CCMoveTo(0.3f, initialFromPosition));

            //const float timeToTake = 0.3f; // in seconds

            ////  Animate the swapping of the candies
            //CCFiniteTimeAction swapFromToAction = new CCMoveTo(timeToTake, initialToLocation);
            //fromCandy.AddAction(swapFromToAction);
            //CCFiniteTimeAction swapToFromAction = new CCMoveTo(timeToTake, initialFromLocation);
            //toCandy.AddAction(swapToFromAction);

            //fromCandy.RunAction(swapFromToAction);
            //toCandy.RunAction(swapToFromAction);

            ////  Update the row and column positions for each candy
            //fromCandy.gridLocation = initialToLocation;
            //toCandy.gridLocation = initialFromLocation;

        }

        //    //  Animation for a failed swap
        //    private static async void FaildSwapAnimation(Swap swap)
        //    {
        //        const float timeToTake = 0.1f; // in seconds
        //        CCFiniteTimeAction coreAction = null;
        //        CCFiniteTimeAction secondAction = null;

        //        //  Store the positions of the candies to be used to swap them
        //        CCPoint positionA = new CCPoint(swap.candyA.Position);
        //        CCPoint positionB = new CCPoint(swap.candyB.Position);

        //        //  Animate moving the candies back and forth
        //        coreAction = new CCMoveTo(timeToTake, positionB);
        //        secondAction = new CCMoveTo(timeToTake, positionA);
        //        swap.candyA.RunActions(coreAction, secondAction);
        //        coreAction = new CCMoveTo(timeToTake, positionA);
        //        secondAction = new CCMoveTo(timeToTake, positionB);
        //        swap.candyB.RunActions(coreAction, secondAction);

        //        //  Wait for the animation to complete before moving on
        //        await Task.Delay(300);
        //    }
    }
}