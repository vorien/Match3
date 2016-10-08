using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CocosSharp;
using Match3.Entities;
using Match3.Scenes;
using Match3;
using Match3.Functions;

namespace Match3
{
    public class LevelLayer : CCLayer
    {

        public HomeButton homeButton;
        public ScoreLayer scoreLayer;
        private TargetLayer targetLayer;
        public MovesLeftLayer movesLeftLayer;
        private CCLayer tileLayer;
        private GridLayer gridLayer;

        private Random rand = new Random();

        private Level level;
        public LevelLayer()
        {
            //CCDrawNode drawNode = new CCDrawNode();
            //AddChild(drawNode);
            //var shape = new CCRect(
            //    0, 0, ScreenInfo.preferredWidth,ScreenInfo.preferredHeight);
            //drawNode.DrawRect(shape,
            //    fillColor: CCColor4B.LightGray,
            //    borderWidth: 1,
            //    borderColor: CCColor4B.Blue);

            scoreLayer = new ScoreLayer();
            scoreLayer.Position = new CCPoint(20, ScreenInfo.preferredHeight - 20);
            AddChild(scoreLayer);

            targetLayer = new TargetLayer();
            AddChild(targetLayer);

            movesLeftLayer = new MovesLeftLayer();
            AddChild(movesLeftLayer);
            movesLeftLayer.Position = new CCPoint(ScreenInfo.preferredWidth - 20, ScreenInfo.preferredHeight - 20);

            CCPoint blockOffset = new CCPoint(Configuration.gridWidthSpacing, Configuration.gridVerticalOffset);
            // Display background tiles
            tileLayer = new CCLayer();
            for (int gRow = 0; gRow < Configuration.gridRows; gRow++)
            {
                for (int gColumn = 0; gColumn < Configuration.gridColumns; gColumn++)
                {
                    if (ActiveLevel.level.tiles[gColumn, gRow] == 1)
                    {
                        Tile tile = new Tile();
                        tile.Position = new CCPoint((tile.ContentSize.Width * (gRow + .5f)), tile.ContentSize.Height * (gColumn + .5f)) + blockOffset;
                        tileLayer.AddChild(tile);
                    }
                }
            }
            AddChild(tileLayer);

            homeButton = new HomeButton();
            AddChild(homeButton);

            level = ActiveLevel.level;

            gridLayer = new GridLayer(this);
            AddChild(gridLayer);


            //string debugString = "Not Updated";
            //debugString = scoreLayer.scoreLabel.ContentSize.ToString() + "\n" + scoreLayer.scoreLabel.BoundingBoxTransformedToWorld.MinY.ToString();
            //CCLayer testLayer = new CCLayer();
            //CCLabel testLabel = new CCLabel(debugString, "Arial", 50, CCLabelFormat.SystemFont);
            //testLabel.Color = CCColor3B.White;
            //testLabel.AnchorPoint = CCPoint.AnchorMiddle;
            //testLabel.Position = new CCPoint(ScreenInfo.preferredWidth / 2, ScreenInfo.preferredHeight / 2);
            //testLayer.AddChild(testLabel);
            //AddChild(testLayer);



        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            //var bounds = VisibleBoundsWorldspace;

            //label.Text = Director.RunningScene.ContentSize.Width.ToString();
            //buttonLayer.ContentSize = new CCSize(ScreenInfo.preferredWidth, ScreenInfo.preferredHeight);
            //// Register for touch events
            //var touchListener = new CCEventListenerTouchAllAtOnce();
            //touchListener.OnTouchesEnded = OnTouchesEnded;
            //AddEventListener(touchListener, this);
        }

        private void GameOver(bool win)
        {
            GameOverScene gameOverScene = new GameOverScene(GameView);
            gameOverScene.win = win;
            Director.ReplaceScene(gameOverScene);
        }

        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            //var location = arg1[0].Location;
            ////  Determine if the user touched one of the buttons
            //if ((location.X > 289 && location.X < 351) && (location.Y > 169 && location.Y < 231))
            //{
            //    Director.RunWithScene(new GameScene(GameView));
            //}
        }


        public void disableListeners()
        {
            PauseListeners();
        }

        public void enableListeners()
        {
            ResumeListeners();
        }

    }
}
