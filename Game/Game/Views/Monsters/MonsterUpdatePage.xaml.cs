using System;
using Xamarin.Forms;
using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    public partial class MonsterUpdatePage : ContentPage
    {
        // View Model for monster
        readonly GenericViewModel<MonsterModel> ViewModel;

        /// <summary>
        /// Constructor that takes and existing data monster
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            //Need to make the SelectedItem a string, so it can select the correct item.
            SkillPicker.SelectedItem = data.Data.Skill.ToString();

        }

        

    }
}