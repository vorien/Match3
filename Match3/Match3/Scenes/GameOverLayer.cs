using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using Match3.Entities;
using System.Diagnostics;

namespace Match3.Scenes
{
    class GameOverLayer : CCLayer
    {
        private CCLabel gameOverLabel;
        //private CCDrawNode dimmer;
        private string labeltext;
        private HomeButton homeButton;
        private ReplayButton replayButton;
        public GameOverLayer(int id, int score, int needed, bool win)
        {
            //dimmer = new CCDrawNode();
            //AddChild(dimmer, 0);

            //Debug.WriteLine("GameOverLayer");
            //Debug.WriteLine(this.ContentSize);
            //Debug.WriteLine(this.ScaledContentSize);
            ////Debug.WriteLine(this.VisibleBoundsWorldspace);
            //Debug.WriteLine(VisibleBoundsWorldspace);

            switch (win)
            {
                case false: // Lost
                    labeltext = "GAME OVER";
                    break;
                case true: // Won
                    labeltext = "WINNER!!";
                    break;
                default: // Technically an error
                    labeltext = "OOPS!";
                    break;
            }
            gameOverLabel = new CCLabel(labeltext, "Arial", 150, CCLabelFormat.SystemFont);
            gameOverLabel.Color = CCColor3B.White;
            gameOverLabel.HorizontalAlignment = CCTextAlignment.Center;
            gameOverLabel.VerticalAlignment = CCVerticalTextAlignment.Center;
            gameOverLabel.AnchorPoint = CCPoint.AnchorMiddleBottom;
            gameOverLabel.Dimensions = ContentSize;
            AddChild(gameOverLabel, 1);

            homeButton = new HomeButton();
            AddChild(homeButton);

            replayButton = new ReplayButton(id);
            AddChild(replayButton);

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            var bounds = VisibleBoundsWorldspace;

            //dimmer.Position = bounds.LowerLeft;
            //dimmer.DrawRect(new CCRect(bounds.MinX, bounds.MinY, bounds.MaxX, bounds.MaxY), new CCColor4B(0, 255, 0, 25));

            gameOverLabel.Position = bounds.Center;

            homeButton.Position = new CCPoint(200, 200);
            replayButton.Position = new CCPoint(568, 200);

        }
    }
}
