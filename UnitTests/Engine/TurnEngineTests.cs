using NUnit.Framework;

using System.Linq;
using System.Collections.Generic;

using Game.Engine;
using Game.Models;
using Game.Helpers;
using Game.ViewModels;

namespace UnitTests.Engine
{
    [TestFixture]
    public class TurnEngineTests
    {
        TurnEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new TurnEngine();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void TurnEngine_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TurnEngine_Attack_Valid_Empty_Monster_List_Should_Fail()
        {
            // Arrange
            var PlayerInfo = new CharacterModel();

            Engine.Target = null;

            // Act
            var result = Engine.Attack(new DungeonFighterModel(PlayerInfo));

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_Attack_InValid_Empty_Character_List_Should_Fail()
        {
            // Arrange

            // Cause an error by making the list empty
            Engine.Attacker = null;

            // Act
            var result = Engine.Attack(Engine.Attacker);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}