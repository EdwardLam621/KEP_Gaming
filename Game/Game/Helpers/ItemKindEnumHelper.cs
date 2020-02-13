using System;
using System.Collections.Generic;
using Game.Models;
using System.Linq;


namespace Game.Helpers
{
    class ItemKindEnumHelper
    {
        /// <summary>
        /// Gets the list of kind that an Item can have.
        /// </summary>
        public static List<string> GetListItem
        {
            get
            {
                var myList = Enum.GetNames(typeof(ItemKindEnumHelper)).ToList();
                var myReturn = myList.OrderBy(a => a).ToList();
                return myReturn;
            }
        }

        /// <summary>
        ///  Gets the list of kind a character can use
        /// </summary>
        public static List<string> GetListCharacter
        {
            get
            {
                var myList = Enum.GetNames(typeof(ItemKindEnum)).ToList();
                var myReturn = myList.OrderBy(a => a).ToList();
                return myReturn;
            }
        }

       
    }
}
