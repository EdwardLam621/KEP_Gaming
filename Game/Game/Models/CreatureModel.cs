using System;
using System.Collections.Generic;
using System.Text;
using Game.Services;

namespace Game.Models
{
    public class CreatureModel<T> : BaseModel<T>
    {
        public int Health { get; set; }
        public int SpeedAttribute { get; set; }
        public int OffenseAttribute { get; set; }
        public int DefenseAttribute { get; set; }
        public CreatureSkillEnum Skill { get; set; }

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
