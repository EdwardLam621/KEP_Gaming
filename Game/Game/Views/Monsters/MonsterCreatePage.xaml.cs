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

            data.Data = new CharacterModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create " + data.Title;
        }
    }
}