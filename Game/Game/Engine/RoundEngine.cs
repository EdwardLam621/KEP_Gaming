﻿using System;
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
        public const int MAX_NUM_MONSTERS = 1;

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

        public void RoundNextTurnAttack()
        {
            CurrentPlayer = GetNextPlayerInList();


        }


        public void RoundNextTurn()
        {
            // See whose turn it is
            CurrentPlayer = GetNextPlayerInList();
            
            if (CurrentPlayer.PlayerType.Equals(CreatureEnum.Monster))
            {
                MonsterNextTurn();
            }

        }

        public void MonsterNextTurn()
        {

            var nextPlayer = GetNextPlayerInList();

            // Loop through monster attacks
            while (nextPlayer.PlayerType.Equals(CreatureEnum.Monster))
            {
                NextTurnAuto();
            }
        }

        public void NextTurnAuto()
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
                CurrentPlayer = GetNextPlayerInList();

                // Select turn choice (move, attack, skill)
                var choice = TurnChoice();

                // Do the turn with the current player
                TakeTurn(choice);

                // Check the round result
                RoundResult = GetRoundState();
            }

            // Round is over, return result to BattleEngine

            return RoundResult;
        }

        /// <summary>
        /// Perform the next turn using the specified choice
        /// </summary>
        public void TakeTurn(TurnChoiceEnum choice)
        {
            // Set up turn engine and do the turn
            var turn = new TurnEngine(Referee, CurrentPlayer, choice);
            turn.TakeTurn();
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

            OrderFight();

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
            GetItemFromPoolIfBetter(fighter, ItemLocationEnum.Body);
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

            if (Referee.AutoBattleEnabled)
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



    }
}

