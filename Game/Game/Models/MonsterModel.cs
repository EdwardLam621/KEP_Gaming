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
            DropItems = new List<ItemModel>();
        }

        public ItemModel GetDropItems(string id)
        {
            return DropItems.Find(x => x.Id == id);
        }


        //method to update a monster
        public void Update(MonsterModel newData)
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
            Description = newData.Description;
            SpeedAttribute = newData.SpeedAttribute;
            OffenseAttribute = newData.OffenseAttribute;
            DefenseAttribute = newData.DefenseAttribute;
            ImageURI = newData.ImageURI;
            Skill = newData.Skill;
            DropItems = newData.DropItems;
        }
    }
}
