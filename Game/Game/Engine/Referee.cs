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
        // List of characters in the fight
        public List<DungeonFighterModel> Characters;

        // List of monsters in the current round
        public List<DungeonFighterModel> Monsters;

        // Item pool
        public List<ItemModel> ItemPool = new List<ItemModel>();

        // Hold the official ScoreModel
        public ScoreModel BattleScore = new ScoreModel();

        // Hold the Battle Messages as they happen
        public BattleMessagesModel BattleMessages = new BattleMessagesModel();

        /// <summary>
        /// Default constructor
        /// </summary>
        public Referee()
        {

        }

        /// <summary>
        /// Constructor taking a list of characters
        /// </summary>
        /// <param name="characters"></param>
        public Referee(List<DungeonFighterModel> characters)
        {
            Characters = characters;
        }
    }
}
