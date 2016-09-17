using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CocosSharp;
using Match3.Entities;
using Match3;
using Match3.Information;
using Match3.Functions;

namespace Match3.Scenes
{
    //  A class for a grid
    public class GridLayer : CCLayer
    {
        public GridLayer(LevelLayer levelLayer)
        {
            ActiveLevel.grid = new Candy[Configuration.gridRows, Configuration.gridColumns];


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

            GridFunctions.InitializeGrid();
            DisplayGrid();

        }

        //  Adds the candies to the layer and positions them on screen
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
