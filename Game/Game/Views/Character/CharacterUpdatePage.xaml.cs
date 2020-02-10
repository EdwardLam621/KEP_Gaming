using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    public partial class CharacterUpdatePage : ContentPage
    {
        // View Model for Item
        readonly GenericViewModel<CharacterModel> ViewModel;

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            //Need to make the SelectedItem a string, so it can select the correct item.
            SkillPicker.SelectedItem = data.Data.Skill.ToString();

        }

       
    }
}