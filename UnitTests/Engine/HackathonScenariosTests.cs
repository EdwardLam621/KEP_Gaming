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



        [Test]
        public void HackathonScenario_Scenario_9_Character_Revived_After_Death()
        {
            /* 
             * Scenario Number:  
             *  9
             * 
             * Description: 
             *  
             *  Miracle Max steps in and helps characters who are Mostly Dead avoid Total Death.
             *  If the damaged received would make the currentHealth points Zero or below, then 
             *  Miracle Max steps in and helps out. One time per battle, a character may be revived 
             *  by magic to their maxhealth. This happens instead of death. Because this is allowed 
             *  only one time per battle per character, they can have more fun storming the castle.
             *  
             *  Miracle Max loves it when you advertise his business in the output window
             *      
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      AboutPage: Add debug switch for resurrections
             *      CreatureModel: Add field for resurrection count
             *      TurnEngine
             *      
             *                 
             * Test Algrorithm:
             *  Create Character with 1 health and -1 speed
             *  Create Monster with level 20 and 10 speed
             *  Call Turn, check if character is alive
             *  Call Turn again, check if character is dead
             * 
             * Test Conditions:
             *  Test with Character health
             * 
             * Validation:
             *      Verify character death count has increased
             *      Verify character revive count has increased
             *      Verify character cannot revive more than once
             *  
             */


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

            // Create strong monster
            var MonsterPlayer = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 10,
                    Level = 20,
                    CurrentHealth = 100,
                    ExperiencePoints = 100,
                    Name = "Monster",
                });

            // Remove auto added monsters
            BattleEngine.CurrentRound.MonsterList.Clear();

            // Add this monster instead
            BattleEngine.CurrentRound.MonsterList.Add(MonsterPlayer);
            
            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            // Choose Character
            BattleEngine.CurrentRound.CurrentPlayer = BattleEngine.Referee.Characters.FirstOrDefault();

            // Choose Monster
            BattleEngine.CurrentRound.Target = BattleEngine.Referee.Monsters.FirstOrDefault();

            //Act
            var result = BattleEngine.CurrentRound.TakeTurn(Game.Models.Enum.TurnChoiceEnum.Attack);

            // Assert Mike not dead

            //Reset
            DiceHelper.DisableForcedRolls();
            BattleEngine.NewRound();

            
        }




        [Test]
        public void HackathonScenario_Scenario_15_If_TimeWarp_True_Slower_Character_Moves_First()
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
             *      Change to Round Engine
             *      Changed OrderFight method
             *      Check for First fighter's name
             *                 
             * Test Algrorithm:
             *  Create Character with higher speed attribute (200)
             *  Create Monster with lower speed attribute (1)
             *  Order FighterList and see who moves first
             * 
             * Test Conditions:
             *  Test Current Figher's name
             * 
             * Validation:
             *      Verify Current player's name is Monster's name "ABC"
             *  
             */


            // Set Character Conditions

            var CharacterPlayerMike = new CharacterModel

            {
                SpeedAttribute = 200,
                Level = 10,
                MaxHealth = 100,
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
                    CurrentHealth = 5,
                    ExperiencePoints = 1,
                    Name = "ABC",
                });


            // Remove the automatically added monsters from the RoundEngine
            BattleEngine.CurrentRound.MonsterList.Clear();
            
            // Add this monster instead
            BattleEngine.CurrentRound.MonsterList.Add(MonsterPlayer);

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            //set timewarp true
            BattleEngine.CurrentRound.TimeWarp = true;
            BattleEngine.CurrentRound.OrderFighters();

            // Choose only character in party
            BattleEngine.CurrentRound.CurrentPlayer = BattleEngine.CurrentRound.FighterList.FirstOrDefault();

            //After sort the first player's name should be monster
            Assert.IsTrue(BattleEngine.CurrentRound.CurrentPlayer.Name.Equals("ABC"));
        }

        [Test]
        public void HackathonScenario_Scenario_15_If_TimeWarp_False_Faster_Character_Moves_First()
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
             *      Change to Round Engine
             *      Changed OrderFight method
             *      Check for First fighter's name
             *                 
             * Test Algrorithm:
             *  Create Character with higher speed attribute (200)
             *  Create Monster with lower speed attribute (1)
             *  Order FighterList and see who moves first
             * 
             * Test Conditions:
             *  Test Current Figher's name
             * 
             * Validation:
             *      Verify Mike has taken first move
             *  
             */


            // Set Character Conditions

            var CharacterPlayerMike = new CharacterModel

            {
                SpeedAttribute = 200,
                Level = 10,
                MaxHealth = 100,
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
                    CurrentHealth = 5,
                    ExperiencePoints = 1,
                    Name = "ABC",
                });


            // Remove the automatically added monsters from the RoundEngine
            BattleEngine.CurrentRound.MonsterList.Clear();

            // Add this monster instead
            BattleEngine.CurrentRound.MonsterList.Add(MonsterPlayer);

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            //set timewarp true
            BattleEngine.CurrentRound.TimeWarp = false;
            BattleEngine.CurrentRound.OrderFighters();

            // Choose only character in party
            BattleEngine.CurrentRound.CurrentPlayer = BattleEngine.CurrentRound.FighterList.FirstOrDefault();

            //After sort the first player's name should be monster
            Assert.IsTrue(BattleEngine.CurrentRound.CurrentPlayer.Name.Equals("Mike"));
        }


        [Test]
        public void HackathonScenario_Scenario_5_Critical_Hit_Enable_Should_Double_Damage()
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
             *          TurnAsAttack method: added critical hit case where target takes double damage amount
             *      HitStatusEnum: added status for critical hit
             *                 
             * Test Algrorithm:
             *      Create an character BigBoy
             *      Create a monster 
             *      Enable critical hit
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

            //enable critical hit
            TurnEngine.criticalHitEnable = true;

            //do not enable autobattle
            BattleEngine.Referee.AutoBattleEnabled = false;

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
            MonsterPlayer.CurrentHealth = 100;

            //Act
            var result = BattleEngine.CurrentRound.TakeTurn(Game.Models.Enum.TurnChoiceEnum.Attack);

            //Reset
            TurnEngine.criticalHitEnable = false;
            DiceHelper.DisableForcedRolls();
            BattleEngine.NewRound();

            //Assert
            Assert.AreEqual(result, true);
            Assert.AreEqual(BattleEngine.Referee.BattleMessages.DamageAmount * 2, (100 - MonsterPlayer.CurrentHealth));
        }

        [Test]
        public void HackathonScenario_Scenario_5_Critical_Hit_Not_Enable_Roll_20_Always_Hit_()
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
             *          TurnAsAttack method: added critical hit case where target takes double damage amount
             *      HitStatusEnum: added status for critical hit
             *                 
             * Test Algrorithm:
             *      Create an character BigBoy
             *      Create a monster
             *      Disable Critical Hit
             *      Let the character BigBoy rolls 20 and attack monster
             * 
             * Test Conditions:
             *      Check if monster get hit, but not double damage from character
             *  
             *  Validation
             *      Verify monster gets hit with the right damage amount
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

            //enable critical hit
            TurnEngine.criticalHitEnable = false;

            //do not enable autobattle
            BattleEngine.Referee.AutoBattleEnabled = false;

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
            MonsterPlayer.CurrentHealth = 100;

            //Act
            var result = BattleEngine.CurrentRound.TakeTurn(Game.Models.Enum.TurnChoiceEnum.Attack);

            //Reset
            TurnEngine.criticalHitEnable = false;
            DiceHelper.DisableForcedRolls();
            BattleEngine.NewRound();

            //Assert
            Assert.AreEqual(result, true);
            Assert.AreEqual(BattleEngine.Referee.BattleMessages.DamageAmount, (100 - MonsterPlayer.CurrentHealth));
        }

        [Test]
        public void HackathonScenario_Scenario_16_Monster_Becomes_Zombie()
        {
            /* 
             * Scenario Number:  
             *      16
             *      
             * Description: 
             *      When a monster is killed, it returns from the dead and continue attacking as a zombie monster
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Added a switch to enable zombie mode to Turn Engine
             *      Add default percentage for a monster to return to live as a zombie = 20%
             *      Added check condition to RemoveIfDie in Turn Engine
             *          When a monster is killed:
             *          Roll a random number from 1 to 100, if number is < 20%, monster is back to life
             *          Else, monster is dead
             *                 
             * Test Algrorithm:
             *      Create an character 
             *      Create a monster
             *      Set % of monster return to live = 100%
             *      Let character attacks monster, and monster is killed after the attack  
             * 
             * Test Conditions:
             *      With 100% chance to return to live, check if monster is still alive
             *      after being killed
             *  
             *  Validation
             *      Verify monster is alive (alive = true)
             *      Verify a new HP = 1/2 original HP
             *      Verify new name = Zombie Monster
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

            //added character to character list
            var playerList = new List<CharacterModel>();
            playerList.Add(CharacterPlayerBigBoy);

            BattleEngine.SetParty(playerList);

            // Set Monster Conditions with health = 1
            var MonsterPlayer = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 1,
                    Level = 1,
                    CurrentHealth = 1,
                    ExperiencePoints = 1,
                    Name = "Monster",
                    MaxHealth = 100
                }) ;

            // Remove auto added monsters
            BattleEngine.Referee.Monsters.Clear();

            // Add this monster instead
            BattleEngine.Referee.Monsters.Add(MonsterPlayer);

            //do not enable autobattle
            BattleEngine.Referee.AutoBattleEnabled = false;

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            // Choose BigBoy
            BattleEngine.CurrentRound.CurrentPlayer = BattleEngine.Referee.Characters.FirstOrDefault();

            // Choose Monster
            BattleEngine.CurrentRound.Target = BattleEngine.Referee.Monsters.FirstOrDefault();

            //set monster health = 1 so it will die after next attack
            MonsterPlayer.CurrentHealth = 1;

            //enable feature that monster can return to live as zombies
            TurnEngine.zombieMonstersEnable = true;

            //set % of return to live of target of this round = 100%
            TurnEngine.returnToLiveAsZombie = 100;

            //Act
            var result = BattleEngine.CurrentRound.TakeTurn(Game.Models.Enum.TurnChoiceEnum.Attack);

            //Reset
            DiceHelper.DisableForcedRolls();
            BattleEngine.NewRound();

            //Assert
            Assert.AreEqual("Zombie Monster", MonsterPlayer.Name);
            Assert.AreEqual(50, MonsterPlayer.CurrentHealth);
            Assert.AreEqual(true, MonsterPlayer.Alive);
        }

        [Test]
        public void HackathonScenario_Scenario_32_If_RoundCount_Is_Times_Of_5_Grandma_Goes_First_True()
        {

            /* 
             * Scenario Number:  
             *  32
             *  
             * Description:
             *      Every 5th round, the sort order for turn order changes and list is sorted by Characters first, 
             *      then lowest health, then lowest speed
             * 
             *      Make 3 characters and 3 monsters,
             *      CharacterA has 100 health and 2 speed  but since it has lowest health in character so it goes first
             *      CharacterB has 200 health and 1 speed  same health with CharacterC but lower speed so it goes faster
             *      CharacterC has 200 health and 3 speed  but since it is character it should go third
             *      Monster A has 100 health and 4 speed  but since it has lowest health in monster so it goes fourth
             *      Monster B has 200 health and 5 speed  same health with CharacterC but lower speed so it goes fifth
             *      MonsterC has 200 health and 6 speed  but it has higher speed so it goes last
             *      Normal: Monster C > Monster A > Monster B > Character C > Character A > Character B
             *      Round in times of 5: Character A > Character B > Character C > Monster A > Monster B > Monster C
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Round Engine
             *      Changed OrderFight method
             *      Check for First fighter's name
             *                 
             * Test Algrorithm:
             *      Create three characters
             *      Create three monster
             *      Check for the orders.
             * 
             * Test Conditions:
             *      Test Fighters' name in order
             * 
             * Validation:
             *      Verify order of characters' name is MonsterB > Character > MonsterA
             *  
             */


            // Set Character Conditions

            var CharacterPlayerA = new CharacterModel

            {
                SpeedAttribute = 2,
                Level = 10,
                MaxHealth = 100,
                CurrentHealth = 100,
                ExperiencePoints = 100,
                Name = "CharacterA",
            };

            var CharacterPlayerB = new CharacterModel

            {
                SpeedAttribute = 1,
                Level = 10,
                MaxHealth = 200,
                CurrentHealth = 100,
                ExperiencePoints = 100,
                Name = "CharacterB",
            };

            var CharacterPlayerC = new CharacterModel

            {
                SpeedAttribute = 3,
                Level = 10,
                MaxHealth = 200,
                CurrentHealth = 100,
                ExperiencePoints = 100,
                Name = "CharacterC",
            };

            // Make new player list
            var playerList = new List<CharacterModel>();

            // Add Characters
            playerList.Add(CharacterPlayerA);
            playerList.Add(CharacterPlayerB);
            playerList.Add(CharacterPlayerC);

            // Give player list to BattleEngine
            BattleEngine.SetParty(playerList);

            // Set Monster Conditions

            // Add a monster to attack

            var MonsterPlayerA = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 4,
                    Level = 10,
                    CurrentHealth = 100,
                    ExperiencePoints = 1,
                    Name = "MonsterA",
                });
            var MonsterPlayerB = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 5,
                    Level = 10,
                    CurrentHealth = 200,
                    ExperiencePoints = 1,
                    Name = "MonsterB",
                });

            var MonsterPlayerC = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 6,
                    Level = 1,
                    CurrentHealth = 200,
                    ExperiencePoints = 1,
                    Name = "MonsterC",
                });

            //Set current round count = 5
            BattleEngine.CurrentRound.RoundCount = 5;

            // Remove the automatically added monsters from the RoundEngine
            BattleEngine.CurrentRound.MonsterList.Clear();
            BattleEngine.CurrentRound.FighterList.Clear();

            // Add this monster instead
            BattleEngine.CurrentRound.MonsterList.Add(MonsterPlayerA);
            BattleEngine.CurrentRound.MonsterList.Add(MonsterPlayerB);
            BattleEngine.CurrentRound.MonsterList.Add(MonsterPlayerC);

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            BattleEngine.CurrentRound.OrderFighters();

            // Choose only character in party
            BattleEngine.CurrentRound.CurrentPlayer = BattleEngine.CurrentRound.FighterList.FirstOrDefault();

            //After sort the first player's name should be monster
            Assert.IsTrue(BattleEngine.CurrentRound.FighterList[0].Name.Equals("CharacterA"));
            Assert.IsTrue(BattleEngine.CurrentRound.FighterList[1].Name.Equals("CharacterB"));
            Assert.IsTrue(BattleEngine.CurrentRound.FighterList[2].Name.Equals("CharacterC"));
            Assert.IsTrue(BattleEngine.CurrentRound.FighterList[3].Name.Equals("MonsterA"));
            Assert.IsTrue(BattleEngine.CurrentRound.FighterList[4].Name.Equals("MonsterB"));
            Assert.IsTrue(BattleEngine.CurrentRound.FighterList[5].Name.Equals("MonsterC"));
        }

        [Test]
        public void HackathonScenario_Scenario_32_If_RoundCount_Is_Not_Times_Of_5_Grandma_Goes_First_False()
        {

            /* 
             * Scenario Number:  
             *  32
             *  
             * Description:
             *      Every 5th round, the sort order for turn order changes and list is sorted by Characters first, 
             *      then lowest health, then lowest speed
             * 
             *      Make one characters and two monsters,
             *      Character has 100 health and 2 speed  but since it is character it should go first
             *      Monster A has only 1 speed but 1 health, so it moves 2nd place
             *      Monster B has 50 speed and 50 health, so it moves third
             *      Normal: Monster B > Character > Monster A
             *      Round in 5th Character> Monster A > Monster B
             * 
             * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
             *      Change to Round Engine
             *      Changed OrderFight method
             *      Check for First fighter's name
             *                 
             * Test Algrorithm:
             *      Create a character
             *      Create two monster, one with higher speed and health and one with lowers.
             *      Check for the orders.
             * 
             * Test Conditions:
             *      Test Fighters' name in order
             * 
             * Validation:
             *      Verify order of characters' name is MonsterB > Character > MonsterA
             *  
             */


            // Set Character Conditions

            var CharacterPlayerMike = new CharacterModel

            {
                SpeedAttribute = 2,
                Level = 10,
                MaxHealth = 100,
                CurrentHealth = 100,
                ExperiencePoints = 100,
                Name = "Character",
            };

            // Make new player list
            var playerList = new List<CharacterModel>();

            // Add Mike
            playerList.Add(CharacterPlayerMike);

            // Give player list to BattleEngine
            BattleEngine.SetParty(playerList);

            // Set Monster Conditions

            // Add a monster to attack

            var MonsterPlayerA = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 1,
                    Level = 1,
                    CurrentHealth = 5,
                    ExperiencePoints = 1,
                    Name = "MonsterA",
                });
            var MonsterPlayerB = new DungeonFighterModel(
                new MonsterModel
                {
                    SpeedAttribute = 50,
                    Level = 1,
                    CurrentHealth = 50,
                    ExperiencePoints = 1,
                    Name = "MonsterB",
                });

            //Set current round count = 5
            BattleEngine.CurrentRound.RoundCount = 1;

            // Remove the automatically added monsters from the RoundEngine
            BattleEngine.CurrentRound.MonsterList.Clear();
            BattleEngine.CurrentRound.FighterList.Clear();

            // Add this monster instead
            BattleEngine.CurrentRound.MonsterList.Add(MonsterPlayerA);
            BattleEngine.CurrentRound.MonsterList.Add(MonsterPlayerB);

            // Have dice roll 20
            DiceHelper.EnableForcedRolls();
            DiceHelper.SetForcedRollValue(20);

            BattleEngine.CurrentRound.OrderFighters();

            // Choose only character in party
            BattleEngine.CurrentRound.CurrentPlayer = BattleEngine.CurrentRound.FighterList.FirstOrDefault();

            //After sort the first player's name should be monster
            Assert.IsTrue(BattleEngine.CurrentRound.FighterList[0].Name.Equals("MonsterB"));
            Assert.IsTrue(BattleEngine.CurrentRound.FighterList[1].Name.Equals("Character"));
            Assert.IsTrue(BattleEngine.CurrentRound.FighterList[2].Name.Equals("MonsterA"));
        }

    }
}