using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class DungeonCharacterModel : CreatureModel<DungeonCharacterModel>
    {

        /// <summary>
        /// Current health of the character
        /// </summary>
        public int CurrentHealth { get; set; } = 0;
        
        /// <summary>
        /// Currently equipped items
        /// </summary>
        public HashSet<ItemModel> Equipment;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public DungeonCharacterModel()
        {

        }

        /// <summary>
        /// Constructor based on CharacterModel
        /// </summary>
        /// <param name="character"></param>
        public DungeonCharacterModel(CharacterModel character)
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
    }
}
