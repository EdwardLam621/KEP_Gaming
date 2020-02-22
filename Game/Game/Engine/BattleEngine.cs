using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;

namespace Game.Engine
{
    /// <summary>
    /// Main battle engine 
    /// </summary>
    public class BattleEngine 
    {

        public Referee Referee;

        public RoundEngine RoundEngine;

        public BattleEngine()
        {

        }

        public BattleEngine(List<CharacterModel> party)
        {
            var dungeonFighterModels = new List<DungeonFighterModel>();
            
            foreach (CharacterModel character in party)
            {
                dungeonFighterModels.Add(new DungeonFighterModel(character));
            }
            
            Referee = new Referee(dungeonFighterModels);

            RoundEngine = new RoundEngine(Referee);

        }

        public void startBattle()
        {
            RoundEngine.StartFight();
            //referee.getScores <- to be implemented
        }
    }
}
