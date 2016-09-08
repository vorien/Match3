using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace Match3.Scenes
{
    class StartScene : CCScene
    {
        public StartScene(CCGameView gameView) : base(gameView)
        {
            CCLayer startLayer = new StartLayer();
            AddChild(startLayer);
        }
    }
}
