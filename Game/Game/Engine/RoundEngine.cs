using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;
using System.Linq;

namespace Game.Engine
{
    public class RoundEngine
    {
        public int Round { get; set; } = 1;

        public const int MAX_NUM_MONSTERS = 6;

        public List<DungeonFighterModel> HeroList;

        public List<DungeonFighterModel> MonsterList;

        public List<DungeonFighterModel> PlayerList;

        public DungeonFighterModel CurrentPlayer;

        public Referee Referee;
        
        /// <summary>
        /// Default empty constructor
        /// </summary>
        public RoundEngine()
        {

        }

        public RoundEngine(Referee referee)
        {
            Referee = referee;
            // Referee.Score.RoundCount++  <-- to be implemented
            StartRound();
        }

        public bool StartRound()
        {
            GetNewMonsters();
            MakeList();
            OrderFight();

            //fight loop

            var nextPlayer = GetNextPlayerInList();

            var turn = new TurnEngine(nextPlayer, Referee);
            turn.TakeTurn();
            return true;
        }

        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
        /// Clear the MonsterModel List
        /// </summary>
        /// <returns></returns>
        public bool EndRound()
        {
            // Have each character pickup items...
            foreach (var character in Referee.Characters)
            {
                //PickupItemsFromPool(character);
            }

            // Reset Monster and Item Lists
            //ClearLists();

            return true;
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
        public void MakeList()
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

        }

        public void OrderFight()
        {
            PlayerList = PlayerList.OrderByDescending(a => a.SpeedAttribute)
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperiencePoints)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();
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
