using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models.Enum
{
    public enum BattleGridEnum
    {
        // Empty tile
        Empty = 0, 

        // Tile holding a character
        HasCharacter = 1, 
        
        // Tile holding a monster
        HasMonster = 2,

    }
}
