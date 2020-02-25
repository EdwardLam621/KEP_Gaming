using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MonsterReadPage : ContentPage
    {
        // View Model for Monster
        readonly GenericViewModel<MonsterModel> ViewModel;

        //drop item list of monsters
        List<string> dropItems = new List<string>();
     
        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public MonsterReadPage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            AddItemsToDisplay();
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterUpdatePage(new GenericViewModel<MonsterModel>(ViewModel.Data))));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterDeletePage(new GenericViewModel<MonsterModel>(ViewModel.Data))));
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        public void AddItemsToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();
            foreach (var data in FlexList)
            {
                ItemBox.Children.Remove(data);
            }

            //if not null, read all items
            for(int i = 0; i < ViewModel.Data.DropItems.Count; i++)
            {
                ItemBox.Children.Add(GetItemToDisplay(ViewModel.Data.DropItems.ElementAt(i)));
            }

            //if null, add a null item
            if (ViewModel.Data.DropItems.Count == 0)
            {
                ItemBox.Children.Add(GetItemToDisplay(ViewModel.Data.DropItems.ElementAt(0)));
            }


        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(string itemid)
        {
            // Defualt Image is the Plus
            var ImageSource = "icon_cancel.png";
            var ClickableButton = true;

            var data = ViewModel.Data.GetItem(itemid);
            if (data == null)
            {
                // Show the Default Icon for the Location
                data = new ItemModel { Name = "None", ImageURI = ImageSource };

                // Turn off click action
                ClickableButton = false;
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            if (ClickableButton)
            {
                // Add a event to the user can click the item and see more
                ItemButton.Clicked += (sender, args) => ShowPopup(data);
            }

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = data.Name,
                Style = (Style)Application.Current.Resources["ValueStyle"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                    ItemLabel
                },
            };

            return ItemStack;
        }

        /// <summary>
        /// Show the Popup for the Item
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemModel data)
        {
            PopupLoadingView.IsVisible = true;
            PopupItemImage.Source = data.ImageURI;

            PopupItemName.Text = data.Name;
            PopupItemDescription.Text = data.Description;
            PopupItemLocation.Text = data.Location.ToMessage();
            PopupItemAttribute.Text = data.Attribute.ToMessage();
            PopupItemValue.Text = " + " + data.Value.ToString();

            //// Close the popup after 3 seconds
            //Device.StartTimer(TimeSpan.FromSeconds(3), () => { 
            //        PopupLoadingView.IsVisible = false;
            //        return true; }); 

            return true;
        }

        /// <summary>
        /// When the user clicks the close in the Popup
        /// hide the view
        /// show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            PopupLoadingView.IsVisible = false;
        }
    }
}