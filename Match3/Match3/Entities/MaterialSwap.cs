using CocosSharp;
using Match3.Functions;

namespace Match3.Entities
{
    // Handles the mechanics of swapping two materials and holds
    // the information to reverse the swap if it produces no matches
    public static class MaterialSwap
    {
        // materials that will be swapped
        public static CCPointI fromGridLocation { get; set; }
        public static CCPointI toGridLocation { get; set; }

        private static Material fromMaterial;
        private static Material toMaterial;
        private static CCPoint fromPosition;
        private static CCPoint toPosition;

        //  Visually animates the swap using the CCMoveTo function provided by CocosSharp
        //  and swaps the sprites along with the MaterialTypes ID.

        public static void AnimateSwap()
        {
            fromMaterial = GridFunctions.GetMaterialAtGridLocation(fromGridLocation);
            toMaterial = GridFunctions.GetMaterialAtGridLocation(toGridLocation);
            fromPosition = fromMaterial.position;
            toPosition = toMaterial.position;

            fromMaterial.RunAction(new CCMoveTo(0.3f, toPosition));
            toMaterial.RunAction(new CCMoveTo(0.3f, fromPosition));

            fromMaterial.gridLocation = toGridLocation;
            ActiveLevel.grid[toGridLocation.X, toGridLocation.Y] = fromMaterial;

            toMaterial.gridLocation = fromGridLocation;
            ActiveLevel.grid[fromGridLocation.X, fromGridLocation.Y] = toMaterial;

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
        //    private static async void FaildSwapAnimation(MaterialSwap swap)
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