using NUnit.Framework;

using Game.Engine;
using Game.Models;
using Game.Helpers;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

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


            //Act
            var result = Engine.startBattle();

            //Reset
            DiceHelper.DisableForcedRolls();

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}