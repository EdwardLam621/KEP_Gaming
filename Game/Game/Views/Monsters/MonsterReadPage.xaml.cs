using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;

namespace Game.Views
{
    public partial class MonsterReadPage : ContentPage
    {
        readonly GenericViewModel<MonsterModel> ViewModel;

        public MonsterReadPage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
        }
    }
}