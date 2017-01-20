using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Match3.Entities;

namespace Match3
{
    public static class Configuration
    {
        //public const int gridRows = 9;
        //public const int gridColumns = 9;
        public const int gridRows = 4;
        public const int gridColumns = 3;
        public const int gridWidthSpacing = 10;
        public const int gridVerticalOffset = 120;
        public const float tileSize = (ScreenInfo.preferredWidth - (Configuration.gridWidthSpacing * 2)) / Configuration.gridColumns;
        public static float worldTileSize;
        public static List<Tuple<string, string>> materialTypes = new MaterialTypes();

    }

    class MaterialTypes : List<Tuple<string, string>>
    {

        public MaterialTypes()
        {
            // Parametres are Filename, Display Name
            this.Add(Tuple.Create("PeppermintSwirl", "Peppermint Swirl"));
            this.Add(Tuple.Create("BlueJollyRancher", "Blue Jolly Rancher"));
            this.Add(Tuple.Create("CandyCorn", "Candy Corn"));
            this.Add(Tuple.Create("PurpleNerds", "Purple Nerds"));
            this.Add(Tuple.Create("GreenElliptical", "Green Elliptical"));
        }
    }

}
