using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;
using Xamarin.Forms.Xaml;

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
            LevelValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the Stepper for Health
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Health_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            HealthValue.Text = String.Format("{0}", e.NewValue);
        }
    }
}