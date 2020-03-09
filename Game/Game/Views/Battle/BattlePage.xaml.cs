using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.ViewModels;
using Game.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattlePage: ContentPage
	{

		public BattleEngineViewModel BattleEngine = BattleEngineViewModel.Instance;

		public Engine.RoundEngine CurrentRound;

		private List<CharacterModel> Party;

		/// <summary>
		/// Constructor
		/// </summary>
		public BattlePage ()
		{
			InitializeComponent ();

			BindingContext = BattleEngine;

			BattleEngine.Engine.NewRound();

			CurrentRound = BattleEngine.Engine.CurrentRound;

			AddBattlefieldGridCharacter();



		}

		public BattlePage(List<CharacterModel> party)
		{
			InitializeComponent();

			BindingContext = BattleEngine;

			//List<CharacterModel> testFighter = new List<CharacterModel>();

			//testFighter.Add(new CharacterModel
			//{
			//	Name = "The Delinquent",
			//	MaxHealth = 20,
			//	CurrentHealth = 20,
			//	Level = 1,
			//	Description = "The mischief class skipper. Low in defense but high in attack",
			//	ImageURI = "https://clipartart.com/images/sleeping-at-school-clipart.png",
			//	DefenseAttribute = 1,
			//	OffenseAttribute = 2,
			//	SpeedAttribute = 1,
			//	Skill = CreatureSkillEnum.None,
			//	//Equipments = equipments
			//});

			BattleEngine.Engine.SetParty(BattleEngine.Engine.CharacterList);
			BattleEngine.Engine.startBattle();



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
		}


		//below are just for demo, please erase during final turn in
		private void ShowDeadButton_Clicked(object sender, EventArgs e)
		{
			monster_1.Source = "https://lh3.googleusercontent.com/proxy/7caqmNL_b6e1C70tV6CU7hmaZGL9t-hvar2QHUo1JabVoEfhw456zFAt9zhG2GYx3zOEK_-kOG7Xb8qveL18MGMfE_Qpq1gj";
		}

		private void ShowLevelUpButton_Clicked(object sender, EventArgs e)
		{
			if (LevelUp.IsVisible == false)
				LevelUp.IsVisible = true;
			else LevelUp.IsVisible = false;

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
	}
}