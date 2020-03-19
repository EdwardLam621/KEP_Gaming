using NUnit.Framework;

using Game.Engine;
using Game.Models;
using Game.Helpers;
using Game.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace UnitTests.Engine
{
    [TestFixture]
    public class AutoBattleEngineTests
    {
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();

            Engine.Referee.AutoBattleEnabled = true;
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void AutoBattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AutoBattleEngine_ScoreTotal_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.Referee.BattleScore.ScoreTotal;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AutoBattleEngine_RunAutoBattle_Default_Should_Pass()
        {
            //Arrange
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(3);

            var characterList = new List<CharacterModel>();

            var data = new CharacterModel { Level = 1, MaxHealth = 10 };

            characterList.Add(new CharacterModel(data));
            characterList.Add(new CharacterModel(data));
            characterList.Add(new CharacterModel(data));
            characterList.Add(new CharacterModel(data));
            characterList.Add(new CharacterModel(data));
            characterList.Add(new CharacterModel(data));

            Engine.Referee.SetParty(characterList);


            //Act
            var result = Engine.startBattle();

            //Reset
            DiceHelper.DisableForcedRolls();


            //Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void AutoBattleEngine_CreateCharacterParty_Characters_Should_Assign_6()
        {
            //Arrange
            Engine.MaxNumberPartyCharacters = 6;

            var data1 = new CharacterModel { Name = "name 1" };
            var data2 = new CharacterModel { Name = "name 2" };
            var data3 = new CharacterModel { Name = "name 3" };
            var data4 = new CharacterModel { Name = "name 4" };
            var data5 = new CharacterModel { Name = "name 5" };
            var data6 = new CharacterModel { Name = "name 6" };

            var characterList = new List<CharacterModel>();

            characterList.Add(new CharacterModel(data1));
            characterList.Add(new CharacterModel(data2));
            characterList.Add(new CharacterModel(data3));
            characterList.Add(new CharacterModel(data4));
            characterList.Add(new CharacterModel(data5));
            characterList.Add(new CharacterModel(data6));

            //Act
            Engine.Referee.SetParty(characterList);

            //Reset

            //Assert
            Assert.AreEqual(6, Engine.Referee.Characters.Count());
            Assert.AreEqual("name 6", Engine.Referee.Characters.ElementAt(5).Name);
        }


    }

    
}