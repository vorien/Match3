using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CocosSharp;
using Match3.Entities;
using Match3;
using Match3.Functions;
using System.Diagnostics;

namespace Match3.Scenes
{
    //  A class for a grid
    public class GridLayer : CCLayer
    {
        private int possibleSwapCount;

        public GridLayer(LevelLayer levelLayer)
        {
            ActiveLevel.grid = new Material[Configuration.gridColumns, Configuration.gridRows];

            InitializeGrid();
            DisplayGrid();



            //string parentString = "Still Testing";
            //Tile checktilesize = new Tile();
            //parentString = tileLayer.AnchorPoint.ToString() + "\n" + tileLayer.Position.ToString();
            //parentString = tileLayer.AnchorPoint.ToString() + "\n" + tileLayer.Position.ToString() + "\n" + levelLayer.scoreLayer.scoreLabel.BoundingBoxTransformedToWorld.MinY.ToString();
            //CCLayer testLayer = new CCLayer();
            //CCLabel testLabel = new CCLabel(parentString, "Arial", 50, CCLabelFormat.SystemFont);
            //testLabel.Color = CCColor3B.White;
            //testLabel.AnchorPoint = CCPoint.AnchorMiddle;
            //testLabel.Position = new CCPoint(ScreenInfo.preferredWidth / 2, ScreenInfo.preferredHeight / 2);
            //testLayer.AddChild(testLabel);
            //AddChild(testLayer);
        }

        public void InitializeGrid()
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

        //  Fills the grid up with new materials
        private void FillGrid()
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
        public void AssignMaterial(int column, int row)
        {
            Material newMaterial;
            do
            {
                newMaterial = new Material(column, row, this);
            }
            while
            (
                newMaterial.IsPartOfChain()
            );

            ActiveLevel.grid[column, row] = newMaterial;
        }



        //  Adds the materials to the layer and positions them on screen
        //  based on their position in the grid
        private void DisplayGrid()
        {
            for (int gRow = 0; gRow < Configuration.gridRows; gRow++)
            {
                for (int gColumn = 0; gColumn < Configuration.gridColumns; gColumn++)
                {
                    if (ActiveLevel.grid[gColumn, gRow] != null)
                    {
                        AddChild(ActiveLevel.grid[gColumn, gRow],0);
                    }
                }
            }
        }

    }
}
