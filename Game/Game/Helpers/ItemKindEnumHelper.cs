using System;
using System.Collections.Generic;
using Game.Models;
using System.Linq;


namespace Game.Helpers
{
    class ItemKindEnumHelper
    {
        /// <summary>
        /// Gets the list of locations that an Item can have.
        /// Does not include the Left and Right Finger 
        /// </summary>
        public static List<string> GetListItem
        {
            get
            {
                var myList = Enum.GetNames(typeof(ItemKindEnumHelper)).ToList();
                var myReturn = myList.Where(a =>
                                            a.ToString() != ItemKindEnum.Armor.ToString() &&
                                            a.ToString() != ItemKindEnum.CombatWeapon.ToString() &&
                                            a.ToString() != ItemKindEnum.RangedWeapon.ToString()
                                            )
                                            .OrderBy(a => a)
                                            .ToList();
                return myReturn;
            }
        }

        /// <summary>
        ///  Gets the list of locations a character can use
        ///  Removes Finger for example, and allows for left and right finger
        /// </summary>
        public static List<string> GetListCharacter
        {
            get
            {
                var myList = Enum.GetNames(typeof(ItemKindEnumHelper)).ToList();
                var myReturn = myList.Where(a =>
                                           a.ToString() != ItemKindEnum.Armor.ToString() &&
                                            a.ToString() != ItemKindEnum.RangedWeapon.ToString() &&
                                            a.ToString() != ItemKindEnum.CombatWeapon.ToString()
                                            )
                                            .OrderBy(a => a)
                                            .ToList();

                return myReturn;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ItemKindEnumHelper ConvertStringToEnum(string value)
        {
            return (ItemKindEnumHelper)Enum.Parse(typeof(ItemKindEnumHelper), value);
        }

        /// <summary>
        /// If asked for a position number, return a location.  Head as 1 etc. 
        /// This compsenstates for the enum #s not being sequential, but allows for calls to the postion for random allocation etc (roll 1-7 dice and pick a item to equipt), etc... 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static ItemKindEnum GetLocationByPosition(int position)
        {
            switch (position)
            {
                case 1:
                    return ItemKindEnum.RangedWeapon;

                case 2:
                    return ItemKindEnum.CombatWeapon;

                case 3:
                default:
                    return ItemKindEnum.Armor;

               
            }
        }
    }
}
