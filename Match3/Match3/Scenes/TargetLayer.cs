using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using Match3;

namespace Match3.Scenes
{
    public class TargetLayer : CCLayer
    {
        public CCLabel targetLabel;
        public TargetLayer()
        {
            //  Label to display the targetScore the user has to meet to beat the level
            targetLabel = new CCLabel("/" + ActiveLevel.level.targetScore.ToString(), "Arial", ScreenInfo.fontMedium, CCLabelFormat.SystemFont);
            targetLabel.Color = CCColor3B.Green;
            targetLabel.AnchorPoint = new CCPoint(0, 0);
            targetLabel.Position = new CCPoint(500, 980);
            AddChild(targetLabel);
        }

    }
}

