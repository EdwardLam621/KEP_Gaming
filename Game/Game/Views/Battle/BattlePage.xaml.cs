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

		private BattleEngineViewModel BattleEngine = BattleEngineViewModel.Instance;

		private List<CharacterModel> Party;

		/// <summary>
		/// Constructor
		/// </summary>
		public BattlePage ()
		{
			InitializeComponent ();

			BindingContext = BattleEngine;

			
		}

		public BattlePage(List<CharacterModel> party)
		{
			InitializeComponent();

			BindingContext = BattleEngine;

			List<CharacterModel> testFighter = new List<CharacterModel>();

			testFighter.Add(new CharacterModel
			{
				Name = "The Delinquent",
				MaxHealth = 20,
				CurrentHealth = 20,
				Level = 1,
				Description = "The mischief class skipper. Low in defense but high in attack",
				ImageURI = "https://clipartart.com/images/sleeping-at-school-clipart.png",
				DefenseAttribute = 1,
				OffenseAttribute = 2,
				SpeedAttribute = 1,
				Skill = CreatureSkillEnum.None,
				//Equipments = equipments
			});

			BattleEngine.Engine.SetParty(testFighter);
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
	}
}