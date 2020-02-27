using System;
using System.Collections.Generic;
using System.Text;
using Game.Models.Enum;

namespace Game.Models
{
    /// <summary>
    /// Holds information relevant to a single cell in the BattleGrid, such as players
    /// </summary>
    public class BattleGridCellModel
    {
        // Whether the cell is occupied with a character or monster, or empty
        private BattleGridEnum CellStatus { get; set; } = BattleGridEnum.Empty;

    }
}
