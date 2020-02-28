using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;
using System.Linq;
using System.Diagnostics;

namespace Game.Engine
{
    /// <summary>
    /// Manages the rounds
    /// </summary>
    public class RoundEngine
    {
        // Maximum number of monsters to fight
        public const int MAX_NUM_MONSTERS = 1;

        // Which round we are on
        public int RoundCount = 1;

        // List of monsters in this round
        public List<DungeonFighterModel> MonsterList;

        // List of total players (monsters and characters) in this round
        public List<DungeonFighterModel> FighterList;

        // Player whose turn it is currently
        public DungeonFighterModel CurrentPlayer;

        // The Referee object that handles scores/items/skills
        public RefereeModel Referee;


        /// <summary>
        /// Default empty constructor
        /// </summary>
        public RoundEngine()
        {

        }

        /// <summary>
        /// Constructor that takes a Referee object
        /// </summary>
        /// <param name="referee"></param>
        public RoundEngine(RefereeModel referee, int roundCount)
        {
            Referee = referee;
            RoundCount = roundCount;
            
            FighterList = new List<DungeonFighterModel>();
            MonsterList = new List<DungeonFighterModel>();

        }

       

        /// <summary>
        /// Start a new round
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public RoundEnum StartRound()
        {
            // Switch from Unknown to NextTurn
            var roundResult = RoundEnum.NextTurn;
            
            // Populate round with monsters
            GetNewMonsters();
            Referee.Monsters = MonsterList;


            // Make a list of characters+monsters for turn order
            MakeList();

            // Order fight as per game rules
            OrderFight();

            // Turn fight loop, go until monsters or characters are dead
            
            while (roundResult.Equals(RoundEnum.NextTurn))
            {
                // Fight still going
                roundResult = RoundNextTurn();
            }

            if (roundResult.Equals(RoundEnum.GameOver))
            {
                // Monsters won
                return RoundEnum.GameOver;
            }


            if (roundResult.Equals(RoundEnum.NewRound))
            {
                // Characters won, start a new round
                RoundCount++;
                Debug.Write("Starting round " + RoundCount);
                return RoundEnum.NewRound;
            }

            return RoundEnum.Unknown;

        }

        /// <summary>
        /// At the end of the round
        /// Clear the ItemModel List
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
            Referee.Monsters.Clear();
            Referee.ItemPool.Clear();
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
            RoundEnum roundResult = RoundEnum.NextTurn;

            // Decide Who gets next turn
            // Remember who just went...
            CurrentPlayer = GetNextPlayerInList();

            // Do the turn....
            var turn = new TurnEngine(CurrentPlayer, Referee);
            turn.TakeTurn();

            // No characters, game is over...
            if (Referee.Characters.Count < 1)
            {
                // Game Over
                return RoundEnum.GameOver;
            }

            // Check if round is over
            if (MonsterList.Count < 1)
            {
                // If over, New Round
                return RoundEnum.NewRound;
            }

            return roundResult;
        }

        /// <summary>
        /// Create monsters for a new round, with levels scaled to the round number
        /// </summary>
        /// <returns>List of new monsters</returns>
        private void GetNewMonsters()
        {
            // Clear out monster list
            MonsterList.Clear();

            // Fill up round with the maximum number of monsters
            //for (int i = 0; i < MAX_NUM_MONSTERS; i++)
            //{
            //    // unimplemented
            //    // need to scale monsters to appropriate level based on round
            //}
            
            // FOR DEBUG
            MonsterList.Add(new DungeonFighterModel(new MonsterModel
            {
                Name = "The Coronavirus",
                MaxHealth = 5,
                CurrentHealth = 1,
                Level = 1,
                Description = "Human disaster",
                ImageURI = "https://pngimg.com/uploads/coronavirus/coronavirus_PNG33.png",
                DefenseAttribute = 1,
                OffenseAttribute = 1,
                SpeedAttribute = 1,
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
            FighterList.Clear();

            // Remember the Insert order, used for Sorting
            var order = 0;

            foreach (var hero in Referee.Characters)
            {
                if (hero.Alive)
                {
                    hero.ListOrder = order;
                    FighterList.Add(hero);
                    order++;
                }
            }

            foreach (var mob in MonsterList)
            {
                if (mob.Alive)
                {
                    mob.ListOrder = order;
                    FighterList.Add(mob);
                    order++;
                }
            }

        }

        /// <summary>
        /// Order the fight list based on the Game rules:
        /// Speed, then Level, then XP, then Player (Characters before Monsters),
        /// then by Name, then list order.
        /// </summary>
        public void OrderFight()
        {
            FighterList = FighterList.OrderByDescending(a => a.SpeedAttribute)
                .ThenByDescending(a => a.Level)
                .ThenByDescending(a => a.ExperiencePoints)
                .ThenByDescending(a => a.PlayerType)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.ListOrder)
                .ToList();
        }

        


        /// <summary>
        /// Get the player whose turn it is
        /// </summary>
        /// <returns></returns>
        public DungeonFighterModel GetNextPlayerInList()
        {
            // Walk the list from top to bottom
            // If Player is found, then see if next player exist, if so return that.
            // If not, return first player (looped)

            // If List is empty, return null
            if (FighterList.Count == 0)
            {
                return null;
            }

            // No current player, so set the first one
            if (CurrentPlayer == null)
            {
                return FighterList.FirstOrDefault();
            }

            // Find current player in the list
            var index = FighterList.FindIndex(m => m.Id.Equals(CurrentPlayer.Id));

            // If at the end of the list, return the first element
            if (index == FighterList.Count() - 1)
            {
                return FighterList.FirstOrDefault();
            }

            // Return the next element
            return FighterList[index + 1];

        }
    }
}
