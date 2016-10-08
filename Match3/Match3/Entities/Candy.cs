using System;

using CocosSharp;
using Match3;
using Match3.Functions;
using System.Collections.Generic;
using Match3.Scenes;

namespace Match3.Entities
{
    // Class that creates one of several different candies,
    // it will contain private variables for it's Name, location, and whether or not it's a special candy
    public class Candy : CCSprite
    {
        public CCSize spriteSize;
        public int chainGroup;
        private string candyName;
        float candyDimensions;
        public CCSprite candySprite;   // sprite for the candy
        public int candyTypeID;    // variables to hold the location of the candy in the grid
        private CCPoint swipeStart; // Keeps track of where the swipe started from
        private CCPoint swipeMoved; // Keeps track of the current touch location
        public CCPointI gridLocation;
        public CCLabel debugLabel;
        public CCPoint position;
        private Random rand = new Random();
        //private CCDrawNode drawNode;

        //private LevelLayer levelLayer { get; set; }
        //private GridLayer gridLayer { get; set; }
        private int candyWNChainGroup, candyESChainGroup;
        private Candy candyN, candyS, candyE, candyW;

        // default constructor
        // generates a random candy
        public Candy(int column, int row)
        {
            //  Adding a debug label that will display the candy's row and column numbers, to see if the candy visually match with it's array locations
            //debugLabel = new CCLabel("", "Arial", 40, CCLabelFormat.SystemFont);
            ////debugLabel.Text = "[" + row + ", " + column + "]";
            //debugLabel.Color = CCColor3B.White;
            //debugLabel.Position = new CCPoint(0, 200);
            //debugLabel.AnchorPoint = CCPoint.AnchorLowerLeft;
            //AddChild(debugLabel);

            gridLocation = new CCPointI(column, row);

            candyTypeID = SetRandomCandyType();
            candyName = Configuration.candyTypes[candyTypeID].Item2;
            string candyFile = Configuration.candyTypes[candyTypeID].Item1;

            candySprite = new CCSprite(candyFile);

            candyDimensions = (ScreenInfo.preferredWidth - (Configuration.gridWidthSpacing * 2)) / Configuration.gridColumns;
            spriteSize = candySprite.ContentSize = new CCSize(candyDimensions, candyDimensions);
            candySprite.AnchorPoint = CCPoint.AnchorMiddle;
            candySprite.Position = new CCPoint((spriteSize.Width * (column + .5f)), spriteSize.Height * (row + .5f)) + new CCPoint(Configuration.gridWidthSpacing, Configuration.gridVerticalOffset);
            ;

            //  Adding a debug label to the candy for testing
            debugLabel = new CCLabel("", "Arial", 30, CCLabelFormat.SystemFont);
            //debugLabel.Text = "[" + row + ", " + column + "]";
            //debugLabel.Text = "?";
            //debugLabel.Text = "[" + candySprite.BoundingBoxTransformedToWorld.Center.X + ", " + candySprite.BoundingBoxTransformedToWorld.Center.Y + "]";
            //debugLabel.Text = ThisChainGroup().ToString();
            debugLabel.Color = CCColor3B.Black;
            debugLabel.PositionX = candySprite.ContentSize.Width / 2.0f;
            debugLabel.PositionY = candySprite.ContentSize.Height / 2.0f;
            debugLabel.AnchorPoint = CCPoint.AnchorMiddle;
            candySprite.AddChild(debugLabel);

            //candySprite.AddChild(drawNode, 10);
            //drawNode.DrawCircle(
            //    center: candySprite.AnchorPoint,
            //    radius: candyDimensions / 2,
            //    color: CCColor4B.Orange);

            AddChild(candySprite);
            position = candySprite.Position;

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Register for touch events
            var touchListener = new CCEventListenerTouchOneByOne();
            touchListener.IsSwallowTouches = true;
            touchListener.OnTouchBegan = HandleTouchBegan;
            touchListener.OnTouchMoved = HandleTouchMoved;
            touchListener.OnTouchEnded = HandleTouchEnded;
            //touchListener.OnTouchCancelled = HandleTouchCancelled;
            AddEventListener(touchListener, candySprite);

        }


        private bool HandleTouchBegan(CCTouch touch, CCEvent touchEvent)
        {
            if (candySprite.BoundingBox.ContainsPoint(touch.Location))
            {
                // The user touched this candy";
                swipeStart = touch.Location;
                //debugLabel.Text = "Touched";
                //debugLabel.Text = candySprite.BoundingBox.ToString();
                //debugLabel.Color = CCColor3B.Green;
                //candySprite.Position = touch.Location;
                debugLabel.Text = "[" + position.X + ", " + position.Y + "]";
                return true;
            }
            else
            {
                return false;
            }

        }

        private void HandleTouchMoved(CCTouch touch, CCEvent touchEvent)
        {
            swipeMoved = touch.Location;
        }

        private void HandleTouchEnded(CCTouch touch, CCEvent touchEvent)
        {
            CCPoint touchDelta = swipeMoved - swipeStart;
            CCPointI swipeDirection = new CCPointI(0, 0);
            if (Math.Abs(touchDelta.X) > Math.Abs(touchDelta.Y))
            {
                swipeDirection.X = Math.Sign(touchDelta.X);
                swipeDirection.Y = 0;
            }
            else
            {
                swipeDirection.X = 0;
                swipeDirection.Y = Math.Sign(touchDelta.Y);
            }

            //debugLabel.Text = swipeDirection.X + " , " + swipeDirection.Y;
            
            //  Turn off the user interaction as the user should be allowed to move any of candies while candies are swapped, removed, and the grid refilled
            PauseListeners();
            GridFunctions.TrySwap(this, swipeDirection);
            ResumeListeners();
        }

        //  If a touch was cancelled, call the touchesEnded method to reset swipe variables
        private void touchesCancelled(CCTouch touch, CCEvent touchEvent)
        {
            HandleTouchEnded(touch, touchEvent);
        }

        //  Check to see if this candy is part of a chain
        public bool IsPartOfChain()
        {

            //  Check to see if there's a row chain
            int horizontalLength = 1;
            int column = gridLocation.X;
            int row = gridLocation.Y;

            for (int ctr = column - 1; ctr > -1; ctr--)
            {
                Candy checkCandy = ActiveLevel.grid[ctr, row];
                if (checkCandy != null && checkCandy.candyTypeID == candyTypeID)
                {
                    horizontalLength++;
                }
                else
                {
                    break;
                }
            }
            for (int ctr = column + 1; ctr < Configuration.gridColumns; ctr++)
            {
                Candy checkCandy = ActiveLevel.grid[ctr, row];
                if (checkCandy != null && checkCandy.candyTypeID == candyTypeID)
                {
                    horizontalLength++;
                }
                else
                {
                    break;
                }
            }
            if (horizontalLength > 2) { return true; }

            //  Check to see if there's a row chain
            int verticalLength = 1;

            for (int ctr = row - 1; ctr > -1; ctr--)
            {
                if (ActiveLevel.grid[column, ctr] != null && ActiveLevel.grid[column, ctr].candyTypeID == candyTypeID)
                {
                    verticalLength++;
                }
                else
                {
                    break;
                }
            }
            for (int ctr = row + 1; ctr < Configuration.gridColumns; ctr++)
            {
                if (ActiveLevel.grid[column, ctr] != null && ActiveLevel.grid[column, ctr].candyTypeID == candyTypeID)
                {
                    verticalLength++;
                }
                else
                {
                    break;
                }
            }
            if (verticalLength > 2) { return true; }

            return false;
        }



        //  Returns what type of candy it is
        public int GetCandyTypeID()
        {
            return candyTypeID;
        }

        //  points the caller to the location of the candySprite in memory
        public CCSprite GetCandySprite()
        {
            return candySprite;
        }

        //  Returns which row the candy is in
        public int GetCandyRow()
        {
            return gridLocation.X;
        }

        //  Returns which column the candy is in
        public int GetCandyColumn()
        {
            return gridLocation.Y;
        }

        //  Sets the new grid position of the candy
        public void SetCandyPosition(int column, int row)
        {
            gridLocation.X = row;
            gridLocation.Y = column;
        }

        // Generate a random number within the range of 1-5
        // 1 -> Red, 2 -> Blue, 3 -> Yellow, 4 -> Purple, 5 -> Green
        public int SetRandomCandyType()
        {
            int randMax = Configuration.candyTypes.Count; // Number of candies in the configuration file
            return rand.Next(0, randMax); // random list item id
        }

        public void CheckForChains()
        {
            candyN = GridFunctions.GetCandyAtGridLocation(gridLocation + new CCPointI(0, 1));
            candyS = GridFunctions.GetCandyAtGridLocation(gridLocation + new CCPointI(0, -1));
            candyE = GridFunctions.GetCandyAtGridLocation(gridLocation + new CCPointI(1, 0));
            candyW = GridFunctions.GetCandyAtGridLocation(gridLocation + new CCPointI(-1, 0));

            SetChainGroup("Horizontal");
            SetChainGroup("Vertical");
        }
        public void SetChainGroup(string direction)
        {
            switch (direction)
            {
                case "Horizontal":
                    if (candyW == null || candyE == null)
                    {
                        return;
                    }
                    if (candyW.candyTypeID != this.candyTypeID || candyE.candyTypeID != this.candyTypeID)
                    {
                        return;
                    }
                    candyWNChainGroup = candyW.chainGroup;
                    candyESChainGroup = candyE.chainGroup;
                    break;
                case "Vertical":
                    if (candyN == null || candyS == null)
                    {
                        return;
                    }
                    if (candyN.candyTypeID != this.candyTypeID || candyS.candyTypeID != this.candyTypeID)
                    {
                        return;
                    }
                    candyWNChainGroup = candyN.chainGroup;
                    candyESChainGroup = candyS.chainGroup;
                    break;
                default:
                    throw new Exception("direction must be Horizontal or Vertical");
                    //break;
            }

            if (candyWNChainGroup == 0 && candyESChainGroup == 0) //neither is part of a chain group
            {
                // New Chain Group
                chainGroup = candyWNChainGroup = candyESChainGroup = ThisChainGroup();
                if (Configuration.chains.ContainsKey(chainGroup))
                {
                    throw new Exception("Should be a new chain, but already exists in chains dictionary");
                }
                else
                {
                    Chain newChain = new Chain();
                    newChain.chainDirection = direction;
                    newChain.chainCount = 3;
                    Configuration.chains.Add(chainGroup, newChain);
                    Configuration.chains[chainGroup].UpdateChainLengths(direction, 3);
                }
            }
            else
            {
                if (candyWNChainGroup != 0 && candyESChainGroup != 0)
                {
                    if (candyWNChainGroup != candyESChainGroup)
                    {
                        throw new Exception("Multiple chains should not connect: current chainGroup not set");
                    }
                    else
                    {
                        if (chainGroup == 0)
                        {
                            chainGroup = candyWNChainGroup;
                            Configuration.chains[chainGroup].chainCount += 1;
                            Configuration.chains[chainGroup].UpdateChainLengths(direction, 1);
                            //TODO: Check direction vs chainDirection to see if chainType should be X
                        }
                        else
                        {
                            throw new Exception("Current candy should never have both neighbors set");
                        }
                    }
                }
                else
                {
                    chainGroup = candyWNChainGroup = candyESChainGroup = Math.Max(candyWNChainGroup, candyESChainGroup);
                    Configuration.chains[chainGroup].chainCount += 2;
                    Configuration.chains[chainGroup].UpdateChainLengths(direction, 2);
                    //TODO: Check direction vs chainDirection to see if chainType should be T
                }
            }
            this.debugLabel.Text = chainGroup.ToString();
        }

        private int ThisChainGroup()
        {
            int thisChainGroup = (gridLocation.Y - 1) * Configuration.gridColumns + gridLocation.X + 1;
            return thisChainGroup;
        }
    }
}