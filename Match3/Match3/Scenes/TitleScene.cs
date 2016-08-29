using CocosSharp;

using System.Collections.Generic;

namespace Match3.Scenes
{
    public class TitleScene : CCScene
    {
        CCLayer layer;

        public TitleScene(CCGameView gameView) : base(gameView)
        {
            layer = new CCLayer();
            var background = new CCSprite("background");
            background.AnchorPoint = new CCPoint(0, 0);
            background.IsAntialiased = false;
            layer.AddChild(background);
            this.AddChild(layer);

            addButtons();

            CreateTouchListener();

        }

        //  Add labels that will act as buttons for each level
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
                layer.AddChild(button);
            }
        }

        private void CreateTouchListener()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce();
			touchListener.OnTouchesBegan = HandleTouchesBegan;
            layer.AddEventListener(touchListener);
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
                Director.PushScene(new TitleScene(GameView));
            }
        }
    }
}
