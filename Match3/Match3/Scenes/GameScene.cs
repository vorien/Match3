using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace Match3.Scenes
{
    class GameScene : CCScene
    {
        public GameScene(CCGameView gameView) : base(gameView)
        {
            CCLayer gameLayer = new GameLayer();
            AddChild(gameLayer);
        }
    }
}
