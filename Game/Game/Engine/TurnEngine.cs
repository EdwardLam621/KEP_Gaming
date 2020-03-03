using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Game.Helpers;
using Game.Models;
using Game.Models.Enum;

namespace Game.Engine
{
    /// <summary>
    /// Manages a game turn
    /// </summary>
    public class TurnEngine
    {
        // Current attacker, set in constructor
        public DungeonFighterModel Attacker;
        
        // Current defender
        public DungeonFighterModel Target;
        
        // Referee object to help manage the turn
        public RefereeModel Referee;
        
        // Action choice (Move, Attack, Skill)
        public TurnChoiceEnum ActionChoice;

        /// <summary>
        /// Default constructor
        /// </summary>
        public TurnEngine()
        {

        }

        /// <summary>
        /// Constructor taking a Referee, attacker, and the turn choice of the attacker
        /// </summary>
        /// <param name="referee"></param>
        /// <param name="attacker"></param>
        /// <param name="choice"></param>
        public TurnEngine(RefereeModel referee, 
            DungeonFighterModel attacker, 
            DungeonFighterModel target, 
            TurnChoiceEnum choice)
        {
            Referee = referee;
            Attacker = attacker;
            Target = target;
            ActionChoice = choice;
        }


        /// <summary>
        /// Do the turn
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool TakeTurn()
        {

            // Todo: Add Move, Skill options and helper methods
                
            if (ActionChoice.Equals(TurnChoiceEnum.Attack))
            {
                Attack(Attacker);
            }

            Referee.BattleScore.TurnCount++;

            return true;
        }

        

        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool Attack(DungeonFighterModel Attacker)
        {

            if (Target == null)
            {
                return false;
            }

            // Do Attack
            TurnAsAttack(Attacker, Target);

            return true;
        }

        

        

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="AttackScore"></param>
        /// <param name="Target"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public bool TurnAsAttack(DungeonFighterModel Attacker, DungeonFighterModel Target)
        {
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            // Set Messages to empty
            Referee.BattleMessages.TurnMessage = string.Empty;
            Referee.BattleMessages.TurnMessageSpecial = string.Empty;
            Referee.BattleMessages.AttackStatus = string.Empty;

            // Remember Current Player
            Referee.BattleMessages.AttackerName = Attacker.Name;
            Referee.BattleMessages.PlayerType = Attacker.PlayerType;
            Referee.BattleMessages.AttackerHealth = Attacker.CurrentHealth;
            Referee.BattleMessages.TargetHealth = Target.CurrentHealth;

            // Choose who to attack
            Referee.BattleMessages.TargetName = Target.Name;



            Debug.WriteLine(Referee.BattleMessages.GetPreamble());
            


            // Set Attack and Defense
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            var DefenseScore = Target.GetDefense() + Target.Level;


            Referee.BattleMessages.HitStatus = RollToHitTarget(AttackScore, DefenseScore);

            switch (Referee.BattleMessages.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss
                    Debug.WriteLine("It's a miss!");

                    break;

                case HitStatusEnum.Hit:
                    // It's a Hit
                    //Calculate Damage
                    Debug.WriteLine("It's a hit!");

                    Referee.BattleMessages.DamageAmount = Attacker.GetDamageRollValue();

                    Target.TakeDamage(Referee.BattleMessages.DamageAmount);
                    
                    
                    Referee.BattleMessages.TargetHealth = Target.CurrentHealth;
                    Debug.WriteLine(Referee.BattleMessages.GetHitMessage());
                    Debug.WriteLine(Referee.BattleMessages.GetCurrentHealthMessage());

                    RemoveIfDead(Target);
                    break;
            }
            Referee.BattleMessages.ClearMessages();
            //Thread.Sleep(1000);

            return true;
        }

        /// <summary>
        /// If Dead process Targed Died
        /// </summary>
        /// <param name="Target"></param>
        public bool RemoveIfDead(DungeonFighterModel Target)
        {
            // Check for alive
            if (Target.CurrentHealth <= 0)
            {
                TargedDied(Target);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        /// <param name="Target"></param>
        public bool TargedDied(DungeonFighterModel Target)
        {
            // Mark Status in output
            //Referee.BattleMessages.TurnMessageSpecial = " and causes death";

            // Remove target from list...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case CreatureEnum.Character:
                    Referee.Characters.Remove(Target);

                    Referee.BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    Referee.BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    DropItems(Target);

                    return true;

                case CreatureEnum.Monster:
                default:
                    Referee.Monsters.Remove(Target);

                    // Add one to the monsters killed count...
                    Referee.BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    Referee.BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    DropItems(Target);

                    return true;
            }
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        /// <param name="Target"></param>
        public int DropItems(DungeonFighterModel Target)
        {
            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            //myItemList.AddRange(GetRandomMonsterItemDrops(Referee.BattleScore.RoundCount));

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                Referee.BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                Referee.BattleMessages.TurnMessageSpecial += " ItemModel " + ItemModel.Name + " dropped";
            }

            Referee.ItemPool.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        /// <returns></returns>
        public HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            var d20 = DiceHelper.RollDice(1, 20);

            if (d20 == 1)
            {
                // Force Miss
                Referee.BattleMessages.HitStatus = HitStatusEnum.Miss;
                return Referee.BattleMessages.HitStatus;
            }

            if (d20 == 20)
            {
                // Force Hit
                Referee.BattleMessages.HitStatus = HitStatusEnum.Hit;
                return Referee.BattleMessages.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                Referee.BattleMessages.AttackStatus = " misses ";
                // Miss
                Referee.BattleMessages.HitStatus = HitStatusEnum.Miss;
                Referee.BattleMessages.DamageAmount = 0;
                return Referee.BattleMessages.HitStatus;
            }

            // Hit
            Referee.BattleMessages.HitStatus = HitStatusEnum.Hit;
            return Referee.BattleMessages.HitStatus;
        }


        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            // You decide how to drop monster items, level, etc.

            var NumberToDrop = DiceHelper.RollDice(1, round);

            var myList = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                myList.Add(new ItemModel());
            }
            return myList;
        }

    }
}
