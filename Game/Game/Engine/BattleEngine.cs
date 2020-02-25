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
        // The referee object that keeps track of the fight
        public RefereeModel Referee;

        // Which round we are on
        public int RoundCount = 1;

        // Current round
        public RoundEngine CurrentRound;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BattleEngine()
        {

        }

        
        /// <summary>
        /// Constructor that takes the party of characters and converts them to fighters
        /// Also initializes the Referee and RoundEngine
        /// </summary>
        /// <param name="party"></param>
        public BattleEngine(List<CharacterModel> party, bool autoBattleEnabled = false)
        {
            
            var dungeonFighterModels = new List<DungeonFighterModel>();
            
            foreach (CharacterModel character in party)
            {
                dungeonFighterModels.Add(new DungeonFighterModel(character));
            }
            
            Referee = new RefereeModel(dungeonFighterModels);
            Referee.AutoBattleEnabled = autoBattleEnabled;

        }

        /// <summary>
        /// Start the round(s)!
        /// </summary>
        public void startBattle()
        {
            var roundResult = Begin();
            //referee.getScores <- to be implemented
        }

        /// <summary>
        /// Begin starts round 1, and takes care of looping through rounds
        /// </summary>
        /// <returns></returns>
        private bool Begin()
        {

            CurrentRound = new RoundEngine(Referee, RoundCount);


            // Round fight loop
            var fightResult = CurrentRound.StartRound();
            while (fightResult.Equals(RoundEnum.NewRound))
            {
                RoundCount++;
                CurrentRound = new RoundEngine(Referee, RoundCount);
            }

            if (fightResult.Equals(RoundEnum.GameOver))
            {
                // display game over screen with statistics
                return true;
            }

            return false;
        }

    }
}
