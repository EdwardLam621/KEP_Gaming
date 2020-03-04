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

			DrawCharacterList();

			DrawItemLists();
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

		/// <summary>
		/// Closes the Round Over Popup
		/// 
		/// Launches the Next Round Popup
		/// 
		/// Resets the Game Round
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void CloseButton_Clicked(object sender, EventArgs e)
		{
			// Reset to a new Round
			BattleEngineViewModel.Instance.Engine.NewRound();

			// Show the New Round Screen
			ShowModalNewRoundPage();
		}

		/// <summary>
		/// Show the Page for New Round
		/// 
		/// Upcomming Monsters
		/// 
		/// </summary>
		public async void ShowModalNewRoundPage()
		{
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Clear and Add the Characters that survived
		/// </summary>
		public void DrawCharacterList()
		{
			// Clear and Populate the Characters Remaining
			var FlexList = CharacterListFrame.Children.ToList();
			foreach (var data in FlexList)
			{
				CharacterListFrame.Children.Remove(data);
			}

			// Draw the Characters
			foreach (var data in BattleEngineViewModel.Instance.Engine.CharacterList)
			{
				CharacterListFrame.Children.Add(CreatePlayerDisplayBox(data));
			}
		}

		/// <summary>
		/// Draw the List of Items
		/// 
		/// The Ones Dropped
		/// 
		/// The Ones Selected
		/// 
		/// </summary>
		public void DrawItemLists()
		{
			DrawDroppedItems();
			DrawSelectedItems();

			// Only need to update the selected, the Dropped is set in the constructor
			TotalSelected.Text = BattleEngineViewModel.Instance.Engine.BattleScore.ItemModelSelectList.Count().ToString();
		}

		/// <summary>
		/// Add the Dropped Items to the Display
		/// </summary>
		public void DrawDroppedItems()
		{
			// Clear and Populate the Dropped Items
			var FlexList = ItemListFoundFrame.Children.ToList();
			foreach (var data in FlexList)
			{
				ItemListFoundFrame.Children.Remove(data);
			}

			foreach (var data in BattleEngineViewModel.Instance.Engine.BattleScore.ItemModelDropList)
			{
				ItemListFoundFrame.Children.Add(GetItemToDisplay(data));
			}
		}

		/// <summary>
		/// Add the Dropped Items to the Display
		/// </summary>
		public void DrawSelectedItems()
		{
			// Clear and Populate the Dropped Items
			var FlexList = ItemListSelectedFrame.Children.ToList();
			foreach (var data in FlexList)
			{
				ItemListSelectedFrame.Children.Remove(data);
			}

			foreach (var data in BattleEngineViewModel.Instance.Engine.BattleScore.ItemModelSelectList)
			{
				ItemListSelectedFrame.Children.Add(GetItemToDisplay(data));
			}
		}
	}
}