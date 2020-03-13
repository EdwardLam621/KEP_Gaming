using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using Game.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage: ContentPage
	{

		// HTML Formatting for message output box
		public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

		// Battle Engine instance
		public BattleEngineViewModel BattleEngine = BattleEngineViewModel.Instance;

		// Round holds state
		public Engine.RoundEngine CurrentRound;

		// Which player is currently attacking
		public DungeonFighterModel CurrentlySelectedPlayer;

		// The target 
		public DungeonFighterModel SelectedTarget;

		// Height of the playable area
		// Not the same as battle grid height...
		private const int PLAYER_GRID_HEIGHT = 3;

		// Width of the playable area
		private const int PLAYER_GRID_WIDTH = 6;

		/// <summary>
		/// Constructor
		/// </summary>
		public BattlePage ()
		{
			InitializeComponent ();

			BindingContext = BattleEngine;

			// Load in characters set from PickCharactersPage
			BattleEngine.Engine.SetParty(BattleEngine.Engine.CharacterList);

			BattleEngine.Engine.NewRound();

			CurrentRound = BattleEngine.Engine.CurrentRound;

			AddBattlefieldGridCharacter();

			DoNextTurn();
		}

		

		public void NextRoundButton_Clicked(object sender, EventArgs e)
		{
			
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
			//AttackButton.IsVisible = true;
			//MessageDisplayBox.IsVisible = true;
			BattlePlayerInfomationBox.IsVisible = true;
		}

		public void HideUIElements()
		{
			//NextRoundButton.IsVisible = false;
			StartBattleButton.IsVisible = false;
			//AttackButton.IsVisible = false;
			//MessageDisplayBox.IsVisible = false;
			BattlePlayerInfomationBox.IsVisible = false;
		}


		/// <summary>
		/// Builds up the output message
		/// </summary>
		/// <param name="message"></param>
		public void GameMessage()
		{
			// Output The Message that happened.
			//BattleMessages.Text = string.Format("battle message");

			// Output The Message that happened.
			BattleMessages.Text = string.Format("{0} \n{1}", BattleEngine.Engine.Referee.BattleMessages.TurnMessage, BattleMessages.Text);

			if (!string.IsNullOrEmpty(BattleEngine.Engine.Referee.BattleMessages.LevelUpMessage))
			{
				BattleMessages.Text = string.Format("{0} \n{1}", BattleEngine.Engine.Referee.BattleMessages.LevelUpMessage, BattleMessages.Text);
			}


		}

		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void RoundOverButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new RoundOverPage());
		}


		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void NewRoundButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NewRoundPage());
		}
		

		/// <summary>
		/// Battle Over
		/// Battle Over button shows when all characters are dead
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void BattleOverButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new ScorePage());
		}

		/// <summary>
		/// Battle Over, so Exit Button
		/// Need to show this for the user to click on.
		/// The Quit does a prompt, exit just exits
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void ExitButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Quit the Battle
		/// 
		/// Quitting out
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void QuitButton_Clicked(object sender, EventArgs e)
		{
			bool answer = await DisplayAlert("Battle", "Are you sure you want to Quit?", "Yes", "No");

			if (answer)
			{
				await Navigation.PopModalAsync();
			}
		}

		private void AddBattlefieldGridCharacter()
		{
			// Clear the currett list
			BattleEngine.Engine.CharacterList.Clear();

			// Load the Characters into the Engine
			foreach (var data in BattleEngine.PartyCharacterList)
			{
				if (!BattleEngine.Engine.CharacterList.Contains(data)){
					BattleEngine.Engine.CharacterList.Add(data);
				}
				
			}

			// Draw Characters
			for (int i = 0; i < BattleEngine.Engine.CharacterList.Count; i++)
			{

				var xLocation = i / PLAYER_GRID_HEIGHT;
				var yLocation = 1 + (i % PLAYER_GRID_HEIGHT);
				BattleGrid.Children.Add(new Image { Source = BattleEngine.Engine.CharacterList[i].ImageURI }, 
					xLocation, yLocation);
			}

			// Draw Monsters
			for (int i = 0; i < BattleEngine.Engine.CurrentRound.MonsterList.Count; i++)
			{

				var xLocation = PLAYER_GRID_WIDTH - 1 - (i / PLAYER_GRID_HEIGHT);
				var yLocation = 1 + (i % PLAYER_GRID_HEIGHT);
				BattleGrid.Children.Add(new Image { Source = BattleEngine.Engine.CurrentRound.MonsterList[i].ImageURI },
					xLocation, yLocation);
			}

		}


		//below are just for demo, please erase during final turn in
		private void ShowDeadButton_Clicked(object sender, EventArgs e)
		{
			//monster_1.Source = "https://lh3.googleusercontent.com/proxy/7caqmNL_b6e1C70tV6CU7hmaZGL9t-hvar2QHUo1JabVoEfhw456zFAt9zhG2GYx3zOEK_-kOG7Xb8qveL18MGMfE_Qpq1gj";
		}

		private void ShowLevelUpButton_Clicked(object sender, EventArgs e)
		{
			//if (LevelUp.IsVisible == false)
			//	LevelUp.IsVisible = true;
			//else LevelUp.IsVisible = false;

		}

		/// <summary>
		/// Show the Game Over Screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public async void ShowScoreButton_Clicked(object sender, EventArgs args)
		{
			Debug.WriteLine("Showing Score Page : " + BattleEngine.Engine.Referee.BattleScore.ScoreTotal.ToString());

			await Navigation.PushModalAsync(new ScorePage());
		}



		// Battle section

		/// <summary>
		/// Run through a turn. 
		/// If it's a monster, the monster will do a turn. 
		/// Characters will be prompted for the next action.
		/// </summary>
		void DoNextTurn()
		{

			// Remember player count
			var currentPlayerCount = BattleEngine.Engine.CurrentRound.FighterList.Count;

			// See whos turn it is
			CurrentlySelectedPlayer = CurrentRound.GetNextPlayerTurn();

			// If it's a monster, let them do their attack
			if (CurrentlySelectedPlayer.PlayerType.Equals(CreatureEnum.Monster))
			{
				// Find the monster in the battle grid 

				// Make their image jiggle or something to show an attack animation

				// update round state
				BattleEngine.Engine.CurrentRound.MonsterNextTurn();

				// redraw board if anyone died
				if (BattleEngine.Engine.CurrentRound.FighterList.Count < currentPlayerCount)
				{
					AddBattlefieldGridCharacter();
				}

				// display hit information
				GameMessage();

				// Recurse until monsters are through attacking
				DoNextTurn();

			} else
			// otherwise, update battlepage state to show actions
			{
				AttackerImage.Source = CurrentlySelectedPlayer.ImageURI;
				BattleEngine.Engine.CurrentRound.CurrentPlayer = CurrentlySelectedPlayer;
				// wait for attack/move/skill...
			}


		}


		/// <summary>
		/// Attack Action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void AttackButton_Clicked(object sender, EventArgs e)
		{
			// auto select first monster that isn't dead
			var target = BattleEngine.Engine.CurrentRound.FighterList.First(m => m.Alive == true);

			// do attack
			BattleEngine.Engine.CurrentRound.AttackClicked(target);

			DoNextTurn();

		}




	}
}