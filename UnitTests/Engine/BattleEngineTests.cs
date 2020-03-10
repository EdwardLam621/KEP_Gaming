using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests.Engine
{
    [TestFixture]
    public class BattleEngineTests
    {
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void BattleEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattleEngine_StartBattle_AutoModel_True_Should_Pass()
        {
            // Arrange

            // Act
            
            Engine.SetAutoBattle(true);
            var result = Engine.startBattle();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, Engine.Referee.BattleScore.AutoBattle);
        }


        [Test]
        public void BattleEngine_PopulateCharacterList_Should_Pass()
        {
            // Arrange
            var characterList = new List<CharacterModel>();
            characterList.Add(new CharacterModel());
            
            // Act
            var result = Engine.SetParty(characterList);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}