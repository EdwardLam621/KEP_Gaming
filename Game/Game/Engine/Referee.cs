using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;

namespace Game.Engine
{
    /// <summary>
    /// Referee holds the ScoreModel, relays battle messages, 
    /// keeps track of the ItemModel pool, and follows the
    /// players *everywhere*
    /// </summary>
    public class Referee
    {

        public List<DungeonFighterModel> Characters;
        public List<ItemModel> ItemPool = new List<ItemModel>();

        // Holds the official ScoreModel
        public ScoreModel BattleScore = new ScoreModel();

        // Holds the Battle Messages as they happen
        public BattleMessagesModel BattleMessages = new BattleMessagesModel();


        public Referee()
        {

        }

        public Referee(List<DungeonFighterModel> characters)
        {
            Characters = characters;

        }
    }
}
