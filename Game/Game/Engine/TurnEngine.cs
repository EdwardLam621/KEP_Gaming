using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Game.Helpers;
using Game.Models;
using Game.Models.Enum;
using Game.ViewModels;

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

        //turn on to enable critical hits for double damage
        public static bool criticalHitEnable = false;

        //turn on to enable the chance that monsters return to live as zombie
        public static bool zombieMonstersEnable = false;

        //percentage for a monster to return to life this turn if it is killed
        public static int returnToLiveAsZombie = 20;    //20% default

        //turn on to enable the chance that monsters is enraged
        public static bool monsterEnragedModeEnable = false;

        //percentage of monster to enter enraged mode
        public static int enragedChance = 20;

        //Generate random number
        Random rnd;



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
            rnd = new Random();
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
            if (Attacker == null)
            {
                return false;
            }

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
            Referee.BattleMessages.ClearMessages();

            // Load current player info to battle messages
            Referee.BattleMessages.AttackerName = Attacker.Name;
            Referee.BattleMessages.PlayerType = Attacker.PlayerType;
            Referee.BattleMessages.AttackerHealth = Attacker.CurrentHealth;

            // Load target info to battle messages
            Referee.BattleMessages.TargetName = Target.Name;
            Referee.BattleMessages.TargetHealth = Target.CurrentHealth;

            

            // Attacker prepares to attack message
            Debug.WriteLine(Referee.BattleMessages.GetPreamble());
            

            // Set Attack and Defense
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            var DefenseScore = Target.GetDefense() + Target.Level;


            // Poor Bob always misses
            if (Attacker.Name.Equals("Bob"))
            {
                Referee.BattleMessages.HitStatus = HitStatusEnum.Miss;

            } else
            {
                Referee.BattleMessages.HitStatus = RollToHitTarget(AttackScore, DefenseScore);
            }

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

                    //if the enrage mode is on
                    if(monsterEnragedModeEnable)
                    {
                        //if attacker is monster and enraged
                        if (Attacker.PlayerType == CreatureEnum.Monster && Attacker.isEnraged)
                        {
                            Target.TakeDamage(Referee.BattleMessages.DamageAmount * 2);
                            Attacker.isEnraged = false;
                        }

                        //if target is monster
                        if (Target.PlayerType == CreatureEnum.Monster)
                        {
                            //random a number
                            Random rnd = new Random();
                            int random = rnd.Next(1, 101);
                            //if random number is <= 20 (20%)
                            //monster is enraged
                            if (random <= enragedChance)
                            {
                                Target.isEnraged = true;
                            }
                        }
                    }
                    
                    else
                    {
                        
                        Target.TakeDamage(Referee.BattleMessages.DamageAmount);

                        // If it is a character apply the experience earned
                        CalculateExperience(Attacker, Target);

                    }

                    // Update target's health for game display
                    Referee.BattleMessages.TargetHealth = Target.CurrentHealth;

                    // Set
                    Referee.BattleMessages.TurnMessage = Referee.BattleMessages.GetTurnMessage();

                    //Referee.BattleMessages.TurnMessage = Referee.BattleMessages.GetHTMLFormattedTurnMessage();

                    break;

                case HitStatusEnum.CriticalHit:
                    // It's a Hit
                    //Calculate Damage
                    Debug.WriteLine(Referee.BattleMessages.GetCriticalHitMessage());

                    Referee.BattleMessages.DamageAmount = Attacker.GetDamageRollValue();

                    Target.TakeDamage(Referee.BattleMessages.DamageAmount * 2);


                    Referee.BattleMessages.TargetHealth = Target.CurrentHealth;
                    Debug.WriteLine(Referee.BattleMessages.GetHitMessage());
                    Debug.WriteLine(Referee.BattleMessages.GetCurrentHealthMessage());

                    break;

            }
            
            RemoveIfDead(Target);

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

                // check if miracle max is enabled
                if (Referee.ResurrectionsEnabled)
                {
                    int resurrected;

                    // works on characters only
                    if (Referee.UsedResurrection.TryGetValue(Target, out resurrected))
                    {
                        // If character has not resurrected before
                        if (resurrected == 0)
                        {
                            // Set health back to max
                            Target.CurrentHealth = Target.MaxHealth;

                            // Toggle resurrection
                            Referee.UsedResurrection[Target] = 1;

                            return false;
                        }
                    }
                }

                

                //check if monster can return to live

                if(zombieMonstersEnable && Target.PlayerType == CreatureEnum.Monster)
                {
                    Random rnd = new Random();
                    int random = rnd.Next(0, 100 + 1);

                    //if monster is return to live, change its name and health = 1/2 original health
                    if (random <= returnToLiveAsZombie)
                    {
                        Target.CurrentHealth = Target.MaxHealth / 2;
                        Target.Name = "Zombie " + Target.Name;
                        return false;
                    }
                }

                Referee.BattleMessages.TurnMessageSpecial = Referee.BattleMessages.GetDeathMessage();
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

            Referee.BattleMessages.TurnMessageSpecial = Referee.BattleMessages.GetDeathMessage();

            // Remove target from list...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                
                case CreatureEnum.Character:

                    Target.Alive = false;
                    Referee.Characters.Remove(Target);

                    Referee.BattleScore.CharacterModelDeathList.Add(Target);

                    // Add the CharacterModel to the killed list
                    Referee.BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    DropItems(Target);
                    break;

                default:

                    Target.Alive = false;
                    Referee.Monsters.Remove(Target);

                    // Add one to the monsters killed count...
                    Referee.BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    Referee.BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    DropItems(Target);
                    break;
            }

            Debug.WriteLine(Referee.BattleMessages.TurnMessageSpecial);
            return true;

        }

        /// <summary>
        /// Drop Items
        /// </summary>
        /// <param name="Target"></param>
        public int DropItems(DungeonFighterModel Target)
        {
            var DroppedMessage = "\nItems Dropped : \n";

            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            myItemList.AddRange(GetRandomMonsterItemDrops());

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                Referee.BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            Referee.ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            Referee.BattleMessages.DroppedMessage = DroppedMessage;

            Referee.BattleScore.ItemModelDropList.AddRange(myItemList);

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
                //check if criticalHit enable, double the damage
                if (criticalHitEnable)
                {
                    Referee.BattleMessages.HitStatus = HitStatusEnum.CriticalHit;
                    return Referee.BattleMessages.HitStatus;
                }

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
            Referee.BattleMessages.AttackStatus = " hits ";
            Referee.BattleMessages.HitStatus = HitStatusEnum.Hit;
            return Referee.BattleMessages.HitStatus;
        }


        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        public List<ItemModel> GetRandomMonsterItemDrops()
        {
            //pick 1 to 3 items every monster dies
            var NumberToDrop = DiceHelper.RollDice(1, rnd.Next(1,4));

            var myList = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                myList.Add(data);
            }
            return myList;
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <param name="Target"></param>
        public bool CalculateExperience(DungeonFighterModel Attacker, DungeonFighterModel Target)
        {
            if (Attacker.PlayerType == CreatureEnum.Character)
            {
                var experienceEarned = Target.CalculateExperienceEarned(Referee.BattleMessages.DamageAmount);
                Referee.BattleMessages.ExperienceEarned = " Earned " + experienceEarned + " points";

                var LevelUp = Attacker.AddExperience(experienceEarned);
                if (LevelUp)
                {
                    Referee.BattleMessages.LevelUpMessage = Attacker.Name + " is now Level " + Attacker.Level + " With Health Max of " + Attacker.GetMaxHealthTotal;
                    Debug.WriteLine(Referee.BattleMessages.LevelUpMessage);
                }

                // Add Experinece to the Score
                Referee.BattleScore.ExperienceGainedTotal += experienceEarned;
            }

            return true;
        }


    }
}
