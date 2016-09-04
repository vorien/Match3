using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace Match3.Scenes
{
    class BackgroundLayer : CCLayer
    {
        private CCLayer backgroundLayer;
        private CCSprite background;
        public BackgroundLayer()
        {
            backgroundLayer = new CCLayer();
            background = new CCSprite("background");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            background.Scale = 2.5f;
            backgroundLayer.AddChild(background);
            AddChild(backgroundLayer);
        }
    }
}
