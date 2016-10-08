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
        private static List<Swap> possibleSwaps;
        //private static List<Chain> deleteChains;
        private static int possibleSwapCount = 0;
        private static bool dropped, filledAgain, finishedRemoving, doneShuffling; //, pointGone;

        private static Swap swap;

        //  Fills the grid up with new candies
        public static void FillGrid()
        {
            for (int gRow = 0; gRow < Configuration.gridRows; gRow++)
            {
                for (int gColumn = 0; gColumn < Configuration.gridColumns; gColumn++)
                {
                    if (ActiveLevel.level.tiles[gColumn, gRow] == 1)
                    {
                        AssignCandy(gColumn, gRow); // assigns a new candy the location [gRow,gColumn] in the grid
                    }
                }
            }
        }

        // Generates a random for each grid location [column, row] until it doesn't create a chain,
        // then adds it to the grid.
        public static void AssignCandy(int column, int row)
        {
            Candy newCandy;
            do
            {
                newCandy = new Candy(column, row);
            }
            while
            (
                newCandy.IsPartOfChain()
            );

            ActiveLevel.grid[column, row] = newCandy;
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
            //  Add the candies to the layer to be displayed to the screen
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
        //    //  Remove all of the old candies from the screen
        //    for (int gRow = 0; gRow < Configuration.gridRows; gRow++)
        //    {
        //        for (int gColumn = 0; gColumn < Configuration.gridColumns; gColumn++)
        //        {
        //            if (ActiveLevel.level.tiles[gColumn, gRow] == 1)
        //            {
        //                Candy candy = GetCandyAt(gColumn, gRow);
        //                candy.RemoveFromParent();
        //            }
        //        }
        //    }
        //    //  Refills the grid up
        //    InitializeGrid();
        //    doneShuffling = true;
        //    shuffleLayer.RemoveFromParent();
        //}




        //  gets the candy that's at the given [column, row] position in the grid
        public static Candy GetCandyAt(int column, int row)
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
        public static Candy GetCandyAtGridLocation(CCPointI gridLocation)
        {
            return GetCandyAt(gridLocation.X, gridLocation.Y);
        }
        public static Candy GetCandyAtOffset(Candy fromCandy, CCPointI toCandyOffset)
        {
            return GetCandyAtGridLocation(fromCandy.gridLocation + toCandyOffset);
        }
        public static Candy GetCandyAtOffset(Candy fromCandy, int offsetX, int offsetY)
        {
            return GetCandyAtOffset(fromCandy, fromCandy.gridLocation + new CCPointI(offsetX, offsetY));
        }

        public static void TrySwap(Candy fromCandy, CCPointI toCandyOffset)
        {
            Candy toCandy = GetCandyAtOffset(fromCandy, toCandyOffset);
            if (toCandy == null)
            {
                return;
            }

            toCandy.debugLabel.Text = "SWAPTO";
            //debugLabel.Text = "Switching candy at [" + fromRow + ", " + fromCol + "] with candy at [" + toRow + ", " + toCol + "].";

            swap = new Swap(fromCandy, toCandy);
            swap.AnimateSwap();

            //After swap, call on all candies
            Candy checkCandy;
            Configuration.chains.Clear();
            for (int gRow = 0; gRow < Configuration.gridRows; gRow++)
            {
                for (int gColumn = 0; gColumn < Configuration.gridColumns; gColumn++)
                {
                    if (ActiveLevel.level.tiles[gColumn, gRow] == 1)
                    {
                        checkCandy = GetCandyAt(gColumn, gRow);
                        checkCandy.CheckForChains();
                        AssignCandy(gColumn, gRow); // assigns a new candy the location [gRow,gColumn] in the grid
                    }
                }
            }
            Debug.WriteLine(Configuration.chains.Count.ToString());
            Debug.WriteLine("");
            //foreach (KeyValuePair<int, Chain> chain in Configuration.chains)
            //{
            //    // do something with entry.Value or entry.Key
            //}


            //    if (IsSwapPossible(swap))
            //    {
            //        // Swap them
            //        AnimateSwap(swap);

            //        await Task.Delay(300);  // Wait for the swap animation to finish before continuing
            //        dropped = false;    // Sets dropped to false, it will be used to check if the game finished dropping all of the candies
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
            //            DropCandies();          // Move the candies down
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

            //        // In the case that grid ends up with no possible swaps, we need to refill the grid new candies
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
            //        //  failedSwapAnimation only needs to run if there's valid candies
            //        if (swap.candyA != null && swap.candyB != null)
            //        {
            //            //  Swap is not possible so run the failed swap animation
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
        //        //    possibleSwaps = new List<Swap>();
        //        //    // for loop to go through the grid to find possible swaps
        //        //    for (int row = 0; row < Configuration.gridRows; row++)
        //        //    {
        //        //        for (int column = 0; column < Configuration.gridColumns; column++)
        //        //        {
        //        //            //  Grab the candy from grid
        //        //            Candy checkCandy = ActiveLevel.grid[column, row];

        //        //            //  Make sure that there's a candy at the given grid location
        //        //            if (checkCandy != null)
        //        //            {
        //        //                //  See if it's possible to swap to the right
        //        //                if (column < Configuration.gridColumns - 1)
        //        //                {
        //        //                    //  Grab the candy to the right from the checkCandy
        //        //                    Candy otherCandy = ActiveLevel.grid[column, row + 1];
        //        //                    if (otherCandy != null)
        //        //                    {
        //        //                        //  Swap the candies
        //        //                        ActiveLevel.grid[column, row] = otherCandy;
        //        //                        ActiveLevel.grid[column, row + 1] = checkCandy;

        //        //                        //  Check to see if either one of the swapped candies is now part of a chain
        //        //                        if (HasChainAt(column, row + 1) || HasChainAt(column, row))
        //        //                        {
        //        //                            Swap swap = new Swap();
        //        //                            swap.candyA = checkCandy;
        //        //                            swap.candyB = otherCandy;

        //        //                            //  Add the candies to the array of possibleSwaps
        //        //                            possibleSwaps.Add(swap);
        //        //                        }

        //        //                        //  Swap the candies back to their original positions
        //        //                        ActiveLevel.grid[column, row] = checkCandy;
        //        //                        ActiveLevel.grid[column, row + 1] = otherCandy;
        //        //                    }
        //        //                }

        //        //                //  See if it's possible to swap below
        //        //                if (row < Configuration.gridRows - 1)
        //        //                {
        //        //                    //  Grab the candy to the right from the checkCandy
        //        //                    Candy otherCandy = ActiveLevel.grid[row + 1, column];
        //        //                    if (otherCandy != null)
        //        //                    {
        //        //                        //  Swap the candies
        //        //                        ActiveLevel.grid[column, row] = otherCandy;
        //        //                        ActiveLevel.grid[row + 1, column] = checkCandy;

        //        //                        //  Check to see if either one of the swapped candies is now part of a chain
        //        //                        if (HasChainAt(row + 1, column) || HasChainAt(column, row))
        //        //                        {
        //        //                            Swap swap = new Swap();
        //        //                            swap.candyA = checkCandy;
        //        //                            swap.candyB = otherCandy;

        //        //                            //  Add the candies to the array of possibleSwaps
        //        //                            possibleSwaps.Add(swap);
        //        //                        }

        //        //                        //  Swap the candies back to their original positions
        //        //                        ActiveLevel.grid[column, row] = checkCandy;
        //        //                        ActiveLevel.grid[row + 1, column] = otherCandy;
        //        //                    }
        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //    }

        ////  Check to see if a swap is possible
        //private static bool IsSwapPossible(Swap swap)
        //    {
        //        foreach (var item in possibleSwaps)
        //        {
        //            if ((item.candyA == swap.candyA && item.candyB == swap.candyB) || (item.candyA == swap.candyB && item.candyB == swap.candyA))
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }

        //    //  Check to see if the touch location is within the grid and if it is
        //    //  then returns true and the row and column position of the candy
        //    public static bool ConvertToPoint(CCPoint location, ref int row, ref int column)
        //    {
        //        if (location.X >= 38 && location.X < 598 && location.Y >= 216 && location.Y < 846)
        //        {
        //            //debugLabel.Text = "Touch was within the grid.";
        //            row = ConvertYToRow(location.Y);   //(846 - Convert.ToInt32(location.Y)) / 70;
        //            column = ConvertXToColumn(location.X);    //(Convert.ToInt32(location.X) - 38) / 62;
        //            Candy debugCandy = GetCandyAt(column, row);
        //            //debugLabel.Text = "Touched the candy at [" + debugCandy.getRow() + ", " + debugCandy.getColumn() + "]";
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    public static bool HasCandy(CCPointI gridLocation)
        //    {
        //        //  Make sure that the user didn't swipe out of the grid as there isn't any candies to swap with out there
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

        //        // Logic to remove the candies from the grid goes here, possibly call a method that takes the list of chains to work with
        //        // Don't forget that candies have to be removed from the grid and then afterwards the sprites need to be removed from the screen separately
        //        // which can be handle by another method
        //        foreach (Chain item in verticalChains)
        //        {
        //            horizontalChains.Add(item);
        //        }
        //        deleteChains = horizontalChains;
        //        RemoveCandies(horizontalChains);
        //    }

        //    //  Remove the candy objects from the screen and the grid
        //    private static async void RemoveCandies(List<Chain> chains)
        //    {
        //        if (finishedRemoving != false)
        //        {
        //            finishedRemoving = false;
        //        }

        //        foreach (Chain chain in chains)
        //        {
        //            foreach (Candy candy in chain.candies)
        //            {
        //                //  Remove the candy from the grid
        //                ActiveLevel.grid[candy.GetCandyRow(), candy.GetCandyColumn()] = null;
        //                CCSprite removeCandy = candy.GetCandySprite();
        //                if (removeCandy != null)
        //                {
        //                    const float timeToTake = 0.3f; // in seconds
        //                    CCFiniteTimeAction coreAction = null;
        //                    CCAction easing = null;

        //                    coreAction = new CCScaleTo(timeToTake, 0.3f);
        //                    easing = new CCEaseOut(coreAction, 0.1f);
        //                    removeCandy.RunAction(coreAction);

        //                    await Task.Delay(50);   // Wait for the scaling animation to show a bit before continuing on to remove the candy
        //                    //pointGone = false;
        //                    //pointLabel(candy.getRow(), candy.getColumn());
        //                    //while (!pointGone)
        //                    //{
        //                    //    await Task.Delay(1);
        //                    //}
        //                    removeCandy.RemoveFromParent(); // This should remove the candy from the screen
        //                    HandlePoints();
        //                }
        //            }
        //            //  Wait for all of the candies to be removed before moving on to the next chain in the list of chains
        //            await Task.Delay(300);
        //        }
        //        //  Since the method is finished removing all of chains, needed to set the finishedRemoving bool variable to true
        //        //  so the calling method can get out of it's await loop
        //        finishedRemoving = true;
        //    }

        //    //  Adds a label that shows "+10" for every candy that is destroyed, it then proceeds towards that scoreLabel, implying that it gets added to the score.
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

        //    //  Drops the candies down 
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
        //    //                Candy candy = GetCandyAt(column, row);
        //    //                if (candy == null)
        //    //                {
        //    //                    // Find which row number to drop the candy from
        //    //                    int tempRow = row - 1;
        //    //                    while (tempRow >= 0 && ActiveLevel.grid[tempcolumn, row] == null)
        //    //                    {
        //    //                        tempRow--;
        //    //                    }
        //    //                    //  Only runs if there's a row that has a candy in it
        //    //                    if (tempRow >= 0)
        //    //                    {
        //    //                        CCPoint position = new CCPoint(70 + (62 * column), 810 - (70 * row));
        //    //                        candy = GetCandyAt(tempcolumn, row);
        //    //                        candy.AddAction(new CCEaseOut(new CCMoveTo(0.3f, position), 0.3f));
        //    //                        candy.SetCandyPosition(column, row);    // Update the row and column of the candy
        //    //                        ActiveLevel.grid[column, row] = candy;             // Update the position of the candy within the grid
        //    //                        ActiveLevel.grid[tempcolumn, row] = null;
        //    //                        //  Wait for the candy to drop before moving to on the next candy
        //    //                        await Task.Delay(50);
        //    //                    }
        //    //                }
        //    //            }
        //    //        }
        //    //    }

        //    //    // Since the method should have gone through the entire grid and finished dropping the candies
        //    //    // need to set dropped to true so the calling method can get out of the await loop
        //    //    dropped = true;
        //    //}

        //    //  Fill the holes at the top of the of each column
        //    private static void FillColumns()
        //    {
        //        int candyType = 0;
        //        if (filledAgain != false)
        //        {
        //            filledAgain = false;
        //        }
        //        for (int column = 0; column < Configuration.gridColumns; column++)
        //        {
        //            //  Starting at the top and working downwards, add a new candy where it's needed
        //            for (int row = 0; row < Configuration.gridRows && ActiveLevel.grid[column, row] == null; row++)
        //            {
        //                if (ActiveLevel.level.tiles[column, row] == 1)
        //                {
        //                    int newCandyType = 0;
        //                    //  Have to first create a new candy outside of the while loop or otherwise the IDE won't let me use the variable newCandy
        //                    //  as it will be seen as using an unassigned variable, even though it will be assigned a new candy in the while loop
        //                    Candy newCandy = new Candy(column, row);
        //                    newCandyType = newCandy.GetCandyTypeID();
        //                    //  Make sure that each candy that is being added isn't the same as the one that was added previously
        //                    while (newCandyType == candyType)
        //                    {
        //                        newCandy = new Candy(column, row);
        //                        newCandyType = newCandy.GetCandyTypeID();
        //                    }
        //                    candyType = newCandyType;
        //                    ActiveLevel.grid[column, row] = newCandy;

        //                    // Once all of the candy is created to fill the grid back up
        //                    // Use an animation to add it to the screen
        //                    AnimateAddingNewCandies(column, row);
        //                }
        //            }
        //        }
        //        //  Since the entire grid was filled back up with candies, need to set the filledAgain bool variable to true
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

        //    //  Using an animation to add all of the new candies to screen
        //    private static async void AnimateAddingNewCandies(int row, int column)
        //    {
        //        //  Find the top most tile for the column
        //        int topMostRowLocation = 0;
        //        while (ActiveLevel.level.tiles[topMostRowLocation, column] != 1 && topMostRowLocation <= row)
        //        {
        //            topMostRowLocation++;
        //        }
        //        // Starting position for the candy to be added
        //        CCPoint beginningPosition = new CCPoint(70 + (62 * column), 810 - (70 * topMostRowLocation) + 50);
        //        // The final position of where the candy goes
        //        CCPoint endPosition = new CCPoint(70 + (62 * column), 810 - (70 * row));
        //        ActiveLevel.grid[column, row].Position = beginningPosition;
        //        //AddChild(ActiveLevel.grid[column, row]);   // Add the candy to the screen
        //        // Animation to move the candy into it's proper position
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
        //                    int matchType = ActiveLevel.grid[column, row].GetCandyTypeID();
        //                    //  If the next two candies match than there's a chain here
        //                    if ((ActiveLevel.grid[column, row + 1] != null && ActiveLevel.grid[column, row + 2] != null)
        //                        && ActiveLevel.grid[column, row + 1].GetCandyTypeID() == matchType && ActiveLevel.grid[column, row + 2].GetCandyTypeID() == matchType)
        //                    {
        //                        //  Create a new chain
        //                        var chain = new Chain();
        //                        chain.chainType = Chain.ChainType.Horizontal;
        //                        //  Add all of the candies within the chain to chain variable
        //                        do
        //                        {
        //                            chain.addCandy(GetCandyAt(column, row));
        //                            column += 1;
        //                        }
        //                        while ((column < Configuration.gridColumns && ActiveLevel.grid[column, row] != null) && ActiveLevel.grid[column, row].GetCandyTypeID() == matchType);
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
        //                    int matchType = ActiveLevel.grid[column, row].GetCandyTypeID();
        //                    //  If the two candies below it matches, then there's a chain
        //                    if ((ActiveLevel.grid[row + 1, column] != null && ActiveLevel.grid[row + 2, column] != null)
        //                        && ActiveLevel.grid[row + 1, column].GetCandyTypeID() == matchType && ActiveLevel.grid[row + 2, column].GetCandyTypeID() == matchType)
        //                    {
        //                        //  Create a new chain
        //                        var chain = new Chain();
        //                        chain.chainType = Chain.ChainType.Vertical;
        //                        //  Add all of the candies within the chain to the chain variable
        //                        do
        //                        {
        //                            chain.addCandy(GetCandyAt(column, row));
        //                            row += 1;
        //                        }
        //                        while ((row < Configuration.gridRows && ActiveLevel.grid[column, row] != null) && ActiveLevel.grid[column, row].GetCandyTypeID() == matchType);
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
