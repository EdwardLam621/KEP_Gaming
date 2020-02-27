using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class BattleGridModel
    {

        private int[,] MapArray; 

        /// <summary>
        /// Default constructor
        /// </summary>
        public BattleGridModel()
        {
            
        }

        public BattleGridModel(int width, int height)
        {
            MapArray = new int[width, height];
        }



    }
}
