﻿using Game.Services;

namespace Game.Models
{
    /// <summary>
    /// Item for the Game
    /// 
    /// The Items that a character can use, a Monster may drop, or may be randomly available.
    /// The items are stored in the DB, and during game time a random item is selected.
    /// The system supports CRUDi operatoins on the items
    /// When in test mode, a test set of items is loaded
    /// When in run mode the items from from the database
    /// When in online mode, the items come from an api call to a webservice
    /// 
    /// When characters or monsters die, they drop items into the Items Pool for the Battle
    /// 
    /// </summary>
    public class ItemModel : BaseModel<ItemModel>
    {
        //Kind of the item: combat, ranged, armor
        public ItemKindEnum Kind{ get;set; } = ItemKindEnum.Unknown;

        // Range of the item, swords are 1, hats/rings are 0, bows are >1
        public int Range { get; set; } = 0;

        // The Damage the Item can do if it is used as a weapon in the primary hand
        public int Damage { get; set; } = 0;

        // Enum of the different attributes that the item modifies, Items can only modify one item
        public AttributeEnum Attribute { get; set; } = AttributeEnum.Unknown;

        // Where the Item goes on the character.  Head, Foot etc.
        public ItemLocationEnum Location { get; set; } = ItemLocationEnum.Unknown;

        // The Value item modifies.  So a ring of Health +3, has a Value of 3
        public int Value { get; set; } = 0;

        // Add Unique attributes for Item

        /// <summary>
        /// Default ItemModel
        /// Establish the Default Image Path
        /// </summary>
        public ItemModel() {
            ImageURI = ItemService.DefaultImageURI;
        }

        /// <summary>
        /// Constructor to create an item based on what is passed in
        /// </summary>
        /// <param name="data"></param>
        public ItemModel(ItemModel data)
        {
            Update(data);
        }

        /// <summary>
        /// Update the Record
        /// </summary>
        /// <param name="newData">The new data</param>
        public override void Update(ItemModel newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id and guid
            Name = newData.Name;
            Description = newData.Description;
            Value = newData.Value;
            Attribute = newData.Attribute;
            Location = newData.Location;
            ImageURI = newData.ImageURI;
            Damage = newData.Damage;
            Kind = newData.Kind;

            //if ranged weapon, update the range by user's request
            //or it will be 1 for CombatWeapon or 0 for Armor(Accessories)
            if (Kind == ItemKindEnum.RangedWeapon)
            {
                Range = newData.Range;
            }
            else if (Kind == ItemKindEnum.CombatWeapon)
            {
                Range = 1;
            }
            else
            {
                Range = 0;
            }
        }

        // Helper to combine the attributes into a single line, to make it easier to display the item as a string
        public string FormatOutput()
        {
            var myReturn = Name + " , " +
                            Description + " for " +
                            Location.ToString() + " with " +
                            Attribute.ToString() +
                            "+" + Value + " , " +
                            "Damage : " + Damage + " , " +
                            "Range : " + Range;

            return myReturn.Trim();
        }
    }
}