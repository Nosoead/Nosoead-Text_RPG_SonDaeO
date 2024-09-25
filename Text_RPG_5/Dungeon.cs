using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_5
{
    internal class Dungeon
    {
        public string DungeonName { get; set; }
        public float RecommendedDEF { get; set; }
        public float ATKValueReward { get; set; }
        public int GoldReward { get; set; }
        public int EXPReward { get; set; }

        public Dungeon(string dungeonName, float recommendedDEF, float aTKValueReward, int goldReward, int eXPReward)
        {
            DungeonName = dungeonName;
            RecommendedDEF = recommendedDEF;
            ATKValueReward = aTKValueReward;
            GoldReward = goldReward;
            EXPReward = eXPReward;
        }
    }
}
