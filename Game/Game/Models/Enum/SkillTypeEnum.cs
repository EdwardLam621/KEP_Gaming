using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models.Enum
{
    /// <summary>
    /// Models which players a skill affects
    /// </summary>
    public enum SkillTypeEnum
    {
        // Unknown
        Unknown = 0, 
        
        // Affect a single enemy
        AttackOne = 1,

        // Affect all enemies
        AttackAll = 2, 

        // Affect one party member
        BuffOne = 3, 

        // Affect all party members
        BuffAll = 4,

    }
}
