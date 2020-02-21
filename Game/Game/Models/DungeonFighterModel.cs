using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class DungeonFighterModel : CreatureModel<DungeonFighterModel>
    {

        private HashSet<ItemModel> Equipment;
        private List<ItemModel> DropItems;

        /// <summary>
        /// Current health of the character
        /// </summary>
        public int CurrentHealth { get; set; } = 0;
        

        /// <summary>
        /// Empty constructor
        /// </summary>
        public DungeonFighterModel()
        {

        }

        /// <summary>
        /// Constructor based on CharacterModel
        /// </summary>
        /// <param name="character"></param>
        public DungeonFighterModel(CharacterModel character)
        {
            ImageURI = character.ImageURI;
            
            Id = character.Id;
            Name = character.Name;
            Description = character.Description;
            
            Level = character.Level;
            MaxHealth = character.MaxHealth;
            SpeedAttribute = character.SpeedAttribute;
            OffenseAttribute = character.OffenseAttribute;
            DefenseAttribute = character.DefenseAttribute;
            Skill = character.Skill;
            
            Equipment = character.Equipment;
            CurrentHealth = MaxHealth;
        }

        /// <summary>
        /// Constructor based on MonsterModel
        /// </summary>
        /// <param name="character"></param>
        public DungeonFighterModel(MonsterModel monster)
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
