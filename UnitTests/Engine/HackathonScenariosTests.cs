using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using Game.Helpers;
using System.Linq;
using Game.ViewModels;
using System.Collections.Generic;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        //AutoBattleEngine AutoBattleEngine;
        BattleEngine BattleEngine;

        [SetUp]
        public void Setup()
        {
            BattleEngine = EngineViewModel.Engine;
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void HakathonScenario_Constructor_0_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = BattleEngine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task HackathonScenario_Scenario_1_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            BattleEngine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new CharacterModel
            {
                SpeedAttribute = -1, // Will go last...
                Level = 1,
                CurrentHealth = 1,
                ExperiencePoints = 1,
                Name = "Mike",
            };


            // Make list of players
            var playerList = new List<CharacterModel>();
            
            // Add Mike to player list
            playerList.Add(CharacterPlayerMike);

            // Give player list to BattleEngine
            BattleEngine.SetParty(playerList);

            // Enable auto battle
            BattleEngine.SetAutoBattle(true);

            // Set Monster Conditions

            // (Auto Battle will add the monsters)

            //Act
            var result = BattleEngine.startBattle();

            //Reset
            BattleEngine.Referee.AutoBattleEnabled = false;
            BattleEngine.NewRound();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, BattleEngine.Referee.Characters.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, BattleEngine.Referee.BattleScore.RoundCount);
        }

        [Test]
        public void HackathonScenario_Scenario_2_Character_Bob_Should_Miss()
        {
            /* 
             * Scenario Number:  
             *  2
             *  
             * Description: 
             *      Make a Character called Bob
             *      Bob Always Misses
             *      Other Characters Always Hit
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Turn Engine
             *      Changed TurnAsAttack method
             *      Check for Name of Bob and return miss
             *                 
             * Test Algrorithm:
             *  Create Character named Bob
             *  Create Monster
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with Character of Named Bob
             *  Test with Character of any other name
             * 
             * Validation:
             *      Verify Enum is Miss
             *  
             */

            //Arrange

            // Set Character Conditions


            // Create Bob
            var CharacterPlayerBob = new CharacterModel
            {
                SpeedAttribute = 200,
                Level = 10,
                CurrentHealth = 100,
                ExperiencePoints = 100,
                Name = "Bob",
            };

            // Create new player list
            var playerList = new List<CharacterModel>();

            // Add Bob to the list of players
            playerList.Add(CharacterPlayerBob);

            // Give list to BattleEngine
            BattleEngine.SetParty(playerList);


            // Set Monster Conditions

            // Add a monster to attack

            var MonsterPlayer = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperiencePoints = 1,
                    Name = "Monster",
                });

            // Remove the automatically added monsters from the RoundEngine
            BattleEngine.CurrentRound.MonsterList.Clear();

            // Add this monster instead
            BattleEngine.CurrentRound.MonsterList.Add(MonsterPlayer);

            // Have dice rull 19
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(19);

            // Set up turn

            // Choose Bob
            BattleEngine.CurrentRound.CurrentPlayer = BattleEngine.Referee.Characters.FirstOrDefault();

            // Choose Monster
            BattleEngine.CurrentRound.Target = BattleEngine.Referee.Monsters.FirstOrDefault();

            //Act
            var result = BattleEngine.CurrentRound.TakeTurn(Game.Models.Enum.TurnChoiceEnum.Attack);

            //Reset
            DiceHelper.DisableForcedRolls();
            BattleEngine.NewRound();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Miss, BattleEngine.Referee.BattleMessages.HitStatus);
        }

        [Test]
        public void HackathonScenario_Scenario_2_Character_Not_Bob_Should_Hit()
        {
            /* 
             * Scenario Number:  
             *      2
             *      
             * Description: 
             *      See Default Test
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      See Defualt Test
             *                 
             * Test Algrorithm:
             *      Create Character named Mike
             *      Create Monster
             *      Call TurnAsAttack so Mike can attack Monster
             * 
             * Test Conditions:
             *      Control Dice roll so natural hit
             *      Test with Character of not named Bob
             *  
             *  Validation
             *      Verify Enum is Hit
             *      
             */

            //Arrange


            // Set Character Conditions

            var CharacterPlayerMike = new CharacterModel
            {
                SpeedAttribute = 200,
                Level = 10,
                CurrentHealth = 100,
                ExperiencePoints = 100,
                Name = "Mike",
            };

            // Make new player list
            var playerList = new List<CharacterModel>();
            
            // Add Mike
            playerList.Add(CharacterPlayerMike);

            // Give player list to BattleEngine
            BattleEngine.SetParty(playerList);

            // Set Monster Conditions

            // Add a monster to attack

            var MonsterPlayer = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperiencePoints = 1,
                    Name = "Monster",
                });


            // Remove auto added monsters
            BattleEngine.CurrentRound.MonsterList.Clear();

            // Add this monster instead
            BattleEngine.CurrentRound.MonsterList.Add(MonsterPlayer);

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            // Choose Not Bob
            BattleEngine.CurrentRound.CurrentPlayer = BattleEngine.Referee.Characters.FirstOrDefault();

            // Choose Monster
            BattleEngine.CurrentRound.Target = BattleEngine.Referee.Monsters.FirstOrDefault();

            //Act
            var result = BattleEngine.CurrentRound.TakeTurn(Game.Models.Enum.TurnChoiceEnum.Attack);

            //Reset
            DiceHelper.DisableForcedRolls();
            BattleEngine.NewRound();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Hit, BattleEngine.Referee.BattleMessages.HitStatus);
        }
        public void HackathonScenario_Scenario_15_Slower_Character_Moves_First()
        {

            /* 
             * Scenario Number:  
             *  15
             *  
             * Description: 
             *      Make a character and a monster, Character has higher speed than monster
             *      Time warp boolean
             *      If true character acts first, if false monster moves first
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Turn Engine
             *      Changed TurnAsAttack method
             *      Check for Name of Bob and return miss
             *                 
             * Test Algrorithm:
             *  Create Character with higher speed attribute (20)
             *  Create Monster with lower speed attribute (1)
             *  Call TurnAsAttack
             * 
             * Test Conditions:
             *  Test with Character health
             *  Test with Monster health
             * 
             * Validation:
             *      Verify monster has taken first move
             *  
             */


            // set a character with speed attribute 20
            var CharacterPlayerMike = new CharacterModel
            {
                SpeedAttribute = 20,
                Level = 1,
                CurrentHealth = 100,
                ExperiencePoints = 100,
                Name = "Mike",
            };

            
            var playerList = new List<CharacterModel>();
            playerList.Add(CharacterPlayerMike);

            // set a monster with speed attribute 1
            var MonsterPlayer = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperiencePoints = 1,
                    Name = "Monster",
                });

            // Remove auto added monsters
            BattleEngine.Referee.Monsters.Clear();

            // Add this monster instead
            BattleEngine.Referee.Monsters.Add(MonsterPlayer);

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            // Choose only character in party
            BattleEngine.CurrentRound.CurrentPlayer = BattleEngine.Referee.Characters.FirstOrDefault();

            // Choose only monster as enemy
            BattleEngine.CurrentRound.Target = BattleEngine.Referee.Monsters.FirstOrDefault();

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            //Act
            var result = BattleEngine.CurrentRound.TakeTurn(Game.Models.Enum.TurnChoiceEnum.Attack);

            //Reset
            DiceHelper.DisableForcedRolls();
            BattleEngine.NewRound();

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Hit, BattleEngine.Referee.BattleMessages.HitStatus);
        }


    }
}