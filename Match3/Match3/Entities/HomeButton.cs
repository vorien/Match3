using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using Match3.Scenes;
using System.Diagnostics;
using Match3;

namespace Match3.Entities
{
    public class HomeButton : CCNode
    {

        CCSprite sprite;
        public HomeButton()
        {
            sprite = new CCSprite("button");
            sprite.ContentSize = new CCSize(sprite.ContentSize.Width * 2, sprite.ContentSize.Height * 2);
            sprite.AnchorPoint = CCPoint.AnchorMiddle;
            sprite.Scale = 2.5f;
            //sprite.Position = new CCPoint(50, 0);
            var label = new CCLabel("HOME", "Arial", 20, CCLabelFormat.SystemFont);
            label.Color = CCColor3B.Black;
            label.PositionX = sprite.ContentSize.Width / 2.0f;
            label.PositionY = sprite.ContentSize.Height / 2.0f;
            sprite.AddChild(label);
            AddChild(sprite);
        }
        protected override void AddedToScene()
        {
            base.AddedToScene();

            var touchListener = new CCEventListenerTouchOneByOne();
            touchListener.IsSwallowTouches = true;
            touchListener.OnTouchBegan = OnTouchBegan;
            AddEventListener(touchListener, this);
        }
        bool OnTouchBegan(CCTouch touch, CCEvent touchEvent)
        {
            if (sprite.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
            {
                Debug.WriteLine("Home Button Touched");
                Director.ReplaceScene(new StartScene(GameView));
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}

