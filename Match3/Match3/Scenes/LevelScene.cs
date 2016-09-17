using System.Collections.Generic;

using CocosSharp;
using Match3.Entities;

namespace Match3.Scenes
{
    public class LevelScene : CCScene
    {
        //private CCLayer backgroundLayer;
        public LevelLayer levelLayer;
        private DebugLayer debugLayer;
        private LevelLoader loader;

        public LevelScene(CCGameView gameView, int level) : base(gameView)
        {
            //  Load the level
            loader = new LevelLoader();
            loader.LoadLevel(level);

            //backgroundLayer = new BackgroundLayer();
            //AddChild(backgroundLayer, 0);

            levelLayer = new LevelLayer();
            AddChild(levelLayer, 1);

            debugLayer = new DebugLayer();
            AddChild(debugLayer, 2);
        }
    }
}
