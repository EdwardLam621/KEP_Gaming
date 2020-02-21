using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    /// <summary>
    /// Character model
    /// </summary>
    public class CharacterModel : CreatureModel<CharacterModel>
    {
        // Current equipment
        public HashSet<ItemModel> Equipment;

        //constructor of the class
        public CharacterModel()
        {
            this.Name = "this is Name";
            this.Description = "this is Character Description";
        }

        //methods to update a character
        public override void Update(CharacterModel newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Level = newData.Level;
            MaxHealth = newData.MaxHealth;
            Description = newData.Description;
            SpeedAttribute = newData.SpeedAttribute;
            OffenseAttribute = newData.OffenseAttribute;
            DefenseAttribute = newData.DefenseAttribute;
            ImageURI = newData.ImageURI;
            Skill = newData.Skill;
            Equipment = newData.Equipment;
        }

    }
}
