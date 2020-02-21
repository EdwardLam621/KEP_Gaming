using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class DungeonMonsterModel : MonsterModel
    {

        /// <summary>
        /// Current health of the monster
        /// </summary>
        public int CurrentHealth { get; set; } = 0;

        public DungeonMonsterModel()
        {

        }

        /// <summary>
        /// Constructor based on MonsterModel
        /// </summary>
        /// <param name="monster"></param>
        public DungeonMonsterModel(MonsterModel monster)
        {
            ImageURI = monster.ImageURI;

            Id = monster.Id;
            Name = monster.Name;
            Description = monster.Description;

            Level = monster.Level;
            MaxHealth = monster.MaxHealth;
            SpeedAttribute = monster.SpeedAttribute;
            OffenseAttribute = monster.OffenseAttribute;
            DefenseAttribute = monster.DefenseAttribute;
            Skill = monster.Skill;

            DropItems = monster.DropItems;
            CurrentHealth = MaxHealth;
        }

    }
}
