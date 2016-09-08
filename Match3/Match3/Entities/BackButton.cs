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
    public class BackButton : CCNode
    {

        CCSprite sprite;
        public BackButton()
        {
            sprite = new CCSprite("button");
            //sprite.ContentSize = new CCSize(sprite.ContentSize.Width * 1.5, sprite.ContentSize.Height * 1.5);
            sprite.AnchorPoint = CCPoint.AnchorMiddleBottom;
            sprite.Scale = 2.5f;
            sprite.Position = new CCPoint(768 / 2, 0);
            var label = new CCLabel("BACK", "Arial", 20, CCLabelFormat.SystemFont);
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

