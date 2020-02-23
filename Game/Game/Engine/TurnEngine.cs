using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Game.Helpers;
using Game.Models;

namespace Game.Engine
{
    public class TurnEngine
    {
        public DungeonFighterModel Attacker;
        public DungeonFighterModel Defender;
        public Referee Referee;

        public TurnEngine()
        {

        }

        public TurnEngine(DungeonFighterModel attacker, Referee referee)
        {
            Attacker = attacker;
            Referee = referee;
        }

        public bool ChooseTarget()
        {
            //unimplemented
            return false;
        }

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool TakeTurn()
        {
            // Choose Action.  Such as Move, Attack etc.

            //var result = Attack(Attacker);

            Referee.BattleScore.TurnCount++;

            return false;
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
        //public bool Attack(DungeonFighterModel Attacker)
        //{
        //    // For Attack, Choose Who
        //    var Target = AttackChoice(Attacker);

        //    if (Target == null)
        //    {
        //        return false;
        //    }

        //    // Do Attack
        //    TurnAsAttack(Attacker, Target);

        //    CurrentAttacker = new DungeonFighterModel(Attacker);
        //    CurrentDefender = new DungeonFighterModel(Target);

        //    return true;
        //}

        /// <summary>
        /// Decide which to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DungeonFighterModel AttackChoice(DungeonFighterModel data)
        {
            switch (data.PlayerType)
            {
                case CreatureEnum.Monster:
                    return SelectCharacterToAttack();

                case CreatureEnum.Character:
                default:
                    return SelectMonsterToAttack();
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
        /// Drop Items
        /// </summary>
        /// <param name="Target"></param>
        public int DropItems(DungeonFighterModel Target)
        {
            // Drop Items to ItemModel Pool
            var myItemList = new List<ItemModel>();
            //var myItemList = Target.DropAllItems();

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
