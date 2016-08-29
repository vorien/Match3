using System.Collections.Generic;

using CocosSharp;
using Match3.Entities;

namespace Match3.Scenes
{
    public class GameScene : CCScene
    {
        private CCScene gScene;
        private CCLayer backgroundLayer;
        private CandyLayer cLayer;
        private int swipeFromRow, swipeFromCol; // Keeps track of where the swipe started from
        private CCLabel debugLabel;

        public GameScene(CCGameView gameView, int level) : base(gameView)
        {
            gScene = new CCScene(gameView);
            addBackground();
            addCandyLayer(level);
            //addDebug();
            swipeFromRow = swipeFromCol = 90;   //initializes the variables to 90 even though this isn't a valid location in the grid
            CreateTouchListener();
        }

        private void addDebug()
        {
            debugLabel = new CCLabel("Debug info shows here...", "Arial", 30, CCLabelFormat.SystemFont);
            debugLabel.Color = CCColor3B.Red;
            debugLabel.AnchorPoint = new CCPoint(0, 0);
            debugLabel.Position = new CCPoint(0, 30);
            cLayer.AddChild(debugLabel);
        }

        private void addBackground()
        {
            backgroundLayer = new CCLayer();
            var background = new CCSprite("background");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            backgroundLayer.AddChild(background);
            AddChild(backgroundLayer);
        }

        private void addCandyLayer(int level)
        {
            cLayer = new CandyLayer(level);
            AddChild(cLayer);
        }

        private void CreateTouchListener()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesMoved = HandleTouchesMoved;
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            touchListener.OnTouchesEnded = touchesEnded;
            touchListener.OnTouchesCancelled = touchesCancelled;
            cLayer.AddEventListener(touchListener);
        }

        private void HandleTouchesBegan(List<CCTouch> touches, CCEvent touchEvent)
        {
            CCPoint location = touches[0].Location;

            //debugLabel.Text = "The user touched the screen.";
            int row = 90, col = 90;
            //  Checks to see if the touch was within the grid
            if (cLayer.convertToPoint(location, ref row, ref col))
            {
                candy candy = cLayer.candyAt(row, col);
                if (candy != null)
                {
                    swipeFromRow = candy.getRow();
                    swipeFromCol = candy.getColumn();
                }
            }
            else if ((location.X > 19 && location.X < 81) && (location.Y > 19 && location.Y < 81))
            {
                //  The user touched the back button
                Director.PushScene(new TitleScene(GameView));
            }
        }

        private void HandleTouchesMoved(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (swipeFromCol > 8)
            {
                return;
            }
            //debugLabel.Text = "The user moved from the initial touch point.";
            // we only care about the first touch:
            var locationOnScreen = touches[0].Location;

            int column, row;
            row = column = 90;
            if (cLayer.convertToPoint(locationOnScreen, ref row, ref column))
            {

                int horzDelta = 0, vertDelta = 0;
                if (column < swipeFromCol)
                {          // swipe left
                    horzDelta = -1;
                }
                else if (column > swipeFromCol)
                {   // swipe right
                    horzDelta = 1;
                }
                else if (row < swipeFromRow)
                {         // swipe down
                    vertDelta = -1;
                }
                else if (row > swipeFromRow)
                {         // swipe up
                    vertDelta = 1;
                }

                if (horzDelta != 0 || vertDelta != 0)
                {
                    //debugLabel.Text = "Checking to see if the swap is valid.";
                    //  Turn off the user interaction as the user should be allowed to move any of candies while candies are swapped, removed, and the grid refilled
                    cLayer.disableListeners();
                    cLayer.trySwap(horzDelta, vertDelta, swipeFromRow, swipeFromCol);
                }
            }
        }

        //  Once the touch is finished reset the swipeFromRow and swipeFromCol
        private void touchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            swipeFromRow = 90;
            swipeFromCol = 90;
        }

        //  If a touch was cancelled, call the touchesEnded method to reset swipe variables
        private void touchesCancelled(List<CCTouch> touches, CCEvent touchEvent)
        {
            touchesEnded(touches, touchEvent);
        }
    }
}