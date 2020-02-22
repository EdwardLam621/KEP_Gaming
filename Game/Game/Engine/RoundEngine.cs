using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;
namespace Game.Engine
{
    public class RoundEngine
    {
        private int Round { get; set; } = 1;
        
        private const int MAX_NUM_MONSTERS = 6;
        
        private List<DungeonFighterModel> HeroList;

        private List<DungeonFighterModel> MonsterList;

        private List<DungeonFighterModel> PlayerList;

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public RoundEngine()
        {
            GetNewMonsters();
        }

        public RoundEngine(List<DungeonFighterModel> party)
        {
            HeroList = party;
            GetNewMonsters();
        }


        /// <summary>
        /// Create monsters for a new round, with levels scaled to the round number
        /// </summary>
        /// <returns>List of new monsters</returns>
        private void GetNewMonsters()
        {

            MonsterList = new List<DungeonFighterModel>(); 
            for (int i = 0; i < MAX_NUM_MONSTERS; i++)
            {
                // unimplemented
            }
           
        }

        private List<DungeonFighterModel> getTurnOrder()
        {
            List<DungeonFighterModel> turnOrder = new List<DungeonFighterModel>();
            return null;   
        }



    }
}
