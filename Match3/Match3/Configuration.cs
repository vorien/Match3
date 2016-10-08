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
        public const int gridRows = 9;
        public const int gridColumns = 9;
        public const int gridWidthSpacing = 10;
        public const int gridVerticalOffset = 120;
        public static List<Tuple<string, string>> candyTypes = new CandyTypes();
        public static Dictionary<int, Chain> chains = new Dictionary<int, Chain>();
    }

    class CandyTypes : List<Tuple<string, string>>
    {

        public CandyTypes()
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
