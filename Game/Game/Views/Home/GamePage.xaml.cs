using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Engine;
using Game.Models;
using System.Collections.Generic;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public GamePage ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// Jump to the Dungeon
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        async void ManualBattleButton_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new PickCharactersPage());
		}

		/// <summary>
		/// Jump to the Village
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void VillageButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new VillagePage());
		}

		/// <summary>
		/// Jump to the Dungeon
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void AutobattleButton_Clicked(object sender, EventArgs e)
		{
			// Run the Autobattle simulation from here

			// Call to the Score Page
			//await Navigation.PushModalAsync(new NavigationPage(new ScorePage()));
			
			
			// Battle testing
			List<CharacterModel> testFighter = new List<CharacterModel>();
			
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

			BattleEngine battle = new BattleEngine();
			battle.Referee.AutoBattleEnabled = true;
			battle.startBattle();
		}
	}
}