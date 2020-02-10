using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterCreatePage : ContentPage
    {
        // View Model for Item
        readonly GenericViewModel<CharacterModel> viewModel;

        public CharacterCreatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.viewModel = data;

            this.viewModel.Title = "Create " + data.Title;
        }

        /// <summary>
        /// Save calls to Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Create_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Create", viewModel.Data);
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
    }

}