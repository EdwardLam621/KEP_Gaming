using System;
using System.Collections.Generic;
using System.Text;
using Game.Engine;
using Game.Models.Enum;

namespace Game.Models
{
    public class BattleGridModel
    {
        
        // TileMap of pretty background images -- need to implement
        
        private int[,] TileMap;
        
        // Keep track of player and monster locations
        private BattleGridEnum[,] PlayerGrid;

        // Size of the board
        private const int GRID_WIDTH = 8;
        private const int GRID_HEIGHT = 6;


        /// <summary>
        /// Default constructor
        /// </summary>
        public BattleGridModel()
        {
            // Set up tile and player board sizes
            TileMap = new int[GRID_WIDTH, GRID_HEIGHT];
            PlayerGrid = new BattleGridEnum[GRID_WIDTH, GRID_HEIGHT];
        }

    }
}
