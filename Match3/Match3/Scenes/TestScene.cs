using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;

namespace Match3.Scenes
{
    public class TestScene : CCScene
    {
        private TestLayer testLayer;
        //private CCLabel debugLabel;
        int levelid;

        public TestScene(CCGameView gameView, int level) : base(gameView)
        {
            levelid = level;
            testLayer = new TestLayer(level);
            AddChild(testLayer);
        }

    }
}
