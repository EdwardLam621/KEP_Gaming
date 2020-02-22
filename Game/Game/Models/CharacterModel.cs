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

        // Head, Body, Primary Hand, Off Hand, Left Finger, Right Finger, Feet,


        #region Items
        // ItemModel is a string referencing the database table
        public string Head { get; set; } = null;

        // Feet is a string referencing the database table
        public string Feet { get; set; } = null;

        // Body is a string referencing the database table
        public string Body { get; set; } = null;

        // PrimaryHand is a string referencing the database table
        public string PrimaryHand { get; set; } = null;

        // Offhand is a string referencing the database table
        public string OffHand { get; set; } = null;

        // RightFinger is a string referencing the database table
        public string RightFinger { get; set; } = null;

        // LeftFinger is a string referencing the database table
        public string LeftFinger { get; set; } = null;
        #endregion Items

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

            Head = newData.Head;
            Feet = newData.Feet;
            Body = newData.Body;
            RightFinger = newData.RightFinger;
            LeftFinger = newData.LeftFinger;
            PrimaryHand = newData.PrimaryHand;
            OffHand = newData.OffHand;

        }

    }
}
