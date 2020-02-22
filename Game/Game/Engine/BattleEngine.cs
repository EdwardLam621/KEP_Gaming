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

        // Holds the official ScoreModel
        public ScoreModel BattleScore = new ScoreModel();

        // Holds the Battle Messages as they happen
        public BattleMessagesModel BattleMessagesModel = new BattleMessagesModel();

        // The Pool of items collected during the round as turns happen
        public List<ItemModel> ItemPool = new List<ItemModel>();


        /// <summary>
        /// List of characters in the party
        /// </summary>
        private List<DungeonFighterModel> Party;
        
        /// <summary>
        /// List of monsters currently fighting
        /// </summary>
        //private List<DungeonFighterModel> Mobs;

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

        public void startBattle()
        {
            RoundEngine round = new RoundEngine();
        }
    }
}
