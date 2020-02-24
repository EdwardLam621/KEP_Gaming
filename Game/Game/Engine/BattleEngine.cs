﻿using System;
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

        // The round engine that will manage the different rounds
        public RoundEngine RoundEngine;


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

            RoundEngine = new RoundEngine(Referee);

        }

        /// <summary>
        /// Start the round(s)!
        /// </summary>
        public void startBattle()
        {
            RoundEngine.Begin();
            //referee.getScores <- to be implemented
        }
    }
}
