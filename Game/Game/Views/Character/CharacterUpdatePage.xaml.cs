using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    public partial class CharacterUpdatePage : ContentView
    {

        readonly GenericViewModel<CharacterModel> ViewModel;

        public CharacterUpdatePage()
        {
            InitializeComponent();
        }
    }
}