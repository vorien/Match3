using System;
using System.Collections.Generic;

using CocosSharp;
using CocosDenshion;
using Match3.Scenes;

namespace Match3
{
    public static class GameDelegate
    {
        public static CCGameView gameView;
        static CCDirector director;
        //public static CCScene gameScene;
        public static CCSizeI viewSize;

        public static void LoadGame(object sender, EventArgs e)
        {
            gameView = sender as CCGameView;
            director = new CCDirector();

            if (gameView != null)
            {
                var contentSearchPaths = new List<string>() { "Fonts", "Sounds", "Images" };
                viewSize = gameView.ViewSize;

                ScreenInfo.Width = viewSize.Width;
                ScreenInfo.Height = viewSize.Height;
                ScreenInfo.setFontSizes();

                int width = (int)ScreenInfo.preferredWidth;
                int height = (int)ScreenInfo.preferredHeight;

                // Set world dimensions
                gameView.DesignResolution = new CCSizeI(width, height);
                gameView.ResolutionPolicy = CCViewResolutionPolicy.ShowAll;

                // Determine whether to use the high or low def versions of our images
                // Make sure the default texel to content size ratio is set correctly
                // Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
                //if (width < viewSize.Width)
                //{
                //    contentSearchPaths.Add("Images/Hd");
                //    CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
                //}
                //else
                //{
                //    contentSearchPaths.Add("Images/Ld");
                //    CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
                //}

                gameView.ContentManager.SearchPaths = contentSearchPaths;

                //gameScene = new GameScene(gameView);
                //gameScene.AddLayer(new GameLayer());
                gameView.RunWithScene(new StartScene(gameView));
            }
        }

    }
}