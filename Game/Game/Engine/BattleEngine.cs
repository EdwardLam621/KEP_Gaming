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
        public List<CharacterModel> PickedCharacters = new List<CharacterModel>();

        // The Grid that keeps track of player locations
        //public BattleGridCellModel BattleGrid;

        // Which round we are on
        public int RoundCount = 0;

        // Current round
        public RoundEngine CurrentRound;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BattleEngine()
        {
            // Initialize Referee
            Referee = new RefereeModel();

            // Keep track of battle count/date
            Referee.BattleScore.GameDate = System.DateTime.Now;

            // Battle count not implemented

        }


        /// <summary>
        /// Start the battle
        /// </summary>
        public bool startBattle()
        {

            Referee.SetParty(PickedCharacters);

            // Autobattle 
            if (Referee.AutoBattleEnabled)
            {
                // Record battle was auto
                Referee.BattleScore.AutoBattle = true;
                
                // Start autobattle
                NewRound();
                var roundResult = CurrentRound.StartRoundAuto();

                while (roundResult.Equals(RoundEnum.NewRound))
                {
                    // Fight while characters keep entering new rounds
                    NewRound();
                    roundResult = CurrentRound.StartRoundAuto();
                }
            } 
            

            // stop if characters dead or round count goes too high
            if (CurrentRound.GetRoundState().Equals(RoundEnum.GameOver))
            {
                // display game over screen with statistics

                // 

                Debug.WriteLine("GAME OVER");
                Debug.WriteLine("Total turns taken: " + Referee.BattleScore.TurnCount);
                Debug.WriteLine("Monsters killed: " + Referee.BattleScore.MonsterSlainNumber);
                Debug.WriteLine("Highest round: " + RoundCount);

                // 
                return true;
            }

            return false;




        }


        /// <summary>
        /// Start a new round
        /// </summary>
        public void NewRound()
        {
            RoundCount++;

            Debug.WriteLine("Round " + RoundCount.ToString());

            // Start new round
            CurrentRound = new RoundEngine(Referee, RoundCount);
        }

        //public bool SetParty(List<CharacterModel> party)
        //{
        //    var dungeonFighterModels = new List<DungeonFighterModel>();

        //    foreach (CharacterModel character in party)
        //    {
        //        dungeonFighterModels.Add(new DungeonFighterModel(character));
        //    }

        //    Referee.Characters = dungeonFighterModels;

        //    return true;
        //}

        public void SetAutoBattle(bool toggle)
        {
            Referee.AutoBattleEnabled = toggle;
        }
    }
}
