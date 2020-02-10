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

        public CharacterReadPage()
        {
            InitializeComponent();
        }
    }
}