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
        

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public RoundEngine()
        {

        }

        public RoundEngine(List<DungeonFighterModel> party)
        {

        }


        /// <summary>
        /// Create monsters for a new round, with levels scaled to the round number
        /// </summary>
        /// <returns>List of new monsters</returns>
        private List<DungeonFighterModel> GetNewMonsters()
        {

            List<DungeonFighterModel> monsterList = new List<DungeonFighterModel>(); 
            for (int i = 0; i < MAX_NUM_MONSTERS; i++)
            {
                // unimplemented
            }
            return monsterList;
        }

        private List<DungeonFighterModel> getTurnOrder()
        {
            List<DungeonFighterModel> turnOrder = new List<DungeonFighterModel>();
            return null;   
        }
    }
}
