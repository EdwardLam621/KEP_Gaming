using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;
using System.Linq;

namespace Game.Engine
{
    public class RoundEngine
    {
        public const int MAX_NUM_MONSTERS = 6;

        public int roundCount = 1;

        public List<DungeonFighterModel> MonsterList;

        public List<DungeonFighterModel> PlayerList;

        public DungeonFighterModel CurrentPlayer;

        public Referee Referee;

        // Current Round State
        public RoundEnum RoundStateEnum = RoundEnum.Unknown;


        /// <summary>
        /// Default empty constructor
        /// </summary>
        public RoundEngine()
        {

        }

        public RoundEngine(Referee referee)
        {
            Referee = referee;
            Referee.BattleScore.RoundCount++;
            
        }

        /// <summary>
        /// Begin starts round 1, and takes care of looping through rounds
        /// </summary>
        /// <returns></returns>
        public bool Begin()
        {
            // Round fight loop
            var fight = StartRound(roundCount);
            while (fight.Equals(RoundEnum.NewRound))
            {
                roundCount++;
                fight = StartRound(roundCount);
            }
            
            if (fight.Equals(RoundEnum.GameOver))
            {
                // display game over screen
                return true;
            }

            return false;
        }

        /// <summary>
        /// Start a new round
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public RoundEnum StartRound(int round)
        {
            
            RoundStateEnum = RoundEnum.NextTurn;
            
            // Populate round with monsters
            GetNewMonsters();

            // Make a list of characters+monsters for turn order
            MakeList();

            // Order fight as per game rules
            OrderFight();

            // Turn fight loop, go until monsters or characters are dead
            var nextPlayer = GetNextPlayerInList();
            while (RoundStateEnum.Equals(RoundEnum.NextTurn))
            {
                RoundStateEnum = RoundNextTurn();
            }

            if (RoundStateEnum.Equals(RoundEnum.GameOver))
            {
                return RoundEnum.GameOver;
            }

            if (RoundStateEnum.Equals(RoundEnum.NewRound))
            {
                return RoundEnum.NewRound;
            }

            return RoundEnum.Unknown;

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
        /// Manage Next Turn
        /// 
        /// Decides Who's Turn
        /// Remembers Current Player
        /// 
        /// Starts the Turn
        /// 
        /// </summary>
        /// <returns></returns>
        public RoundEnum RoundNextTurn()
        {
            // No characters, game is over...
            if (Referee.Characters.Count < 1)
            {
                // Game Over
                RoundStateEnum = RoundEnum.GameOver;
                return RoundStateEnum;
            }

            // Check if round is over
            if (MonsterList.Count < 1)
            {
                // If over, New Round
                RoundStateEnum = RoundEnum.NewRound;
                return RoundEnum.NewRound;
            }

            // Decide Who gets next turn
            // Remember who just went...
            var playerCurrent = GetNextPlayerInList();

            // Do the turn....
            var turn = new TurnEngine(playerCurrent, Referee);
            turn.TakeTurn();

            RoundStateEnum = RoundEnum.NextTurn;

            return RoundStateEnum;
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
                // need to scale monsters to appropriate level based on round
            }
            
            // FOR DEBUG
            MonsterList.Add(new DungeonFighterModel(new MonsterModel
            {
                Name = "The Coronavirus",
                MaxHealth = 20,
                Level = 1,
                Description = "Human disaster",
                ImageURI = "https://pngimg.com/uploads/coronavirus/coronavirus_PNG33.png",
                DefenseAttribute = 1,
                OffenseAttribute = 10,
                SpeedAttribute = 15,
                Skill = CreatureSkillEnum.Boss,
                DropItems = new List<ItemModel>(), 
            }));
            

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
