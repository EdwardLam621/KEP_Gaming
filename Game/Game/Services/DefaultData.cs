using Game.Models;
using System.Collections.Generic;

namespace Game.Services
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "Gold Sword",
                    Description = "Sword made of Gold, really expensive looking",
                    ImageURI = "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png",
                    Range = 0,
                    Damage = 9,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Strong Shield",
                    Description = "Enough to hide behind",
                    ImageURI = "http://www.clipartbest.com/cliparts/4T9/LaR/4T9LaReTE.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Bunny Hat",
                    Description = "Pink hat with fluffy ears",
                    ImageURI = "http://www.clipartbest.com/cliparts/yik/e9k/yike9kMyT.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed},
            };

            return datalist;
        }

        /// <summary>
        /// Load the Default character data
        /// </summary>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Default Character",
                    Health = 10,
                    Level = 1,
                    Description = "Default description",
                    ImageURI = "http://www.clker.com/cliparts/e/L/A/m/I/c/sword-md.png",
                    DefenseAttribute = 5,
                    OffenseAttribute = 5,
                    SpeedAttribute = 1,
                    Skill = CreatureSkillEnum.None,
                },

                 new CharacterModel {
                    Name = "Wiki Expert",
                    Health = 20,
                    Level = 1,
                    Description = "Search Wikipedia for everything!",
                    ImageURI = "https://cn.i.cdn.ti-platform.com/cnapac/content/438/showpage/teen-titans-go%21/sa/showicon.png",
                    DefenseAttribute = 10,
                    OffenseAttribute = 1,
                    SpeedAttribute = 8,
                    Skill = CreatureSkillEnum.TeachersPet,
                },

                 new CharacterModel {
                    Name = "Unknown",
                    Health = 10,
                    Level = 1,
                    Description = "No one ever saw him in the class. Does he/she even exist?",
                    ImageURI = "https://cn.i.cdn.ti-platform.com/cnapac/content/438/showpage/teen-titans-go%21/sa/showicon.png",
                    DefenseAttribute = 7,
                    OffenseAttribute = 4,
                    SpeedAttribute = 3,
                    Skill = CreatureSkillEnum.Slacker,
                },

                 new CharacterModel {
                    Name = "Straight A",
                    Health = 10,
                    Level = 20,
                    Description = "Finish every assignment with perfect grade. Instructor's favorite",
                    ImageURI = "https://cn.i.cdn.ti-platform.com/cnapac/content/438/showpage/teen-titans-go%21/sa/showicon.png",
                    DefenseAttribute = 10,
                    OffenseAttribute = 10,
                    SpeedAttribute = 10,
                    Skill = CreatureSkillEnum.Bookworm,
                }


            };

            return datalist;
        }

        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

            return datalist;
        }
    }
}