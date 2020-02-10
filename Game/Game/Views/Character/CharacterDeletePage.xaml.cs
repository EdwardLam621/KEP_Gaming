using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;

namespace Game.Views.Character
{
    public partial class CharacterDeletePage : ContentView
    {

        readonly GenericViewModel<CharacterModel> viewModel;

        public CharacterDeletePage()
        {
            InitializeComponent();
        }
    }
}