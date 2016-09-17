using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Entities
{
    class CandyTypes : List<Tuple<string, string>>
    {
        private static List<Tuple<string, string>> candyTypes = new List<Tuple<string, string>>();

        public CandyTypes()
        {
            this.Add(Tuple.Create("PeppermintSwirl", "PeppermintSwirl"));
            this.Add(Tuple.Create("BlueJollyRancher", "BlueJollyRancher"));
            this.Add(Tuple.Create("CandyCorn", "CandyCorn"));
            this.Add(Tuple.Create("PurpleNerds", "PurpleNerds"));
            this.Add(Tuple.Create("GreenElliptical", "GreenElliptical"));
        }
    }
}
