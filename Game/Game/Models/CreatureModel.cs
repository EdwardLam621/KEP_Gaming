using System;
using System.Collections.Generic;
using System.Text;
using Game.Services;

namespace Game.Models
{
    /// <summary>
    /// Creature Model of the game
    /// 
    /// Base class of Creature to be inherited by Character and Monster
    /// </summary>
    public class CreatureModel<T> : BaseModel<T>
    {
        //max health of a creature
        public int MaxHealth { get; set; } = 0;
        //current health of a creature
        public int CurrentHealth { get; set; } = 0;
        //Spped value of a creature
        public int SpeedAttribute { get; set; } = 0;
        //Attack value of a creature
        public int OffenseAttribute { get; set; } = 0;
        //Defense value of a creature
        public int DefenseAttribute { get; set; } = 0;
        //Skill of a creature
        public CreatureSkillEnum Skill { get; set; } = CreatureSkillEnum.None;

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
    }
}
