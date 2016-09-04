﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Match3.Entities
{
    class LevelLoader
    {

        private List<string> levels;

        //  Loads the given level
        public LevelLoader()
        {
            levels = new List<string>();
            levels.Add("{  'tiles' : [ [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ] ],  'targetScore' : 1000,  'moves' : 1}");
            levels.Add("{  'tiles' : [ [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ] ],  'targetScore' : 1000,  'moves' : 20}");
            levels.Add("{  'tiles' : [ [0, 1, 1, 0, 0, 0, 1, 1, 0 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [0, 0, 1, 1, 1, 1, 1, 0, 0 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [0, 1, 1, 0, 0, 0, 1, 1, 0 ] ],  'targetScore' : 1200,  'moves' : 20}");
            levels.Add("{  'tiles' : [ [1, 1, 0, 0, 1, 0, 0, 1, 1 ], [1, 0, 0, 1, 1, 1, 0, 0, 1 ], [0, 0, 1, 1, 1, 1, 1, 0, 0 ], [0, 1, 1, 1, 0, 1, 1, 1, 0 ], [1, 1, 1, 0, 0, 0, 1, 1, 1 ], [0, 1, 1, 1, 0, 1, 1, 1, 0 ], [0, 0, 1, 1, 1, 1, 1, 0, 0 ], [1, 0, 0, 1, 1, 1, 0, 0, 1 ], [1, 1, 0, 0, 1, 0, 0, 1, 1 ] ],  'targetScore' : 3000,  'moves' : 30}");
            levels.Add("{  'tiles' : [ [0, 0, 1, 0, 0, 0, 1, 0, 0 ], [0, 1, 1, 1, 1, 1, 1, 1, 0 ], [1, 1, 0, 1, 1, 1, 0, 1, 1 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [0, 1, 1, 1, 1, 1, 1, 1, 0 ], [1, 1, 1, 1, 1, 1, 1, 1, 1 ], [1, 0, 1, 1, 1, 1, 1, 0, 1 ], [1, 0, 1, 1, 1, 1, 1, 0, 1 ], [0, 0, 1, 0, 0, 0, 1, 0, 0 ] ],  'targetScore' : 1000,  'moves' : 15}");
            levels.Add("{  'tiles' : [ [0, 0, 0, 0, 0, 0, 0, 0, 0 ], [0, 0, 0, 0, 0, 0, 0, 0, 0 ], [0, 0, 0, 0, 0, 0, 0, 0, 0 ], [0, 0, 0, 1, 1, 1, 0, 0, 0 ], [0, 0, 0, 1, 1, 1, 0, 0, 0 ], [0, 0, 0, 1, 1, 1, 0, 0, 0 ], [0, 0, 0, 0, 0, 0, 0, 0, 0 ], [0, 0, 0, 0, 0, 0, 0, 0, 0 ], [0, 0, 0, 0, 0, 0, 0, 0, 0 ] ],  'targetScore' : 600,  'moves' : 15}");
        }

        public Level LoadLevel(int levelID) { 
            Level level = new Level();
            level = JsonConvert.DeserializeObject<Level>(levels[levelID]);

            return level;
        }
    }
}

