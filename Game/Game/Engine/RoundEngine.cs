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




        /// <summary>
        /// Create monsters for a new round, with levels scaled to the round number
        /// </summary>
        /// <returns>List of new monsters</returns>
        public List<DungeonMonsterModel> GetNewMonsters()
        {
            // unimplemented
            return null;
        }

        
    }
}
