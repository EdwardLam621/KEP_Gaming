using Game.Models;
using Game.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoundOverPage: ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public RoundOverPage()
		{
			// Update the Round Count
			TotalRound.Text = BattleEngineViewModel.Instance.Engine.Referee.BattleScore.RoundCount.ToString();

			// Update the Found Number
			TotalFound.Text = BattleEngineViewModel.Instance.Engine.Referee.BattleScore.ItemsDroppedList.Count().ToString();

			// Update the Selected Number, this gets updated later when selected refresh happens
			TotalSelected.Text = BattleEngineViewModel.Instance.Engine.Referee.BattleScore.ItemModelSelectList.Count().ToString();

			InitializeComponent ();
		}

		/// <summary>
		/// Start next Round, returning to the battle screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void NextButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Start next Round, returning to the battle screen
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void PickItems_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new PickItemsPage());
		}
	}
}