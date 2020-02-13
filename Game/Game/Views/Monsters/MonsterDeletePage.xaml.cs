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

        
    }
}