using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace Match3.Scenes
{
    class DebugLayer : CCLayer
    {
        public CCLabel debugLabel;
        public DebugLayer()
        {
            debugLabel = new CCLabel("", "Arial", 30, CCLabelFormat.SystemFont);
            debugLabel.Color = CCColor3B.Red;
            debugLabel.AnchorPoint = new CCPoint(0, 0);
            debugLabel.Position = new CCPoint(0, 30);
            AddChild(debugLabel);
        }
    }
}
