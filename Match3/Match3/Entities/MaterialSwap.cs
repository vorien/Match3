using CocosSharp;
using Match3.Functions;
using System.Diagnostics;

namespace Match3.Entities
{
    // Handles the mechanics of swapping two materials and holds
    // the information to reverse the swap if it produces no matches
    public static class MaterialSwap
    {
        // materials that will be swapped
        public static CCPointI fromGridLocation;
        public static CCPointI toGridLocation;

        private static Material fromMaterial;
        private static Material toMaterial;
        private static CCPoint fromPosition;
        private static CCPoint toPosition;

        //  Visually animates the swap using the CCMoveTo function provided by CocosSharp
        //  and swaps the sprites along with the MaterialTypes ID.

        public static void AnimateSwap(bool reverse = false)
        {
            if(reverse == true)
            {
                GeneralFunctions.ExchangeValues(ref fromGridLocation, ref toGridLocation);
            }
            fromMaterial = GridFunctions.GetMaterialAtGridLocation(fromGridLocation);
            toMaterial = GridFunctions.GetMaterialAtGridLocation(toGridLocation);
            fromPosition = fromMaterial.Position;
            toPosition = toMaterial.Position;

            for (int gRow = 0; gRow < Configuration.gridRows; gRow++)
            {
                for (int gColumn = 0; gColumn < Configuration.gridColumns; gColumn++)
                {
                    if (ActiveLevel.level.tiles[gColumn, gRow] == 1)
                    {
                        ActiveLevel.grid[gColumn, gRow].debugLabel.Text = ""; // assigns a new material the location [gRow,gColumn] in the grid
                        ActiveLevel.grid[gColumn, gRow].ZOrder = 1;
                    }
                }
            }

            Debug.WriteLine(fromGridLocation.X + "," + fromGridLocation.Y + " = " + fromPosition.X + "," + fromPosition.Y);
            Debug.WriteLine(toGridLocation.X + "," + toGridLocation.Y + " = " + toPosition.X + "," + toPosition.Y);

            //fromMaterial.debugLabel.Text = fromGridLocation.X + "," + fromGridLocation.Y;
            //toMaterial.debugLabel.Text = toGridLocation.X + "," + toGridLocation.Y;

            fromMaterial.debugLabel.Text = fromMaterial.gridLocation.X + "," + fromMaterial.gridLocation.Y;
            toMaterial.debugLabel.Text = toMaterial.gridLocation.X + "," + toMaterial.gridLocation.Y;

            fromMaterial.ZOrder = 5;
            toMaterial.ZOrder = 10;

            fromMaterial.RunAction(new CCMoveTo(0.3f, toPosition));
            toMaterial.RunAction(new CCMoveTo(0.3f, fromPosition));

            //            ActiveLevel.grid[toGridLocation.X, toGridLocation.Y] = fromMaterial;
            //ActiveLevel.grid[toGridLocation.X, toGridLocation.Y].gridLocation = toGridLocation;

            //            ActiveLevel.grid[fromGridLocation.X, fromGridLocation.Y] = toMaterial;
            //ActiveLevel.grid[fromGridLocation.X, fromGridLocation.Y].gridLocation = fromGridLocation;

            GeneralFunctions.ExchangeValues(ref ActiveLevel.grid[fromGridLocation.X, fromGridLocation.Y], ref ActiveLevel.grid[toGridLocation.X, toGridLocation.Y]);
            //GeneralFunctions.ExchangeValues(ref ActiveLevel.grid[fromGridLocation.X, fromGridLocation.Y].gridLocation, ref ActiveLevel.grid[toGridLocation.X, toGridLocation.Y].gridLocation);

            //fromMaterial.debugLabel.Text += "\n" + ActiveLevel.grid[toGridLocation.X, toGridLocation.Y].gridLocation.X + "," + ActiveLevel.grid[toGridLocation.X, toGridLocation.Y].gridLocation.Y;
            //toMaterial.debugLabel.Text += "\n" + ActiveLevel.grid[fromGridLocation.X, fromGridLocation.Y].gridLocation.X + "," + ActiveLevel.grid[fromGridLocation.X, fromGridLocation.Y].gridLocation.Y;

            fromMaterial.debugLabel.Text += "\n" + fromMaterial.gridLocation.X + "," + fromMaterial.gridLocation.Y;
            toMaterial.debugLabel.Text += "\n" + toMaterial.gridLocation.X + "," + toMaterial.gridLocation.Y;

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

            //FailedSwapAnimation();

        }

        //  Animation for a failed swap
        private static void FailedSwapAnimation()
        {
            fromMaterial.RunAction(new CCMoveTo(0.3f, toPosition));
            toMaterial.RunAction(new CCMoveTo(0.3f, fromPosition));


            GeneralFunctions.ExchangeValues(ref ActiveLevel.grid[fromGridLocation.X, fromGridLocation.Y], ref ActiveLevel.grid[toGridLocation.X, toGridLocation.Y]);
            GeneralFunctions.ExchangeValues(ref ActiveLevel.grid[fromGridLocation.X, fromGridLocation.Y].gridLocation, ref ActiveLevel.grid[toGridLocation.X, toGridLocation.Y].gridLocation);

            //const float timeToTake = 0.1f; // in seconds
            //CCFiniteTimeAction coreAction = null;
            //CCFiniteTimeAction secondAction = null;
            ////  Store the positions of the materials to be used to swap them
            //CCPoint positionA = new CCPoint(swap.materialA.Position);
            //CCPoint positionB = new CCPoint(swap.materialB.Position);

            ////  Animate moving the materials back and forth
            //coreAction = new CCMoveTo(timeToTake, positionB);
            //secondAction = new CCMoveTo(timeToTake, positionA);
            //swap.materialA.RunActions(coreAction, secondAction);
            //coreAction = new CCMoveTo(timeToTake, positionA);
            //secondAction = new CCMoveTo(timeToTake, positionB);
            //swap.materialB.RunActions(coreAction, secondAction);

            //  Wait for the animation to complete before moving on
            //await Task.Delay(300);
        }
    }
}