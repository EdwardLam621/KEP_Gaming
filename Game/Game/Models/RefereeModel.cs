using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    /// <summary>
    /// Referee holds the ScoreModel, relays battle messages, 
    /// keeps track of the ItemModel pool, and follows the
    /// players *everywhere*
    /// </summary>
    public class RefereeModel
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

        // Whether auto battle is enabled
        public bool AutoBattleEnabled { get; set; } = false;

        // Whether resurrections are enabled
        public bool ResurrectionsEnabled { get; set; } = false;

        // Keep track of whether a character has been resurrected
        public Dictionary<DungeonFighterModel, int> UsedResurrection = new Dictionary<DungeonFighterModel, int>();

        // Keep track of which characters died during battle
        public List<DungeonFighterModel> DeadCharacters = new List<DungeonFighterModel>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public RefereeModel()
        {

        }

        /// <summary>
        /// Constructor taking a list of characters
        /// </summary>
        /// <param name="characters"></param>
        public RefereeModel(List<DungeonFighterModel> characters)
        {
            Characters = characters;
        }

        /// <summary>
        /// Enable or disable resurrections
        /// </summary>
        /// <param name="toggle"></param>
        /// <returns></returns>
        public bool SetResurrection(bool toggle)
        {
            if (toggle)
            {
                ResurrectionsEnabled = true;
                foreach (DungeonFighterModel character in Characters)
                {
                    UsedResurrection.Add(character, 0);
                }

                return true;
            } else
            {
                UsedResurrection.Clear();
                ResurrectionsEnabled = false;
                return true;
            }
        }

        /// <summary>
        /// Convert from characters to fighters. Should only be set once at start of battle...
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        public bool SetParty(List<CharacterModel> party)
        {
            Characters = new List<DungeonFighterModel>();

            foreach (CharacterModel character in party)
            {
                Characters.Add(new DungeonFighterModel(character));
            }

            return true;
        }


        /// <summary>
        /// Reset RefereeModel
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        public void Reset()
        {
            this.Characters.Clear();
            this.Monsters.Clear();
            this.ItemPool.Clear();
            this.BattleScore = new ScoreModel();
            this.UsedResurrection.Clear();
            this.DeadCharacters.Clear();
        }
    }
}
