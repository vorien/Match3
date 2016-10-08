using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using Match3;

namespace Match3.Scenes
{
    public class ScoreLayer : CCLayer
    {
        public CCLabel scoreLabel;
        public int score;
        public ScoreLayer()
        {
            //  Label to display the user's current score
            scoreLabel = new CCLabel("1000", "Arial", ScreenInfo.fontLarge, CCLabelFormat.SystemFont);
            scoreLabel.Color = CCColor3B.Green;
            scoreLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
            //scoreLabel.Position = new CCPoint(0, ContentSize.Height);
            AddChild(scoreLabel);
        }
        public void updateScore(int delta)
        {
            score += delta;
            updateScoreLabel();
        }

        public void updateScoreLabel()
        {
            scoreLabel.Text = score.ToString();
        }

    }
}

