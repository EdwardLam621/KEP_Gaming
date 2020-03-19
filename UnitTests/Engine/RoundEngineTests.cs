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
            Assert.IsNotNull(result.FighterList);
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
            Assert.AreEqual(engine.Referee.Monsters.Count(), 0);
            Assert.AreEqual(engine.Referee.ItemPool.Count(), 0);
            Assert.AreEqual(result, true);
        }


        [Test]
        public void RoundEngine_OrderFighters_Character_Must_Go_First()
        {
            // Arrange

            // Act
            var engine = Engine;
            DungeonFighterModel player1 = new DungeonFighterModel();
            DungeonFighterModel player2 = new DungeonFighterModel();
            engine.Referee.Characters.Add(player1);
            engine.MonsterList.Add(player2);
            engine.OrderFighters();

            // Reset

            // Assert
            Assert.AreEqual(engine.FighterList[0], player1);
            Assert.AreEqual(engine.FighterList[1], player2);
        }
    }

}