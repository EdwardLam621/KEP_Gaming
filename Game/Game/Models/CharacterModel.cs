using System;
using System.Collections.Generic;
using Game.ViewModels;
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

        // Head, Body, Primary Hand, Off Hand, Left Finger, Right Finger, Feet
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

        public CharacterModel(CharacterModel data)
        {
            Update(data);
        }

        public ItemModel GetItem(string itemString)
        {
            return ItemIndexViewModel.Instance.GetItem(itemString);
        }

        public ItemModel GetItemByLocation(ItemLocationEnum itemLocation)
        {
            switch (itemLocation)
            {
                case ItemLocationEnum.Head:
                    return GetItem(Head);

                case ItemLocationEnum.Body:
                    return GetItem(Body);

                case ItemLocationEnum.PrimaryHand:
                    return GetItem(PrimaryHand);

                case ItemLocationEnum.OffHand:
                    return GetItem(OffHand);

                case ItemLocationEnum.RightFinger:
                    return GetItem(RightFinger);

                case ItemLocationEnum.LeftFinger:
                    return GetItem(LeftFinger);

                case ItemLocationEnum.Feet:
                    return GetItem(Feet);
            }

            return null;
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


        // Add ItemModel
        // Looks up the ItemModel
        // Puts the ItemModel ID as a string in the location slot
        // If ItemModel is null, then puts null in the slot
        // Returns the ItemModel that was in the location
        public ItemModel AddItem(ItemLocationEnum itemLocation, string itemID)
        {
            var myReturn = GetItemByLocation(itemLocation);

            switch (itemLocation)
            {
                case ItemLocationEnum.Feet:
                    Feet = itemID;
                    break;

                case ItemLocationEnum.Head:
                    Head = itemID;
                    break;

                case ItemLocationEnum.Body:
                    Body = itemID;
                    break;

                case ItemLocationEnum.PrimaryHand:
                    PrimaryHand = itemID;
                    break;

                case ItemLocationEnum.OffHand:
                    OffHand = itemID;
                    break;

                case ItemLocationEnum.RightFinger:
                    RightFinger = itemID;
                    break;

                case ItemLocationEnum.LeftFinger:
                    LeftFinger = itemID;
                    break;

                default:
                    myReturn = null;
                    break;
            }

            return myReturn;
        }

        
    }
}
