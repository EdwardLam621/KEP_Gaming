using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class MonsterModel : CreatureModel<MonsterModel>
    {
        public List<ItemModel> DropItems;

        public MonsterModel()
        {
            this.Name = "this is Name";
            this.Description = "this is Monster Description";
            this.CurrentHealth = MaxHealth;
        }

        public int Level { get; set; }

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
