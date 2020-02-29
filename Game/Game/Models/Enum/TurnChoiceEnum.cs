using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models.Enum
{
    /// <summary>
    /// List of choices 
    /// </summary>
    public enum TurnChoiceEnum
    {
        // Unknown
        Unknown = 0,

        // Choose monster to attack
        Attack = 1, 
        
        // Choose to move around the board
        Move = 2, 

        // Choose to use a skill
        Skill = 3,
    }
}
