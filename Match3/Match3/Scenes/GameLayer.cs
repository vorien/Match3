using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Match3.Scenes;
using Match3.Entities;

namespace Match3
{
    public class GameLayer : CCLayerColor
    {
        CCLayer backgroundLayer;
        CCLayer labelLayer;
        CCLayer buttonLayer;
        LevelButton button;
        public CCLabel label;
        int levelcount = 6;

        public GameLayer() : base(CCColor4B.Black)
        {
            backgroundLayer = new BackgroundLayer();
            AddChild(backgroundLayer,0);

            labelLayer = new CCLayer();
            label = new CCLabel("Match3 is running.", "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            label.Color = CCColor3B.White;
            label.Scale = 2.0f;
            label.Position = new CCPoint(10, 1000);
            label.AnchorPoint = CCPoint.AnchorUpperLeft;
            labelLayer.AddChild(label);
            AddChild(labelLayer,1);

            buttonLayer = new CCLayer();
            for (int i = 0; i < levelcount; i++)
            {
                button = new LevelButton(i, levelcount);
                buttonLayer.AddChild(button);
            }
            AddChild(buttonLayer,2);

            CreateTouchListener();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;

            //// Register for touch events
            //var touchListener = new CCEventListenerTouchAllAtOnce();
            //touchListener.OnTouchesEnded = OnTouchesEnded;
            //AddEventListener(touchListener, this);
        }

        private void CreateTouchListener()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            AddEventListener(touchListener);
        }

        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            //int level = 0;
            ////  Determine if the user touched one of the buttons
            //if (buttonPressed(arg1[0].Location, ref level))
            //{
            //    Director.PushScene(new GameScene(GameView, 1));
            //}
        }
    }
}


