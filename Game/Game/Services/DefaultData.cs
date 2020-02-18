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
                    Name = "Doctor Letter",
                    Description = "a file used to write a note on a given patient to prove that they were indeed sick",
                    ImageURI = "https://iconbug.com/data/74/256/54e923381a3dae598a53b9287f415137.png",
                    Range = 0,
                    Damage = 9,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Calculator",
                    Description = "something used for making mathematical calculations",
                    ImageURI = "https://lh3.googleusercontent.com/proxy/3nEsnJtzGXTp2EU3dTot-EcZKWVq6cY2P1hM7a3KM5d0ATjJXhHT75hMCGTjXCau3dEPVHdKAzAxeevO3Ov2wZKBhinYN3Z_JMSYULbKZM7GpUFPVm8yYhsFuoD8j3eR-Gb_ljTJ8GbXR5nCWmdvmccL",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Energy Drink",
                    Description = "a drink that provides mental and physical stimulation",
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
                    Name = "Wiki Expert",
                    MaxHealth = 20,
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
                    MaxHealth = 10,
                    Level = 1,
                    Description = "No one ever saw him in the class. Does he/she even exist?",
                    ImageURI = "https://previews.123rf.com/images/feferoni/feferoni0803/feferoni080300032/2752612-invisible-man-concept.jpg",
                    DefenseAttribute = 7,
                    OffenseAttribute = 4,
                    SpeedAttribute = 3,
                    Skill = CreatureSkillEnum.Slacker,
                },

                 new CharacterModel {
                    Name = "Straight A",
                    MaxHealth = 10,
                    Level = 20,
                    Description = "Finish every assignment with perfect grade. Instructor's favorite",
                    ImageURI = "https://previews.123rf.com/images/hermandesign2015/hermandesign20151704/hermandesign2015170400338/77046434-young-boy-scientist.jpg",
                    DefenseAttribute = 10,
                    OffenseAttribute = 10,
                    SpeedAttribute = 10,
                    Skill = CreatureSkillEnum.Bookworm,
                },

                 new CharacterModel {
                    Name = "The Delinquent",
                    MaxHealth = 20,
                    Level = 1,
                    Description = "THe mischief class skipper. Low in defense but high in attack",
                    ImageURI = "https://icon2.cleanpng.com/20190628/pzy/kisspng-vector-graphics-graffiti-drawing-illustration-clip-juvenile-delinquent-creates-graffiti-vector-imag-5d15fb87863623.8126502415617217355497.jpg",
                    DefenseAttribute = 1,
                    OffenseAttribute = 10,
                    SpeedAttribute = 15,
                    Skill = CreatureSkillEnum.None,
                }


            };

            return datalist;
        }

        /// <summary>
        /// Load the Default character data
        /// </summary>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {

            var dropItems = new List<ItemModel>()
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
            };

            var datalist = new List<MonsterModel>()
            {
                new MonsterModel
                {
                Name = "The Coronavirus",
                MaxHealth = 20,
                Level = 1,
                Description = "Human disaster",
                ImageURI = "https://pngimg.com/uploads/coronavirus/coronavirus_PNG33.png",
                DefenseAttribute = 1,
                OffenseAttribute = 10,
                SpeedAttribute = 15,
                Skill = CreatureSkillEnum.Boss,
                DropItems = dropItems
                },

                new MonsterModel
                {
                Name = "Needy Boyfriend",
                MaxHealth = 100,
                Level = 2,
                Description = "A demanding, and annoying boyfriend",
                ImageURI = "https://i.ya-webdesign.com/images/poor-drawing-animated-1.png",
                DefenseAttribute = 10,
                OffenseAttribute = 4,
                SpeedAttribute = 10,
                Skill = CreatureSkillEnum.PersonalIssue,
                DropItems = dropItems
                },

                new MonsterModel
                {
                Name = "Netflix Series",
                MaxHealth = 20,
                Level = 4,
                Description = "Never-ending Netflix Series",
                ImageURI = "https://lh3.googleusercontent.com/proxy/yy2ps4Ec1L3hMcbyN9rdVLcZALxywhbundLTqBodZjIAtq1BggIb6vcmOEZsciQq0P4G1E3HDSLT0nL3arSmAVpvQ-YE9R8FuFuB3jmHsTgTBdEnku8",
                DefenseAttribute = 9,
                OffenseAttribute = 3,
                SpeedAttribute = 6,
                Skill = CreatureSkillEnum.PersonalIssue,
                DropItems = dropItems
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