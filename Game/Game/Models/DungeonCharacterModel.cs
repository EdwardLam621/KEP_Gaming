using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class DungeonCharacterModel : CreatureModel<DungeonCharacterModel>
    {

        private int Level;
        private HashSet<ItemModel> Equipment;

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
            CurrentHealth = MaxHealth;

            SpeedAttribute = character.SpeedAttribute;
            OffenseAttribute = character.OffenseAttribute;
            DefenseAttribute = character.DefenseAttribute;

            Skill = character.Skill;
            Equipment = character.Equipments;

        }
    }
}
