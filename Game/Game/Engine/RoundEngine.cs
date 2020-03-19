using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;
using System.Linq;
using System.Diagnostics;
using Game.Models.Enum;

namespace Game.Engine
{
    /// <summary>
    /// Manages the rounds
    /// </summary>
    public class RoundEngine
    {
        // Maximum number of monsters to fight
        public const int MAX_NUM_MONSTERS = 6;

        // Which round we are on
        public int RoundCount = 1;

        // List of monsters in this round
        public List<DungeonFighterModel> MonsterList;

        // List of total players (monsters and characters) in this round
        public List<DungeonFighterModel> FighterList;

        // Player whose turn it is currently
        public DungeonFighterModel CurrentPlayer;

        // Recipient of the attack
        public DungeonFighterModel Target;

        // The Referee object that handles scores/items/skills
        public RefereeModel Referee;

        // Current Round state
        public RoundEnum RoundResult = RoundEnum.NextTurn;

        // Time warp boolean, true = slower character moves, false = faster goes
        public bool TimeWarp = false;

        // Enable critical hits for hackathon
        public bool CriticalHitsEnabled { get; set; } = false;

        // Enable resurrection for hackathon
        public bool ResurrectionEnabled { get; set; } = false;

        // generate random number
        Random rand = new Random();

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

            // Populate round with monsters
            GetNewMonsters();
            Referee.Monsters = MonsterList;

            // Make a list of characters+monsters for turn order
            OrderFighters();
            
        }

        /// <summary>
        /// 
        /// </summary>
        public void AttackClicked()
        {

            // Auto select target
            Target = SelectMonsterToAttack();
            
            // Do the turn
            TakeTurn(TurnChoiceEnum.Attack);
            
            // Update round state
            RoundResult = GetRoundState();
        
        }


        public void MonsterNextTurn()
        {
            // Select turn choice using same method as autobattle
            var choice = TurnChoice();

            // Do the turn
            TakeTurn(choice);

        }

        /// <summary>
        /// Start a new autobattle round and returns round result to BattleEngine
        /// </summary>
        /// <returns></returns>
        public RoundEnum StartRoundAuto()
        {
            
            // Turn fight loop, go until monsters or characters are dead
            while (RoundResult.Equals(RoundEnum.NextTurn))
            {
                // Fight still going    

                // See whose turn it is
                CurrentPlayer = GetNextPlayerTurn();

                // Select turn choice (move, attack, skill)
                var choice = TurnChoice();

                // Do the turn with the current player
                TakeTurn(choice);

                // Check the round result
                RoundResult = GetRoundState();
            }

            
            Referee.BattleScore.RoundCount++;
            if (Referee.BattleScore.RoundCount > 100)
            {
                return RoundEnum.GameOver;
            }

            // Round is over, return result to BattleEngine
            return RoundResult;
        }

        /// <summary>
        /// Perform the next turn using the specified choice
        /// </summary>
        public bool TakeTurn(TurnChoiceEnum choice)
        {

            // Select target automatically if monster is currently attacking or autobattle is enabled
            if (Referee.AutoBattleEnabled || CurrentPlayer.PlayerType.Equals(CreatureEnum.Monster))
            {
                Target = ChooseTarget(CurrentPlayer);
            }
                       
            // otherwise the target is being set externally through the Battle Page
            var turn = new TurnEngine(Referee, CurrentPlayer, Target, choice);

            turn.TakeTurn();

            return true;
        }


        /// <summary>
        /// Get the Next Player to have a turn
        /// </summary>
        /// <returns></returns>
        public DungeonFighterModel GetNextPlayerTurn()
        {
            // Remove the Dead
            RemoveDeadPlayersFromList();


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

        /// <summary>
        /// Remove Dead Players from the List
        /// </summary>
        /// <returns></returns>
        public List<DungeonFighterModel> RemoveDeadPlayersFromList()
        {
            FighterList = FighterList.Where(m => m.Alive == true).ToList();
            return FighterList;
        }

        /// <summary>
        /// Get the current state of the Round
        /// </summary>
        /// <returns></returns>
        public RoundEnum GetRoundState()
        {
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

            return RoundEnum.NextTurn;
        }

        /// <summary>
        /// Create monsters for a new round, with levels scaled to the round number
        /// </summary>
        /// <returns>List of new monsters</returns>
        private void GetNewMonsters()
        {
            // Clear out monster list
            MonsterList.Clear();

            int TargetLevel = 1;

            if (Referee.Characters.Count() > 0)
            {
                // Get the Min Character Level (linq is soo cool....)
                TargetLevel = Convert.ToInt32(Referee.Characters.Min(m => m.Level));
            }

            for (var i = 0; i < MAX_NUM_MONSTERS; i++)
            {
                var data = Helpers.RandomPlayerHelper.GetRandomMonster(TargetLevel - 1 + rand.Next(0, 3));


                // Help identify which Monster it is
                data.Name += " " + MonsterList.Count() + " Lv. " + data.Level;

                // Set ExperienceRemaining so Monsters can both use this method
                data.ExperienceRemaining = LevelTableHelper.Instance.LevelDetailsList[data.Level + 1].Experience;


                MonsterList.Add(new DungeonFighterModel(data));
            }

            
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
                PickupItemsFromPool(character);
            }

            // Reset Monster and Item Lists
            Referee.Monsters.Clear();
            Referee.ItemPool.Clear();
            return true;
        }

        /// <summary>
        /// Who is Playing this round?
        /// </summary>
        public void OrderFighters()
        {

            // Remember the Insert order, used for Sorting
            var order = 0;

            foreach (var hero in Referee.Characters)
            {
                if (hero.Alive)
                {
                    hero.PlayerCreatureType = CreatureEnum.Character;
                    hero.ListOrder = order;
                    FighterList.Add(hero);
                    order++;
                }
            }

            foreach (var mob in MonsterList)
            {
                if (mob.Alive)
                {
                    mob.PlayerCreatureType = CreatureEnum.Monster;
                    mob.ListOrder = order;
                    FighterList.Add(mob);
                    order++;
                }
            }

            OrderFight();

        }

        /// <summary>
        /// Order the fight list based on the Game rules:
        /// Speed, then Level, then XP, then Player (Characters before Monsters),
        /// then by Name, then list order.
        /// </summary>
        public void OrderFight()
        {
            if(RoundCount % 5 == 0)
            {
                FighterList = FighterList.OrderByDescending(a => a.PlayerCreatureType == CreatureEnum.Character)
                   .ThenBy(a => a.CurrentHealth)
                   .ThenBy(a => a.SpeedAttribute)
                   .ThenByDescending(a => a.Level)
                   .ThenByDescending(a => a.ExperiencePoints)
                   .ThenByDescending(a => a.PlayerType)
                   .ThenBy(a => a.Name)
                   .ThenBy(a => a.ListOrder)
                   .ToList();
            }
            else
            {
                if (TimeWarp)
                {
                    FighterList = FighterList.OrderBy(a => a.SpeedAttribute)
                   .ThenByDescending(a => a.Level)
                   .ThenByDescending(a => a.ExperiencePoints)
                   .ThenByDescending(a => a.PlayerType)
                   .ThenBy(a => a.Name)
                   .ThenBy(a => a.ListOrder)
                   .ToList();
                }
                else
                {
                    FighterList = FighterList.OrderByDescending(a => a.SpeedAttribute)
                   .ThenByDescending(a => a.Level)
                   .ThenByDescending(a => a.ExperiencePoints)
                   .ThenByDescending(a => a.PlayerType)
                   .ThenBy(a => a.Name)
                   .ThenBy(a => a.ListOrder)
                   .ToList();
                }
            }
            

            //reset TimeWarp
            TimeWarp = false;
        }


        /// <summary>
        /// Pickup Items Dropped
        /// </summary>
        /// <param name="fighter"></param>
        public bool PickupItemsFromPool(DungeonFighterModel fighter)
        {

            // TODO: Teams, You need to implement your own Logic if not using auto apply

            // I use the same logic for Auto Battle as I do for Manual Battle

            // Have the character, walk the items in the pool, and decide if any are better than current one.

            // Use Mike's auto apply for now
            GetItemFromPoolIfBetter(fighter, ItemLocationEnum.Head);
            GetItemFromPoolIfBetter(fighter, ItemLocationEnum.Necklass);
            GetItemFromPoolIfBetter(fighter, ItemLocationEnum.PrimaryHand);
            GetItemFromPoolIfBetter(fighter, ItemLocationEnum.OffHand);
            GetItemFromPoolIfBetter(fighter, ItemLocationEnum.RightFinger);
            GetItemFromPoolIfBetter(fighter, ItemLocationEnum.LeftFinger);
            GetItemFromPoolIfBetter(fighter, ItemLocationEnum.Feet);
   
            return true;
        }

        /// <summary>
        /// Swap out the item if it is better
        /// 
        /// Uses Value to determine
        /// </summary>
        /// <param name="fighter"></param>
        /// <param name="itemLocation"></param>
        public bool GetItemFromPoolIfBetter(DungeonFighterModel fighter, ItemLocationEnum itemLocation)
        {
            var items = Referee.ItemPool.Where(a => a.Location == itemLocation)
                .OrderByDescending(a => a.Value)
                .ToList();

            // If no items in the list, return...
            if (!items.Any())
            {
                return false;
            }

            var currentItem = fighter.GetItemByLocation(itemLocation);

            if (currentItem == null)
            {
                SwapCharacterItem(fighter, itemLocation, items.FirstOrDefault());
                return true;
            }

            foreach (var droppedItem in items)
            {
                if (droppedItem.Value > currentItem.Value)
                {
                    SwapCharacterItem(fighter, itemLocation, currentItem);
                    return true;
                }
            }

            return true;
        }


        /// <summary>
        /// Swap the Item the character has for one from the pool
        /// 
        /// Drop the current item back into the Pool
        /// 
        /// </summary>
        /// <param name="character"></param>
        /// <param name="setLocation"></param>
        /// <param name="newItem"></param>
        /// <returns></returns>
        private ItemModel SwapCharacterItem(DungeonFighterModel character, ItemLocationEnum setLocation, ItemModel newItem)
        {
            // Put on the new ItemModel, which drops the one back to the pool
            var droppedItem = character.AddItem(setLocation, newItem.Id);

            // Add the PoolItem to the list of selected items
            // ?
            //ItemModelSelectList.Add(newItem);

            // Remove the ItemModel just put on from the pool
            Referee.ItemPool.Remove(newItem);

            if (droppedItem != null)
            {
                // Add the dropped ItemModel to the pool
                Referee.ItemPool.Add(droppedItem);
            }

            return droppedItem;
        }

        /// <summary>
        /// Get user choice from BattleViewModel
        /// </summary>
        /// <returns></returns>
        public TurnChoiceEnum TurnChoice()
        {

            if (Referee.AutoBattleEnabled || CurrentPlayer.PlayerType.Equals(CreatureEnum.Monster))
            {
                // if enemies in range
                //// if skill countdown == 0, UseSkill()
                //// else attack
                // else move closer to enemy

                // Return Attack for now 
                // TODO: remove this hardcoded result
                return TurnChoiceEnum.Attack;
            }

            return TurnChoiceEnum.Unknown;
        }


        /// <summary>
        /// Decide whom to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DungeonFighterModel ChooseTarget(DungeonFighterModel data)
        {
            switch (data.PlayerType)
            {
                case CreatureEnum.Monster:
                    return SelectCharacterToAttack();

                case CreatureEnum.Character:
                    return SelectMonsterToAttack();

                default:
                    return null;
            }
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        /// <returns></returns>
        public DungeonFighterModel SelectCharacterToAttack()
        {
            if (Referee.Characters == null)
            {
                return null;
            }

            if (Referee.Characters.Count < 1)
            {
                return null;
            }

            // Select first in the list
            var Defender = Referee.Characters
                .Where(m => m.Alive)
                .OrderBy(m => m.ListOrder).FirstOrDefault();

            return Defender;
        }


        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        /// <returns></returns>
        public DungeonFighterModel SelectMonsterToAttack()
        {
            if (Referee.Monsters == null)
            {
                return null;
            }

            if (Referee.Monsters.Count < 1)
            {
                return null;
            }

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first 
            var Defender = Referee.Monsters
                .Where(m => m.Alive)
                .OrderBy(m => m.CurrentHealth).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// Set the target of the attack. Used in manual mode.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool SetTarget(DungeonFighterModel target)
        {
            Target = target;
            return true;
        }

    }
}

