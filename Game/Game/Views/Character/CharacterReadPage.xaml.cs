using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;

namespace Game.Views.Character
{
    public partial class CharacterReadPage : ContentView
    {
        readonly GenericViewModel<CharacterModel> ViewModel;

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public CharacterReadPage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
        }
    }
}