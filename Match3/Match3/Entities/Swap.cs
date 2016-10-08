using CocosSharp;

namespace Match3.Entities
{
    class Swap : CCNode
    {
        // materials that will be swapped
        public Material fromMaterial, toMaterial;
        private CCPointI initialFromLocation, initialToLocation;
        private CCPoint initialFromPosition, initialToPosition;

        // This class is supposed to be a set of materials that can/(are to) be swapped
        public Swap(Material from, Material to)
        {
            // initializes the two material pointers to null
            fromMaterial = from;
            toMaterial = to;
            initialFromLocation = fromMaterial.gridLocation;
            initialToLocation = toMaterial.gridLocation;
            initialFromPosition = fromMaterial.Position;
            initialToPosition = toMaterial.Position;

            //toMaterial.debugLabel.Text = "TO";

        }

        //  Visually animates the swap using the CCMoveTo function provided by CocosSharp,
        //  also updates the grid location of the materials

        public void AnimateSwap()
        {
            fromMaterial.RunAction(new CCMoveTo(0.3f, initialToPosition));
            toMaterial.RunAction(new CCMoveTo(0.3f, initialFromPosition));

            //const float timeToTake = 0.3f; // in seconds

            ////  Animate the swapping of the materials
            //CCFiniteTimeAction swapFromToAction = new CCMoveTo(timeToTake, initialToLocation);
            //fromMaterial.AddAction(swapFromToAction);
            //CCFiniteTimeAction swapToFromAction = new CCMoveTo(timeToTake, initialFromLocation);
            //toMaterial.AddAction(swapToFromAction);

            //fromMaterial.RunAction(swapFromToAction);
            //toMaterial.RunAction(swapToFromAction);

            ////  Update the row and column positions for each material
            //fromMaterial.gridLocation = initialToLocation;
            //toMaterial.gridLocation = initialFromLocation;

        }

        //    //  Animation for a failed swap
        //    private static async void FaildSwapAnimation(Swap swap)
        //    {
        //        const float timeToTake = 0.1f; // in seconds
        //        CCFiniteTimeAction coreAction = null;
        //        CCFiniteTimeAction secondAction = null;

        //        //  Store the positions of the materials to be used to swap them
        //        CCPoint positionA = new CCPoint(swap.materialA.Position);
        //        CCPoint positionB = new CCPoint(swap.materialB.Position);

        //        //  Animate moving the materials back and forth
        //        coreAction = new CCMoveTo(timeToTake, positionB);
        //        secondAction = new CCMoveTo(timeToTake, positionA);
        //        swap.materialA.RunActions(coreAction, secondAction);
        //        coreAction = new CCMoveTo(timeToTake, positionA);
        //        secondAction = new CCMoveTo(timeToTake, positionB);
        //        swap.materialB.RunActions(coreAction, secondAction);

        //        //  Wait for the animation to complete before moving on
        //        await Task.Delay(300);
        //    }
    }
}