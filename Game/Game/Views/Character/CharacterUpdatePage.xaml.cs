using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using Game.Models;
using Game.Helpers;

namespace Game.Views
{
    public partial class CharacterUpdatePage : ContentPage
    {
        // View Model for Item
        readonly GenericViewModel<CharacterModel> ViewModel;

        // as a reference to find location
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            AddItemsToDisplay();

            //Need to make the SelectedItem a string, so it can select the correct item.
            SkillPicker.SelectedItem = data.Data.Skill.ToString();

        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Speed_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Offense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Offense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            OffenseValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the stepper for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Defense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = String.Format("{0}", e.NewValue);
        }


        public void AddItemsToDisplay()
        {

            // Get the List of Locations a Character can have
            var LocationList = ItemLocationEnumHelper.GetListCharacter;

            // Add Each item in the list
            foreach (var location in LocationList)
            {
                var LocationString = ItemLocationEnumHelper.ConvertStringToEnum(location).ToMessage();
                ItemBox.Children.Add(
                    GetItemToDisplay(
                        LocationString,
                        ViewModel.Data.GetItemByLocation(
                            ItemLocationEnumHelper.ConvertStringToEnum(location))));
            }
        }

        public StackLayout GetItemToDisplay(string LocationString, ItemModel data)
        {
            if (data == null)
            {
                return new StackLayout();
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            // Add a event to the user can click the item and see more
            ItemButton.Clicked += (sender, args) => ShowPopup(data);

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = LocationString,
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

        public bool ShowPopup(ItemModel data)
        {
            PopupLoadingView.IsVisible = true;

            PopupLocationLabel.Text = "Avaliable Items for: " + data.Location.ToMessage();

            PopupLocationItemListView.ItemsSource = ItemIndexViewModel.Instance.GetLocationItems(data.Location);

            PopupLocationEnum = data.Location;


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
            ClosePopup();
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        private void ClosePopup()
        {
            PopupLoadingView.IsVisible = false;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            ViewModel.Data.AddItem(PopupLocationEnum, data.Id);

            AddItemsToDisplay();

            ClosePopup();
        }
    }
}