using Game.Models;
using Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    ImageURI = "item_doctor_letter.png",
                    Range = 1,
                    Damage = 5,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Doctor Prescription",
                    Description = "More convincing than Doctor Letter",
                    ImageURI = "item_doctor_prescription.png",
                    Range = 1,
                    Damage = 9,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Calculator",
                    Description = "something used for making mathematical calculations",
                    ImageURI = "item_calculator.png",
                    Range = 0,
                    Damage = 0,
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                 new ItemModel {
                    Name = "Rain Coat",
                    Description = "item_rain_coat.png",
                    ImageURI = "https://i.pinimg.com/originals/e8/14/71/e8147125c3fd38997fbd76f93aff0b2e.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.MaxHealth
                 },

                 new ItemModel {
                    Name = "Seahawks Cap",
                    Description = "Go Hawks!!",
                    ImageURI = "item_seahawks_cap.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense
                 },

                 new ItemModel {
                    Name = "Pandora",
                    Description = "$$$$$",
                    ImageURI = "item_pandora.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
                    Location = ItemLocationEnum.LeftFinger,
                    Attribute = AttributeEnum.Attack
                 },

                 new ItemModel {
                    Name = "Just a ring",
                    Description = "5 dollars from Amazon",
                    ImageURI = "item_just_a_ring.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
                    Location = ItemLocationEnum.RightFinger,
                    Attribute = AttributeEnum.Attack
                 },


                 new ItemModel {
                    Name = "Nike!",
                    Description = "Impossible is nothing",
                    ImageURI = "item_nike.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed
                 },

                 new ItemModel {
                    Name = "Super Pencil",
                    Description = "Easy to write and clean",
                    ImageURI = "item_super_pencil.png",
                    Range = 3,
                    Damage = 3,
                    Value = 5,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed
                 },

                  new ItemModel {
                    Name = "Optical Glasses",
                    Description = "Everyone can be Hawkeye now!",
                    ImageURI = "item_optical_glasses.png",
                    Range = 0,
                    Damage = 0,
                    Value = 7,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Attack
                 },

                 new ItemModel {
                    Name = "Laptop",
                    Description = "Take note faster, read faster, work faster!",
                    ImageURI = "item_laptop.png",
                    Range = 2,
                    Damage = 8,
                    Value = 8,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Speed
                 },

                 new ItemModel {
                    Name = "Leather Shoes",
                    Description = "Extra (impression) credit!",
                    ImageURI = "item_leather_shoes.png",
                    Range = 0,
                    Damage = 0,
                    Value = 7,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Speed
                 },
            };

            return datalist;
        }

        /// <summary>
        /// Load the Default character data
        /// </summary>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            string HeadString = null;
            string BodyString = null;
            string PrimaryHandString = null;
            string OffHandString = null;
            string FeetString = null;
            string RightFingerString = null;
            string LeftFingerString = null;

            try
            {
                HeadString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.Head).ElementAtOrDefault(0).Id;
            }
            catch (Exception e) { }

            try
            {
                BodyString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.Necklass).FirstOrDefault().Id;
            }
            catch (Exception e) { }

            try
            {
                PrimaryHandString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.PrimaryHand).FirstOrDefault().Id;
            }
            catch (Exception e) { }

            try
            {
                OffHandString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.OffHand).FirstOrDefault().Id;
            }
            catch (Exception e) { }

            try
            {
                FeetString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.Feet).FirstOrDefault().Id;
            }
            catch (Exception e) { }

            try
            {
                RightFingerString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.RightFinger).FirstOrDefault().Id;
            }
            catch (Exception e) { }

            try
            {
                LeftFingerString = ItemIndexViewModel.Instance.Dataset.Where(m => m.Location == ItemLocationEnum.LeftFinger).LastOrDefault().Id;
            }
            catch (Exception e) { }

            var datalist = new List<CharacterModel>()
            {

                 new CharacterModel {
                    Name = "Wiki Expert",
                    MaxHealth = 1,
                    Level = 1,
                    Description = "Search Wikipedia for everything!",
                    ImageURI = "character_wiki_expert.png",
                    DefenseAttribute = 20,
                    OffenseAttribute = 20,
                    SpeedAttribute = 20,
                    Skill = CreatureSkillEnum.TeachersPet,
                    //Equipments = equipments

                    Head = HeadString,
                    Body = BodyString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,

                },

                 new CharacterModel {
                    Name = "Unknown",
                    MaxHealth = 100,
                    Level = 1,
                    Description = "No one ever saw him in the class. Does he/she even exist?",
                    ImageURI = "character_unknown.png",
                    DefenseAttribute = 20,
                    OffenseAttribute = 20,
                    SpeedAttribute = 20,
                    Skill = CreatureSkillEnum.Slacker,
                    //Equipments = equipments

                },

                 new CharacterModel {
                    Name = "Straight A",
                    MaxHealth = 100,
                    Level = 20,
                    Description = "Finish every assignment with perfect grade. Instructor's favorite",
                    ImageURI = "character_straight_A.png",
                    DefenseAttribute = 20,
                    OffenseAttribute = 20,
                    SpeedAttribute = 1,
                    Skill = CreatureSkillEnum.Bookworm,
                    //Equipments = equipments
                },

                 new CharacterModel {
                    Name = "Delinquent",
                    MaxHealth = 200,
                    Level = 1,
                    Description = "The mischief class skipper. Low in defense but high in attack",
                    ImageURI = "character_Delinquent.png",
                    DefenseAttribute = 1,
                    OffenseAttribute = 10,
                    SpeedAttribute = 15,
                    Skill = CreatureSkillEnum.None,
                    //Equipments = equipments
                },

                 new CharacterModel {
                    Name = "Slacker",
                    MaxHealth = 300,
                    Level = 1,
                    Description = "A student who always avoid work of effort",
                    ImageURI = "character_slacker.png",
                    DefenseAttribute = 1,
                    OffenseAttribute = 10,
                    SpeedAttribute = 15,
                    Skill = CreatureSkillEnum.None,
                    //Equipments = equipments
                },

                new CharacterModel
                {
                    Name = "Teacher's pet",
                    MaxHealth = 400,
                    Level = 1,
                    Description = "a pupil who has won the teacher's special favor.",
                    ImageURI = "character_teachers_pet.png",
                    DefenseAttribute = 1,
                    OffenseAttribute = 15,
                    SpeedAttribute = 5,
                    Skill = CreatureSkillEnum.None,
                    //Equipments = equipments
                },

                new CharacterModel
                {
                    Name = "Daydreaming",
                    MaxHealth = 400,
                    Level = 1,
                    Description = "dreamer, escapist, wishful thinker. a person who escapes into a world of fantasy",
                    ImageURI = "character_daydreaming.png",
                    DefenseAttribute = 1,
                    OffenseAttribute = 15,
                    SpeedAttribute = 5,
                    Skill = CreatureSkillEnum.None,
                    //Equipments = equipments
                }


            };

            return datalist;
        }

        /// <summary>
        /// Load the Default monster data
        /// </summary>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            List<ItemModel> test = new List<ItemModel>();
            test.Add(ItemIndexViewModel.Instance.Dataset.FirstOrDefault());


            var datalist = new List<MonsterModel>()
            {
                new MonsterModel
                {
                Name = "The Coronavirus",
                MaxHealth = 5,
                Level = 1,
                Description = "Human disaster",
                ImageURI = "https://pngimg.com/uploads/coronavirus/coronavirus_PNG33.png",
                DefenseAttribute = 1,
                OffenseAttribute = 1,
                SpeedAttribute = 1,
                Skill = CreatureSkillEnum.Boss,
                DropItems = test,
                },

                new MonsterModel
                {
                Name = "Needy Boyfriend",
                MaxHealth = 5,
                Level = 2,
                Description = "A demanding, and annoying boyfriend",
                ImageURI = "https://i.ya-webdesign.com/images/poor-drawing-animated-1.png",
                DefenseAttribute = 1,
                OffenseAttribute = 4,
                SpeedAttribute = 10,
                Skill = CreatureSkillEnum.PersonalIssue,
                DropItems = test,
                },

                new MonsterModel
                {
                Name = "Netflix Series",
                MaxHealth = 5,
                Level = 4,
                Description = "Never-ending Netflix Series",
                ImageURI = "http://www.pngall.com/wp-content/uploads/4/Round-Netflix-Logo.png",
                DefenseAttribute = 1,
                OffenseAttribute = 3,
                SpeedAttribute = 6,
                Skill = CreatureSkillEnum.PersonalIssue,
                DropItems = test,
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
                    CharacterModelDeathList = new List<DungeonFighterModel>()
                    {
                        new DungeonFighterModel {
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

                         new DungeonFighterModel {
                            Name = "Unknown",
                            MaxHealth = 10,
                            Level = 1,
                            Description = "No one ever saw him in the class. Does he/she even exist?",
                            ImageURI = "https://ya-webdesign.com/images250_/thief-clipart-sneak-11.png",
                            DefenseAttribute = 7,
                            OffenseAttribute = 4,
                            SpeedAttribute = 3,
                            Skill = CreatureSkillEnum.Slacker,
                        },
                    }
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data 2",
                    CharacterModelDeathList = new List<DungeonFighterModel>()
                    {
                        new DungeonFighterModel {
                            Name = "Straight A",
                            MaxHealth = 10,
                            Level = 20,
                            Description = "Finish every assignment with perfect grade. Instructor's favorite",
                            ImageURI = "https://pngimage.net/wp-content/uploads/2018/06/thai-student-png-5.png",
                            DefenseAttribute = 10,
                            OffenseAttribute = 10,
                            SpeedAttribute = 10,
                            Skill = CreatureSkillEnum.Bookworm,
                        },

                         new DungeonFighterModel {
                            Name = "The Delinquent",
                            MaxHealth = 20,
                            Level = 1,
                            Description = "The mischief class skipper. Low in defense but high in attack",
                            ImageURI = "https://clipartart.com/images/sleeping-at-school-clipart.png",
                            DefenseAttribute = 1,
                            OffenseAttribute = 10,
                            SpeedAttribute = 15,
                            Skill = CreatureSkillEnum.None,
                        }
                    }
                },
            };

            return datalist;
        }
    }
}