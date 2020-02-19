using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class MonsterModel : CreatureModel<MonsterModel>
    {
        //list of a drop items of a monster
        public List<ItemModel> DropItems;

        //constructor of Monster Model class
        public MonsterModel()
        {
            this.Name = "this is Name";
            this.Description = "this is Monster Description";
            this.CurrentHealth = MaxHealth;
        }

        //level of a monster
        public int Level { get; set; }

        //method to update a monster
        public override void Update(MonsterModel newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Level = newData.Level;
            DropItems = newData.DropItems;
            MaxHealth = newData.MaxHealth;
            CurrentHealth = MaxHealth;
            Description = newData.Description;
            SpeedAttribute = newData.SpeedAttribute;
            OffenseAttribute = newData.OffenseAttribute;
            DefenseAttribute = newData.DefenseAttribute;
            ImageURI = newData.ImageURI;
            Skill = newData.Skill;
        }
    }
}
