//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xamarin.Forms;


using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using Match3.Entities;

namespace Match3.Levels
{
    class LevelLoader
    {

        //  Loads the given level
        public Level LoadLevel(int levelID)
        {
            Level level = new Level();

            var assembly = typeof(LevelLoader).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("Match3.Level_" + levelID + ".json");



            using (var reader = new System.IO.StreamReader(stream))
            {

                string json = reader.ReadToEnd();
                level = JsonConvert.DeserializeObject<Level>(json);

            }
            return level;
        }
    }
}

