using System;

using CocosSharp;
using Match3;
using Match3.Functions;
using System.Collections.Generic;
using Match3.Scenes;
using System.Diagnostics;

namespace Match3.Entities
{
    // Class that creates one of several different materials,
    // it will contain private variables for it's Name, location, and whether or not it's a special material
    public class Material : CCSprite
    {
        public CCSize spriteSize;
        public CCSize nodeSize;
        public int chainGroup;
        private string materialName;
        float materialDimensions;
        private CCTexture2D texture;   // image for the material
        public int materialTypeID;    // Configuration.MaterialTypes key for the material image
        private CCPoint swipeStart; // Keeps track of where the swipe started from
        private CCPoint swipeMoved; // Keeps track of the current touch location
        public CCPointI gridLocation;
        public CCLabel debugLabel;
        //public CCPoint position;
        private Random rand = new Random();
        private GridLayer gridLayer;
        public int id;
        //private CCDrawNode drawNode;

        //private LevelLayer levelLayer { get; set; }
        //private GridLayer gridLayer { get; set; }
        private int materialWNChainGroup, materialESChainGroup;
        private Material materialN, materialS, materialE, materialW;

        // default constructor
        // generates a random material
        public Material(int column, int row, GridLayer gLayer)
        {
            id = column * 10 + row;
            gridLayer = gLayer;
            materialTypeID = SetRandomMaterialType();
            materialName = Configuration.materialTypes[materialTypeID].Item2;
            string materialFile = Configuration.materialTypes[materialTypeID].Item1;
            this.Texture = texture = new CCTexture2D(materialFile);

            //  Adding a debug label that will display the material's row and column numbers, to see if the material visually match with it's array locations
            //debugLabel = new CCLabel("", "Arial", 40, CCLabelFormat.SystemFont);
            ////debugLabel.Text = "[" + row + ", " + column + "]";
            //debugLabel.Color = CCColor3B.White;
            //debugLabel.Position = new CCPoint(0, 200);
            //debugLabel.AnchorPoint = CCPoint.AnchorLowerLeft;
            //AddChild(debugLabel);

            gridLocation = new CCPointI(column, row);
            AnchorPoint = CCPoint.AnchorMiddle;
            materialDimensions = (ScreenInfo.preferredWidth - (Configuration.gridWidthSpacing * 2)) / Configuration.gridColumns;
            Position = new CCPoint((materialDimensions * (column + .5f)), materialDimensions * (row + .5f)) + new CCPoint(Configuration.gridWidthSpacing, Configuration.gridVerticalOffset);
            ContentSize = nodeSize = new CCSize(materialDimensions, materialDimensions);


            //spriteSize = materialSprite.ContentSize = new CCSize(materialDimensions, materialDimensions);
            //materialSprite.AnchorPoint = CCPoint.AnchorMiddle;
            //materialSprite.Position = position;
            ;

            //  Adding a debug label to the material for testing
            debugLabel = new CCLabel("", "Arial", 70, CCLabelFormat.SystemFont);
            //debugLabel.Text = "[" + row + ", " + column + "]";
            //debugLabel.Text = "?";
            //debugLabel.Text = "[" + materialSprite.BoundingBoxTransformedToWorld.Center.X + ", " + materialSprite.BoundingBoxTransformedToWorld.Center.Y + "]";
            //debugLabel.Text = ThisChainGroup().ToString();
            debugLabel.Color = CCColor3B.Black;
            debugLabel.PositionX = ContentSize.Width / 2.0f;
            debugLabel.PositionY = ContentSize.Height / 2.0f;
            debugLabel.AnchorPoint = CCPoint.AnchorMiddle;
            AddChild(debugLabel);

            //materialSprite.AddChild(drawNode, 10);
            //drawNode.DrawCircle(
            //    center: materialSprite.AnchorPoint,
            //    radius: materialDimensions / 2,
            //    color: CCColor4B.Orange);

            //AddChild(materialSprite);


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
            AddEventListener(touchListener, this);
            debugLabel.Text = "[" + Position.X + ", " + Position.Y + "]";

        }


        private bool HandleTouchBegan(CCTouch touch, CCEvent touchEvent)
        {
            if (BoundingBox.ContainsPoint(touch.Location))
            {
                // The user touched this material";
                swipeStart = ScreenToWorldspace(touch.Location);
                swipeMoved = ScreenToWorldspace(touch.Location);
                //debugLabel.Text = "Touched";
                //debugLabel.Text = materialSprite.BoundingBox.ToString();
                //debugLabel.Color = CCColor3B.Green;
                //materialSprite.Position = touch.Location;
                //materialSprite.Texture = new CCTexture2D("PeppermintSwirl");
                debugLabel.Text = "[" + Position.X + ", " + Position.Y + "]";
                return true;
            }
            else
            {
                return false;
            }

        }

        private void HandleTouchMoved(CCTouch touch, CCEvent touchEvent)
        {
            swipeMoved = ScreenToWorldspace(touch.Location);
            Debug.WriteLine("swipeStart: " + swipeStart.ToString() + " ~ swipeMoved: " + swipeMoved.ToString());
        }

        private void HandleTouchEnded(CCTouch touch, CCEvent touchEvent)
        {
            //if(swipeMoved.X < 5 || swipeMoved.Y < 5)
            //{
            //    return;
            //}
            Debug.WriteLine("swipeStart: " + swipeStart.ToString() + " ~ swipeMoved: " + swipeMoved.ToString());
            CCPoint touchDelta = swipeMoved - swipeStart;
            if (CCPoint.Distance(swipeMoved, swipeStart) > (materialDimensions / 2))
            {
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
                
                debugLabel.Text = swipeDirection.X + " , " + swipeDirection.Y;

                //  Turn off the user interaction as the user should be allowed to move any of materials while materials are swapped, removed, and the grid refilled
                PauseListeners();
                GridFunctions.TrySwap(gridLocation, swipeDirection);
                ResumeListeners();
            }
            else
            {
                debugLabel.Text = gridLocation.X + " , " + gridLocation.Y;
            }
        }

        //  If a touch was cancelled, call the touchesEnded method to reset swipe variables
        private void touchesCancelled(CCTouch touch, CCEvent touchEvent)
        {
            HandleTouchEnded(touch, touchEvent);
        }

        //  Check to see if this material is part of a chain
        public bool IsPartOfChain()
        {

            //  Check to see if there's a row chain
            int horizontalLength = 1;
            int column = gridLocation.X;
            int row = gridLocation.Y;

            for (int ctr = column - 1; ctr > -1; ctr--)
            {
                Material checkMaterial = ActiveLevel.grid[ctr, row];
                if (checkMaterial != null && checkMaterial.materialTypeID == materialTypeID)
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
                Material checkMaterial = ActiveLevel.grid[ctr, row];
                if (checkMaterial != null && checkMaterial.materialTypeID == materialTypeID)
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
                if (ActiveLevel.grid[column, ctr] != null && ActiveLevel.grid[column, ctr].materialTypeID == materialTypeID)
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
                if (ActiveLevel.grid[column, ctr] != null && ActiveLevel.grid[column, ctr].materialTypeID == materialTypeID)
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



        //  Returns what type of material it is
        public int GetMaterialTypeID()
        {
            return materialTypeID;
        }

        ////  points the caller to the location of the materialSprite in memory
        //public CCSprite GetMaterialSprite()
        //{
        //    return materialSprite;
        //}

        //  Returns which row the material is in
        public int GetMaterialRow()
        {
            return gridLocation.X;
        }

        //  Returns which column the material is in
        public int GetMaterialColumn()
        {
            return gridLocation.Y;
        }

        //  Sets the new grid position of the material
        public void SetMaterialPosition(int column, int row)
        {
            gridLocation.X = row;
            gridLocation.Y = column;
        }

        // Generate a random number within the range of 1-5
        // 1 -> Red, 2 -> Blue, 3 -> Yellow, 4 -> Purple, 5 -> Green
        public int SetRandomMaterialType()
        {
            int randMax = Configuration.materialTypes.Count; // Number of materials in the configuration file
            return rand.Next(0, randMax); // random list item id
        }

        public void CheckForChains()
        {
            Debug.WriteLine(gridLocation.X + "," + gridLocation.Y);
            materialN = GridFunctions.GetMaterialAtGridLocation(gridLocation + new CCPointI(0, 1));
            materialS = GridFunctions.GetMaterialAtGridLocation(gridLocation + new CCPointI(0, -1));
            materialE = GridFunctions.GetMaterialAtGridLocation(gridLocation + new CCPointI(1, 0));
            materialW = GridFunctions.GetMaterialAtGridLocation(gridLocation + new CCPointI(-1, 0));

            SetChainGroup("Horizontal");
            SetChainGroup("Vertical");
        }
        public void SetChainGroup(string direction)
        {
            switch (direction)
            {
                case "Horizontal":
                    if (materialW == null || materialE == null)
                    {
                        Debug.WriteLine("E or W null");
                        return;
                    }
                    if (materialW.materialTypeID != this.materialTypeID || materialE.materialTypeID != this.materialTypeID)
                    {
                        Debug.WriteLine("No EW 3 match");
                        return;
                    }
                    Debug.WriteLine("Setting chain groups to EW");
                    materialWNChainGroup = materialW.chainGroup;
                    materialESChainGroup = materialE.chainGroup;
                    break;
                case "Vertical":
                    if (materialN == null || materialS == null)
                    {
                        Debug.WriteLine("N or S null");
                        return;
                    }
                    if (materialN.materialTypeID != this.materialTypeID || materialS.materialTypeID != this.materialTypeID)
                    {
                        Debug.WriteLine("No NS 3 match");
                        return;
                    }
                    Debug.WriteLine("Setting chain groups to NS");
                    materialWNChainGroup = materialN.chainGroup;
                    materialESChainGroup = materialS.chainGroup;
                    break;
                default:
                    throw new Exception("direction must be Horizontal or Vertical");
                    //break;
            }

            if (materialWNChainGroup == 0 && materialESChainGroup == 0) //neither is part of a chain group
            {
                Debug.WriteLine("Neither is part of a chain group");
                // New Chain Group
                chainGroup = materialWNChainGroup = materialESChainGroup = ThisChainGroup();
                if (ActiveLevel.chains.ContainsKey(chainGroup))
                {
                    throw new Exception("Should be a new chain, but already exists in chains dictionary");
                }
                else
                {
                    Debug.WriteLine("Adding chain group");
                    Chain newChain = new Chain();
                    newChain.chainDirection = direction;
                    newChain.chainCount = 3;
                    ActiveLevel.chains.Add(chainGroup, newChain);
                    ActiveLevel.chains[chainGroup].UpdateChainLengths(direction, 3);
                }
            }
            else
            {
                if (materialWNChainGroup != 0 && materialESChainGroup != 0)
                {
                    Debug.WriteLine("Both are part of a chain group");
                    if (materialWNChainGroup != materialESChainGroup)
                    {
                        throw new Exception("Multiple chains should not connect: current chainGroup not set");
                    }
                    else
                    {
                        if (chainGroup == 0)
                        {
                            Debug.WriteLine("active chain group is 0");
                            chainGroup = materialWNChainGroup;
                            ActiveLevel.chains[chainGroup].chainCount += 1;
                            ActiveLevel.chains[chainGroup].UpdateChainLengths(direction, 1);
                            //TODO: Check direction vs chainDirection to see if chainType should be X
                        }
                        else
                        {
                            throw new Exception("Current material should never have both neighbors set");
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("Only one is part of a chain groupe");
                    chainGroup = materialWNChainGroup = materialESChainGroup = Math.Max(materialWNChainGroup, materialESChainGroup);
                    ActiveLevel.chains[chainGroup].chainCount += 2;
                    ActiveLevel.chains[chainGroup].UpdateChainLengths(direction, 2);
                    //TODO: Check direction vs chainDirection to see if chainType should be T
                }
            }
            debugLabel.Text = chainGroup.ToString();
        }

        private int ThisChainGroup()
        {
            int thisChainGroup = (gridLocation.Y - 1) * Configuration.gridColumns + gridLocation.X + 1;
            return thisChainGroup;
        }
    }
}