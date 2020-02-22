using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;

namespace Game.Engine
{
    public class TurnEngine
    {
        public DungeonFighterModel Attacker;
        public DungeonFighterModel Defender;

        public TurnEngine()
        {

        }

        public TurnEngine(DungeonFighterModel attacker)
        {
            Attacker = attacker;
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

            //BattleScore.TurnCount++;

            return false;
        }
    }
}
