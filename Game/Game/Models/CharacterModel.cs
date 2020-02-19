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
        //List of equipments
        public List<ItemModel> Equipments;

        //constructor of the class
        public CharacterModel()
        {
            this.Name = "this is Name";
            this.Description = "this is Character Description";
        }
        //level of character
        public int Level { get; set; }

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
            CurrentHealth = MaxHealth;
            Description = newData.Description;
            SpeedAttribute = newData.SpeedAttribute;
            OffenseAttribute = newData.OffenseAttribute;
            DefenseAttribute = newData.DefenseAttribute;
            ImageURI = newData.ImageURI;
            Skill = newData.Skill;
            Equipments = newData.Equipments;
        }

    }
}
