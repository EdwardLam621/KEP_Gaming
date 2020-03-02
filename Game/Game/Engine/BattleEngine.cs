using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //max number of character party
        public int MaxNumberPartyCharacters = 6;

        // List of Characters
        public List<CharacterModel> CharacterList = new List<CharacterModel>();

        // The Grid that keeps track of player locations
        //public BattleGridCellModel BattleGrid;


        private List<DungeonFighterModel> Fighters;

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
        public BattleEngine(List<CharacterModel> party)
        {

            SetParty(party);
        }

        /// <summary>
        /// Start the round(s)!
        /// </summary>
        public void startBattle()
        {
            
            var roundResult = BeginAutoBattle();
            
            //referee.getScores <- to be implemented
        }

        /// <summary>
        /// Begin starts round 1, and takes care of looping through rounds
        /// </summary>
        /// <returns></returns>
        private bool BeginAutoBattle()
        {

            // Set up first round
            CurrentRound = new RoundEngine(Referee, RoundCount);

            // Round fight loop
            var roundResult = CurrentRound.StartRoundAuto();
            
            while (roundResult.Equals(RoundEnum.NewRound))
            {
                RoundCount++;
                
                // Start new round
                CurrentRound = new RoundEngine(Referee, RoundCount);
                
                // Fight while characters keep entering new rounds
                roundResult = CurrentRound.StartRoundAuto();
            }

            if (roundResult.Equals(RoundEnum.GameOver))
            {
                // display game over screen with statistics
                Debug.WriteLine("GAME OVER");
                Debug.WriteLine("Total turns taken: " + Referee.BattleScore.TurnCount);
                Debug.WriteLine("Monsters killed: " + Referee.BattleScore.MonsterSlainNumber);
                Debug.WriteLine("Highest round: " + RoundCount);

                return true;
            }

            return false;
        }

        public void SetParty(List<CharacterModel> party)
        {
            var dungeonFighterModels = new List<DungeonFighterModel>();

            foreach (CharacterModel character in party)
            {
                dungeonFighterModels.Add(new DungeonFighterModel(character));
            }

            Fighters = dungeonFighterModels;
            Referee = new RefereeModel(Fighters);

        }

        public void SetAutoBattle(bool toggle)
        {
            Referee.AutoBattleEnabled = toggle;
        }
    }
}
