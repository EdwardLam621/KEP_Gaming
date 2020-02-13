using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;

namespace Game.Views
{

    public partial class MonsterDeletePage : ContentPage
    {
        // View Model for Monsters
        readonly GenericViewModel<MonsterModel> viewModel;

        public MonsterDeletePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.viewModel = data;

            this.viewModel.Title = "Delete " + data.Title;
        }

        /// <summary>
        /// Save calls to Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", viewModel.Data);
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