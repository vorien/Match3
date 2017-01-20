using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using Match3;

namespace Match3.Scenes
{
    public class MovesLeftLayer : CCLayer
    {
        public int movesLeft;
        public CCLabel movesLeftLabel;
        public MovesLeftLayer()
        {
            //  Adds a label that will be used to display the amount of moves the user has left
            AnchorPoint = CCPoint.AnchorUpperRight;
            movesLeft = ActiveLevel.level.moves;
            movesLeftLabel = new CCLabel(movesLeft.ToString(), "Arial", ScreenInfo.fontLarge, CCLabelFormat.SystemFont);
            movesLeftLabel.Color = CCColor3B.Blue;
            movesLeftLabel.AnchorPoint = CCPoint.AnchorUpperRight;
            AddChild(movesLeftLabel);
        }

        public void decrementMovesLeft()
        {
            movesLeft--;
            updateMovesLeftLabel();
        }

        public void updateMovesLeftLabel()
        {
            movesLeftLabel.Text = movesLeft.ToString();
        }
    }
}
