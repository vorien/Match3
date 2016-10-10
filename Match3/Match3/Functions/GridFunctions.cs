using CocosSharp;
using Match3.Entities;
using Match3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Match3.Functions
{
    public static class GridFunctions
    {
        private static void decrementMoves() { }
        private static void enableListeners() { }
        private static void disableListeners() { }


        private static Random rand = new Random();
        //private static List<PossibleSwaps> possibleSwaps;
        //private static List<Chain> deleteChains;
        private static int possibleSwapCount = 0;
        private static bool dropped, filledAgain, finishedRemoving, doneShuffling; //, pointGone;

        //  Fills the grid up with new materials
        public static void FillGrid()
        {
            for (int gRow = 0; gRow < Configuration.gridRows; gRow++)
            {
                for (int gColumn = 0; gColumn < Configuration.gridColumns; gColumn++)
                {
                    if (ActiveLevel.level.tiles[gColumn, gRow] == 1)
                    {
                        AssignMaterial(gColumn, gRow); // assigns a new material the location [gRow,gColumn] in the grid
                    }
                }
            }
        }

        // Generates a random for each grid location [column, row] until it doesn't create a chain,
        // then adds it to the grid.
        public static void AssignMaterial(int column, int row)
        {
            Material newMaterial;
            do
            {
                newMaterial = new Material(column, row);
            }
            while
            (
                newMaterial.IsPartOfChain()
            );

            ActiveLevel.grid[column, row] = newMaterial;
        }

        public static void InitializeGrid()
        {
            do
            {
                FillGrid();
                //DetectPossibleSwaps();
                //possibleSwapCount = possibleSwaps.Count;
                possibleSwapCount = 1;
            }
            while (possibleSwapCount == 0);
            //  Add the materials to the layer to be displayed to the screen
            //addCandies();
        }

        //public static async void ReInitializeGrid()
        //{
        //    CCLayer shuffleLayer = new CCLayer();
        //    var shuffleLabel = new CCLabel("Shuffling", "Arial", 100, CCLabelFormat.SystemFont);
        //    shuffleLabel.Color = CCColor3B.Magenta;
        //    shuffleLabel.PositionX = shuffleLayer.ContentSize.Width / 2.0f;
        //    shuffleLabel.PositionY = shuffleLayer.ContentSize.Height / 2.0f;
        //    shuffleLayer.AddChild(shuffleLabel);
        //    //shuffleLayer.PositionX = this.ContentSize.Width / 2.0f;
        //    //shuffleLayer.PositionY = this.ContentSize.Height / 2.0f;
        //    //AddChild(shuffleLayer);

        //    await Task.Delay(500);
        //    //  Remove all of the old materials from the screen
        //    for (int gRow = 0; gRow < Configuration.gridRows; gRow++)
        //    {
        //        for (int gColumn = 0; gColumn < Configuration.gridColumns; gColumn++)
        //        {
        //            if (ActiveLevel.level.tiles[gColumn, gRow] == 1)
        //            {
        //                Material material = GetMaterialAt(gColumn, gRow);
        //                material.RemoveFromParent();
        //            }
        //        }
        //    }
        //    //  Refills the grid up
        //    InitializeGrid();
        //    doneShuffling = true;
        //    shuffleLayer.RemoveFromParent();
        //}




        //  gets the material that's at the given [column, row] position in the grid
        public static Material GetMaterialAt(int column, int row)
        {
            if (
                column < 0 || column > Configuration.gridColumns
                ||
                row < 0 || row > Configuration.gridRows
            )
            {
                return null;
            }
            return ActiveLevel.grid[column, row];
        }
        public static Material GetMaterialAtGridLocation(CCPointI gridLocation)
        {
            return GetMaterialAt(gridLocation.X, gridLocation.Y);
        }
        public static Material GetMaterialAtOffset(Material fromMaterial, CCPointI toMaterialOffset)
        {
            return GetMaterialAtGridLocation(fromMaterial.gridLocation + toMaterialOffset);
        }
        public static Material GetMaterialAtOffset(Material fromMaterial, int offsetX, int offsetY)
        {
            return GetMaterialAtOffset(fromMaterial, fromMaterial.gridLocation + new CCPointI(offsetX, offsetY));
        }

        public static void TrySwap(CCPointI fromGridLocation, CCPointI toOffset)
        {
            CCPointI toGridLocation = GetMaterialAtGridLocation(fromGridLocation + toOffset).gridLocation;
            if (GetMaterialAtGridLocation(toGridLocation) == null)
            {
                return;
            }

            //toMaterial.debugLabel.Text = "SWAPTO";
            //debugLabel.Text = "Switching material at [" + fromRow + ", " + fromCol + "] with material at [" + toRow + ", " + toCol + "].";

            MaterialSwap.fromGridLocation = fromGridLocation;
            MaterialSwap.toGridLocation = toGridLocation;

            MaterialSwap.AnimateSwap();

            //After swap, call on all materials
            //Material checkMaterial;
            //Configuration.chains.Clear();
            //for (int gRow = 0; gRow < Configuration.gridRows; gRow++)
            //{
            //    for (int gColumn = 0; gColumn < Configuration.gridColumns; gColumn++)
            //    {
            //        if (ActiveLevel.level.tiles[gColumn, gRow] == 1)
            //        {
            //            checkMaterial = GetMaterialAt(gColumn, gRow);
            //            checkMaterial.CheckForChains();
            //            AssignMaterial(gColumn, gRow); // assigns a new material the location [gRow,gColumn] in the grid
            //        }
            //    }
            //}
            //Debug.WriteLine(Configuration.chains.Count.ToString());
            //Debug.WriteLine("");
            //foreach (KeyValuePair<int, Chain> chain in Configuration.chains)
            //{
            //    // do something with entry.Value or entry.Key
            //}


            //    if (IsSwapPossible(swap))
            //    {
            //        // MaterialSwap them
            //        AnimateSwap(swap);

            //        await Task.Delay(300);  // Wait for the swap animation to finish before continuing
            //        dropped = false;    // Sets dropped to false, it will be used to check if the game finished dropping all of the materials
            //        filledAgain = false;
            //        finishedRemoving = false;
            //        do
            //        {
            //            //  My reason to add the while loops with the awaits is that the App seems to come back to this do while even before the
            //            //  the method completely finish running. I'm guessing that awaits in the methods is forcing the App to continue running while it's awaiting
            //            //  to continue within the method. It's possible that the called methods are running on a separate threads from the thread that is running this
            //            //  method, so while those threads are put on hold, the App jumps back to this thread. After putting in while loops the app does seems to work like
            //            //  I want it to so I'm probably on the right track, thought there must be a better way to accomplish as the current way looks ugly.

            //            RemoveMatches();        // Remove the matches
            //            while (!finishedRemoving)
            //            {
            //                await Task.Delay(50);
            //            }
            //            DropCandies();          // Move the materials down
            //            while (!dropped)        // As long as the dropCandies method isn't finished it will keep adding an await
            //            {
            //                await Task.Delay(50);
            //            }
            //            FillColumns();        // Fill the grid back up
            //            while (!filledAgain)
            //            {
            //                await Task.Delay(50);
            //            }
            //            DetectPossibleSwaps();   // Need to update the list of possible swaps
            //            await Task.Delay(300);
            //        }
            //        while (deleteChains.Count != 0);
            //        decrementMoves();

            //        // In the case that grid ends up with no possible swaps, we need to refill the grid new materials
            //        //if (possibleSwaps.Count == 0 && movesLeft != 0)
            //        //{
            //        //    ReInitializeGrid();
            //        //    while (!doneShuffling)
            //        //    {
            //        //        await Task.Delay(50);
            //        //    }
            //        //}
            //    }
            //    else
            //    {
            //        //  failedSwapAnimation only needs to run if there's valid materials
            //        if (swap.materialA != null && swap.materialB != null)
            //        {
            //            //  MaterialSwap is not possible so run the failed swap animation
            //            FaildSwapAnimation(swap);
            //            //  Waiting to make sure the animation has been completed
            //            await Task.Delay(300);
            //        }
            //        else
            //        {
            //            //  The method enables the user interaction again and returns the call point without any type of animation
            //            //  as the user tried to do a swap with an empty location
            //            //ResumeListeners();
            //            return;
            //        }
            //    }
            //    //  Turn user interaction back on as all of the matches were removed and the grid filled back up
            //    //if (movesLeft != 0)
            //    //{
            //    //ResumeListeners();
            //    //}
        }

        //    //  Detects how many swaps are possible, and adds the possible swaps in the possibleSwaps array
        //    //  Will return the number of possible swaps that were detected in the grid
        //    private static void DetectPossibleSwaps()
        //    {
        //        //    possibleSwaps = new List<MaterialSwap>();
        //        //    // for loop to go through the grid to find possible swaps
        //        //    for (int row = 0; row < Configuration.gridRows; row++)
        //        //    {
        //        //        for (int column = 0; column < Configuration.gridColumns; column++)
        //        //        {
        //        //            //  Grab the material from grid
        //        //            Material checkMaterial = ActiveLevel.grid[column, row];

        //        //            //  Make sure that there's a material at the given grid location
        //        //            if (checkMaterial != null)
        //        //            {
        //        //                //  See if it's possible to swap to the right
        //        //                if (column < Configuration.gridColumns - 1)
        //        //                {
        //        //                    //  Grab the material to the right from the checkMaterial
        //        //                    Material otherMaterial = ActiveLevel.grid[column, row + 1];
        //        //                    if (otherMaterial != null)
        //        //                    {
        //        //                        //  MaterialSwap the materials
        //        //                        ActiveLevel.grid[column, row] = otherMaterial;
        //        //                        ActiveLevel.grid[column, row + 1] = checkMaterial;

        //        //                        //  Check to see if either one of the swapped materials is now part of a chain
        //        //                        if (HasChainAt(column, row + 1) || HasChainAt(column, row))
        //        //                        {
        //        //                            MaterialSwap swap = new MaterialSwap();
        //        //                            swap.materialA = checkMaterial;
        //        //                            swap.materialB = otherMaterial;

        //        //                            //  Add the materials to the array of possibleSwaps
        //        //                            possibleSwaps.Add(swap);
        //        //                        }

        //        //                        //  MaterialSwap the materials back to their original positions
        //        //                        ActiveLevel.grid[column, row] = checkMaterial;
        //        //                        ActiveLevel.grid[column, row + 1] = otherMaterial;
        //        //                    }
        //        //                }

        //        //                //  See if it's possible to swap below
        //        //                if (row < Configuration.gridRows - 1)
        //        //                {
        //        //                    //  Grab the material to the right from the checkMaterial
        //        //                    Material otherMaterial = ActiveLevel.grid[row + 1, column];
        //        //                    if (otherMaterial != null)
        //        //                    {
        //        //                        //  MaterialSwap the materials
        //        //                        ActiveLevel.grid[column, row] = otherMaterial;
        //        //                        ActiveLevel.grid[row + 1, column] = checkMaterial;

        //        //                        //  Check to see if either one of the swapped materials is now part of a chain
        //        //                        if (HasChainAt(row + 1, column) || HasChainAt(column, row))
        //        //                        {
        //        //                            MaterialSwap swap = new MaterialSwap();
        //        //                            swap.materialA = checkMaterial;
        //        //                            swap.materialB = otherMaterial;

        //        //                            //  Add the materials to the array of possibleSwaps
        //        //                            possibleSwaps.Add(swap);
        //        //                        }

        //        //                        //  MaterialSwap the materials back to their original positions
        //        //                        ActiveLevel.grid[column, row] = checkMaterial;
        //        //                        ActiveLevel.grid[row + 1, column] = otherMaterial;
        //        //                    }
        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //    }

        ////  Check to see if a swap is possible
        //private static bool IsSwapPossible(MaterialSwap swap)
        //    {
        //        foreach (var item in possibleSwaps)
        //        {
        //            if ((item.materialA == swap.materialA && item.materialB == swap.materialB) || (item.materialA == swap.materialB && item.materialB == swap.materialA))
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }

        //    //  Check to see if the touch location is within the grid and if it is
        //    //  then returns true and the row and column position of the material
        //    public static bool ConvertToPoint(CCPoint location, ref int row, ref int column)
        //    {
        //        if (location.X >= 38 && location.X < 598 && location.Y >= 216 && location.Y < 846)
        //        {
        //            //debugLabel.Text = "Touch was within the grid.";
        //            row = ConvertYToRow(location.Y);   //(846 - Convert.ToInt32(location.Y)) / 70;
        //            column = ConvertXToColumn(location.X);    //(Convert.ToInt32(location.X) - 38) / 62;
        //            Material debugMaterial = GetMaterialAt(column, row);
        //            //debugLabel.Text = "Touched the material at [" + debugMaterial.getRow() + ", " + debugMaterial.getColumn() + "]";
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    public static bool HasMaterial(CCPointI gridLocation)
        //    {
        //        //  Make sure that the user didn't swipe out of the grid as there isn't any materials to swap with out there
        //        if (gridLocation.X < 0 || gridLocation.X >= Configuration.gridColumns)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            if (gridLocation.Y < 0 || gridLocation.Y >= Configuration.gridRows)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                if (ActiveLevel.level.tiles[gridLocation.X, gridLocation.Y] != 1)
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //        return true;

        //    }
        //  Checks to see if a swap is possible, if it is then it will do so
        //  otherwise it will call for a failed swap animation


        //    //  Method to find all chains in the grid 
        //    private static void RemoveMatches()
        //    {
        //        List<Chain> horizontalChains = DetectHorizontalMatches();
        //        List<Chain> verticalChains = DetectVerticalMatches();

        //        // Logic to remove the materials from the grid goes here, possibly call a method that takes the list of chains to work with
        //        // Don't forget that materials have to be removed from the grid and then afterwards the sprites need to be removed from the screen separately
        //        // which can be handle by another method
        //        foreach (Chain item in verticalChains)
        //        {
        //            horizontalChains.Add(item);
        //        }
        //        deleteChains = horizontalChains;
        //        RemoveCandies(horizontalChains);
        //    }

        //    //  Remove the material objects from the screen and the grid
        //    private static async void RemoveCandies(List<Chain> chains)
        //    {
        //        if (finishedRemoving != false)
        //        {
        //            finishedRemoving = false;
        //        }

        //        foreach (Chain chain in chains)
        //        {
        //            foreach (Material material in chain.materials)
        //            {
        //                //  Remove the material from the grid
        //                ActiveLevel.grid[material.GetMaterialRow(), material.GetMaterialColumn()] = null;
        //                CCSprite removeMaterial = material.GetMaterialSprite();
        //                if (removeMaterial != null)
        //                {
        //                    const float timeToTake = 0.3f; // in seconds
        //                    CCFiniteTimeAction coreAction = null;
        //                    CCAction easing = null;

        //                    coreAction = new CCScaleTo(timeToTake, 0.3f);
        //                    easing = new CCEaseOut(coreAction, 0.1f);
        //                    removeMaterial.RunAction(coreAction);

        //                    await Task.Delay(50);   // Wait for the scaling animation to show a bit before continuing on to remove the material
        //                    //pointGone = false;
        //                    //pointLabel(material.getRow(), material.getColumn());
        //                    //while (!pointGone)
        //                    //{
        //                    //    await Task.Delay(1);
        //                    //}
        //                    removeMaterial.RemoveFromParent(); // This should remove the material from the screen
        //                    HandlePoints();
        //                }
        //            }
        //            //  Wait for all of the materials to be removed before moving on to the next chain in the list of chains
        //            await Task.Delay(300);
        //        }
        //        //  Since the method is finished removing all of chains, needed to set the finishedRemoving bool variable to true
        //        //  so the calling method can get out of it's await loop
        //        finishedRemoving = true;
        //    }

        //    //  Adds a label that shows "+10" for every material that is destroyed, it then proceeds towards that scoreLabel, implying that it gets added to the score.
        //    private static async void PointLabel(int row, int column)
        //    {
        //        var point = new CCLabel("+10", "Arial", 50, CCLabelFormat.SystemFont);
        //        point.Color = CCColor3B.Green;
        //        point.AnchorPoint = new CCPoint(0, 0);
        //        point.Position = new CCPoint(70 + (62 * column), 810 - (70 * row) + 50);
        //        //AddChild(point);
        //        point.AddAction(new CCMoveTo(0.2f, new CCPoint(500, 1000)));
        //        await Task.Delay(200);
        //        point.RemoveFromParent();
        //        //pointGone = true;
        //    }

        //    //  Drops the materials down 
        //    //private static async void DropCandies()
        //    //{
        //    //    // Makes sure that dropped bool variable is set false before continuing
        //    //    if (dropped != false)
        //    //    {
        //    //        dropped = false;
        //    //    }
        //    //    for (int column = 0; column < Configuration.gridColumns; column++)
        //    //    {
        //    //        for (int row = 8; row > 0; row--)
        //    //        {
        //    //            if (ActiveLevel.level.tiles[column, row] == 1)
        //    //            {
        //    //                Material material = GetMaterialAt(column, row);
        //    //                if (material == null)
        //    //                {
        //    //                    // Find which row number to drop the material from
        //    //                    int tempRow = row - 1;
        //    //                    while (tempRow >= 0 && ActiveLevel.grid[tempcolumn, row] == null)
        //    //                    {
        //    //                        tempRow--;
        //    //                    }
        //    //                    //  Only runs if there's a row that has a material in it
        //    //                    if (tempRow >= 0)
        //    //                    {
        //    //                        CCPoint position = new CCPoint(70 + (62 * column), 810 - (70 * row));
        //    //                        material = GetMaterialAt(tempcolumn, row);
        //    //                        material.AddAction(new CCEaseOut(new CCMoveTo(0.3f, position), 0.3f));
        //    //                        material.SetMaterialPosition(column, row);    // Update the row and column of the material
        //    //                        ActiveLevel.grid[column, row] = material;             // Update the position of the material within the grid
        //    //                        ActiveLevel.grid[tempcolumn, row] = null;
        //    //                        //  Wait for the material to drop before moving to on the next material
        //    //                        await Task.Delay(50);
        //    //                    }
        //    //                }
        //    //            }
        //    //        }
        //    //    }

        //    //    // Since the method should have gone through the entire grid and finished dropping the materials
        //    //    // need to set dropped to true so the calling method can get out of the await loop
        //    //    dropped = true;
        //    //}

        //    //  Fill the holes at the top of the of each column
        //    private static void FillColumns()
        //    {
        //        int materialType = 0;
        //        if (filledAgain != false)
        //        {
        //            filledAgain = false;
        //        }
        //        for (int column = 0; column < Configuration.gridColumns; column++)
        //        {
        //            //  Starting at the top and working downwards, add a new material where it's needed
        //            for (int row = 0; row < Configuration.gridRows && ActiveLevel.grid[column, row] == null; row++)
        //            {
        //                if (ActiveLevel.level.tiles[column, row] == 1)
        //                {
        //                    int newMaterialType = 0;
        //                    //  Have to first create a new material outside of the while loop or otherwise the IDE won't let me use the variable newMaterial
        //                    //  as it will be seen as using an unassigned variable, even though it will be assigned a new material in the while loop
        //                    Material newMaterial = new Material(column, row);
        //                    newMaterialType = newMaterial.GetMaterialTypeID();
        //                    //  Make sure that each material that is being added isn't the same as the one that was added previously
        //                    while (newMaterialType == materialType)
        //                    {
        //                        newMaterial = new Material(column, row);
        //                        newMaterialType = newMaterial.GetMaterialTypeID();
        //                    }
        //                    materialType = newMaterialType;
        //                    ActiveLevel.grid[column, row] = newMaterial;

        //                    // Once all of the material is created to fill the grid back up
        //                    // Use an animation to add it to the screen
        //                    AnimateAddingNewCandies(column, row);
        //                }
        //            }
        //        }
        //        //  Since the entire grid was filled back up with materials, need to set the filledAgain bool variable to true
        //        //  so the calling method can get out the await loop
        //        filledAgain = true;
        //    }

        //    private static void HandlePoints()
        //    {
        //        //var points = Convert.ToInt32(scoreLabel.Text);
        //        //points += 10;
        //        ////if (points < 100)
        //        ////{
        //        ////    scoreLabel.Position = new CCPoint(480, 1000);
        //        ////}
        //        ////else if (points >= 100 && points < 1000)
        //        ////{
        //        ////    scoreLabel.Position = new CCPoint(400, 1000);
        //        ////}
        //        ////else if (points >= 1000 && points < 10000)
        //        ////{
        //        ////    scoreLabel.Position = new CCPoint(320, 1000);
        //        ////}
        //        //scoreLabel.Text = Convert.ToString(points);
        //    }

        //    //  Using an animation to add all of the new materials to screen
        //    private static async void AnimateAddingNewCandies(int row, int column)
        //    {
        //        //  Find the top most tile for the column
        //        int topMostRowLocation = 0;
        //        while (ActiveLevel.level.tiles[topMostRowLocation, column] != 1 && topMostRowLocation <= row)
        //        {
        //            topMostRowLocation++;
        //        }
        //        // Starting position for the material to be added
        //        CCPoint beginningPosition = new CCPoint(70 + (62 * column), 810 - (70 * topMostRowLocation) + 50);
        //        // The final position of where the material goes
        //        CCPoint endPosition = new CCPoint(70 + (62 * column), 810 - (70 * row));
        //        ActiveLevel.grid[column, row].Position = beginningPosition;
        //        //AddChild(ActiveLevel.grid[column, row]);   // Add the material to the screen
        //        // Animation to move the material into it's proper position
        //        ActiveLevel.grid[column, row].AddAction(new CCMoveTo(0.3f, endPosition));
        //        // Wait for the animation before continuing
        //        await Task.Delay(300);
        //    }

        //    //  Detects any and all horizontal chains
        //    private static List<Chain> DetectHorizontalMatches()
        //    {
        //        var horzList = new List<Chain>();
        //        for (int row = 0; row < Configuration.gridRows; row++)
        //        {
        //            for (int column = 0; column < Configuration.gridColumns - 2;)
        //            {
        //                //  Makes sure that location in the grid isn't empty
        //                if (ActiveLevel.grid[column, row] != null)
        //                {
        //                    int matchType = ActiveLevel.grid[column, row].GetMaterialTypeID();
        //                    //  If the next two materials match than there's a chain here
        //                    if ((ActiveLevel.grid[column, row + 1] != null && ActiveLevel.grid[column, row + 2] != null)
        //                        && ActiveLevel.grid[column, row + 1].GetMaterialTypeID() == matchType && ActiveLevel.grid[column, row + 2].GetMaterialTypeID() == matchType)
        //                    {
        //                        //  Create a new chain
        //                        var chain = new Chain();
        //                        chain.chainType = Chain.ChainType.Horizontal;
        //                        //  Add all of the materials within the chain to chain variable
        //                        do
        //                        {
        //                            chain.addMaterial(GetMaterialAt(column, row));
        //                            column += 1;
        //                        }
        //                        while ((column < Configuration.gridColumns && ActiveLevel.grid[column, row] != null) && ActiveLevel.grid[column, row].GetMaterialTypeID() == matchType);
        //                        horzList.Add(chain);    // Add the chain to the list of horizontal chains
        //                    }
        //                }
        //                column += 1;
        //            }
        //        }
        //        return horzList;
        //    }

        //    //  Detects any and all vertical chains
        //    private static List<Chain> DetectVerticalMatches()
        //    {
        //        var vertList = new List<Chain>();
        //        for (int column = 0; column < Configuration.gridColumns; column++)
        //        {
        //            for (int row = 0; row < Configuration.gridRows - 2;)
        //            {
        //                //  Makes sure that the location in the grid isn't empty
        //                if (ActiveLevel.grid[column, row] != null)
        //                {
        //                    int matchType = ActiveLevel.grid[column, row].GetMaterialTypeID();
        //                    //  If the two materials below it matches, then there's a chain
        //                    if ((ActiveLevel.grid[row + 1, column] != null && ActiveLevel.grid[row + 2, column] != null)
        //                        && ActiveLevel.grid[row + 1, column].GetMaterialTypeID() == matchType && ActiveLevel.grid[row + 2, column].GetMaterialTypeID() == matchType)
        //                    {
        //                        //  Create a new chain
        //                        var chain = new Chain();
        //                        chain.chainType = Chain.ChainType.Vertical;
        //                        //  Add all of the materials within the chain to the chain variable
        //                        do
        //                        {
        //                            chain.addMaterial(GetMaterialAt(column, row));
        //                            row += 1;
        //                        }
        //                        while ((row < Configuration.gridRows && ActiveLevel.grid[column, row] != null) && ActiveLevel.grid[column, row].GetMaterialTypeID() == matchType);
        //                        vertList.Add(chain);    // Add the chain to the list of vertical chains
        //                    }
        //                }
        //                row += 1;
        //            }
        //        }
        //        return vertList;
        //    }


    }
}
