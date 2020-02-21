using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class ArenaModel
    {
        private List<DungeonCharacterModel> Party;
        private List<DungeonMonsterModel> Mobs;
        private int CurrentRound = 1;

        public ArenaModel()
        {

        }

        public ArenaModel(List<DungeonCharacterModel> party, List<DungeonMonsterModel> mobs)
        {
            Party = party;
            Mobs = mobs;
        }



    }
}
