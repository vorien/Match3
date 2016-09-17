using System;

using CocosSharp;
using Match3.Information;
using Match3.Functions;
using System.Collections.Generic;

namespace Match3.Entities
{
    // Class that creates one of several different candies,
    // it will contain private variables for it's Name, location, and whether or not it's a special candy
    public class Candy : CCNode
    {
        public CCSize spriteSize;
        private string candyName;
        private CCSprite candySprite;   // sprite for the candy
        public int candyTypeID;    // variables to hold the location of the candy in the grid
        private CCPoint swipeStart; // Keeps track of where the swipe started from
        public CCPointI gridLocation;
        private CCLabel debugLabel;
        private Random rand = new Random();


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

            float candyDimensions = (ScreenInfo.preferredWidth - (Configuration.gridWidthSpacing * 2)) / Configuration.gridColumns;
            spriteSize = candySprite.ContentSize = new CCSize(candyDimensions, candyDimensions);
            candySprite.AnchorPoint = CCPoint.AnchorMiddle;
            candySprite.Position = new CCPoint((spriteSize.Width * (column + .5f)), spriteSize.Height * (row + .5f));

            ////  Adding a debug label that will display the candy's row and column numbers, to see if the candy visually match with it's array locations
            //debugLabel = new CCLabel("", "Arial", 22, CCLabelFormat.SystemFont);
            ////debugLabel.Text = "[" + row + ", " + column + "]";
            //debugLabel.Color = CCColor3B.White;
            //debugLabel.PositionX = candySprite.ContentSize.Width / 2.0f;
            //debugLabel.PositionY = candySprite.ContentSize.Height / 2.0f;
            //debugLabel.AnchorPoint = CCPoint.AnchorMiddle;
            //candySprite.AddChild(debugLabel);
            //candySprite.AddChild(drawNode, 10);

            AddChild(candySprite);


        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Register for touch events
            var touchListener = new CCEventListenerTouchOneByOne();
            touchListener.IsSwallowTouches = true;
            touchListener.OnTouchBegan = HandleTouchBegan;
            touchListener.OnTouchMoved = HandleTouchMoved;
            //touchListener.OnTouchEnded = HandleTouchEnded;
            //touchListener.OnTouchCancelled = HandleTouchCancelled;
            AddEventListener(touchListener, candySprite);

        }


        private bool HandleTouchBegan(CCTouch touch, CCEvent touchEvent)
        {
            if (candySprite.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
            {
                // The user touched this candy";
                swipeStart = touch.Location;
                debugLabel.Text = candySprite.BoundingBox.ToString();
                debugLabel.Color = CCColor3B.Green;
                //candySprite.Position = touch.Location;
                return true;
            }
            else
            {
                return false;
            }

        }

        private void HandleTouchMoved(CCTouch touch, CCEvent touchEvent)
        {
            CCPoint touchDelta = touch.Location - swipeStart;
            CCPointI swipeDirection = new CCPointI(0, 0);
            if (Math.Abs(touchDelta.X) > Math.Abs(touchDelta.Y))
            {
                swipeDirection.X = Math.Sign(touchDelta.X);
            }
            else
            {
                swipeDirection.Y = Math.Sign(touchDelta.Y);
            }

            // we only care about the first touch:
            //var locationOnScreen = touch.Location;

            //int column, row;
            //row = column = 90;
            //if (convertToPoint(locationOnScreen, ref row, ref column))
            //{

            //    int horzDelta = 0, vertDelta = 0;
            //    if (column < swipeFromCol)
            //    {          // swipe left
            //        horzDelta = -1;
            //    }
            //    else if (column > swipeFromCol)
            //    {   // swipe right
            //        horzDelta = 1;
            //    }
            //    else if (row < swipeFromRow)
            //    {         // swipe down
            //        vertDelta = -1;
            //    }
            //    else if (row > swipeFromRow)
            //    {         // swipe up
            //        vertDelta = 1;
            //    }

            //    if (horzDelta != 0 || vertDelta != 0)
            //    {
            //        //debugLabel.Text = "Checking to see if the swap is valid.";
            //        //  Turn off the user interaction as the user should be allowed to move any of candies while candies are swapped, removed, and the grid refilled
            PauseListeners();
            //GridFunctions.TrySwap(this, swipeDirection);
            ResumeListeners();
            //    }
            //}
        }

        //  Once the touch is finished reset the swipeFromRow and swipeFromCol
        private void HandleTouchEnded(CCTouch touch, CCEvent touchEvent)
        {
            //swipeFromRow = 90;
            //swipeFromCol = 90;
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
    }
}