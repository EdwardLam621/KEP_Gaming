using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class CharacterModel : CreatureModel<CharacterModel>
    {
        public List<ItemModel> Equipments;


        public override void Update(CharacterModel newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Health = newData.Health;
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
