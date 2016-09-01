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
        CCSprite background;
        Button button;
        public CCLabel label;
       

        public GameLayer() : base(CCColor4B.Black)
        {
            backgroundLayer = new CCLayer();
            background = new CCSprite("background");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            background.Scale = 2.5f;
            backgroundLayer.AddChild(background);
            AddChild(backgroundLayer);

            labelLayer = new CCLayer();
            label = new CCLabel("Match3 is running.", "Fonts/MarkerFelt", 22, CCLabelFormat.SpriteFont);
            label.Color = CCColor3B.White;
            label.Scale = 2.0f;
            label.Position = new CCPoint(10, 1000);
            label.AnchorPoint = CCPoint.AnchorUpperLeft;
            labelLayer.AddChild(label);
            AddChild(labelLayer);

            buttonLayer = new CCLayer();
            for (int i = 0; i < 5; i++)
            {
                button = new Button(i);
                //Because we know there are 5 levels and the initial width is 768 pixels
                //TODO: Make dynamic based on number of levels in levels directory
                //var spaceBetween = ((768 - (5 * button.ScaledContentSize.Width)) / 6);
                //label.Text = spaceBetween.ToString() + " - " + i.ToString();
                //label.Text = button.ContentSize.Width.ToString() + " - " + button.ScaledContentSize.Width.ToString();
                //var position = spaceBetween + (i * (spaceBetween + button.ScaledContentSize.Width));
                //button.Position = new CCPoint(position, 800);
                buttonLayer.AddChild(button);
            }
            AddChild(buttonLayer);

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


