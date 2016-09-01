using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace Match3.Scenes
{
    class TestLayer : CCLayerColor
    {
        CCLayer backgroundLayer, labelLayer;
        CCLabel label;
        public TestLayer(int level) : base(CCColor4B.Black)
        {
            labelLayer = new CCLayer();
            label = new CCLabel("TestLayer - " + level.ToString(), "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            label.Color = CCColor3B.White;
            label.Scale = 2.0f;
            label.Position = new CCPoint(10, 1000);
            label.AnchorPoint = CCPoint.AnchorUpperLeft;
            labelLayer.AddChild(label);
            AddChild(labelLayer);

            backgroundLayer = new CCLayer();
            var background = new CCSprite("background");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            backgroundLayer.AddChild(background);
            AddChild(backgroundLayer);
        }
    }
}
