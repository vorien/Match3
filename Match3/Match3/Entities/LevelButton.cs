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
    public class LevelButton : CCNode
    {
        CCSprite sprite;
        int levelID;
        int levelct;

        public LevelButton(int id, int levelcount)
        {
            levelID = id;
            levelct = levelcount;
            sprite = new CCSprite("button");
            sprite.AnchorPoint = CCPoint.AnchorMiddleLeft;
            sprite.Scale = 3.0f;
            var label = new CCLabel((id + 1).ToString(), "Arial", 30, CCLabelFormat.SystemFont);
            label.Color = CCColor3B.Black;
            label.PositionX = sprite.ContentSize.Width / 2.0f;
            label.PositionY = sprite.ContentSize.Height / 2.0f;
            sprite.AddChild(label);
            //Because we know there are 5 levels and the initial width is 768 pixels
            //TODO: Make dynamic based on number of levels in levels directory
            var spaceBetween = ((768 - (levelcount * sprite.ScaledContentSize.Width)) / (levelcount + 1));
            var position = spaceBetween + (id * (spaceBetween + sprite.ScaledContentSize.Width));
            sprite.Position = new CCPoint(position, 800);
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
                Debug.WriteLine("Button pressed: " + levelID.ToString());
                Director.ReplaceScene(new LevelScene(GameView, levelID));
                return true;
            }
            else
            {
                Debug.WriteLine("Button NOT pressed: " + levelID);
                return false;
            }
        }

    }

}

