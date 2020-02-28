using System;
using System.Collections.Generic;
using System.Text;
using Game.Engine;
using Game.Models.Enum;

namespace Game.Models
{
    public class BattleGridModel
    {
        
        // 2D array of BattleGridCells, holding characters and sprites
        private BattleGridCellModel[,] PlayerBoard;

        // Size of the board
        private const int GRID_WIDTH = 8;
        private const int GRID_HEIGHT = 6;


        /// <summary>
        /// Default constructor
        /// </summary>
        public BattleGridModel()
        {
            // Set up player board
            PlayerBoard = new BattleGridCellModel[GRID_WIDTH, GRID_HEIGHT];
        }

        public bool SetPlayerLocation(DungeonFighterModel player, Tuple<int, int> location)
        {
            (int x, int y) = location;
            if (PlayerBoard[x, y].IsEmpty())
            {
                PlayerBoard[x, y].SetPlayer(player);
                return true;
            }
            return false;
        }

    }
}
