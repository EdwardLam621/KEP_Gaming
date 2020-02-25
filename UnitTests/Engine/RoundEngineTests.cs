using NUnit.Framework;

using Game.Engine;
using Game.Models;
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
            Engine = new RoundEngine(Referee);
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
        }
    }
}