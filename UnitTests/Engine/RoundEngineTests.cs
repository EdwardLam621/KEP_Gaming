using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Game.ViewModels;

namespace UnitTests.Engine
{
    [TestFixture]
    public class RoundEngineTests
    {
        RoundEngine Engine;
        RefereeModel Referee;

        [SetUp]
        public void Setup()
        {
            Referee = new RefereeModel();
            Referee.Monsters = new List<DungeonFighterModel>();
            Referee.Characters = new List<DungeonFighterModel>();
            Engine = new RoundEngine(Referee, 9);
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void RoundEngine_Constructor_With_Referee_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result.MonsterList);
            Assert.IsNotNull(result.PlayerList);
            Assert.AreEqual(result.RoundCount, 9);

        }

        [Test]
        public void RoundEngine_EndRound_Should_Pass()
        {
            // Arrange

            // Act
            var engine = Engine;
            bool result = engine.EndRound();

            // Reset

            // Assert
            Assert.AreEqual(engine.MonsterList.Count, 0);
            Assert.AreEqual(engine.PlayerList.Count, 0);
            Assert.AreEqual(result, true);
        }
    }
}