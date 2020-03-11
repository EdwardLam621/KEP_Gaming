using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class DungeonFighterModel : CreatureModel<DungeonFighterModel>
    {

        /// <summary>
        /// For heroes, the set of equipment that is currently equipped
        /// </summary>
        private HashSet<ItemModel> Equipment;
        
        /// <summary>
        /// For mobs, the list of items that are able to be dropped
        /// </summary>
        private List<ItemModel> DropItems;
        
        /// <summary>
        /// Whether the Player is alive or dead
        /// </summary>
        public bool Alive { get; set; } = true;
        
        /// <summary>
        /// Keep track of the turn order
        /// </summary>
        public int ListOrder = 0;

        public CreatureEnum PlayerType { get; set; } = CreatureEnum.Unknown;

        public CreatureEnum PlayerCreatureType { get; set; } = CreatureEnum.Unknown;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public DungeonFighterModel()
        {

        }

        /// <summary>
        /// Copy from one PlayerInfoModel into Another
        /// </summary>
        /// <param name="data"></param>
        public DungeonFighterModel(DungeonFighterModel data)
        {
            ImageURI = data.ImageURI;

            Id = data.Id;
            Name = data.Name;
            Description = data.Description;

            Level = data.Level;
            CurrentHealth = data.CurrentHealth;
            MaxHealth = data.MaxHealth;
            SpeedAttribute = data.SpeedAttribute;
            OffenseAttribute = data.OffenseAttribute;
            DefenseAttribute = data.DefenseAttribute;
            Skill = data.Skill;
            
            Equipment = data.Equipment;
            CurrentHealth = MaxHealth;
            PlayerType = data.PlayerType;
            
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
            PlayerType = CreatureEnum.Character;
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
            PlayerType = CreatureEnum.Monster;
        }
    }
}
