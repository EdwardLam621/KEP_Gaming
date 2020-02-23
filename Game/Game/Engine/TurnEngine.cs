using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //    CurrentAttacker = new PlayerInfoModel(Attacker);
        //    CurrentDefender = new PlayerInfoModel(Target);

        //    return true;
        //}

        /// <summary>
        /// Decide which to attack
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //public DungeonFighterModel AttackChoice(DungeonFighterModel data)
        //{
        //    switch (data.PlayerType)
        //    {
        //        case CreatureEnum.Monster:
        //            return SelectCharacterToAttack();

        //        case CreatureEnum.Character:
        //        default:
        //            return SelectMonsterToAttack();
        //    }
        //}

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

    }
}
