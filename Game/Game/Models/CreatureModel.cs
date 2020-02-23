using System;
using System.Collections.Generic;
using System.Text;
using Game.Services;
using SQLite;

namespace Game.Models
{
    /// <summary>
    /// Creature Model of the game
    /// 
    /// Base class of Creature to be inherited by Character and Monster
    /// </summary>
    public class CreatureModel<T> : BaseModel<T>
    {
        #region Attributes

        #region GameEngineAttributes
        // alive status, !alive will be removed from the list
        [Ignore]
        public bool Alive { get; set; } = true;

        // The type of player, character comes before monster
        [Ignore]
        public CreatureSkillEnum PlayerType { get; set; } = CreatureSkillEnum.None;

        // TurnOrder
        [Ignore]
        public int Order { get; set; } = 0;

        // Remember who was first into the list...
        [Ignore]
        public int ListOrder { get; set; } = 0;

        #endregion GameEngineAttributes
        //max health of a creature
        public int MaxHealth { get; set; } = 0;
        //Spped value of a creature
        public int SpeedAttribute { get; set; } = 0;
        //Attack value of a creature
        public int OffenseAttribute { get; set; } = 0;
        //Defense value of a creature
        public int DefenseAttribute { get; set; } = 0;
        //Level of a creature
        public int Level { get; set; } = 0;
        //Skill of a creature
        public CreatureSkillEnum Skill { get; set; } = CreatureSkillEnum.None;
        //experience points player has used
        public int ExperiencePoints { get; set; } = 0;

        #endregion Attributes

        // Add Unique attributes for Item

        /// <summary>
        /// Default MonsterModel
        /// Establish the Default Image Path
        /// </summary>
        public CreatureModel()
        {
            ImageURI = CharacterService.DefaultImageURI;
        }

        /// <summary>
        /// Constructor to create an item based on what is passed in
        /// </summary>
        /// <param name="data"></param>
        public CreatureModel(T data)
        {
            Update(data);
        }

        /// <summary>
        /// Update the Record
        /// </summary>
        /// <param name="newData">The new data</param>
        public override void Update(T newData)
        {
            if (newData == null)
            {
                return;
            }
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var myReturn = Name + " , " +
                            Description + " for speed " + SpeedAttribute +
                            " and offense " + OffenseAttribute +
                            " and defense " + DefenseAttribute +
                            "which its type is " + Skill;


            return myReturn.Trim();
        }

        /// <summary>
        /// Get the total attack power of the fighter
        /// </summary>
        /// <returns></returns>
        public int GetAttack()
        {
            return 0; // unimplemented
        }

        /// <summary>
        /// Get the total defense of the fighter
        /// </summary>
        /// <returns></returns>
        public int GetDefense()
        {
            return 0; // unimplemented
        }

        /// <summary>
        /// Get the total Speed of the fighter
        /// </summary>
        /// <returns></returns>
        public int GetSpeed()
        {
            return 0; // unimplemented
        }

    }
}
