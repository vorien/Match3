using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using Match3.Entities;
using System.Diagnostics;

namespace Match3.Scenes
{
    class GameOverScene : CCScene
    {
        public int id { get; set; }
        public int score { get; set; }
        public int needed { get; set; }
        public bool win { get; set; }

        private CCLayer gameOverLayer;

        public GameOverScene(CCGameView gameView) : base(gameView)
        {
            gameOverLayer = new GameOverLayer(id, score, needed, win);
            AddChild(gameOverLayer);

        }

    }
}
