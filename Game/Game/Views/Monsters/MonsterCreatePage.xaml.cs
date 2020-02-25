using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;
using Xamarin.Forms.Xaml;
using System.Linq;
using Game.Helpers;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCreatePage : ContentPage
    {
        readonly GenericViewModel<MonsterModel> ViewModel;

        public MonsterCreatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            data.Data = new MonsterModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create " + data.Title;
        }


        /// <summary>
        /// Show the Items the Monster will drop
        /// </summary>
        public void AddItemsToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();
            foreach (var data in FlexList)
            {
                ItemBox.Children.Remove(data);
            }

            //if not null, read all items
            for (int i = 0; i < ViewModel.Data.DropItems.Count; i++)
            {
                ItemBox.Children.Add(GetItemToDisplay(ViewModel.Data.DropItems.ElementAt(i)));
            }

            //add null item as new item space
            ItemBox.Children.Add(GetItemToDisplay(new ItemModel()));

        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemModel item)
        {
            // Get the Item, if it exist show the info
            // If it does not exist, show a Plus Icon for the location

            // Defualt Image is the Plus
            var ImageSource = "https://icons.iconarchive.com/icons/google/noto-emoji-smileys/1024/10024-thinking-face-icon.png";

            var data = ViewModel.Data.GetDropItems(item.Id);
            if (data == null)
            {
                data = new ItemModel { Name = "Add", ImageURI = ImageSource };
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            // Add a event to the user can click the item and see more
            ItemButton.Clicked += (sender, args) => ShowPopup();

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
        public bool ShowPopup()
        {
            PopupLoadingView.IsVisible = true;

            PopupLocationLabel.Text = "Avaliable Items: ";

            PopupLocationItemListView.ItemsSource = ItemIndexViewModel.Instance.GetDefaultData();



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

            ViewModel.Data.DropItems.Add(data);

            AddItemsToDisplay();

            ClosePopup();
        }


        /// <summary>
        /// Save calls to Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Create_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.CharacterService.DefaultImageURI; ;
            }

            MessagingCenter.Send(this, "Create", ViewModel.Data);
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
        /// Catch the change to the Stepper for Level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Level_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            var level = e.NewValue;
            LevelValue.Text = level.ToString();
            ViewModel.Data.MaxHealth = DiceHelper.RollDice((int)level, 10);
            HealthValue.Text = string.Format(" : {0:G}", ViewModel.Data.MaxHealth);
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
        /// Catch the change to the Stepper for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Defense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the Stepper for Offense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Offense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            OffenseValue.Text = String.Format("{0}", e.NewValue);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}