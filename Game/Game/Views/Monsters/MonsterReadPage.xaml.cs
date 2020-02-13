using System.ComponentModel;
using Xamarin.Forms;
using Game.ViewModels;
using System;
using Game.Models;
using System.Collections.ObjectModel;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MonsterReadPage : ContentPage
    {
        // View Model for Character
        readonly GenericViewModel<MonsterModel> ViewModel;

        ObservableCollection<ItemModel> dropItems = new ObservableCollection<ItemModel>();
     
        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public MonsterReadPage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            DropItemListView.ItemsSource = dropItems;
            for (int i = 0; i < data.Data.DropItems.Count; i++)
            {
                dropItems.Add(data.Data.DropItems[i]);
            }
        }
    }
}