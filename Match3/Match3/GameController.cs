using System.Collections.Generic;

using CocosSharp;
using CandyCrush.Scenes;

namespace CandyCrush
{
    public static class GameController
    {
        public static CCGameView GameView
        {
            get;
            private set;
        }

        public static void Initialize (CCGameView gameView)
        {
            GameView = gameView;

            var contentSearchPaths = new List<string>() { "Fonts", "Sounds", "Levels" };
            contentSearchPaths.Add("Images");
            //contentSearchPaths.Add("Levels");
            GameView.ContentManager.SearchPaths = contentSearchPaths;

            // Set world dimensions
            int width = 640;
            int height = 1136;
            gameView.DesignResolution = new CCSizeI(width, height);

            var scene = new TitleScene(GameView);
            GameView.Director.RunWithScene(scene);
        }

        public static void GoToScene(CCScene scene)
        {
            GameView.Director.ReplaceScene(scene);
        }
    }
}