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
            ExperienceTotal = data.ExperienceTotal;
            ExperienceRemaining = data.ExperienceRemaining;
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
            ExperienceTotal = character.ExperienceTotal;
            ExperienceRemaining = character.ExperienceRemaining;
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
            ExperienceTotal = monster.ExperienceTotal;
            ExperienceRemaining = monster.ExperienceRemaining;
            DropItems = monster.DropItems;
            CurrentHealth = MaxHealth;
            PlayerType = CreatureEnum.Monster;

        }

        public override List<ItemModel> DropAllItems()
        {
            var myReturn = new List<ItemModel>();

            // Drop all Items
            ItemModel myItem;

            switch (this.PlayerType)
            {

                case CreatureEnum.Character:
                    myItem = RemoveItem(ItemLocationEnum.Head);
                    if (myItem != null)
                    {
                        myReturn.Add(myItem);
                    }

                    myItem = RemoveItem(ItemLocationEnum.Necklass);
                    if (myItem != null)
                    {
                        myReturn.Add(myItem);
                    }

                    myItem = RemoveItem(ItemLocationEnum.PrimaryHand);
                    if (myItem != null)
                    {
                        myReturn.Add(myItem);
                    }

                    myItem = RemoveItem(ItemLocationEnum.OffHand);
                    if (myItem != null)
                    {
                        myReturn.Add(myItem);
                    }

                    myItem = RemoveItem(ItemLocationEnum.Finger);
                    if (myItem != null)
                    {
                        myReturn.Add(myItem);
                    }

                    myItem = RemoveItem(ItemLocationEnum.RightFinger);
                    if (myItem != null)
                    {
                        myReturn.Add(myItem);
                    }

                    myItem = RemoveItem(ItemLocationEnum.LeftFinger);
                    if (myItem != null)
                    {
                        myReturn.Add(myItem);
                    }

                    myItem = RemoveItem(ItemLocationEnum.Feet);
                    if (myItem != null)
                    {
                        myReturn.Add(myItem);
                    }

                    break;


                default:
                    foreach(var data in DropItems)
                    {
                        myReturn.Add(data);
                    }

                    break;
            }

            return myReturn;
        }

    }
}
