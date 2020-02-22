using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;
using System.Linq;

namespace Game.Engine
{
    public class RoundEngine
    {
        private int Round { get; set; } = 1;

        private const int MAX_NUM_MONSTERS = 6;

        private List<DungeonFighterModel> HeroList;

        private List<DungeonFighterModel> MonsterList;

        private List<DungeonFighterModel> PlayerList;

        private DungeonFighterModel CurrentPlayer;

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public RoundEngine()
        {
            GetNewMonsters();
        }

        public RoundEngine(List<DungeonFighterModel> party)
        {
            HeroList = party;
            GetNewMonsters();
        }


        /// <summary>
        /// Create monsters for a new round, with levels scaled to the round number
        /// </summary>
        /// <returns>List of new monsters</returns>
        private void GetNewMonsters()
        {

            MonsterList = new List<DungeonFighterModel>();
            for (int i = 0; i < MAX_NUM_MONSTERS; i++)
            {
                // unimplemented
            }

        }


        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public void OrderTurns()
        {
            // Start from a clean list of players
            PlayerList.Clear();

            // Remember the Insert order, used for Sorting
            var order = 0;

            foreach (var hero in PlayerList)
            {
                if (hero.Alive)
                {
                    hero.ListOrder = order;
                    PlayerList.Add(hero);
                    order++;
                }
            }

            foreach (var mob in MonsterList)
            {
                if (mob.Alive)
                {
                    mob.ListOrder = order;
                    PlayerList.Add(mob);
                    order++;
                }
            }

            PlayerList = PlayerList.OrderByDescending(a => a.SpeedAttribute)
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperiencePoints)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();

        }
        public bool StartFight()
        {
            //var nextPlayer = 


            return true;
        }

        /// <summary>
        /// Get the Information about the Player
        /// </summary>
        /// <returns></returns>
        public DungeonFighterModel GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // If List is empty, return null
            if (PlayerList.Count == 0)
            {
                return null;
            }

            // No current player, so set the first one
            if (CurrentPlayer == null)
            {
                return PlayerList.FirstOrDefault();
            }

            // Find current player in the list
            var index = PlayerList.FindIndex(m => m.Id.Equals(CurrentPlayer.Id));

            // If at the end of the list, return the first element
            if (index == PlayerList.Count() - 1)
            {
                return PlayerList.FirstOrDefault();
            }

            // Return the next element
            return PlayerList[index + 1];

        }
    }
}
