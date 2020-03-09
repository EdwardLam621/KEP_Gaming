using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using Game.Models;
using System.Collections.Generic;
using System.Diagnostics;
using Game.Engine;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage: ContentPage
	{

		private BattleEngineViewModel BattleEngine = BattleEngineViewModel.Instance;

		private List<CharacterModel> Party;

		public RoundEngine CurrentRound;

		/// <summary>
		/// Constructor
		/// </summary>

		public BattlePage()
		{
			InitializeComponent();

			BindingContext = BattleEngine;

			BattleEngine.Engine.SetParty(BattleEngine.Engine.CharacterList);

			// Start the Battle Engine
			BattleEngine.Engine.NewRound();

			// Load current round into local namespace
			CurrentRound = BattleEngine.Engine.CurrentRound;

			// Ask the Game engine to select who goes first
			// Game Starts with No Attacker or Defender selected
			CurrentRound.CurrentPlayer = null;

		}


		/// <summary>
		/// Attack Action
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void AttackButton_Clicked(object sender, EventArgs e)
		{
			Debug.WriteLine("Attack clicked");



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

		private void ImageButton_Clicked(object sender, EventArgs e)
		{

		}
	}
}