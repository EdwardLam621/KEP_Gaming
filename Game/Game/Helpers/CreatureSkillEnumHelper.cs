using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Game.Models;

namespace Game.Helpers
{
    class CreatureSkillEnumHelper
    {
        /// <summary>
        /// Gets the list of locations that an Item can have.
        /// Does not include the Left and Right Finger 
        /// </summary>
        public static List<string> GetListItem
        {
            get
            {
                var myList = Enum.GetNames(typeof(CreatureSkillEnum)).ToList();
                var myReturn = myList.OrderBy(a => a).ToList();
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
                var myList = Enum.GetNames(typeof(CreatureSkillEnum)).ToList();
                var myReturn = myList.OrderBy(a => a).ToList();

                return myReturn;
            }

        }

        public static List<string> GetListSkill
        {
            get
            {
                var myList = Enum.GetNames(typeof(CreatureSkillEnum)).ToList();
                var myReturn = myList.OrderBy(a => a).ToList();

                return myReturn;
            }

        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CreatureSkillEnum ConvertStringToEnum(string value)
        {
            return (CreatureSkillEnum)Enum.Parse(typeof(CreatureSkillEnum), value);
        }

        /// <summary> 
        /// This compsenstates for the enum #s not being sequential, but allows for calls to the postion for random allocation etc (roll 1-7 dice and pick a item to equipt), etc... 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static CreatureSkillEnum GetLocationByPosition(int position)
        {
            switch (position)
            {
                case 0:
                default:
                    return CreatureSkillEnum.None;
                case 1:
                    return CreatureSkillEnum.Bookworm;

                case 2:
                    return CreatureSkillEnum.Boss;

                case 3:
                    return CreatureSkillEnum.Slacker;

                case 4:
                    return CreatureSkillEnum.ClassClown;

                case 5:
                    return CreatureSkillEnum.Daydreamer;

                case 6:
                    return CreatureSkillEnum.TeachersPet;
            }
        }
    }
}
