using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;

namespace Game.Engine
{
    /// <summary>
    /// Main battle engine 
    /// </summary>
    public class BattleEngine 
    {
        /// <summary>
        /// List of characters in the party
        /// </summary>
        private List<DungeonFighterModel> Party;
        
        /// <summary>
        /// List of monsters currently fighting
        /// </summary>
        private List<DungeonFighterModel> Mobs;

        public BattleEngine()
        {

        }

        public BattleEngine(List<CharacterModel> party)
        {
            foreach (CharacterModel character in party)
            {
                Party.Add(new DungeonFighterModel(character));
            }
        }


    }
}
