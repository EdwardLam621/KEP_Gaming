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


            var playerList = new List<CharacterModel>();
            playerList.Add(CharacterPlayerMike);

            BattleEngine.SetParty(playerList);

            BattleEngine.SetAutoBattle(true);

            // Set Monster Conditions

            // Auto Battle will add the monsters


            //Act
            var result = BattleEngine.startBattle();

            //Reset
            BattleEngine.Referee.AutoBattleEnabled = false;

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


            BattleEngine.NewRound();

            var CharacterPlayerBob = new CharacterModel
            {
                SpeedAttribute = 200,
                Level = 10,
                CurrentHealth = 100,
                ExperiencePoints = 100,
                Name = "Bob",
            };

            // Add Bob to the list of players
            var playerList = new List<CharacterModel>();
            playerList.Add(CharacterPlayerBob);

            // Convert to DungeonFighter
            EngineViewModel.Engine.SetParty(playerList);

               
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
            BattleEngine.Referee.Monsters.Clear();

            // Add this monster instead
            BattleEngine.Referee.Monsters.Add(MonsterPlayer);

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


            var playerList = new List<CharacterModel>();
            playerList.Add(CharacterPlayerMike);

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
            BattleEngine.Referee.Monsters.Clear();

            // Add this monster instead
            BattleEngine.Referee.Monsters.Add(MonsterPlayer);

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

        [Test]
        public void HackathonScenario_Scenario_5_Character_Critical_Hits_Should_Double_Damage()
        {
            /* 
             * Scenario Number:  
             *      5
             *      
             * Description: 
             *      The attacker will automatically hit and cause double damage
             *      if the tohit roll is a natural 20 
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      TurnEngine:
             *          Create bool to enable critical hit
             *          TurnAsAttack method: added critical hit case
             *      HitStatusEnum: added status for critical hit
             *                 
             * Test Algrorithm:
             *      Create an character BigBoy with attack score = 10
             *      Create a monster with defense score = 1 (so the monsters's
             *      defense is always < character's attack score
             *      Let the character BigBoy rolls 20 and attack monster
             * 
             * Test Conditions:
             *      Check if monster get double damage from character
             *  
             *  Validation
             *      Verify monster gets double damage compared to the damage
             *      amount the character rolls
             *      
             */

            //Arrange


            // Set Character Conditions

            var CharacterPlayerBigBoy = new CharacterModel
            {
                SpeedAttribute = 200,
                Level = 10,
                CurrentHealth = 100,
                ExperiencePoints = 100,
                Name = "BigBoy",
            };


            var playerList = new List<CharacterModel>();
            playerList.Add(CharacterPlayerBigBoy);

            BattleEngine.SetParty(playerList);

            // Set Monster Conditions
            var MonsterPlayer = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 1,
                    Level = 1,
                    CurrentHealth = 100,
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

            // Choose BigBoy
            BattleEngine.CurrentRound.CurrentPlayer = BattleEngine.Referee.Characters.FirstOrDefault();

            // Choose Monster
            BattleEngine.CurrentRound.Target = BattleEngine.Referee.Monsters.FirstOrDefault();

            //Act
            var result = BattleEngine.CurrentRound.TakeTurn(Game.Models.Enum.TurnChoiceEnum.Attack);

            //Reset
            DiceHelper.DisableForcedRolls();
            BattleEngine.NewRound();

            //Assert
            Assert.AreEqual(BattleEngine.Referee.BattleMessages.DamageAmount * 2, 100 - BattleEngine.Referee.Monsters.FirstOrDefault().CurrentHealth);
        }
    }
}