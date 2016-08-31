using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Match3.Scenes;

namespace Match3
{
    public class GameLayer : CCLayerColor
    {
        CCLayer backgroundLayer;
        CCSprite background;

        public GameLayer() : base(CCColor4B.Black)
        {
            backgroundLayer = new CCLayer();
            background = new CCSprite("background");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            background.Scale = 2.5f;
            backgroundLayer.AddChild(background);
            AddChild(backgroundLayer);

            addButtons();

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

        private void addButtons()
        {
            for (int i = 0; i < 5; i++)
            {
                var button = new CCSprite("button.png");
                button.Position = new CCPoint(80 + (i * 120), 810);
                var label = new CCLabel((i + 1).ToString(), "Arial", 30, CCLabelFormat.SystemFont);
                label.Color = CCColor3B.Black;
                label.PositionX = button.ContentSize.Width / 2.0f;
                label.PositionY = button.ContentSize.Height / 2.0f;
                button.AddChild(label);
                AddChild(button);
            }
        }

        private void CreateTouchListener()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = HandleTouchesBegan;
            AddEventListener(touchListener);
        }

        private bool buttonPressed(CCPoint location, ref int level)
        {
            for (int i = 0; i < 5; i++)
            {
                if ((location.Y >= 779 && location.Y < 841) && (location.X >= (49 + (i * 120)) && location.X <= (111 + (i * 120))))
                {
                    level = i;
                    return true;
                }
            }
            return false;
        }

        private void HandleTouchesBegan(List<CCTouch> arg1, CCEvent arg2)
        {
            int level = 0;
            //  Determine if the user touched one of the buttons
            if (buttonPressed(arg1[0].Location, ref level))
            {
                Director.PushScene(new GameScene(GameView, 1));
            }
        }
    }
}


