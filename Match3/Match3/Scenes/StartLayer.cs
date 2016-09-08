using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Match3.Scenes;
using Match3.Entities;

namespace Match3
{
    public class StartLayer : CCLayerColor
    {
        //CCLayer backgroundLayer;
        CCLayer labelLayer;
        CCLayer buttonLayer;
        LevelButton button;
        public CCLabel label;
        int levelcount = 6;

        public StartLayer() : base(CCColor4B.Black)
        {
            //backgroundLayer = new BackgroundLayer();
            //AddChild(backgroundLayer,0);

            //var drawNode = new CCDrawNode();
            //this.AddChild(drawNode, 4);

            //drawNode.DrawCircle(
            //    center: new CCPoint(ScreenInfo.Width / 2, ScreenInfo.Height / 2),
            //    radius: ScreenInfo.Width / 2,
            //    color: CCColor4B.White
            //    );


            labelLayer = new CCLayer();
            label = new CCLabel("Select a Level to Play... \n( Level 1 is a one-move test level)", "Arial", 36, CCLabelFormat.SystemFont);
            //label.SystemFontSize = 70;
            //label.Scale = ScreenInfo.Scale;
            //label.Text = ScreenInfo.Width.ToString() + " - " + ScreenInfo.Scale.ToString();
            label.Color = CCColor3B.White;
            label.HorizontalAlignment = CCTextAlignment.Center;
            //label.Scale = 2.0f;
            //label.Position = new CCPoint(Configuration.Width / 2, Configuration.Height - label.ContentSize.Height - 10);
            label.Position = new CCPoint(ScreenInfo.preferredWidth / 2, ScreenInfo.preferredHeight - label.ContentSize.Height - 10);
            label.AnchorPoint = CCPoint.AnchorMiddleTop;
            labelLayer.AddChild(label);
            AddChild(labelLayer,1);

            buttonLayer = new CCLayer();
            for (int i = 0; i < levelcount; i++)
            {
                button = new LevelButton(i, levelcount);
                buttonLayer.AddChild(button);
            }
            AddChild(buttonLayer, 2);

            //CreateTouchListener();
        }

        protected override void AddedToScene()
        {
            //base.AddedToScene();
            //var bounds = VisibleBoundsWorldspace;

            //label.Text = Director.RunningScene.ContentSize.Width.ToString();
            //buttonLayer.ContentSize = new CCSize(ScreenInfo.Width, ScreenInfo.Height);
            //// Register for touch events
            //var touchListener = new CCEventListenerTouchAllAtOnce();
            //touchListener.OnTouchesEnded = OnTouchesEnded;
            //AddEventListener(touchListener, this);
        }

        //private void CreateTouchListener()
        //{
        //    var touchListener = new CCEventListenerTouchAllAtOnce();
        //    touchListener.OnTouchesBegan = HandleTouchesBegan;
        //    AddEventListener(touchListener);
        //}

        //private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        //{
        //    //int level = 0;
        //    ////  Determine if the user touched one of the buttons
        //    //if (buttonPressed(arg1[0].Location, ref level))
        //    //{
        //    //    Director.PushScene(new GameScene(GameView, 1));
        //    //}
        //}
    }
}


