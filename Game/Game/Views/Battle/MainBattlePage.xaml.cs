using System;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainBattlePage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        public Engine.RoundEngine CurrentRound;

        // HTML Formatting for message output box
        public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        // Wait time before proceeding
        public int WaitTime = 1500;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainBattlePage()
        {
            InitializeComponent();

            // Set up the UI to Defaults
            BindingContext = EngineViewModel;

            // Start the Battle Engine
            EngineViewModel.Engine.NewRound();

            // Load current round into local namespace
            CurrentRound = EngineViewModel.Engine.CurrentRound;

            // Todo! 
            // Show the New Round Screen
            //ShowModalNewRoundPage();


            // Ask the Game engine to select who goes first
            // Game Starts with No Attacker or Defender selected
            CurrentRound.CurrentPlayer = null;

            // Add Players to Display
            DrawGameAttackerDefenderBoard();

            HideUIElements();

            StartBattleButton.IsVisible = true;
        }

        /// <summary>
        /// 
        /// Hide the differnt button states
        /// 
        /// Hide the message display box
        /// 
        /// </summary>
        public void HideUIElements()
        {
            NextRoundButton.IsVisible = false;
            StartBattleButton.IsVisible = false;
            AttackButton.IsVisible = false;
            MessageDisplayBox.IsVisible = false;
            BattlePlayerInfomationBox.IsVisible = false;
        }

        /// <summary>
        /// Draw the Player Boxes
        /// </summary>
        public void DrawPlayerBoxes()
        {
            var CharacterBoxList = CharacterBox.Children.ToList();
            foreach (var data in CharacterBoxList)
            {
                CharacterBox.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in EngineViewModel.Engine.CurrentRound.FighterList.Where(m => m.PlayerType == CreatureEnum.Character).ToList())
            {
                CharacterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            var MonsterBoxList = MonsterBox.Children.ToList();
            foreach (var data in MonsterBoxList)
            {
                MonsterBox.Children.Remove(data);
            }

            // Draw the Monsters
            foreach (var data in EngineViewModel.Engine.CurrentRound.FighterList.Where(m => m.PlayerType == CreatureEnum.Monster).ToList())
            {
                MonsterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            // Add one black PlayerInfoDisplayBox to hold space incase the list is empty
            CharacterBox.Children.Add(PlayerInfoDisplayBox(null));

            // Add one black PlayerInfoDisplayBox to hold space incase the list is empty
            MonsterBox.Children.Add(PlayerInfoDisplayBox(null));

        }

        /// <summary>
        /// Put the Player into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout PlayerInfoDisplayBox(DungeonFighterModel data)
        {
            if (data == null)
            {
                data = new DungeonFighterModel();
                data.ImageURI = "";
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleSmallStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["BattlePlayerInfoInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Draw the UI for
        ///
        /// Attacker vs Defender Mode
        /// 
        /// </summary>
        public void DrawGameAttackerDefenderBoard()
        {
            // Clear the current UI
            DrawGameBoardClear();

            // Show Characters across the Top
            DrawPlayerBoxes();

            // Show the Attacker and Defender
            DrawGameBoardAttackerDefenderSection();
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender
        /// </summary>
        public void DrawGameBoardAttackerDefenderSection()
        {
            BattlePlayerBoxVersus.Text = "Click to Begin";

            if (EngineViewModel.Engine.CurrentRound.CurrentPlayer == null)
            {
                return;
            }

            if (EngineViewModel.Engine.CurrentRound.Target == null)
            {
                return;
            }

            AttackerImage.Source = EngineViewModel.Engine.CurrentRound.CurrentPlayer.ImageURI;
            AttackerName.Text = EngineViewModel.Engine.CurrentRound.CurrentPlayer.Name;
            AttackerHealth.Text = EngineViewModel.Engine.CurrentRound.CurrentPlayer.GetCurrentHealthTotal.ToString() + " / " + EngineViewModel.Engine.CurrentRound.CurrentPlayer.GetMaxHealthTotal.ToString();

            DefenderImage.Source = EngineViewModel.Engine.CurrentRound.Target.ImageURI;
            DefenderName.Text = EngineViewModel.Engine.CurrentRound.Target.Name;
            DefenderHealth.Text = EngineViewModel.Engine.CurrentRound.Target.GetCurrentHealthTotal.ToString() + " / " + EngineViewModel.Engine.CurrentRound.Target.GetMaxHealthTotal.ToString();

            if (EngineViewModel.Engine.CurrentRound.Target.Alive == false)
            {
                DefenderImage.BackgroundColor = Color.Red;
            }

            BattlePlayerBoxVersus.Text = "vs";
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender areas to be null
        /// </summary>
        public void DrawGameBoardClear()
        {
            AttackerImage.Source = string.Empty;
            AttackerName.Text = string.Empty;
            AttackerHealth.Text = string.Empty;

            DefenderImage.Source = string.Empty;
            DefenderName.Text = string.Empty;
            DefenderHealth.Text = string.Empty;
            DefenderImage.BackgroundColor = Color.Transparent;

            BattlePlayerBoxVersus.Text = string.Empty;
        }

        /// <summary>
        /// Attack Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttackButton_Clicked(object sender, EventArgs e)
        {
            NextAttackExample();
        }

        /// <summary>
        /// Next Attack Example
        /// 
        /// This code example follows the rule of
        /// 
        /// Auto Select Attacker
        /// Auto Select Defender
        /// 
        /// Do the Attack and show the result
        /// 
        /// So the pattern is Click Next, Next, Next until game is over
        /// 
        /// </summary>
        public void NextAttackExample()
        {
            // Get the turn, set the current player and attacker to match
            SetAttackerAndDefender();

            EngineViewModel.Engine.CurrentRound.AttackClicked(EngineViewModel.Engine.CurrentRound.Target);
            // Hold the current state
            var RoundCondition = EngineViewModel.Engine.CurrentRound.GetRoundState();

            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            DrawGameAttackerDefenderBoard();

            if (RoundCondition == RoundEnum.NewRound)
            {
                // Pause
                Task.Delay(WaitTime);

                Debug.WriteLine("New Round");

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                //ShowModalRoundOverPage();
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver)
            {
                // Pause
                Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                GameOver();
                return;
            }
        }

        /// <summary>
        /// Decide The Turn and who to Attack
        /// </summary>
        public void SetAttackerAndDefender()
        {
            EngineViewModel.Engine.CurrentRound.CurrentPlayer = EngineViewModel.Engine.CurrentRound.GetNextPlayerTurn();

            switch (EngineViewModel.Engine.CurrentRound.CurrentPlayer.PlayerType)
            {
                case CreatureEnum.Character:
                    // User would select who to attack

                    // for now just auto selecting
                    EngineViewModel.Engine.CurrentRound.Target = EngineViewModel.Engine.CurrentRound.ChooseTarget(EngineViewModel.Engine.CurrentRound.CurrentPlayer);
                    break;

                case CreatureEnum.Monster:
                default:

                    // Monsters turn, so auto pick a Character to Attack
                    EngineViewModel.Engine.CurrentRound.Target = EngineViewModel.Engine.CurrentRound.ChooseTarget(EngineViewModel.Engine.CurrentRound.CurrentPlayer);
                    break;
            }
        }

        /// <summary>
        /// Game is over
        /// 
        /// Show Buttons
        /// 
        /// Clean up the Engine
        /// 
        /// Show the Score
        /// 
        /// Clear the Board
        /// 
        /// </summary>
        public void GameOver()
        {
            // Wrap up
            //EngineViewModel.Engine.EndBattle();

            // Save the Score to the Score View Model, by sending a message to it.
            var Score = EngineViewModel.Engine.Referee.BattleScore;
            MessagingCenter.Send(this, "AddData", Score);

            // Hide the Game Board
            GameUIDisplay.IsVisible = false;

            // Show the Game Over Display
            GameOverDisplay.IsVisible = true;
        }

        #region MessageHandelers

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage()
        {
            // Output The Message that happened.
            BattleMessages.Text = string.Format("{0} \n {1}", EngineViewModel.Engine.Referee.BattleMessages.TurnMessage, BattleMessages.Text);

            Debug.WriteLine(BattleMessages.Text);

            if (!string.IsNullOrEmpty(EngineViewModel.Engine.Referee.BattleMessages.LevelUpMessage))
            {
                BattleMessages.Text = string.Format("{0} \n {1}", EngineViewModel.Engine.Referee.BattleMessages.LevelUpMessage, BattleMessages.Text);
            }

            htmlSource.Html = EngineViewModel.Engine.Referee.BattleMessages.GetHTMLFormattedTurnMessage();
            HtmlBox.Source = HtmlBox.Source = htmlSource;
        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            BattleMessages.Text = "";
            //htmlSource.Html = EngineViewModel.Engine.Referee.BattleMessages.GetHTMLBlankMessage();
            HtmlBox.Source = htmlSource;
        }

        #endregion MessageHandelers

        #region PageHandelers

        /// <summary>
        /// Battle Over, so Exit Button
        /// Need to show this for the user to click on.
        /// The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// The Next Round Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            ShowModalNewRoundPage();
        }

        /// <summary>
        /// The Start Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StartButton_Clicked(object sender, EventArgs e)
        {
            HideUIElements();

            // Set for a trun to begin
            AttackButton.IsVisible = true;
            MessageDisplayBox.IsVisible = true;
            BattlePlayerInfomationBox.IsVisible = true;
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            Debug.WriteLine("Showing Score Page : " + EngineViewModel.Engine.Referee.BattleScore.ScoreTotal.ToString());

            await Navigation.PushModalAsync(new ScorePage());
        }

        /// <summary>
        /// Show the Round Over page
        /// 
        /// Round Over is where characters get items
        /// 
        /// </summary>
        public async void ShowModalRoundOverPage()
        {
            HideUIElements();

            // Show the Round Over page
            // Then show the Next Round Button
            NextRoundButton.IsVisible = true;

            await Navigation.PushModalAsync(new RoundOverPage());
        }

        /// <summary>
        /// Show the Page for New Round
        /// 
        /// Upcomming Monsters
        /// 
        /// </summary>
        public async void ShowModalNewRoundPage()
        {
            await Navigation.PushModalAsync(new NewRoundPage());

            HideUIElements();

            ClearMessages();

            // Show the Attack Button Set
            BattlePlayerInfomationBox.IsVisible = true;
            MessageDisplayBox.IsVisible = true;
            AttackButton.IsVisible = true;
        }
        #endregion PageHandelers
    }
}