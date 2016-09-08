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
        CCLabel label;

        public LevelButton(int id, int levelcount)
        {
            levelID = id;
            levelct = levelcount;
            sprite = new CCSprite("button");
            sprite.AnchorPoint = CCPoint.AnchorMiddleLeft;
            float space = ScreenInfo.preferredWidth / ((levelcount * 3) + 1);
            sprite.ContentSize = new CCSize(space * 2, space * 2);
            //sprite.Scale = 3.0f;
            label = new CCLabel((id + 1).ToString(), "Arial", 30, CCLabelFormat.SystemFont);
            label.Color = CCColor3B.Black;
            label.PositionX = sprite.ContentSize.Width / 2.0f;
            label.PositionY = sprite.ContentSize.Height / 2.0f;
            sprite.AddChild(label, 1);
            float positionX = space + (id * (space * 3));
            float positionY = ScreenInfo.preferredHeight * .5f;
            sprite.Position = new CCPoint(positionX, positionY);
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
                Director.ReplaceScene(new GameScene(GameView, levelID));
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

