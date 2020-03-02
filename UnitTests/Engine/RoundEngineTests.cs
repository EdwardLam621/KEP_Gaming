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
            Assert.AreEqual(engine.MonsterList.Count, 0);
            Assert.AreEqual(engine.FighterList.Count, 0);
            Assert.AreEqual(result, true);
        }

        [Test]
        public void RoundEngine_RoundNextTurn_Should_Return_GameOver()
        {
            // Arrange

            // Act
            var engine = Engine;
            RoundEnum result = engine.TakeAutoTurn();

            // Reset

            // Assert
            Assert.AreEqual(result, RoundEnum.GameOver);
        }

        [Test]
        public void RoundEngine_RoundNextTurn_Should_Return_NewRound()
        {
            // Arrange

            // Act
            var engine = Engine;
            engine.Referee.Characters.Add(new DungeonFighterModel());
            RoundEnum result = engine.TakeAutoTurn();

            // Reset

            // Assert
            Assert.AreEqual(result, RoundEnum.NewRound);
        }

        [Test]
        public void RoundEngine_RoundNextTurn_Should_Return_NextTurn()
        {
            // Arrange

            // Act
            var engine = Engine;
            engine.Referee.Characters.Add(new DungeonFighterModel());
            engine.MonsterList.Add(new DungeonFighterModel());
            engine.FighterList.Add(new DungeonFighterModel());
            RoundEnum result = engine.TakeAutoTurn();

            // Reset

            // Assert
            Assert.AreEqual(result, RoundEnum.NextTurn);
        }

        [Test]
        public void RoundEngine_OrderFight_Sort_By_Level_Should_Pass()
        {
            // Arrange

            // Act
            var engine = Engine;
            DungeonFighterModel player1 = new DungeonFighterModel();
            player1.SpeedAttribute = 10;
            DungeonFighterModel player2 = new DungeonFighterModel();
            player2.SpeedAttribute = 5;
            engine.FighterList.Add(player2);
            engine.FighterList.Add(player1);
            engine.OrderFight();

            // Reset

            // Assert
            Assert.AreEqual(engine.FighterList[0], player1);
            Assert.AreEqual(engine.FighterList[1], player2);
        }
    }

}