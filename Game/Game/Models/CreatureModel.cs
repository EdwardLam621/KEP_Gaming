using System;
using System.Collections.Generic;
using System.Text;
using Game.Services;
using Game.ViewModels;
using SQLite;

namespace Game.Models
{
    /// <summary>
    /// Creature Model of the game
    /// 
    /// Base class of Creature to be inherited by Character and Monster
    /// </summary>
    public class CreatureModel<T> : BaseModel<T>
    {
        #region Attributes

        #region GameEngineAttributes
        // alive status, not alive will be removed from the list
        [Ignore]
        public bool Alive { get; set; } = true;
        // The type of player, character comes before monster
        [Ignore]
        public CreatureSkillEnum PlayerType { get; set; } = CreatureSkillEnum.None;
        // TurnOrder
        [Ignore]
        public int Order { get; set; } = 0;
        // Remember who was first into the list...
        [Ignore]
        public int ListOrder { get; set; } = 0;
        #endregion GameEngineAttributes

        #region PlayerAttributes
        //max health of a creature
        public int MaxHealth { get; set; } = 0;
        //Spped value of a creature
        public int SpeedAttribute { get; set; } = 0;
        //Attack value of a creature
        public int OffenseAttribute { get; set; } = 0;
        //Defense value of a creature
        public int DefenseAttribute { get; set; } = 0;
        //Level of a creature
        public int Level { get; set; } = 0;
        //Skill of a creature
        public CreatureSkillEnum Skill { get; set; } = CreatureSkillEnum.None;
        //experience points player has used
        public int ExperiencePoints { get; set; } = 0;
        #endregion PlayerAttributes

        #endregion Attributes


        #region Items
        // Head is a string referencing the database table
        public string Head { get; set; } = null;
        // Feet is a string referencing the database table
        public string Body { get; set; } = null;
        // PrimaryHand is a string referencing the database table
        public string PrimaryHand { get; set; } = null;
        // Offhand is a string referencing the database table
        public string OffHand { get; set; } = null;
        // Finger is a string referencing the database table
        public string Finger { get; set; } = null;
        // RightFinger is a string referencing the database table
        public string RightFinger { get; set; } = null;
        // LeftFinger is a string referencing the database table
        public string LeftFinger { get; set; } = null;
        // Feet is a string referencing the database table
        public string Feet { get; set; } = null;
        #endregion Items


        #region AttributesDisplay

        #region Attack        
        [Ignore]
        // Return the attack value
        public int GetAttackLevelBonus { get { return LevelTableHelper.Instance.LevelDetailsList[Level].Attack; } }
        
        #endregion Attack

        #endregion AttributeDisplay

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

        #region Methods

        #region GetAttributesValue
        /// <summary>
        /// Get the total attack power of the fighter
        /// </summary>
        /// <returns></returns>
        public int GetAttack()
        {
            return 0; // unimplemented
        }

        /// <summary>
        /// Get the total defense of the fighter
        /// </summary>
        /// <returns></returns>
        public int GetDefense()
        {
            return 0; // unimplemented
        }

        /// <summary>
        /// Get the total Speed of the fighter
        /// </summary>
        /// <returns></returns>
        public int GetSpeed()
        {
            return 0; // unimplemented
        }
        #endregion GetAttributesValue

        #region Items
        // Get the Item at a known string location (head, foot etc.)
        public ItemModel GetItem(string itemString)
        {
            return ItemIndexViewModel.Instance.GetItem(itemString);
        }

        // Walk all the Items on the Character.
        // Add together all Items that modify the Attribute Enum Passed in
        // Return the sum
        public int GetItemBonus(AttributeEnum attributeEnum)
        {
            var myReturn = 0;
            ItemModel myItem;

            myItem = GetItem(Head);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(Body);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(PrimaryHand);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(OffHand);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(Finger);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(RightFinger);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(LeftFinger);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            myItem = GetItem(Feet);
            if (myItem != null)
            {
                if (myItem.Attribute == attributeEnum)
                {
                    myReturn += myItem.Value;
                }
            }

            return myReturn;
        }

        // Get the ItemModel at a known string location (head, foot etc.)
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

                case ItemLocationEnum.Finger:
                    return GetItem(Finger);

                case ItemLocationEnum.RightFinger:
                    return GetItem(RightFinger);

                case ItemLocationEnum.LeftFinger:
                    return GetItem(LeftFinger);

                case ItemLocationEnum.Feet:
                    return GetItem(Feet);
            }
            return null;
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

                case ItemLocationEnum.Finger:
                    Finger = itemID;
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

        // Remove ItemModel from a set location
        // Does this by adding a new ItemModel of Null to the location
        // This will return the previous ItemModel, and put null in its place
        // Returns the ItemModel that was at the location
        // Nulls out the location
        public ItemModel RemoveItem(ItemLocationEnum itemlocation)
        {
            var myReturn = AddItem(itemlocation, null);

            // Save Changes
            return myReturn;
        }

        // Drop All Items
        // Return a list of items for the pool of items
        public List<ItemModel> DropAllItems()
        {
            var myReturn = new List<ItemModel>();

            // Drop all Items
            ItemModel myItem;

            myItem = RemoveItem(ItemLocationEnum.Head);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Body);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.PrimaryHand);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.OffHand);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Finger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.RightFinger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.LeftFinger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Feet);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            return myReturn;
        }
        #endregion Items

        #endregion Methods

    }
}
